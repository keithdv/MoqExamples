using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using System.Data;
using System.Data.SqlClient;

namespace AdoDal.Test
{
    [TestClass]
    public class IntegrationTest
    {


        IContainer container;
        ILifetimeScope scope;

        [TestInitialize]
        public void TestInitialize()

        {

            if (container == null)
            {

                ContainerBuilder builder = new ContainerBuilder();

                builder.Register<IDbConnection>((x, y) =>
                {
                    var conn = new SqlConnection(@"data source = localhost; initial catalog = MoqExample; integrated security = True; ");

                    conn.Open();

                    return conn;

                }).InstancePerLifetimeScope();

                builder.RegisterType<Dal>();

                container = builder.Build();

            }

            scope = container.BeginLifetimeScope();

        }

        [TestCleanup]
        public void TestCleanup()
        {
            var conn = scope.Resolve<IDbConnection>();

            conn.Close();

            scope.Dispose();
        }

        [TestMethod]
        [TestCategory("ADO"), TestCategory("Dal")]
        public void Fetch()
        {

            var dal = scope.Resolve<Dal>();

            var result = dal.Fetch();

            Assert.AreEqual(13, result.Count);

        }
    }
}
