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
    public class EditorialesController : Controller
    {
        private EditorialesDomain _domain;
        private readonly IConfiguration _Config;
        public EditorialesController(IConfiguration config)
        {
            _domain = new EditorialesDomain(config);
            _Config = config;
        }

        [HttpGet("GetEditList")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<dynamic>> GetEditList()
        {
            Result<dynamic> result = new Result<dynamic> { IsSuccess = true, StatusCode = 200, Message = "Editorials List Complete" };
            try
            {
                List<Editoriales> EditorialesList = new List<Editoriales>();
                EditorialesList = await _domain.GetEditList();
                if (EditorialesList == null)
                {
                    result.IsSuccess = true;
                    result.Message = "No data to display";
                    result.StatusCode = 200;
                    return BadRequest(result);
                }

                if (EditorialesList.Count > 0)
                {
                    result.Data = EditorialesList;
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

        [HttpGet("GetEditListFilters")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<dynamic>> GetEditListFilters(string EditorialId)
        {
            Result<dynamic> result = new Result<dynamic> { IsSuccess = true, StatusCode = 200, Message = "Editorial Complete" };
            try
            {
                List<Editoriales> EditorialesList = new List<Editoriales>();
                EditorialesList = await _domain.GetEditListFilters(EditorialId);
                if (EditorialesList == null)
                {
                    result.IsSuccess = true;
                    result.Message = "No data to display";
                    result.StatusCode = 200;
                    return BadRequest(result);
                }

                if (EditorialesList.Count > 0)
                {
                    result.Data = EditorialesList;
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
        public async Task<ActionResult<dynamic>> InsertEditorial([FromBody] Editoriales editorial)
        {
            try
            {
                Result<dynamic> result = new Result<dynamic> { IsSuccess = true, StatusCode = 200 };
                if (editorial.Id != String.Empty)
                {
                    var resultado = await _domain.InsertEditorial(editorial);
                    return Ok(resultado);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Error inserting Editorial by Id null";
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
