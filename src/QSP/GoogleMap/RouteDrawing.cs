using QSP.RouteFinding.Containers;
using QSP.RouteFinding.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace QSP.GoogleMap
{
    public static class RouteDrawing
    {
        public static StringBuilder MapDrawString(
            IReadOnlyList<Waypoint> rte, int width, int height)
        {
            var mapHtml = new StringBuilder();

            string path = Assembly.GetExecutingAssembly().Location;
            var scriptPath = Path.GetDirectoryName(path) +
                @"\GoogleMap\Library\markerwithlabel.js";

            mapHtml.Append(
@"<!DOCTYPE html>
<html>
<head>
<meta http-equiv=""content-type"" content=""text/html; charset=utf-8"" />
    <style type=""text/css"">
    .labels {
        color: red;
        background-color: white;
        font-family: ""Lucida Grande"", ""Arial"", sans-serif;
        font-size: 10px;
        font-weight: bold;
        text-align: center;
        width: 40px;
        border: 2px solid black;
        white-space: nowrap;
    }
    </style>
    <script type = ""text/javascript"" src=""http://maps.googleapis.com/maps/api/js?v=3&amp;sensor=False""></script>
    <script type = ""text/javascript"" src=""" + scriptPath + @"""></script>
    <script type = ""text/javascript"">

function initialize()
{
");

            int counter = 0;
            foreach (var i in rte)
            {
                mapHtml.AppendLine(
                    $"var wpt{counter++}=new google.maps.LatLng(" +
                    $"{i.Lat},{i.Lon});");
            }

            // Center of map
            var center = GetCenter(rte);

            mapHtml.AppendLine(
                $"var centerP=new google.maps.LatLng({center.Lat},{center.Lon});");

            const int zoomlevel = 6;

            mapHtml.Append(
@"var mapProp = {
center:centerP,
zoom:" + zoomlevel.ToString() + @",
mapTypeId:google.maps.MapTypeId.ROADMAP
};
var map=new google.maps.Map(document.getElementById(""googleMap""),mapProp);
var myTrip=[");

            counter = 0;
            foreach (var i in rte)
            {
                if (counter != rte.Count - 1)
                {
                    mapHtml.Append($"wpt{counter},");
                }
                else
                {
                    mapHtml.AppendLine("wpt" + counter + @"];
    var flightPath=new google.maps.Polyline({
    path:myTrip,
    strokeColor:""#000000"",
    strokeOpacity:1.0,
    strokeWeight:3,
    geodesic: true
    });
    flightPath.setMap(map);");
                }

                counter++;
            }

            counter = 0;
            foreach (var i in rte)
            {
                mapHtml.Append(string.Format(
    @"
    var marker{0}  = new MarkerWithLabel({{
    position: wpt{1},
    icon:'pixel_trans.gif',
    draggable: false,
    raiseOnDrag: true,
    map: map,
    labelContent: ""{2}"",
    labelAnchor: new google.maps.Point(0, 0),
    labelClass: ""labels"", // the CSS class for the label
    labelStyle: {{opacity: 0.75}}
    }});

    var iw{3} = new google.maps.InfoWindow({{
    content: ""Home For Sale""
    }});

", counter, counter, i.ID, counter++));

            }

            mapHtml.Append(@"}
            
            google.maps.event.addDomListener(window, 'load', initialize);
            </script>
            </head>
            
            <body>
            <div id=""googleMap"" style=""width: " + width + "px;height:" + height + @"px;""></div>
            </body>
            </html>");

            return mapHtml;

        }

        private static ICoordinate GetCenter(IReadOnlyList<Waypoint> route)
        {
            if (route.Count < 2) throw new ArgumentException();
            var totalDis = route.TotalDistance();
            double dis = 0.0;
            Waypoint last = null;

            foreach (var i in route)
            {
                if (last != null)
                {
                    dis += last.Distance(i);
                    if (dis * 2.0 >= totalDis) return i;
                }

                last = i;
            }

            return last;
        }
    }
}
