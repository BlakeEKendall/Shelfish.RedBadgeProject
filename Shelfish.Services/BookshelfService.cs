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

        public bool DeleteBookshelf(int shelfId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var shelfToDelete =
                    ctx
                        .Bookshelves
                        .Single(e => e.ShelfId == shelfId && e.UserId == _userId);

                var shelfRecordsToDelete =
                    ctx
                    .ShelfRecords
                    .Where(r => r.ShelfId == shelfId)
                    .ToList();


                //Foreach loop to delete each corresponding ShelfRecord before the Shelf itself is deleted
                foreach (ShelfRecordKeeper entity in shelfRecordsToDelete)
                {
                    try
                    {
                        ctx.ShelfRecords.Remove(entity);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                ctx.Bookshelves.Remove(shelfToDelete);

                return ctx.SaveChanges() == 1;
            }
        }

        //Books to Shelves
        //Working with ShelfRecordKeeper entities to connect books to shelves
        public bool AddBookToShelf(AddBookToShelfViewModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var shelfToAddBookTo =
                    ctx
                    .Bookshelves
                    .Single(s => s.ShelfId == model.SelectedShelfId && s.UserId == _userId);


                var bookToAddToShelf =
                    ctx
                    .Books
                    .Single(b => b.BookId == model.SelectedBookId);

                var shelfRecordToCreate = new ShelfRecordKeeper
                {
                    ShelfId = shelfToAddBookTo.ShelfId,
                    BookId = bookToAddToShelf.BookId
                };

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

        public bool DeleteBookFromShelf(int shelfRecordId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var bookRecordToDelete =
                    ctx
                    .ShelfRecords
                    .Single(r => r.RecordKeeperId == shelfRecordId);

                try
                {
                    ctx.ShelfRecords.Remove(bookRecordToDelete);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return ctx.SaveChanges() == 1;
            }
        }

        //Audiobooks to Shelves
        //TODO: Setup service methods for Audiobooks
        //Working with ShelfRecordKeeper entity to connect Audiobooks to Shelves
        public bool AddAudiobookToShelf(AddAudiobookToShelfViewModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var shelfToAddBookTo =
                    ctx
                    .Bookshelves
                    .Single(s => s.ShelfId == model.SelectedShelfId && s.UserId == _userId);


                var audiobookToAddToShelf =
                    ctx
                    .Audiobooks
                    .Single(b => b.AudiobookId == model.SelectedAudiobookId);

                var shelfAudioRecordToCreate = new ShelfAudioRecordKeeper
                {
                    ShelfId = shelfToAddBookTo.ShelfId,
                    AudiobookId = audiobookToAddToShelf.AudiobookId
                };

                ctx.ShelfAudioRecords.Add(shelfAudioRecordToCreate);

                return ctx.SaveChanges() == 1;
            }
        }

        public AudiobookOnShelfDetailView GetSingleAudiobookOnShelf(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var shelfAudioRecordToView =
                    ctx
                    .ShelfAudioRecords
                    .Single(r => r.AudioRecordKeeperId == id);

                return new AudiobookOnShelfDetailView
                {
                    AudioRecordKeeperId = shelfAudioRecordToView.AudioRecordKeeperId,
                    ShelfId = shelfAudioRecordToView.ShelfId,
                    ShelfName = shelfAudioRecordToView.Bookshelf.ShelfName,
                    AudiobookId = shelfAudioRecordToView.AudiobookId,
                    Title = shelfAudioRecordToView.Audiobook.Title,
                    Author = shelfAudioRecordToView.Audiobook.Author.Name
                };
            }
        }

        public IEnumerable<AudiobookOnShelfRecordView> GetAudiobooksOnShelf(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .ShelfAudioRecords
                        .Where(e => e.ShelfId == id)
                        .Select(
                            e =>
                                new AudiobookOnShelfRecordView
                                {
                                    AudioRecordKeeperId = e.AudioRecordKeeperId,
                                    ShelfId = e.ShelfId,
                                    AudiobookId = e.AudiobookId,
                                    Title = e.Audiobook.Title,
                                    AuthorName = e.Audiobook.Author.Name
                                }
                        );

                return query.ToArray();
            }
        }

        public bool DeleteAudiobookFromShelf(int shelfAudioRecordId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var audiobookRecordToDelete =
                    ctx
                    .ShelfAudioRecords
                    .Single(r => r.AudioRecordKeeperId == shelfAudioRecordId);

                try
                {
                    ctx.ShelfAudioRecords.Remove(audiobookRecordToDelete);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return ctx.SaveChanges() == 1;
            }
        }

    }

}
