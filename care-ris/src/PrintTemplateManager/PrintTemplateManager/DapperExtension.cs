using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Hys.PrintTemplateManager
{
    static class DapperExtension
    {
        public static DataTable ExecuteDataTable(this IDbConnection conn, string sql, object param = null)
        {
            var dataTable = new DataTable();
            var reader = conn.ExecuteReader(sql, param);
            dataTable.Load(reader);
            return dataTable;
        }
    }
}
