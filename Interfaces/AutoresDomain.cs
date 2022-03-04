using ApiLibros.DTOs;
using ApiLibros.Entitidades.Repositorio;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLibros.Interfaces
{
    public class AutoresDomain : IAutores
    {
        AutoresRepositorio repository;
        public AutoresDomain(IConfiguration config)
        {
            repository = new AutoresRepositorio(config);
        }

        public async Task<List<Autores>> GetAuthorList()
        {
            try
            {
                return await repository.GetAuthorList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Autores>> GetAuthorFilters(string authorId)
        {
            try
            {
                return await repository.GetAuthorFilters(authorId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> InsertAuthor(Autores autores)
        {
            try
            {
                return await repository.InsertAuthor(autores);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
