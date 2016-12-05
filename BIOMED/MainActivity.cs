using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using BIOMED.Resources.Activities;
using Newtonsoft.Json;

namespace BIOMED
{
    [Activity(Label = "BIOMED", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var datePicker = FindViewById<DatePicker>(Resource.Id.datePicker);
            var btnDetails = FindViewById<Button>(Resource.Id.btnDetails);

            //Event
            btnDetails.Click += delegate
            {
                var activityBodyParameters = new Intent(this, typeof(ActivityBodyParameters));
                activityBodyParameters.PutExtra("PickedDate", JsonConvert.SerializeObject(datePicker.DateTime));
                this.StartActivity(activityBodyParameters);
            };
        }
    }
}

