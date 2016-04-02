﻿using QSP.MathTools.Tables;

namespace QSP.TOPerfCalculation.Boeing.PerfData
{
    public class SlopeCorrTable : Table2D
    {
        public SlopeCorrTable(double[] physicalLengths,
            double[] slopes,
            double[][] correctedLenth)
            : base(physicalLengths, slopes, correctedLenth)
        { }

        public double CorrectedLength(double physicalLength, double slope)
        {
            return ValueAt(physicalLength, slope);
        }

        public double FieldLengthRequired(double slope, double slopeCorrectedLength)
        {
            return tableFieldLength(slope).ValueAt(slopeCorrectedLength);
        }

        // Maps sloped corrected length into field length.
        private Table1D tableFieldLength(double slope)
        {
            double[] fieldLength = new double[x.Length];

            for (int i = 0; i < fieldLength.Length; i++)
            {
                fieldLength[i] = ValueAt(x[i], slope);
            }

            return new Table1D(fieldLength, x);
        }        
    }
}