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
    public class LibrosRepositorio
    {
        SqlDBClient Acceso;
        private readonly IConfiguration _config;
        public LibrosRepositorio(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<Libros>> GetBooksList()
        {
            try
            {
                string strinConection = string.Empty;
                strinConection = _config["SQLConection:DBConnection"];

                Acceso = new SqlDBClient(strinConection);
                SqlConnection cn = Acceso.getConnection();
                DataTable dataFiltres = new DataTable();
                List<Libros> listaLibros = new List<Libros>();
                byte[] vacio = new byte[0];
                Libros libros = null;
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("ObtenerListaLibros", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter daConsulta = new SqlDataAdapter(cmdConsulta);
                    daConsulta.Fill(dataFiltres);

                    if (dataFiltres.Rows.Count != 0)
                    {
                        foreach (DataRow prop in dataFiltres.Rows)
                        {
                            libros = new Libros()
                            {
                                Id = prop[0].ToString(),
                                Titulo = prop[1].ToString(),
                                Año = prop[2].ToString(),
                                Genero = prop[3].ToString(),
                                NumPaginas = Convert.ToInt32(prop[4].ToString()),
                                Editorial = new Editoriales()
                                {
                                    Id = prop[5].ToString(),
                                    Nombre = prop[6].ToString(),
                                    DireccionCorrespondencia = prop[7].ToString(),
                                    Telefono = prop[8].ToString(),
                                    CorreElectronico = prop[9].ToString(),
                                    MaxLibros = Convert.ToInt32(prop[10].ToString()),
                                },
                                Autor = new Autores()
                                {
                                    Id = prop[11].ToString(),
                                    NombreCompleto = prop[12].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(prop[13].ToString()),
                                    CuidadProcedencia = prop[14].ToString(),
                                    CorreElectronico = prop[15].ToString(),
                                }
                            };
                            listaLibros.Add(libros);
                        }
                        return listaLibros;
                    }
                    else return null;
                }
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
                string strinConection = string.Empty;
                strinConection = _config["SQLConection:DBConnection"];

                Acceso = new SqlDBClient(strinConection);
                SqlConnection cn = Acceso.getConnection();
                DataTable dataFiltres = new DataTable();
                List<Libros> listaLibros = new List<Libros>();
                byte[] vacio = new byte[0];
                Libros libros = null;
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("BuscarLibroFiltros", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@NomAutor", author);
                    cmdConsulta.Parameters.AddWithValue("@Titulo", tittle);
                    cmdConsulta.Parameters.AddWithValue("@AñoLib", year);

                    SqlDataAdapter daConsulta = new SqlDataAdapter(cmdConsulta);
                    daConsulta.Fill(dataFiltres);

                    if (dataFiltres.Rows.Count != 0)
                    {
                        foreach (DataRow prop in dataFiltres.Rows)
                        {
                            libros = new Libros()
                            {
                                Id = prop[0].ToString(),
                                Titulo = prop[1].ToString(),
                                Año = prop[2].ToString(),
                                Genero = prop[3].ToString(),
                                NumPaginas = Convert.ToInt32(prop[4].ToString()),
                                Editorial = new Editoriales()
                                {
                                    Id = prop[5].ToString(),
                                    Nombre = prop[6].ToString(),
                                    DireccionCorrespondencia = prop[7].ToString(),
                                    Telefono = prop[8].ToString(),
                                    CorreElectronico = prop[9].ToString(),
                                    MaxLibros = Convert.ToInt32(prop[10].ToString()),
                                },
                                Autor = new Autores()
                                {
                                    Id = prop[11].ToString(),
                                    NombreCompleto = prop[12].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(prop[13].ToString()),
                                    CuidadProcedencia = prop[14].ToString(),
                                    CorreElectronico = prop[15].ToString(),
                                }
                            };
                            listaLibros.Add(libros);
                        }
                        return listaLibros;
                    }
                    else return null;
                }
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
                string strinConection = string.Empty;
                strinConection = _config["SQLConection:DBConnection"];

                Acceso = new SqlDBClient(strinConection);
                SqlConnection cn = Acceso.getConnection();
                cn.Open();
                using (SqlCommand cmdConsulta = new SqlCommand("InsertarLibro", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@pId", Guid.NewGuid().ToString("D"));
                    cmdConsulta.Parameters.AddWithValue("@Titulo", libros.Id);
                    cmdConsulta.Parameters.AddWithValue("@Año", libros.Año);
                    cmdConsulta.Parameters.AddWithValue("@Genero", libros.Genero);
                    cmdConsulta.Parameters.AddWithValue("@NumPaginas", libros.NumPaginas);
                    cmdConsulta.Parameters.AddWithValue("@IdEditorial", libros.Editorial.Id);
                    cmdConsulta.Parameters.AddWithValue("@IdAutor", libros.Autor.Id);

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
