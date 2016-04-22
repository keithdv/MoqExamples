using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EfDal.Entities;
using System.Linq;

namespace EfDal.Test
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public void Fetch()
        {

            var context = new DbMoqExample();

            var result = (from x in context.MoqExamples select x).ToList();


            Assert.AreEqual(13, result.Count);

        }
    }
}
