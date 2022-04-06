using Library.Common;
using Library.Data;
using Library.Services.Contracts;
using Library.Services.DTOs.AuthorDTOs;
using Library.Services.Mapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryDbContext _db;
        public AuthorService(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<DisplayAuthorDTO> DeleteAsync(long id)
        {
            var author = await _db.Authors
                        .Include(a => a.Books)
                        .ThenInclude(b => b.Loans)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (author is null)
            {
                return new DisplayAuthorDTO { ErrorMessage = GlobalConstants.AUTHOR_NOT_FOUND };
            }

            var authorDTO = author.GetDTO();

            foreach (var book in _db.Books.Where(x => x.AuthorId == id))
            {
                _db.Books.Remove(book);
                foreach (var loan in book.Loans)
                {
                    _db.Loans.Remove(loan);
                }
            }
            _db.Authors.Remove(author);

            await _db.SaveChangesAsync();

            return authorDTO;
        }

        public async Task<IEnumerable<DisplayAuthorDTO>> GetAsync(int page)
        {
            return await _db.Authors
                     .Include(a => a.Books)
                     .Skip(page * GlobalConstants.PageSkip)
                     .Take(10)
                     .Select(x => x.GetDTO())
                     .ToListAsync();
        }

        public async Task<IEnumerable<DisplayAuthorDTO>> FilterAuthorsAsync(int page, string part)
        {
            return await _db.Authors.Where(x => x.Name.Contains(part))
                                           .Include(a => a.Books)
                                           .Skip(page * GlobalConstants.PageSkip)
                                           .Take(10)
                                           .Select(x => x.GetDTO())
                                           .ToListAsync();
        }

        public async Task<DisplayAuthorDTO> GetAuthorById(string id)
        {
            var author = await _db.Authors
                                     .Include(a => a.Books)
                                     .Where(x => x.Id.ToString() == id)
                                     .Select(x => x.GetDTO())
                                     .FirstOrDefaultAsync();

            if (author is null)
            {
                return new DisplayAuthorDTO { ErrorMessage = GlobalConstants.AUTHOR_NOT_FOUND };
            }

            return author;
        }

        public async Task<DisplayAuthorDTO> PostAsync(CreateAuthorDTO obj)
        {
            if (obj.Name == null)
            {
                return new DisplayAuthorDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }
            if (await _db.Authors.FirstOrDefaultAsync(x => x.Name == obj.Name) != null)
            {
                return new DisplayAuthorDTO { ErrorMessage = GlobalConstants.AUTHOR_EXISTS };
            }

            var newAuthor = obj.GetEntity();

            await _db.Authors.AddAsync(newAuthor);
            await _db.SaveChangesAsync();

            var lastAuthor = _db.Authors.Count();

            return await _db.Authors.Where(x => x.Id == lastAuthor)
                                  .Select(x => x.GetDTO())
                                  .FirstOrDefaultAsync();

        }

        public async Task<DisplayAuthorDTO> UpdateAsync(long id, CreateAuthorDTO obj)
        {
            if (await _db.Authors.FirstOrDefaultAsync(x => x.Name == obj.Name) != null)
            {
                return new DisplayAuthorDTO { ErrorMessage = GlobalConstants.AUTHOR_EXISTS };
            }

            var author = await _db.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if (author is null)
            {
                return new DisplayAuthorDTO { ErrorMessage = GlobalConstants.AUTHOR_NOT_FOUND };
            }

            author.Name = obj.Name;

            await _db.SaveChangesAsync();

            return author.GetDTO();
        }

        public async Task<DisplayAuthorDTO> UpdatePicture(long id, string pictureURL)
        {
            var author = await _db.Authors
                                    .Include(a => a.Books)
                                    .FirstOrDefaultAsync(x => x.Id == id);

            if (author is null)
            {
                return new DisplayAuthorDTO { ErrorMessage = GlobalConstants.BOOK_NOT_FOUND };
            }
            author.URL = pictureURL;
            await _db.SaveChangesAsync();

            return author.GetDTO();
        }

        public async Task<int> AuthorCountAsync()
        {
            return await _db.Authors.CountAsync();
        }
    }
}
