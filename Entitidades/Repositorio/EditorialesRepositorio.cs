using ApiLibros.DTOs;
using ApiLibros.Entitidades.DBContext;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiLibros.Entitidades.Repositorio
{
    public class EditorialesRepositorio
    {
        SqlDBClient Acceso;
        private readonly IConfiguration _config;
        public EditorialesRepositorio(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<Editoriales>> GetEditList()
        {
            try
            {
                string strinConection = string.Empty;
                strinConection = _config["SQLConection:DBConnection"];

                Acceso = new SqlDBClient(strinConection);
                SqlConnection cn = Acceso.getConnection();
                DataTable dataFiltres = new DataTable();
                List<Editoriales> listaEditoriales = new List<Editoriales>();
                byte[] vacio = new byte[0];
                Editoriales Editoriales = null;
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("ObtenerListaEditoriales", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter daConsulta = new SqlDataAdapter(cmdConsulta);
                    daConsulta.Fill(dataFiltres);

                    if (dataFiltres.Rows.Count != 0)
                    {
                        foreach (DataRow prop in dataFiltres.Rows)
                        {
                            Editoriales = new Editoriales()
                            {
                                Id = prop[0].ToString(),
                                Nombre = prop[1].ToString(),
                                DireccionCorrespondencia = prop[2].ToString(),
                                Telefono = prop[3].ToString(),
                                CorreElectronico = prop[4].ToString(),
                                MaxLibros = Convert.ToInt32(prop[5].ToString())
                            };
                            listaEditoriales.Add(Editoriales);
                        }
                        return listaEditoriales;
                    }
                    else return null;
                }
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
                string strinConection = string.Empty;
                strinConection = _config["SQLConection:DBConnection"];

                Acceso = new SqlDBClient(strinConection);
                SqlConnection cn = Acceso.getConnection();
                DataTable dataFiltres = new DataTable();
                List<Editoriales> listaEditoriales = new List<Editoriales>();
                byte[] vacio = new byte[0];
                Editoriales Editoriales = null;
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("ObtenerEditorialId", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@IdEditoriales", EditorialId);

                    SqlDataAdapter daConsulta = new SqlDataAdapter(cmdConsulta);
                    daConsulta.Fill(dataFiltres);

                    if (dataFiltres.Rows.Count != 0)
                    {
                        foreach (DataRow prop in dataFiltres.Rows)
                        {
                            Editoriales = new Editoriales()
                            {
                                Id = prop[0].ToString(),
                                Nombre = prop[1].ToString(),
                                DireccionCorrespondencia = prop[2].ToString(),
                                Telefono = prop[3].ToString(),
                                CorreElectronico = prop[4].ToString(),
                                MaxLibros = Convert.ToInt32(prop[5].ToString())
                            };
                            listaEditoriales.Add(Editoriales);
                        }
                        return listaEditoriales;
                    }
                    else return null;
                }
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
                string strinConection = string.Empty;
                strinConection = _config["SQLConection:DBConnection"];

                Acceso = new SqlDBClient(strinConection);
                SqlConnection cn = Acceso.getConnection();
                cn.Open();
                using (SqlCommand cmdConsulta = new SqlCommand("InsertarAutor", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@pId", Guid.NewGuid().ToString("D"));
                    cmdConsulta.Parameters.AddWithValue("@Nombre", editorial.Id);
                    cmdConsulta.Parameters.AddWithValue("@DireccionCorrespondencia", editorial.DireccionCorrespondencia);
                    cmdConsulta.Parameters.AddWithValue("@Telefono", editorial.Telefono);
                    cmdConsulta.Parameters.AddWithValue("@CorreElectronico", editorial.CorreElectronico);
                    cmdConsulta.Parameters.AddWithValue("@MaxLibros", editorial.MaxLibros);

                    cmdConsulta.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetBooksMaxEditorial(string EditorialId)
        {
            try
            {
                string strinConection = string.Empty;
                strinConection = _config["SQLConection:DBConnection"];

                Acceso = new SqlDBClient(strinConection);
                SqlConnection cn = Acceso.getConnection();
                DataTable dataFiltres = new DataTable();
                byte[] vacio = new byte[0];
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("ConsultarMaximosLibrosEditorial", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@IdEditorial", EditorialId);

                    SqlDataAdapter daConsulta = new SqlDataAdapter(cmdConsulta);
                    daConsulta.Fill(dataFiltres);

                    if (dataFiltres.Rows.Count != 0)
                    {
                        if (Convert.ToInt32(dataFiltres.Rows[0][0].ToString()) == Convert.ToInt32(dataFiltres.Rows[0][1].ToString()))
                            return "No es posible registrar el libro, se alcanzó el máximo permitido.";
                        else return "OK";
                    }
                    else return "No hay registros";   
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
