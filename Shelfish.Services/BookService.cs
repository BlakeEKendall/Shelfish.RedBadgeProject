using Shelfish.Data;
using Shelfish.Models.BookModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Services
{
    public class BookService
    {
        public bool CreateBook(BookCreate bookToCreate)
        {
            var entity =
                new Book()
                {
                    Title = bookToCreate.Title,
                    SeriesTitle = bookToCreate.SeriesTitle,
                    AuthorId = bookToCreate.AuthorId,
                    Isbn = bookToCreate.Isbn,
                    Rating = bookToCreate.Rating,
                    Genre = bookToCreate.Genre,
                    Language = bookToCreate.Language,
                    Publisher = bookToCreate.Publisher,
                    IsEbook = bookToCreate.IsEbook
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Books.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BookListItem> GetBooks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Books
                        .Select(
                            e =>
                                new BookListItem
                                {
                                    BookId = e.BookId,
                                    Title = e.Title,
                                    AuthorName = e.Author.Name,
                                    Isbn = e.Isbn
                                }
                        );

                return query.ToArray();
            }
        }

        public BookDetail GetBookById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Books
                        .Single(e => e.BookId == id);
                return
                    new BookDetail
                    {
                        BookId = entity.BookId,
                        Title = entity.Title,
                        SeriesTitle = entity.SeriesTitle,
                        AuthorName = entity.Author.Name,
                        Isbn = entity.Isbn,
                        Rating = entity.Rating,
                        Genre = entity.Genre,
                        Language = entity.Language,
                        Publisher = entity.Publisher,
                        IsEbook = entity.IsEbook
                    };
            }
        }

        public bool UpdateBook(BookEdit bookToBeUpdated)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Books
                        .Single(e => e.BookId == bookToBeUpdated.BookId);

                entity.Title = bookToBeUpdated.Title;
                entity.SeriesTitle = bookToBeUpdated.SeriesTitle;
                entity.Isbn = bookToBeUpdated.Isbn;
                entity.Rating = bookToBeUpdated.Rating;
                entity.Genre = bookToBeUpdated.Genre;
                entity.Language = bookToBeUpdated.Language;
                entity.Publisher = bookToBeUpdated.Publisher;
                entity.IsEbook = bookToBeUpdated.IsEbook;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBook(int bookId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var bookToDelete =
                    ctx
                        .Books
                        .Single(e => e.BookId == bookId);

                ctx.Books.Remove(bookToDelete);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
