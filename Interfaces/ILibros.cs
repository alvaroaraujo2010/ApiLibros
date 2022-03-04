using ApiLibros.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLibros.Interfaces
{
    public interface ILibros
    {
        Task<List<Libros>> GetBooksList();
        Task<List<Libros>> GetBooksListFilters(string author, string tittle, string year);
        Task<bool> InsertBook(Libros libros);
        string GetBooksMaxEditorial(string EditorialId);
    }
}
