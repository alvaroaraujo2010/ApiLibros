using ApiLibros.DTOs;
using ApiLibros.Excepciones;
using ApiLibros.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ApiLibros.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibrosController : ControllerBase
    {
        private LibrosDomain _domain;
        private readonly IConfiguration _Config;
        public LibrosController(IConfiguration config)
        {
            _domain = new LibrosDomain(config);
            _Config = config;
        }

        [HttpGet("GetBooksList")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<dynamic>> GetBooksList()
        {
            Result<dynamic> result = new Result<dynamic> { IsSuccess = true, StatusCode = 200, Message = "Books List Complete" };
            try
            {
                List<Libros> librosList = new List<Libros>();
                librosList = await _domain.GetBooksList();
                if (librosList == null)
                {
                    result.IsSuccess = true;
                    result.Message = "No data to display";
                    result.StatusCode = 200;
                    return BadRequest(result);
                }

                if (librosList.Count > 0)
                {
                    result.Data = librosList;
                    return Ok(result);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "No data to display";
                    result.StatusCode = 200;
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                result.StatusCode = 400;
                return BadRequest(result);
            }
        }

        [HttpGet("GetBooksListFilters")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<dynamic>> GetBooksListFilters(string author, string tittle, string year)
        {
            Result<dynamic> result = new Result<dynamic> { IsSuccess = true, StatusCode = 200, Message = "Book Complete" };
            try
            {
                List<Libros> librosList = new List<Libros>();
                librosList = await _domain.GetBooksListFilters(author, tittle, year);
                if (librosList == null)
                {
                    result.IsSuccess = true;
                    result.Message = "No data to display";
                    result.StatusCode = 200;
                    return BadRequest(result);
                }

                if (librosList.Count > 0)
                {
                    result.Data = librosList;
                    return Ok(result);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "No data to display";
                    result.StatusCode = 200;
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                result.StatusCode = 400;
                return BadRequest(result);
            }
        }

        [HttpPost("InsertBook")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<dynamic>> InsertBook([FromBody] Libros libros)
        {
            try
            {
                Result<dynamic> result = new Result<dynamic> { IsSuccess = true, StatusCode = 200 };
                if (libros.Id != String.Empty)
                {
                    string respuesta = _domain.GetBooksMaxEditorial(libros.Editorial.Id);
                    if (!respuesta.Equals("OK"))
                    {
                        result.Message = respuesta;
                        return Ok(result);
                    }

                    var resultado = await _domain.InsertBook(libros);
                    return Ok(resultado);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Error inserting book by Id null";
                    result.StatusCode = 200;
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
