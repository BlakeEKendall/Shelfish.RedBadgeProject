using Shelfish.Data;
using Shelfish.Models.BookshelfModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Shelfish.Services
{
    public class BookshelfService
    {
        private readonly Guid _userId;

        public BookshelfService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateBookshelf(BookshelfCreate model)
        {
            //using (var ctx = new ApplicationDbContext())
            //{

            //    var BookList = ctx.Books.ToList();
            //    foreach (Book book in BookList)
            //    {
                    
            //    }
            //}

                var bookshelfToCreate =
                new Bookshelf()
                {
                    UserId = _userId,
                    ShelfName = model.ShelfName,
                    //BooksOnShelf = model.BooksOnShelf,
                    //TotalBooks = BooksOnShelf.Count(),
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Bookshelves.Add(bookshelfToCreate);
                return ctx.SaveChanges() == 1;
            }
        }
    }

    //Possible ways to set up books as SelectListItems to populate dropdownlists. May need to put in separate class/view model
    public class BookHelper
    {
        private readonly List<Book> _books;

        [Display(Name = "Book to Add")]
        public int SelectedBookId { get; set; }

        public IEnumerable<SelectListItem> BookItems
        {
            get
            {
                var allBooks = _books.Select(b => new SelectListItem
                {
                    Value = b.BookId.ToString(),
                    Text = b.Title
                });
                return allBooks;
            }
        }
    }
}
