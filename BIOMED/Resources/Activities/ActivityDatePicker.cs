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
using BIOMED.Resources.Services;
using Newtonsoft.Json;

namespace BIOMED.Resources.Activities
{
    [Activity(Label = "ActivityDatePicker")]
    public class ActivityDatePicker : Activity
    {
        DataBaseService db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DatePicker);

            var datePicker = FindViewById<DatePicker>(Resource.Id.datePicker);
            var btnDateOk = FindViewById<Button>(Resource.Id.btnDateOk);
            db = new DataBaseService();
            btnDateOk.Click += delegate
            {
                var bodyParametersEmpty = db.SelectTableBodyParametersDependsOnDate(datePicker.DateTime);
                if (bodyParametersEmpty.Count == 0)
                {
                    db.InsertParametersUnitIntoTableBodyParametersOnSpecificDate(datePicker.DateTime);
                }
                var activityBodyParameters = new Intent(this, typeof(ActivityBodyParameters));
                activityBodyParameters.PutExtra("PickedDate", JsonConvert.SerializeObject(datePicker.DateTime));
                this.StartActivity(activityBodyParameters);
                this.Finish();
            };
            
            // Create your application here
        }
    }
}