using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace HappyDbAccess
{ 
    public class DbAccess
    {
        private string connectionString = "";
        public DbAccess(string connectionString) {
            this.connectionString = connectionString;
        }
        SqlConnection conn = new SqlConnection();

        SqlConnection GetConnection()
        {
            if (conn.State==ConnectionState.Closed)
            {
                conn = new SqlConnection(this.connectionString);
                conn.Open();
            }
            return conn;
        }

        public int Update(string sql) {

            using (SqlCommand cmd = new SqlCommand (sql,GetConnection()))
            {
                try
                {
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public int Update(string sql, List<SqlParameter> parameters)
        {
            using (SqlCommand cmd = new SqlCommand(sql, GetConnection()))
            {
                try
                {
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.Add(item);
                    }
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

        }

        public DataTable SelectTable(string sql) {

            using (SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection()))
            {
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public DataSet SelectDataSet(string sql) {
            using (SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection()))
            {
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds);
                    return ds;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public DataTable SelectTable(string sql, List<SqlParameter> parameters)
        {

            using (SqlCommand cmd = new SqlCommand(sql, GetConnection()))
            {
                DataTable dt = new DataTable();
                try
                {
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.Add(item);
                    }

                    dt.Load(cmd.ExecuteReader());
                    return dt;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public DataSet SelectDataSet(string sql, List<SqlParameter> parameters)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection()))
            {

                foreach (var item in parameters)
                {
                    da.SelectCommand.Parameters.Add(item);
                }
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds);
                    return ds;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }


        //stored precedure.
        public bool Update(QueryParametersBuddle parameterBuddle, out List<DataOut> dataListOut)
        {

            try
            {
                SqlConnection conn = GetConnection();

                SqlCommand sqlCommand = new SqlCommand(parameterBuddle.SPName, conn);

                IEnumerable<QueryParameters> inParametes = parameterBuddle.QueryParametersList.Where(p => p.ParameterMode == ParameterMode.In);
                IEnumerable<QueryParameters> outParametes = parameterBuddle.QueryParametersList.Where(p => p.ParameterMode == ParameterMode.Out);
                if (inParametes != null)
                {
                    foreach (var item in inParametes)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter(string.Format("@{0}", item.ParameterName), item.Value));
                    }
                }

                List<QueryDataOut> outParaList = new List<QueryDataOut>();
                if (outParametes != null)
                {
                    foreach (var item in outParametes)
                    {
                        SqlParameter outPara = new SqlParameter(string.Format("@{0}", item.ParameterName), item.SqlDbType, item.Size);
                        outPara.Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add(outPara);
                        outParaList.Add(new QueryDataOut { Key = item.ParameterName, Value = outPara });
                    }
                }

                sqlCommand.CommandType = (parameterBuddle.QueryType == QueryType.Inline) ? CommandType.Text : CommandType.StoredProcedure;
                sqlCommand.ExecuteNonQuery();

                List<DataOut> DataList = new List<DataOut>();
                if (outParaList.Count != 0)
                {
                    foreach (var item in outParaList)
                    {
                        DataList.Add(new DataOut { Key = item.Key, Value = item.Value.Value.ToString() });
                    }
                }

                dataListOut = DataList;

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool Select(QueryParametersBuddle parameterBuddle, out List<DataOut> dataListOut, out DataTable dtOut)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand(parameterBuddle.SPName, GetConnection());

                IEnumerable<QueryParameters> inParametes = parameterBuddle.QueryParametersList.Where(p => p.ParameterMode == ParameterMode.In);
                IEnumerable<QueryParameters> outParametes = parameterBuddle.QueryParametersList.Where(p => p.ParameterMode == ParameterMode.Out);
                if (inParametes != null)
                {
                    foreach (var item in inParametes)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter(string.Format("@{0}", item.ParameterName), item.Value));
                    }

                }

                List<QueryDataOut> outParaList = new List<QueryDataOut>();
                if (outParametes != null)
                {
                    foreach (var item in outParametes)
                    {
                        SqlParameter outPara = new SqlParameter(string.Format("@{0}", item.ParameterName), item.SqlDbType);
                        outPara.Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add(outPara);
                        outParaList.Add(new QueryDataOut { Key = item.ParameterName, Value = outPara });
                    }
                }

                sqlCommand.CommandType = (parameterBuddle.QueryType == QueryType.Inline) ? CommandType.Text : CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                dt.Load(sqlCommand.ExecuteReader());

                List<DataOut> DataList = new List<DataOut>();
                if (outParaList.Count != 0)
                {
                    foreach (var item in outParaList)
                    {
                        DataList.Add(new DataOut { Key = item.Key, Value = item.Value.Value.ToString() });
                    }
                }

                dataListOut = DataList;
                dtOut = dt;
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public bool Select(QueryParametersBuddle parameterBuddle, out List<DataOut> dataListOut, out DataSet dsOut)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand(parameterBuddle.SPName, GetConnection());

                IEnumerable<QueryParameters> inParametes = parameterBuddle.QueryParametersList.Where(p => p.ParameterMode == ParameterMode.In);
                IEnumerable<QueryParameters> outParametes = parameterBuddle.QueryParametersList.Where(p => p.ParameterMode == ParameterMode.Out);
                if (inParametes != null)
                {
                    foreach (var item in inParametes)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter(string.Format("@{0}", item.ParameterName), item.Value));
                    }

                }

                List<QueryDataOut> outParaList = new List<QueryDataOut>();
                if (outParametes != null)
                {
                    foreach (var item in outParametes)
                    {
                        SqlParameter outPara = new SqlParameter(string.Format("@{0}", item.ParameterName), item.SqlDbType);
                        outPara.Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add(outPara);
                        outParaList.Add(new QueryDataOut { Key = item.ParameterName, Value = outPara });
                    }
                }

                sqlCommand.CommandType = (parameterBuddle.QueryType == QueryType.Inline) ? CommandType.Text : CommandType.StoredProcedure;

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(ds);

                List<DataOut> DataList = new List<DataOut>();
                if (outParaList.Count != 0)
                {
                    foreach (var item in outParaList)
                    {
                        DataList.Add(new DataOut { Key = item.Key, Value = item.Value.Value.ToString() });
                    }
                }

                dataListOut = DataList;
                dsOut = ds;
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
