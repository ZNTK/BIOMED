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
using Newtonsoft.Json;

namespace BIOMED.Resources.Activities
{
    [Activity(Label = "ActivityBodyParameters")]
    public class ActivityBodyParameters : Activity
    {
        private DateTime pickedDate;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.BodyParameters);

            pickedDate = JsonConvert.DeserializeObject<DateTime>(Intent.GetStringExtra("PickedDate"));

            var editTextPickedDate = FindViewById<EditText>(Resource.Id.editTextPickedDate);
            editTextPickedDate.Text = pickedDate.ToString();
        }
    }
}