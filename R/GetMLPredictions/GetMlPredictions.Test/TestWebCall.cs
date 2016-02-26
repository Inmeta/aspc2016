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
            t.GetCrimeNumberByCoordAndYear(51.20304, 12.134566, 2016);
        }
    }
}
