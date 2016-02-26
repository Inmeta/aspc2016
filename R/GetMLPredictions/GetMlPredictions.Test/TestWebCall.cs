using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GetMLPredictions;

namespace GetMlPredictions.Test
{
    [TestClass]
    public class TestWebCall
    {
        [TestMethod]
        public void DoTestCall()
        {
            var t = new PredictML();
            var numberOfCrimesPerYear = t.GetCrimeNumberByCoordAndYear(51.20304, 12.134566, 2016);
            Assert.IsTrue(numberOfCrimesPerYear >= 0);
        }
    }
}
