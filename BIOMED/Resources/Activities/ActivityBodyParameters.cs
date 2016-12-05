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
using BIOMED.Resources.Model;
using BIOMED.Resources.Services;
using BIOMED.Resources.Adapters;

namespace BIOMED.Resources.Activities
{
    [Activity(Label = "ActivityBodyParameters")]
    public class ActivityBodyParameters : Activity
    {
        DateTime pickedDate;
        ListView listViewData;
        List<BodyParameters> listBodyParametersSource = new List<BodyParameters>();
        DataBaseService db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.BodyParameters);

            pickedDate = JsonConvert.DeserializeObject<DateTime>(Intent.GetStringExtra("PickedDate"));

            var editTextPickedDate = FindViewById<EditText>(Resource.Id.editTextPickedDate);
            editTextPickedDate.Text = pickedDate.ToString();
            //baza dancyh
            db = new DataBaseService();
            //db.CreateDataBase();

            //do listview
            listViewData = FindViewById<ListView>(Resource.Id.listViewParameters);

            var edtName = FindViewById<EditText>(Resource.Id.editTextName);
            var edtAmount = FindViewById<EditText>(Resource.Id.editTextAmount);
            var edtUnit = FindViewById<EditText>(Resource.Id.editTextUnit);

            var btnEdit = FindViewById<Button>(Resource.Id.btnEdit);

            //LoadData
            LoadData();

            //Events
            btnEdit.Click += delegate
            {
                BodyParameters bodyParameters = new BodyParameters()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text,
                    Date = pickedDate.Date,
                    Unit = edtUnit.Text,
                    Amount = int.Parse(edtAmount.Text)
                };
                db.UpdateTableBodyParameters(bodyParameters);
                LoadData();
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
                var txtAmount = e.View.FindViewById<TextView>(Resource.Id.textViewAmount);
                var txtUnit = e.View.FindViewById<TextView>(Resource.Id.textViewUnit);

                edtName.Text = txtName.Text;
                edtName.Tag = e.Id;

                edtAmount.Text = txtAmount.Text;
                edtUnit.Text = txtUnit.Text;
            };
        }

        private void LoadData()
        {
            listBodyParametersSource = db.SelectTableBodyParametersDependsOnDate(pickedDate);
            var adapter = new ListViewParametersAdapter(this, listBodyParametersSource);
            listViewData.Adapter = adapter;
        }
    }
}