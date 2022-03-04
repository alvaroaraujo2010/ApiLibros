using ApiLibros.DTOs;
using ApiLibros.Entitidades.Repositorio;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLibros.Interfaces
{
    public class LibrosDomain : ILibros
    {
        LibrosRepositorio repository;
        EditorialesRepositorio editorialesRepositorio;
        public LibrosDomain(IConfiguration config)
        {
            repository = new LibrosRepositorio(config);
            editorialesRepositorio = new EditorialesRepositorio(config);
        }
        public async Task<List<Libros>> GetBooksList()
        {
            try
            {
                return await repository.GetBooksList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Libros>> GetBooksListFilters(string author, string tittle, string year)
        {
            try
            {
                return await repository.GetBooksListFilters(author, tittle, year);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> InsertBook(Libros libros)
        {
            try
            {
                return await repository.InsertBook(libros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetBooksMaxEditorial(string EditorialId)
        {
            try
            {
                return editorialesRepositorio.GetBooksMaxEditorial(EditorialId);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
