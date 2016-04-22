using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoDal.Test
{
    [TestClass]
    public class MockAdoRandomTests : MockAdoRandomBase
    {


        [TestMethod]
        [TestCategory("Moq"), TestCategory("Dal")]
        public void Fetch()
        {

            // Arrange
            base.SetupMockRead();


            var dal = new Dal(base.conn.Object);

            // Act
            var results = dal.Fetch();


            // Assert
            Assert.AreEqual(2, results.Count);

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
