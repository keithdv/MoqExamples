using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoDal.Test
{
    [TestClass]
    public class MoqAdoTests : MockAdoBase
    {

        [TestMethod]
        [TestCategory("Moq"), TestCategory("Dal")]
        public void Fetch()
        {

            // Arrange
            base.SetupMockRead();


            Func<Dictionary<string, object>> makeRecord = new Func<Dictionary<string, object>>(() =>
            {
                var record = new Dictionary<string, object>();

                record.Add("Column1", rnd.Generate<string>());
                record.Add("Column2", rnd.Generate<int>());
                record.Add("Column3", rnd.Generate<decimal>());
                record.Add("Column4", rnd.Generate<string>());

                return record;

            });

            records.Add(makeRecord());
            records.Add(makeRecord());
            records.Add(makeRecord());

            var dal = new Dal(base.conn.Object);

            // Act
            var results = dal.Fetch();


            // Assert
            Assert.AreEqual(3, results.Count);

            var record1 = records[0];
            var dto1 = results[0];

            Assert.AreEqual(record1["Column1"], dto1.Column1);
            Assert.AreEqual(record1["Column2"], dto1.Column2);
            Assert.AreEqual(record1["Column3"], dto1.Column3);
            Assert.AreEqual(record1["Column4"], dto1.Column4);

            Assert.AreEqual(1, sqlParameters.Count);
            var param = sqlParameters[0];

            Assert.AreEqual("Param", param.ParameterName);
            Assert.AreEqual("Value", param.Value);
            
            dbCommand.Verify(x => x.ExecuteReader(), Moq.Times.Once);
            dbCommand.Verify(x => x.Dispose());
            dbReader.Verify(x => x.Dispose());

        }

    }
}
