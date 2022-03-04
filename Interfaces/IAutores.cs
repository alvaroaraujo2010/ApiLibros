using ApiLibros.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLibros.Interfaces
{
    public interface IAutores
    {
        Task<List<Autores>> GetAuthorList();
        Task<List<Autores>> GetAuthorFilters(string authorId);
        Task<bool> InsertAuthor(Autores autores);

    }
}
