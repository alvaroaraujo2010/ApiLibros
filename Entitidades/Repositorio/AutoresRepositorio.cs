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
    public class AutoresRepositorio
    {
        SqlDBClient Acceso;
        private readonly IConfiguration _config;
        public AutoresRepositorio(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<Autores>> GetAuthorList()
        {
            try
            {
                string strinConection = string.Empty;
                strinConection = _config["SQLConection:DBConnection"];

                Acceso = new SqlDBClient(strinConection);
                SqlConnection cn = Acceso.getConnection();
                DataTable dataFiltres = new DataTable();
                List<Autores> listaAutores = new List<Autores>();
                byte[] vacio = new byte[0];
                Autores Autores = null;
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("ObtenerListaAutores", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter daConsulta = new SqlDataAdapter(cmdConsulta);
                    daConsulta.Fill(dataFiltres);

                    if (dataFiltres.Rows.Count != 0)
                    {
                        foreach (DataRow prop in dataFiltres.Rows)
                        {
                            Autores = new Autores()
                            {
                                Id = prop[0].ToString(),
                                NombreCompleto = prop[1].ToString(),
                                FechaNacimiento = Convert.ToDateTime(prop[2].ToString()),
                                CuidadProcedencia = prop[3].ToString(),
                                CorreElectronico = prop[4].ToString()
                            };
                            listaAutores.Add(Autores);
                        }
                        return listaAutores;
                    }
                    else return null;
                }
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
                string strinConection = string.Empty;
                strinConection = _config["SQLConection:DBConnection"];

                Acceso = new SqlDBClient(strinConection);
                SqlConnection cn = Acceso.getConnection();
                DataTable dataFiltres = new DataTable();
                List<Autores> listaAutores = new List<Autores>();
                byte[] vacio = new byte[0];
                Autores Autores = null;
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("ObtenerAutorId", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@IdAutor", authorId);

                    SqlDataAdapter daConsulta = new SqlDataAdapter(cmdConsulta);
                    daConsulta.Fill(dataFiltres);

                    if (dataFiltres.Rows.Count != 0)
                    {
                        foreach (DataRow prop in dataFiltres.Rows)
                        {
                            Autores = new Autores()
                            {
                                Id = prop[0].ToString(),
                                NombreCompleto = prop[1].ToString(),
                                FechaNacimiento = Convert.ToDateTime(prop[2].ToString()),
                                CuidadProcedencia = prop[3].ToString(),
                                CorreElectronico = prop[4].ToString()
                            };
                            listaAutores.Add(Autores);
                        }
                        return listaAutores;
                    }
                    else return null;
                }
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
                    cmdConsulta.Parameters.AddWithValue("@NombreCompleto", autores.Id);
                    cmdConsulta.Parameters.AddWithValue("@FechaNacimiento", autores.FechaNacimiento);
                    cmdConsulta.Parameters.AddWithValue("@CuidadProcedencia", autores.CuidadProcedencia);
                    cmdConsulta.Parameters.AddWithValue("@CorreElectronico", autores.CorreElectronico);

                    cmdConsulta.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
