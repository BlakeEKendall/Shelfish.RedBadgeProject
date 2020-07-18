using Shelfish.Data;
using Shelfish.Models.BookModels;
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

            var bookshelfToCreate =
                new Bookshelf()
                {
                    UserId = _userId,
                    ShelfName = model.ShelfName,
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
                shelfToUpdate.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool AddBookToShelf(int bookId, int shelfId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var bookToAddToShelf =
                        ctx
                            .Books
                            .Single(e => e.BookId == bookId);

                var shelfToAddBookTo =
                    ctx
                        .Bookshelves
                        .Single(e => e.ShelfId == shelfId && e.UserId == _userId);

                shelfToAddBookTo.BooksOnShelf.Add(bookToAddToShelf);
                return ctx.SaveChanges() == 1;
            }
            // From BookshelfDetail page, all info listed. Hit button to add book, want to pass ShelfId into next view with the dropdown. Select book from dropdown, 
            //Data passed into dropdownlist to populate is the BOokId. Keep both Ids, and pass into AddToShelf method.
            // Use hiddenfor tag to pass data to view without showing it. 

            // look @ ViewBags to populate DropDownLists? passing data to the view.
            // Made new SelectList, pass to view via ViewBag?
            // Ask Nick or Marty about this Saturday if needed!


            //Will Need additional method here to Delete book from list!!
        }

        public bool DeleteBookFromShelf(int bookId, int shelfId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var shelfToRemoveBookFrom =
                    ctx
                    .Bookshelves
                    .Single(e => e.ShelfId == shelfId && e.UserId == _userId);

                var bookToBeDeletedFromShelf =
                    shelfToRemoveBookFrom
                    .BooksOnShelf
                    .Single(b => b.BookId == bookId);

                shelfToRemoveBookFrom.BooksOnShelf.Remove(bookToBeDeletedFromShelf);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool AddAudiobookToShelf(int audioId, int shelfId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var audiobookToAddToShelf =
                    ctx
                    .Audiobooks
                    .Single(e => e.AudiobookId == audioId);

                var shelfToAddAudiobookTo =
                    ctx
                    .Bookshelves
                    .Single(e => e.ShelfId == shelfId && e.UserId == _userId);

                shelfToAddAudiobookTo.AudiobooksOnShelf.Add(audiobookToAddToShelf);
                return ctx.SaveChanges() == 1;
            }
        }

        
        public bool DeleteAudiobookFromShelf(int audioId, int shelfId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var shelfToRemoveBookFrom =
                    ctx
                    .Bookshelves
                    .Single(e => e.ShelfId == shelfId && e.UserId == _userId);

                var audiobookToBeDeletedFromShelf =
                    shelfToRemoveBookFrom
                    .AudiobooksOnShelf
                    .Single(a => a.AudiobookId == audioId);

                shelfToRemoveBookFrom.AudiobooksOnShelf.Remove(audiobookToBeDeletedFromShelf);
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

        //public IEnumerable<SelectListItem> BookItems
        //{
        //    get
        //    {
        //        var allBooks = bookList.Select(b => new SelectListItem
        //        {
        //            Value = b.BookId.ToString(),
        //            Text = b.Title
        //        });
        //        return allBooks;
        //    }
        //}
    }
}
