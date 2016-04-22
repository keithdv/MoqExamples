using EfDal.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spackle;
using System.Data.Entity;

namespace EfDal.Test
{

    [TestClass]
    public class MoqTests
    {

        Mock<DbMoqExample> context;
        List<MoqExample> entities;

        [TestInitialize]
        public void TestInitialize()
        {
            context = new Mock<DbMoqExample>();

            entities = new List<MoqExample>();

            entities.Add(EntityCreator.Create<MoqExample>());
            entities.Add(EntityCreator.Create<MoqExample>());
            entities.Add(EntityCreator.Create<MoqExample>());
            entities.Add(EntityCreator.Create<MoqExample>());

            var MoqExamples = new Mock<DbSet<MoqExample>>(MockBehavior.Strict).SetupData(entities);

            context.SetupGet(x => x.MoqExamples).Returns(MoqExamples.Object);

        }

        [TestMethod]
        public void Fetch()
        {

            var result = (from x in context.Object.MoqExamples select x).ToList();

            Assert.AreEqual(4, result.Count);

        }

        [TestMethod]
        public void Insert()
        {
            var entity = EntityCreator.Create<MoqExample>();

            context.Object.MoqExamples.Add(entity);

            context.Object.SaveChanges();

            Assert.AreEqual(5, context.Object.MoqExamples.ToList().Count);

        }

    }
}
