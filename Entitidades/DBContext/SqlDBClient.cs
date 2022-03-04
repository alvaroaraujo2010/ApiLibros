using ApiLibros.Servicios;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ApiLibros.Entitidades.DBContext
{
    public class SqlDBClient
    {
        public static string cadenaConexion = string.Empty;
        public static SqlConnection conn = new SqlConnection();
        private Generals generals = new Generals();

        public SqlDBClient(string conext)
        {
            cadenaConexion = conext;
        }

        public void ConnectToSql()
        {
            try
            {
                conn.ConnectionString = cadenaConexion;
                conn.Open();
            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }
        }

        public void CerrarConexion()
        {
            try
            {
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SqlConnection getConnection()
        {
            try
            {
                return new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable getSpDataTable(string sp)
        {
            SqlConnection cn = this.getConnection();
            SqlCommand cmd = new SqlCommand(sp, cn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cn.Dispose();
                cmd.Dispose();
            }
        }

        public DataSet getSpDataSet(string sp)
        {
            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand();
            SqlConnection cn = this.getConnection();

            try
            {
                command.Connection = cn;
                command.CommandText = sp;
                command.CommandType = CommandType.StoredProcedure;
                new SqlDataAdapter(command).Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open) command.Connection.Close();
                command.Parameters.Clear();
                command.Dispose();
                ds.Dispose();
            }
        }

        public DataSet getSpDataSet(string sp, SqlParameter[] pars)
        {
            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand();
            SqlConnection cn = this.getConnection();

            try
            {
                command.Connection = cn;
                command.CommandText = sp;
                command.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter par in pars)
                {
                    command.Parameters.Add(par);
                }

                new SqlDataAdapter(command).Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open) command.Connection.Close();
                command.Parameters.Clear();
                command.Dispose();
                ds.Dispose();
            }
        }

        public DataTable getSpDataTable(string sp, SqlParameter[] pars)
        {
            DataTable ds = new DataTable();
            SqlCommand command = new SqlCommand();
            SqlConnection cn = this.getConnection();

            try
            {
                command.Connection = cn;
                command.CommandText = sp;
                command.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter par in pars)
                {
                    command.Parameters.Add(par);
                }

                new SqlDataAdapter(command).Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open) command.Connection.Close();
                command.Parameters.Clear();
                command.Dispose();
                ds.Dispose();
            }
        }

        public string getSpJson(string sp, SqlParameter[] pars)
        {
            DataTable ds = new DataTable();
            SqlCommand command = new SqlCommand();
            SqlConnection cn = this.getConnection();

            try
            {
                command.Connection = cn;
                command.CommandText = sp;
                command.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter par in pars)
                {
                    command.Parameters.Add(par);
                }

                new SqlDataAdapter(command).Fill(ds);
                return generals.DataTableToJSON(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open) command.Connection.Close();
                command.Parameters.Clear();
                command.Dispose();
                ds.Dispose();
            }
        }

        public bool executeSp(string sp, SqlParameter[] pars)
        {
            SqlCommand command = new SqlCommand();
            SqlConnection cn = this.getConnection();

            try
            {
                command.Connection = cn;
                command.CommandText = sp;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                foreach (SqlParameter par in pars)
                {
                    command.Parameters.Add(par);
                }

                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open) command.Connection.Close();
                command.Parameters.Clear();
                command.Dispose();
            }
        }

    }
}
