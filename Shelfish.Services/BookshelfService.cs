using Shelfish.Data;
using Shelfish.Models.BookshelfModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
            //        var 
            //    }
            //}

            var bookshelfToCreate =
                new Bookshelf()
                {
                    UserId = _userId,
                    ShelfName = model.ShelfName,
                    BooksOnShelf = model.BooksOnShelf,
                    //TotalBooks = BooksOnShelf.Count(),
                    CreatedUtc = DateTimeOffset.Now
                };
            

            using (var ctx = new ApplicationDbContext())
            {

                

                ctx.Bookshelves.Add(bookshelfToCreate);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BookshelfListItem> GetBookshelves()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bookshelves
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new BookshelfListItem
                                {
                                    ShelfId = e.ShelfId,
                                    ShelfName = e.ShelfName,
                                    TotalBooks = e.TotalBooks,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public BookshelfDetail GetBookshelfById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bookshelves
                        .Single(e => e.ShelfId == id && e.UserId == _userId);
                return
                    new BookshelfDetail
                    {
                        ShelfId = entity.ShelfId,
                        ShelfName = entity.ShelfName,
                        TotalBooks = entity.TotalBooks,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        BooksOnShelf = entity.BooksOnShelf
                    };
            }
        }

        public bool UpdateBookshelf(BookshelfEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var shelfToUpdate =
                    ctx
                        .Bookshelves
                        .Single(e => e.ShelfId == model.ShelfId && e.UserId == _userId);

                shelfToUpdate.ShelfName = model.ShelfName;
                shelfToUpdate.BooksOnShelf = model.BooksOnSHelf;
                shelfToUpdate.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBookshelf(int shelfId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var shelfToDelete =
                    ctx
                        .Bookshelves
                        .Single(e => e.ShelfId == shelfId && e.UserId == _userId);

                ctx.Bookshelves.Remove(shelfToDelete);

                return ctx.SaveChanges() == 1;
            }
        }
    }





    //Possible ways to set up books as SelectListItems to populate dropdownlists. May need to put in separate class/view model
    public class BookHelper
    {
        // BookService service = new BookService();
        // var bookList = service.GetBooks();

        public IEnumerable<SelectListItem> BookItems
        {
            get
            {
                var allBooks = bookList.Select(b => new SelectListItem
                {
                    Value = b.BookId.ToString(),
                    Text = b.Title
                });
                return allBooks;
            }
        }
    }
}
