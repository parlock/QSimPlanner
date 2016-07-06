using QSP.RouteFinding;
using QSP.Utilities.Units;
using System;
using static QSP.UI.FormInstanceGetter;
using static QSP.AviationTools.Constants;

namespace QSP
{
    public class FuelCalculationParameters
    {
        public double Zfw;
        public double ContPerc;
        public double MissedAppFuel;
        public double HoldingMin;
        public double ExtraFuel;
        public double APUTime;
        public double TaxiTime;
        public double FinalRsvMin;
        public double DisToDest;
        public double DisToAltn;
        public double AvgWindToDest;
        public double AvgWindToAltn;
        public Aircraft AC;
        public double ZfwKg;
        public double MissedAppFuelKg;
        public double ExtraFuelKg;
        public WeightUnit WtUnit { get; private set; }

        private MainForm frm = MainFormInstance();        

        private void ReadWtUnit()
        {
            if (frm.WtUnitSel_ComboBox.Text == "KG")
            {
                WtUnit = WeightUnit.KG;
            }
            else
            {
                WtUnit = WeightUnit.LB;
            }
        }

        private void SetPropertyKg()
        {
            if (WtUnit == WeightUnit.KG)
            {
                ZfwKg = Zfw;
                MissedAppFuelKg = MissedAppFuel;
                ExtraFuelKg = ExtraFuel;
            }
            else
            {
                ZfwKg = Zfw * LbKgRatio;
                MissedAppFuelKg = MissedAppFuel * LbKgRatio;
                ExtraFuelKg = ExtraFuel * LbKgRatio;
            }
        }

        private void SetAircraftType()
        {
            if (frm.ACList.Text == "737-600")
            {
                AC = Aircraft.B737600;
            }
            else if (frm.ACList.Text == "737-700")
            {
                AC = Aircraft.B737700;
            }
            else if (frm.ACList.Text == "737-800")
            {
                AC = Aircraft.B737800;
            }
            else if (frm.ACList.Text == "737-900")
            {
                AC = Aircraft.B737900;
            }
            else if (frm.ACList.Text == "777-200LR")
            {
                AC = Aircraft.B777200LR;
            }
            else if (frm.ACList.Text == "777F")
            {
                AC = Aircraft.B777F;
            }
        }

        public void ImportValues()
        {
            ReadWtUnit();
            SetAircraftType();

            TryImportPattern1(ref Zfw, frm.ZFW.Text, "Invalid ZFW.");
            //need a better one
            TryImportPattern1(ref ContPerc, frm.ContPercentToDest.Text, "Invalid contingency fuel.");
            TryImportPattern1(ref MissedAppFuel, frm.MissedAppFuel.Text, "Invalid missed approach fuel.");
            TryImportPattern1(ref HoldingMin, frm.HoldTime_TxtBox.Text, "Invalid holding time.");
            TryImportPattern1(ref ExtraFuel, frm.ExtraFuel.Text, "Invalid extra fuel.");
            TryImportPattern1(ref APUTime, frm.APUTime.Text, "Invalid APU time.");
            TryImportPattern1(ref TaxiTime, frm.TaxiTime.Text, "Invalid taxi time.");
            TryImportPattern1(ref FinalRsvMin, frm.FinalRsv.Text, "Invalid time for final reserve.");
            DisToDest = RouteFindingCore.RouteToDest.Folded.GetTotalDistance();
            DisToAltn = RouteFindingCore.RouteToAltn.Folded.GetTotalDistance();

            AvgWindToDest = 0;
            AvgWindToAltn = 0;

            SetPropertyKg();
        }

        private void TryImportPattern1(ref double var, string str, string exceptionMsg)
        {
            try
            {
                var = Convert.ToDouble(str);

                if (var < 0)
                {
                    throw new Exception(exceptionMsg);
                }
            }
            catch
            {
                throw new Exception(exceptionMsg);
            }
        }

        private void TryImportPattern2(ref double var, string str, string exceptionMsg)
        {
            try
            {
                var = Convert.ToDouble(str);

                if (var < -400 || var > 400)
                {
                    throw new Exception(exceptionMsg);
                }
            }
            catch
            {
                throw new Exception(exceptionMsg);
            }
        }

        public void FillInDefaultValueIfLeftBlank()
        {
            if (string.IsNullOrEmpty(frm.FinalRsv.Text))
            {
                frm.FinalRsv.Text = "30";
            }

            if (string.IsNullOrEmpty(frm.ContPercentToDest.Text))
            {
                frm.ContPercentToDest.Text = "5";
            }

            if (string.IsNullOrEmpty(frm.ExtraFuel.Text))
            {
                frm.ExtraFuel.Text = "0";
            }

            if (string.IsNullOrEmpty(frm.APUTime.Text))
            {
                frm.APUTime.Text = "30";
            }

            if (string.IsNullOrEmpty(frm.TaxiTime.Text))
            {
                frm.TaxiTime.Text = "20";
            }

            if (string.IsNullOrEmpty(frm.HoldTime_TxtBox.Text))
            {
                frm.HoldTime_TxtBox.Text = "0";
            }
        }
    }
}

