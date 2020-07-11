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
    }
}
