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
        public bool CreateBook(BookCreate model)
        {
            var entity =
                new Book()
                {
                    Title = model.Title,
                    SeriesTitle = model.SeriesTitle,
                    AuthorId = model.AuthorId,
                    Isbn = model.Isbn,
                    Rating = model.Rating,
                    Genre = model.Genre,
                    Language = model.Language,
                    Publisher = model.Publisher,
                    IsEbook = model.IsEbook
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

    }
}
