using Library.Common;
using Library.Data;
using Library.Services.Contracts;
using Library.Services.DTOs.BookDTOs;
using Library.Services.Mapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public class BookService : IBookService
    {

        private readonly LibraryDbContext _db;
        public BookService(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<int> BooksCountAsync()
        {
            var count = await _db.Books.CountAsync();

            return count;
        }

        public async Task<DisplayBookDTO> DeleteAsync(long id)
        {
            var book = await _db.Books
            .Include(x => x.Author)
            .Include(x => x.PublishHouse)
            .Include(x => x.Loans)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (book is null)
            {
                return new DisplayBookDTO { ErrorMessage = GlobalConstants.BOOK_NOT_FOUND };
            }

            var bookDTO = book.GetDTO();

            foreach (var item in book.Loans)
            {
                _db.Loans.Remove(item);
            }
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();

            return bookDTO;
        }

        public async Task<IEnumerable<DisplayBookDTO>> GetAsync(int page)
        {
            return await _db.Books
                             .Include(b => b.Author)
                             .Include(b => b.PublishHouse)
                             .Include(b => b.Loans)
                             .Skip(page * GlobalConstants.PageSkip)
                             .Take(10)
                             .Select(x => x.GetDTO())
                             .ToListAsync();
        }

        public async Task<DisplayBookDTO> GetBookByNameOrIdAsync(string nameOrId)
        {
            var book = await _db.Books
                                     .Include(b => b.Author)
                                     .Include(b => b.PublishHouse)
                                     .Include(b => b.Loans)
                                     .Where(x => x.Title == nameOrId || x.Id.ToString() == nameOrId)
                                     .Select(x => x.GetDTO())
                                     .FirstOrDefaultAsync();

            if (book is null)
            {
                return new DisplayBookDTO { ErrorMessage = GlobalConstants.BOOK_NOT_FOUND };
            }

            return book;
        }

        public async Task<DisplayBookDTO> PostAsync(CreateBookDTO obj)
        {
            if (obj.Title == null)
            {
                return new DisplayBookDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }
            if (await _db.Books.FirstOrDefaultAsync(x => x.Title == obj.Title) != null)
            {
                return new DisplayBookDTO { ErrorMessage = GlobalConstants.BOOK_EXISTS };
            }

            var newBook = obj.GetEntity();

            await _db.Books.AddAsync(newBook);
            await _db.SaveChangesAsync();

            var lastBook = _db.Books.Count();

            return await _db.Books.Where(x => x.Id == lastBook)
                                  .Include(x => x.PublishHouse)
                                  .Include(x => x.Author)
                                  .Select(x => x.GetDTO())
                                  .FirstOrDefaultAsync();
        }

        public async Task<DisplayBookDTO> UpdateAsync(long id, CreateBookDTO obj)
        {
            var book = await _db.Books.FirstOrDefaultAsync(x => x.Id == id);
            var checkExistingBook = await _db.Books.FirstOrDefaultAsync(x => x.Title == obj.Title);
            if (checkExistingBook != null
                && checkExistingBook.Id != id)
            {
                return new DisplayBookDTO { ErrorMessage = GlobalConstants.BOOK_EXISTS };
            }

            if (book is null)
            {
                return new DisplayBookDTO { ErrorMessage = GlobalConstants.BOOK_NOT_FOUND };
            }

            book.Title = obj.Title;
            book.Description = obj.Description;
            book.PublishHouseId = obj.PublishHouseId;
            book.AuthorId = obj.AuthorId;
            book.Year = obj.Year;

            await _db.SaveChangesAsync();

            return await _db.Books.Where(x => x.Id == id)
                      .Include(x => x.PublishHouse)
                      .Include(x => x.Author)
                      .Select(x => x.GetDTO())
                      .FirstOrDefaultAsync();
        }

        public async Task<DisplayBookDTO> UpdatePicture(long id, string pictureURL)
        {
            var book = await _db.Books
                                     .Include(b => b.Author)
                                     .Include(b => b.PublishHouse)
                                     .Include(b => b.Loans)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (book is null)
            {
                return new DisplayBookDTO { ErrorMessage = GlobalConstants.BOOK_NOT_FOUND };
            }
            book.URL = pictureURL;
            await _db.SaveChangesAsync();

            return book.GetDTO();
        }
    }
}
