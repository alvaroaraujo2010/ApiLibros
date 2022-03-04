using ApiLibros.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLibros.Interfaces
{
    public interface IEditoriales
    {
        Task<List<Editoriales>> GetEditList();
        Task<List<Editoriales>> GetEditListFilters(string EditorialId);
        Task<bool> InsertEditorial(Editoriales editorial);

    }
}
