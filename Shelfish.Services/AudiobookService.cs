using Shelfish.Data;
using Shelfish.Models.AudiobookModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Services
{
    public class AudiobookService
    {
        public bool CreateAudiobook(AudiobookCreate model)
        {
            var audiobookToCreate =
                new Audiobook()
                {
                    Title = model.Title,
                    SeriesTitle = model.SeriesTitle,
                    AuthorId = model.AuthorId,
                    Isbn = model.Isbn,
                    Rating = model.Rating,
                    Genre = model.Genre,
                    Language = model.Language,
                    Publisher = model.Publisher,
                    NarratorName = model.NarratorName,
                    AudioFormat = model.AudioFormat,
                    IsAbridged = model.IsAbridged
                    
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Audiobooks.Add(audiobookToCreate);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<AudiobookListItem> GetAudiobooks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Audiobooks
                        .Select(
                            e =>
                                new AudiobookListItem
                                {
                                    AudiobookId = e.AudiobookId,
                                    Title = e.Title,
                                    AuthorName = e.Author.Name,
                                    Isbn = e.Isbn
                                }
                        );

                return query.ToArray();
            }
        }

        public AudiobookDetail GetAudiobookById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Audiobooks
                        .Single(e => e.AudiobookId == id);
                return
                    new AudiobookDetail
                    {
                        AudiobookId = entity.AudiobookId,
                        Title = entity.Title,
                        SeriesTitle = entity.SeriesTitle,
                        AuthorName = entity.Author.Name,
                        Isbn = entity.Isbn,
                        Rating = entity.Rating,
                        Genre = entity.Genre,
                        Language = entity.Language,
                        Publisher = entity.Publisher,
                        NarratorName = entity.NarratorName,
                        AudioFormat = entity.AudioFormat,
                        IsAbridged = entity.IsAbridged
                    };
            }
        }

        public bool UpdateAudiobook(AudiobookEdit audiobookToBeUpdated)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Audiobooks
                        .Single(e => e.AudiobookId == audiobookToBeUpdated.AudiobookId);

                entity.Title = audiobookToBeUpdated.Title;
                entity.SeriesTitle = audiobookToBeUpdated.SeriesTitle;
                entity.Isbn = audiobookToBeUpdated.Isbn;
                entity.Rating = audiobookToBeUpdated.Rating;
                entity.Genre = audiobookToBeUpdated.Genre;
                entity.Language = audiobookToBeUpdated.Language;
                entity.Publisher = audiobookToBeUpdated.Publisher;
                entity.NarratorName = audiobookToBeUpdated.NarratorName;
                entity.AudioFormat = audiobookToBeUpdated.AudioFormat;
                entity.IsAbridged = audiobookToBeUpdated.IsAbridged;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAudiobook(int audiobookId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var audiobookToDelete =
                    ctx
                        .Audiobooks
                        .Single(e => e.AudiobookId == audiobookId);

                ctx.Audiobooks.Remove(audiobookToDelete);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
