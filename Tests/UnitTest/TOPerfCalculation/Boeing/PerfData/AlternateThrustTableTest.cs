﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using QSP.TOPerfCalculation.Boeing.PerfData;
using static QSP.TOPerfCalculation.Boeing.PerfData.AlternateThrustTable;

namespace UnitTest.TOPerfCalculation.Boeing.PerfData
{
    [TestClass]
    public class AlternateThrustTableTest
    {
        private const double delta = 1E-7;

        private AlternateThrustTable table = new AlternateThrustTable(
            new double[] { 200.0, 250.0 },
            new double[] { 190.0, 240.0 },
            new double[] { 192.0, 242.0 },
            new double[] { 180.0, 230.0 });

        [TestMethod]
        public void CorrectedLimitWeightTest()
        {
            Assert.AreEqual(202.5,
                table.CorrectedLimitWeight(212.5, TableType.Dry),
                delta);

            Assert.AreEqual(204.5,
                table.CorrectedLimitWeight(212.5, TableType.Wet),
                delta);

            Assert.AreEqual(192.5,
                table.CorrectedLimitWeight(212.5, TableType.Climb),
                delta);
        }

        [TestMethod]
        public void EquivalentFullThrustWeightTest()
        {
            Assert.AreEqual(212.5,
                table.EquivalentFullThrustWeight(202.5, TableType.Dry),
                delta);

            Assert.AreEqual(212.5,
                table.EquivalentFullThrustWeight(204.5, TableType.Wet),
                delta);

            Assert.AreEqual(212.5,
                table.EquivalentFullThrustWeight(192.5, TableType.Climb),
                delta);
        }
    }
}