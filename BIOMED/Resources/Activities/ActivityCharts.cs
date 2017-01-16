using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BIOMED.Resources.Model;
using System.Drawing;
using BIOMED.Resources.Services;
using System.Data;

namespace BIOMED.Resources.Activities
{
    [Activity(Label = "ActivityCharts")]
    public class ActivityCharts : Activity
    {
        DataBaseService db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Charts);

            var parameterName = Intent.GetStringExtra("ParameterName");

            db = new DataBaseService();

            var listBodyParameters = db.SelectTableBodyParametersDependsOnName(parameterName);

            

            Steema.TeeChart.TChart tChart = new Steema.TeeChart.TChart(this);
            Steema.TeeChart.Styles.Line line = new Steema.TeeChart.Styles.Line();
            
            tChart.Series.Add(line);

            foreach(var item in listBodyParameters)
            {
                line.Add(item.Date, item.Amount);
            }
           
            line.ColorEachLine = true;
            line.Color = Color.Red;
            line.Colors = new Steema.TeeChart.Styles.ColorList();

            tChart.Legend.Visible = false;
            tChart.Header.Text = parameterName;

            line.LinePen.Width = 20;
            
            Steema.TeeChart.Themes.BlackIsBackTheme theme = new Steema.TeeChart.Themes.BlackIsBackTheme(tChart.Chart);
            theme.Apply();
            SetContentView(tChart);
        }
    }
}