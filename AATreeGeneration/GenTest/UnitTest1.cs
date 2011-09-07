using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAdd()
        {
            AATreeGeneration.Generation g = new AATreeGeneration.Generation();

            g.Insert("Middle", 0.7f);
            g.Insert("Best", 0.8f);
            g.Insert("Worst", 0.5f);

            if (!"Best".Equals(g.Get(0).Chromosome))
                throw new Exception();
        }

        [TestMethod]
        public void TestMethod1()
        {
            throw new Exception("Kablammo!");
        }
    }
}
