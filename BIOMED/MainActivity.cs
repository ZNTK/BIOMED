using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using BIOMED.Resources.Activities;
using Newtonsoft.Json;
using BIOMED.Resources.Services;
using System;
using BIOMED.Resources.Model;
using System.Linq;
using System.Collections.Generic;
using BIOMED.Resources.Adapters;

namespace BIOMED
{
    [Activity(Label = "BIOMED", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        DataBaseService db;
        ListView listViewData;
        List<BodyParameters> listBodyParametersSource = new List<BodyParameters>();
        DateTime dateNow;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            //var datePicker = FindViewById<DatePicker>(Resource.Id.datePicker);
            var btnDetails = FindViewById<Button>(Resource.Id.btnDetails);
            dateNow = DateTime.Today;

            //database
            db = new DataBaseService();
            db.CreateDataBase();
            ////wyczyszczenie tabeli
            //db.ClearTable("BodyParameters");

            //do listview
            listViewData = FindViewById<ListView>(Resource.Id.listViewParametersMain);

            //LoadData
            if (db.SelectTableLatestAddedBodyParameters(dateNow) != null)
            {
                LoadData();
            }
                      

            //wprowadzenie ty wartosci
            var parameterUnitEmpty = db.SelectTableParameterUnit();
            if (parameterUnitEmpty.Count == 0)
            {
                AddNewParameterUnits();
            }
            //Event
            btnDetails.Click += delegate
            {
                var bodyParametersEmpty = db.SelectTableBodyParametersDependsOnDate(dateNow);
                if (bodyParametersEmpty.Count == 0)
                {
                    db.InsertParametersUnitIntoTableBodyParametersOnSpecificDate(dateNow);
                }
                var activityBodyParameters = new Intent(this, typeof(ActivityBodyParameters));
                activityBodyParameters.PutExtra("PickedDate", JsonConvert.SerializeObject(dateNow));
                this.StartActivity(activityBodyParameters);
            };

            listViewData.ItemClick += (s, e) => {
                for (int i = 0; i < listViewData.Count; i++)
                {
                    if (e.Position == i)
                        listViewData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.DarkGray);
                    else
                        listViewData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                }
                //Binding 
                var txtName = e.View.FindViewById<TextView>(Resource.Id.textViewName);

                var activityCharts = new Intent(this, typeof(ActivityCharts));
                activityCharts.PutExtra("ParameterName", txtName.Text);
                this.StartActivity(activityCharts);
            };
        }

        private void AddNewParameterUnits()
        {
            ParameterUnit tluszcz = new ParameterUnit
            {
                Name = "Tłuszcz",
                Unit = "%"
            };
            ParameterUnit waga = new ParameterUnit
            {
                Name = "Waga",
                Unit = "kg"
            };
            ParameterUnit wzrost = new ParameterUnit
            {
                Name = "Wzrost",
                Unit = "cm"
            };

            db.InsertIntoTableParameterUnit(tluszcz);
            db.InsertIntoTableParameterUnit(waga);
            db.InsertIntoTableParameterUnit(wzrost);
        }

        private void LoadData()
        {
            listBodyParametersSource = db.SelectTableLatestAddedBodyParameters(dateNow);
            var adapter = new ListViewParametersAdapter(this, listBodyParametersSource);
            listViewData.Adapter = adapter;
        }
    }
}

