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
using BarChart;
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

            db = new DataBaseService();

            var listBodyParameters = db.SelectTableBodyParametersFromDateToDate(new DateTime(2017, 1, 7), new DateTime(2017, 1, 14));

            //DataTable table = new DataTable();

            //table.Columns.Add("Amount", typeof(int));
            //table.Columns.Add("Date", typeof(DateTime));

            //table.Rows.Add(25, new DateTime(2017, 1, 14));
            //table.Rows.Add(25, new DateTime(2017, 1, 15));
            //table.Rows.Add(25, new DateTime(2017, 1, 16));
            //table.Rows.Add(25, new DateTime(2017, 1, 17));
            //table.Rows.Add(25, new DateTime(2017, 1, 18));

            Steema.TeeChart.TChart tChart = new Steema.TeeChart.TChart(this);
            Steema.TeeChart.Styles.Line line = new Steema.TeeChart.Styles.Line();
            
            tChart.Series.Add(line);

            line.Add(new DateTime(2017, 1, 14),12);
            line.Add(new DateTime(2017, 1, 16), 15);
            line.Add(new DateTime(2017, 1, 17), 13);
            line.Add(new DateTime(2017, 1, 19), 10);
            line.Add(new DateTime(2017, 1, 20), 11);
            line.Add(new DateTime(2017, 1, 22), 16);
            line.ColorEachLine = true;
            line.Color = Color.Red;
            line.Colors = new Steema.TeeChart.Styles.ColorList();

            tChart.Legend.Visible = false;
            tChart.Header.Text = "wykres";

            line.LinePen.Width = 20;
            
            Steema.TeeChart.Themes.BlackIsBackTheme theme = new Steema.TeeChart.Themes.BlackIsBackTheme(tChart.Chart);
            theme.Apply();
            SetContentView(tChart);



            //var data = new[] { 1f, 2f, 4f, 8f, 16f, 32f };

            //List<BarModel> nie = new List<BarModel>()
            //{

            //};
            //for(int i = 0; i < 15; i++)
            //{
            //    nie.Add(
            //    new BarModel()
            //    {
            //        Value = i,
            //        Legend = new DateTime().Date.ToString()
            //    });
            //}

            //IEnumerable<BarModel> tak = nie;


            //var chart = new BarChartView(this)
            //{
            //    ItemsSource = tak
            //    //Array.ConvertAll(data, v => new BarModel { Value = v, Legend = "cos tam test"}),

            //};

            //AddContentView(chart, new ViewGroup.LayoutParams(
            //  ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent));
        }
    }
}