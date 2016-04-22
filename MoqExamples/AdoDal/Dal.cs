using Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoDal
{
    public class Dal
    {

        private IDbConnection conn;

        public Dal(IDbConnection connection)
        {
            this.conn = connection;
        }


        public List<Dto> Fetch()
        {
            var result = new List<Dto>();

            using (IDbCommand cmd = this.conn.CreateCommand())
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.simpleStoredProcedure";

                cmd.Parameters.Add(new SqlParameter() { ParameterName = "Param", Value = "Value" });


                using (IDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        var dto = new Dto();

                        dto.Column1 = dr.GetString(dr.GetOrdinal("Column1"));
                        dto.Column2 = dr.GetInt32(dr.GetOrdinal("Column2"));
                        dto.Column3 = dr.GetDecimal(dr.GetOrdinal("Column3"));
                        dto.Column4 = dr.GetString(dr.GetOrdinal("Column4"));

                        result.Add(dto);

                    }

                }


            }

            return result;
        }

    }
}
