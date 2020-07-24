using Shelfish.Data;
using Shelfish.Models.BookModels;
using Shelfish.Models.BookshelfModels;
using Shelfish.Models.ViewModels;
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
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
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

        //TODO: Rewrite using new ShelfRecordKeeper entity to add books to shelves. -->DONE 7/21
        //TODO Next: Rewrite Controller to use this method correctly to add a book to the shelf.
        //TODO Last: Rewrite or Add new View to correctly add books to the shelf.
        public bool AddBookToShelf(AddBookToShelfViewModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //var shelfToAddBookTo =
                //    ctx
                //    .Bookshelves
                //    .Single(s => s.ShelfId == model.SelectedShelfId && s.UserId == _userId);

                //model.SelectedShelfName = shelfToAddBookTo.ShelfName;

                var bookToAddToShelf =
                    ctx
                    .Books
                    .Single(b => b.BookId == model.SelectedBookId);

                var shelfRecordToCreate = new ShelfRecordKeeper
                {
                    ShelfId = model.SelectedShelfId,
                    BookId = bookToAddToShelf.BookId
                };

                //var shelfRecordToCreate = new ShelfRecordKeeper
                //{
                //    RecordKeeperId = model.ShelfRecordId,
                //    Bookshelf = shelfToAddBookTo,
                //    Book = bookToAddToShelf
                //};

                //TODO: NEED TO ACTUALLY ADD THIS TO THE DB!!
                ctx.ShelfRecords.Add(shelfRecordToCreate);
                
                return ctx.SaveChanges() == 1;
            }
        }

        public BookOnShelfDetailView GetSingleBookOnShelf(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var shelfRecordToView =
                    ctx
                    .ShelfRecords
                    .Single(r=> r.RecordKeeperId == id);

                return new BookOnShelfDetailView
                {
                    RecordKeeperId = shelfRecordToView.RecordKeeperId,
                    ShelfId = shelfRecordToView.ShelfId,
                    ShelfName = shelfRecordToView.Bookshelf.ShelfName,
                    BookId = shelfRecordToView.BookId,
                    Title = shelfRecordToView.Book.Title,
                    Author = shelfRecordToView.Book.Author.Name
                };
            }
        }

        public bool DeleteBookFromShelf(int shelfRecordId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var bookRecordToDelete =
                    ctx
                    .ShelfRecords
                    .Single(r=> r.RecordKeeperId == shelfRecordId);

                ctx.ShelfRecords.Remove(bookRecordToDelete);

                return ctx.SaveChanges() == 1;
            }
        }

        //TODO: Write controller to display all the books on the shelf using this service method, and then add a view to display those books.
        public IEnumerable<BookOnShelfRecordView> GetBooksOnShelf(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .ShelfRecords
                        .Where(e => e.ShelfId == id)
                        .Select(
                            e =>
                                new BookOnShelfRecordView
                                {
                                   RecordKeeperId = e.RecordKeeperId,
                                   ShelfId = e.ShelfId,
                                   BookId = e.BookId,
                                   Title = e.Book.Title,
                                   AuthorName = e.Book.Author.Name
                                }
                        );

                return query.ToArray();
            }
        }
        //OLD ADDBOOKTOSHELF METHOD CODE
        //var shelfToAddBookTo =
        //    ctx
        //    .Bookshelves
        //    .Single(s => s.ShelfId == model.ShelfId && s.UserId == _userId);


        //var bookToAddToShelf =
        //    ctx
        //    .Books
        //    .Single(b => b.BookId == model.SelectedBookId);


        ////bookToAddToShelf.BookId = model.SelectedBookId;

        //shelfToAddBookTo.BooksOnShelf.Add(bookToAddToShelf);
        ////shelfToAddBookTo.TotalBooks++;
        //return ctx.SaveChanges() == 1;


        // From BookshelfDetail page, all info listed. Hit button to add book, want to pass ShelfId into next view with the dropdown. Select book from dropdown, 
        // Data passed into dropdownlist to populate is the BOokId. Keep both Ids, and pass into AddToShelf method.
        // Use hiddenfor tag to pass data to view without showing it. 

        // look @ ViewBags to populate DropDownLists? passing data to the view.
        // Made new SelectList, pass to view via ViewBag?
        // Ask Nick or Marty about this Saturday if needed!


        //Will Need additional method here to Delete book from list!!


        //public bool DeleteBookFromShelf(int shelfId, int bookId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var shelfToRemoveBookFrom =
        //            ctx
        //            .Bookshelves
        //            .Single(e => e.ShelfId == shelfId && e.UserId == _userId);

        //        var bookToBeDeletedFromShelf =
        //            shelfToRemoveBookFrom
        //            .BooksOnShelf
        //            .Single(b => b.BookId == bookId);

        //        shelfToRemoveBookFrom.BooksOnShelf.Remove(bookToBeDeletedFromShelf);
        //        shelfToRemoveBookFrom.TotalBooks--;
        //        return ctx.SaveChanges() == 1;
        //    }
        //}

        //public bool AddAudiobookToShelf(int audioId, int shelfId)
        //{
        //    using(var ctx = new ApplicationDbContext())
        //    {
        //        var audiobookToAddToShelf =
        //            ctx
        //            .Audiobooks
        //            .Single(e => e.AudiobookId == audioId);

        //        var shelfToAddAudiobookTo =
        //            ctx
        //            .Bookshelves
        //            .Single(e => e.ShelfId == shelfId && e.UserId == _userId);

        //        shelfToAddAudiobookTo.AudiobooksOnShelf.Add(audiobookToAddToShelf);
        //        shelfToAddAudiobookTo.TotalBooks++;
        //        return ctx.SaveChanges() == 1;
        //    }
        //}



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
