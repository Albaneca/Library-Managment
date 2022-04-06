using Library.Services.DTOs.AuthorDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Contracts
{
    public interface IAuthorService
    {
        Task<IEnumerable<DisplayAuthorDTO>> GetAsync(int page);
        Task<DisplayAuthorDTO> PostAsync(CreateAuthorDTO obj);
        Task<DisplayAuthorDTO> UpdateAsync(long id, CreateAuthorDTO obj);
        Task<DisplayAuthorDTO> DeleteAsync(long id);
        Task<IEnumerable<DisplayAuthorDTO>> FilterAuthorsAsync(int page, string part);
        Task<DisplayAuthorDTO> GetAuthorById(string nameOrId);
        Task<DisplayAuthorDTO> UpdatePicture(long id, string pictureURL);

        Task<int> AuthorCountAsync();
    }
}
