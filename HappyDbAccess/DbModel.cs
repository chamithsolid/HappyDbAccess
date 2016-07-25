using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyDbAccess
{
    public class QueryParametersBuddle
    {

        public List<QueryParameters> QueryParametersList { get; set; }
        public QueryType QueryType { get; set; }
        public string SPName { get; set; }

    }

    public class QueryDataOut
    {

        public string Key { get; set; }
        public SqlParameter Value { get; set; }
    }

    public class DataOut
    {

        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class QueryParameters
    {
        public string ParameterName { get; set; }
        public object Value { get; set; }
        public ParameterMode ParameterMode { get; set; }
        public SqlDbType SqlDbType { get; set; }
        public int Size { get; set; }
    }

    public enum ParameterMode
    {

        In = 1,
        Out = 2
    }

    public enum QueryType
    {

        Inline = 1,
        SP = 2
    }

}
