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

namespace BIOMED
{
    [Activity(Label = "BIOMED", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        DataBaseService db;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var datePicker = FindViewById<DatePicker>(Resource.Id.datePicker);
            var btnDetails = FindViewById<Button>(Resource.Id.btnDetails);

            //database
            db = new DataBaseService();
            db.CreateDataBase();
            ////wyczyszczenie tabeli
            //db.ClearTable("BodyParameters");
            //wprowadzenie ty wartosci
            var parameterUnitEmpty = db.SelectTableParameterUnit();
            if (parameterUnitEmpty == null)
            {
                AddNewParameterUnits();
            }
            //Event
            btnDetails.Click += delegate
            {
                var bodyParametersEmpty = db.SelectTableBodyParametersDependsOnDate(datePicker.DateTime);
                if (bodyParametersEmpty.Count == 0)
                {
                    db.InsertParametersUnitIntoTableBodyParametersOnSpecificDate(datePicker.DateTime);
                }
                var activityBodyParameters = new Intent(this, typeof(ActivityBodyParameters));
                activityBodyParameters.PutExtra("PickedDate", JsonConvert.SerializeObject(datePicker.DateTime));
                this.StartActivity(activityBodyParameters);
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
    }
}

