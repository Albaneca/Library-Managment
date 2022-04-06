using Library.Services.DTOs.BookDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<DisplayBookDTO>> GetAsync(int page);
        Task<DisplayBookDTO> PostAsync(CreateBookDTO obj);
        Task<DisplayBookDTO> UpdateAsync(long id, CreateBookDTO obj);
        Task<DisplayBookDTO> DeleteAsync(long id);
        Task<DisplayBookDTO> GetBookByNameOrIdAsync(string nameOrId);
        Task<DisplayBookDTO> UpdatePicture(long id, string pictureURL);
        Task<int> BooksCountAsync();
    }
}
