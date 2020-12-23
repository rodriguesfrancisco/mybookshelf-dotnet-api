using Flunt.Validations;
using MediatR;
using MyBookshelf.Core.Entities;
using MyBookshelf.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookshelf.Application.Commands.CreateBookUser
{
    public class CreateBookUserHandler : IRequestHandler<CreateBookUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserBookRepository _userBookRepository;
        private readonly IStatusRepository _statusRepository;

        public CreateBookUserHandler(
            IUserRepository userRepository,
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            ICategoryRepository categoryRepository,
            IUserBookRepository userBookRepository, 
            IStatusRepository statusRepository)
        {
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _userBookRepository = userBookRepository;
            _statusRepository = statusRepository;
        }

        public Task<Unit> Handle(CreateBookUser command, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetById(command.UserId.GetValueOrDefault());

            command.AddNotifications(new Contract()
                .Requires()
                .IsNotNull(user, "User", "User not found")
            );

            if (command.Invalid) return Task.FromResult(Unit.Value);

            var status = _statusRepository.FindById(command.IdStatus);

            command.AddNotifications(new Contract()
                .Requires()
                .IsNotNull(status, "Status", "Status not found")
            );

            if (command.Invalid) return Task.FromResult(Unit.Value);

            var isbn = command.IndustryIdentifiers
                .Where(x => x.Type == "ISBN_13" || x.Type == "OTHER")
                .Select(x => x.Identifier)
                .FirstOrDefault();

            var book = _bookRepository.FindByIsbn(isbn);
            if(book == null)
            {
                var newBook = new Book(
                    command.Title,
                    command.Subtitle,
                    command.Publisher,
                    isbn,
                    command.PageCount,
                    command.ImageLinks.Thumbnail,
                    command.ImageLinks.SmallThumbnail,
                    command.Description
                );

                foreach(var authorName in command.Authors)
                {
                    var author = _authorRepository.FindByName(authorName);
                    if(author == null)
                    {
                        var newAuthor = new Author(authorName);
                        newBook.AddAuthor(newAuthor);
                    } else
                    {
                        newBook.AddAuthor(author);
                    }
                }
                
                foreach(var categoryName in command.Categories)
                {
                    var category = _categoryRepository.FindByName(categoryName);
                    if(category == null)
                    {
                        var newCategory = new Category(categoryName);
                        newBook.AddCategory(newCategory);
                    } else
                    {
                        newBook.AddCategory(category);
                    }
                }

                _bookRepository.Save(newBook);
                book = newBook;
            }

            var userBook = new UserBook(user, book, status);
            _userBookRepository.Save(userBook);

            return Task.FromResult(Unit.Value);
        }
    }
}
