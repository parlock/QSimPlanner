﻿using QSP.GoogleMap;
using QSP.RouteFinding.Routes;
using QSP.UI.Factories;
using System.Drawing;
using System.Windows.Forms;

namespace QSP.UI.Utilities
{
    public static class ShowMapHelper
    {
        public static void ShowMap(RouteGroup Route)
        {
            if (Route == null)
            {
                MsgBoxHelper.ShowWarning(
                    "Please find or analyze a route first.");
                return;
            }

            var wb = new WebBrowser();
            wb.Size = new Size(1200, 800);

            var GoogleMapDrawRoute = RouteDrawing.MapDrawString(
                Route.Expanded, wb.Size.Width - 20, wb.Size.Height - 30);

            wb.DocumentText = GoogleMapDrawRoute.ToString();

            using (var frm = FormFactory.GetForm(wb.Size))
            {
                frm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.Controls.Add(wb);
                frm.ShowDialog();
            }
        }
    }
}