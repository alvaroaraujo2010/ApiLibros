using ApiLibros.DTOs;
using ApiLibros.Entitidades.Repositorio;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLibros.Interfaces
{
    public class EditorialesDomain : IEditoriales
    {
        EditorialesRepositorio repository;
        public EditorialesDomain(IConfiguration config)
        {
            repository = new EditorialesRepositorio(config);
        }

        public async Task<List<Editoriales>> GetEditList()
        {
            try
            {
                return await repository.GetEditList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Editoriales>> GetEditListFilters(string EditorialId)
        {
            try
            {
                return await repository.GetEditListFilters(EditorialId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> InsertEditorial(Editoriales editorial)
        {
            try
            {
                return await repository.InsertEditorial(editorial);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
