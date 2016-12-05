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
using Java.Lang;

namespace BIOMED.Resources.Adapters
{
    public class ViewParametersHolder : Java.Lang.Object
    {
        public TextView textViewName { get; set; }
        public TextView textViewAmount { get; set; }
        public TextView textViewUnit { get; set; }
    }
    public class ListViewParametersAdapter:BaseAdapter
    {
        private Activity activity;
        private List<BodyParameters> listBodyParameters;
        
        public ListViewParametersAdapter(Activity activity, List<BodyParameters> listBodyParameters)
        {
            this.activity = activity;
            this.listBodyParameters = listBodyParameters;
        }

        public override int Count
        {
            get
            {
                return listBodyParameters.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return listBodyParameters[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.ListViewParameters, parent, false);

            var txtName = view.FindViewById<TextView>(Resource.Id.textViewName);
            var txtAmount = view.FindViewById<TextView>(Resource.Id.textViewAmount);
            var txtUnit = view.FindViewById<TextView>(Resource.Id.textViewUnit);

            txtName.Text = listBodyParameters[position].Name;
            txtAmount.Text = "" + listBodyParameters[position].Amount;
            txtUnit.Text = listBodyParameters[position].Unit;

            return view;
        }
    }
}