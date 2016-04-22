using Moq;
using Spackle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoDal.Test
{
    public class MockAdoRandomBase
    {

        protected RandomObjectGenerator rnd = new RandomObjectGenerator();

        protected Mock<IDbConnection> conn = new Mock<IDbConnection>(MockBehavior.Strict);

        protected Mock<IDbCommand> dbCommand = new Mock<IDbCommand>(MockBehavior.Strict);


        protected Mock<IDataReader> dbReader = new Mock<IDataReader>(MockBehavior.Strict);

        protected List<SqlParameter> sqlParameters = new List<SqlParameter>();


        protected Mock<IDataParameterCollection> dbDataParameterCollection = new Mock<IDataParameterCollection>(MockBehavior.Strict);

        protected Dictionary<string, object> record;
        protected List<Dictionary<string, object>> records = new List<Dictionary<string, object>>();
        protected int recordsReturned;
        protected int recordsToReturn = 2;

        protected int columnCount = 0;

        protected Dictionary<int, string> ordinalColumnName = new Dictionary<int, string>();

        protected virtual void SetupMockRead()
        {

            conn.Setup(_ => _.CreateCommand()).Returns(dbCommand.Object);
            conn.Setup(_ => _.Dispose()).Verifiable();

            /// Review - Use Mock strict or use Assert?
            dbCommand.SetupProperty(x => x.CommandType);
            dbCommand.SetupProperty(x => x.CommandText);

            dbCommand.Setup(_ => _.Dispose()).Verifiable();
            dbCommand.Setup(_ => _.CreateParameter())
                .Returns(() =>
                {
                    var dbDataParameter = new SqlParameter();
                    return dbDataParameter;
                });

            dbCommand.SetupGet(_ => _.Parameters).Returns(dbDataParameterCollection.Object);
            dbCommand.Setup(x => x.ExecuteReader()).Returns(dbReader.Object);

            dbDataParameterCollection.Setup(_ => _.Add(It.IsAny<SqlParameter>()))
                .Returns(0)
                .Callback<SqlParameter>((o) =>
                {
                    // Match the behavior of the logic
                    // If a parameter already exists then just overwrite the value
                    var exists = (from p in sqlParameters where p.ParameterName == o.ParameterName select p).FirstOrDefault();

                    if (exists == null)
                    {
                        sqlParameters.Add(o);
                    }
                    else
                    {
                        exists.Value = o.Value;
                    }
                })
                .Verifiable();


            dbDataParameterCollection.Setup(_ => _.Contains(It.IsAny<string>()))
                .Returns<string>(x => (from p in sqlParameters where p.ParameterName == x select 1).Count() > 0);

            dbDataParameterCollection.Setup(_ => _.Count).Returns(() => sqlParameters.Count);

            //dbDataParameterCollection.Setup(_ => _.IndexOf(It.IsAny<string>()))
            //   .Returns<string>((p) => dbDataParameters.IndexOf((from x in dbDataParameters where x.ParameterName == p select x).FirstOrDefault()))
            //   ;

            //dbDataParameterCollection.Setup(_ => _.RemoveAt(It.IsAny<int>()))
            //    .Callback((int p) => { dbDataParameters.RemoveAt(p); });


            dbReader.Setup(_ => _.Dispose())
                .Callback(() =>
                {
                    if (record != null)
                    {
                        records.Add(record);
                    }
                })

                .Verifiable();

            // Setup the datareader to go thru the Dto list
            dbReader.Setup(_ => _.Read()).Returns(() =>
            {
                if (recordsReturned < recordsToReturn)
                {

                    recordsReturned++;
                    if (record != null)
                    {
                        records.Add(record);
                    }

                    record = new Dictionary<string, object>();

                    return true;

                }

                return false;

            }).Verifiable();

            dbReader.SetupGet(x => x.FieldCount).Returns(() => columnCount);

            dbReader.Setup(x => x.IsDBNull(It.IsAny<int>())).Returns<int>(x =>
            {
                return false;
            });

            dbReader.Setup(_ => _.GetInt16(It.IsAny<int>()))
                .Returns<int>(x =>
                {
                    var result = rnd.Generate<short>();
                    record.Add(ordinalColumnName[x], result);
                    return result;
                });

            dbReader.Setup(_ => _.GetInt32(It.IsAny<int>()))
                .Returns<int>(x =>
                {
                    var result = rnd.Generate<Int32>();
                    record.Add(ordinalColumnName[x], result);
                    return result;
                });

            dbReader.Setup(_ => _.GetInt64(It.IsAny<int>()))
                            .Returns<int>(x =>
                            {
                                var result = rnd.Generate<short>();
                                record.Add(ordinalColumnName[x], result);
                                return result;
                            });

            dbReader.Setup(_ => _.GetDecimal(It.IsAny<int>()))
                            .Returns<int>(x =>
                            {
                                var result = rnd.Generate<decimal>();
                                record.Add(ordinalColumnName[x], result);
                                return result;
                            });

            dbReader.Setup(_ => _.GetString(It.IsAny<int>()))
                            .Returns<int>(x =>
                            {
                                var result = rnd.Generate<string>();
                                record.Add(ordinalColumnName[x], result);
                                return result;
                            });

            dbReader.Setup(_ => _.GetBoolean(It.IsAny<int>()))
                            .Returns<int>(x =>
                            {
                                var result = rnd.Generate<bool>();
                                record.Add(ordinalColumnName[x], result);
                                return result;
                            });

            dbReader.Setup(_ => _.GetDateTime(It.IsAny<int>()))
                            .Returns<int>(x =>
                            {
                                var result = rnd.Generate<DateTime>();
                                record.Add(ordinalColumnName[x], result);
                                return result;
                            });

            dbReader.Setup(_ => _.GetByte(It.IsAny<int>()))
                            .Returns<int>(x =>
                            {
                                var result = rnd.Generate<Byte>();
                                record.Add(ordinalColumnName[x], result);
                                return result;
                            });

            dbReader.Setup(_ => _.GetFloat(It.IsAny<int>()))
                            .Returns<int>(x =>
                            {
                                var result = rnd.Generate<float>();
                                record.Add(ordinalColumnName[x], result);
                                return result;
                            });

            dbReader.Setup(_ => _.GetDouble(It.IsAny<int>()))
                            .Returns<int>(x =>
                            {
                                var result = rnd.Generate<double>();
                                record.Add(ordinalColumnName[x], result);
                                return result;
                            });


            dbReader.Setup(_ => _.GetOrdinal(It.IsAny<string>()))
                .Returns<string>(x =>
                {
                    var l = ordinalColumnName.Where(v => v.Value == x);

                    if (l.Count() == 0)
                    {
                        columnCount++;
                        ordinalColumnName.Add(columnCount, x);
                        return columnCount;
                    } else
                    {

                        return l.First().Key;

                    }

                   

                });

            //dbReader.Setup(_ => _.GetValue(It.IsAny<int>())).Returns<int>(x => enumerator.Current.Values.Take(x + 1).LastOrDefault());

            //dbReader.Setup(x => x.GetName(It.IsAny<int>())).Returns<int>(x => enumerator.Current.Keys.Take(x + 1).LastOrDefault());

        }


    }
}
