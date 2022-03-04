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
    public class AutoresController : Controller
    {
        private AutoresDomain _domain;
        private readonly IConfiguration _Config;
        public AutoresController(IConfiguration config)
        {
            _domain = new AutoresDomain(config);
            _Config = config;
        }

        [HttpGet("GetAuthorList")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<dynamic>> GetAuthorList()
        {
            Result<dynamic> result = new Result<dynamic> { IsSuccess = true, StatusCode = 200, Message = "Authors List Complete" };
            try
            {
                List<Autores> AutoresList = new List<Autores>();
                AutoresList = await _domain.GetAuthorList();
                if (AutoresList == null)
                {
                    result.IsSuccess = true;
                    result.Message = "No data to display";
                    result.StatusCode = 200;
                    return BadRequest(result);
                }

                if (AutoresList.Count > 0)
                {
                    result.Data = AutoresList;
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

        [HttpGet("GetAuthorFilters")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<dynamic>> GetAuthorFilters(string authorId)
        {
            Result<dynamic> result = new Result<dynamic> { IsSuccess = true, StatusCode = 200, Message = "Author Complete" };
            try
            {
                List<Autores> AutoresList = new List<Autores>();
                AutoresList = await _domain.GetAuthorFilters(authorId);
                if (AutoresList == null)
                {
                    result.IsSuccess = true;
                    result.Message = "No data to display";
                    result.StatusCode = 200;
                    return BadRequest(result);
                }

                if (AutoresList.Count > 0)
                {
                    result.Data = AutoresList;
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

        [HttpPost("InsertAuthor")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<dynamic>> InsertAuthor([FromBody] Autores autores)
        {
            try
            {
                Result<dynamic> result = new Result<dynamic> { IsSuccess = true, StatusCode = 200 };
                if (autores.Id != String.Empty)
                {
                    var resultado = await _domain.InsertAuthor(autores);
                    return Ok(resultado);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Error inserting Author by Id null";
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
