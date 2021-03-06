﻿using Shelfish.Data;
using Shelfish.Models.AuthorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Services
{
    public class AuthorService
    {
        
        public bool CreateAuthor(AuthorCreate model)
        {
            var authorToCreate =
                new Author()
                {
                    Name = model.Name,
                    CountryName = model.CountryName,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Authors.Add(authorToCreate);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<AuthorListItem> GetAuthors()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Authors
                        .Select(
                            e =>
                                new AuthorListItem
                                {
                                    AuthorId = e.AuthorId,
                                    Name = e.Name,
                                }
                        );

                return query.ToArray();
            }
        }

        public AuthorDetail GetAuthorById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Authors
                        .Single(e => e.AuthorId == id);
                    return
                        new AuthorDetail
                        {
                            AuthorId = entity.AuthorId,
                            Name = entity.Name,
                            CountryName = entity.CountryName
                        };
            }
        }

        public bool UpdateAuthor(AuthorEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var authorToUpdate =
                    ctx
                        .Authors
                        .Single(e => e.AuthorId == model.AuthorId);

                authorToUpdate.Name = model.Name;
                authorToUpdate.CountryName = model.CountryName;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAuthor(int authorId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var authorToDelete =
                    ctx
                        .Authors
                        .Single(e => e.AuthorId == authorId);

                ctx.Authors.Remove(authorToDelete);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
