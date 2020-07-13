using Shelfish.Data;
using Shelfish.Models.ReviewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Services
{
    public class ReviewService
    {
        private readonly Guid _userId;

        public ReviewService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateReview(ReviewCreate model)
        {
            var reviewToCreate =
                new Review()
                {
                    UserId = _userId,
                    BookId = model.BookId,
                    Title = model.Title,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Reviews.Add(reviewToCreate);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
