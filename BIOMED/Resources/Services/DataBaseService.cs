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
using SQLite;
using Android.Util;
using BIOMED.Resources.Model;

namespace BIOMED.Resources.Services
{
    public class DataBaseService
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool CreateDataBase()
        {
            try
            {
                using(var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "BMDataBase.db")))
                {
                    connection.CreateTable<BodyParameters>();
                    connection.CreateTable<ParameterUnit>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx CreateDataBase", ex.Message);
                return false;
            }
        }
        #region BodyParameters
        public bool InsertIntoTableBodyParameters(BodyParameters bodyParameters)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "BMDataBase.db")))
                {
                    connection.Insert(bodyParameters);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx InsertIntoTableBodyParameters", ex.Message);
                return false;
            }
        }

        public List<BodyParameters> SelectTableBodyParameters()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "BMDataBase.db")))
                {
                    return connection.Table<BodyParameters>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx SelectTableBodyParameters", ex.Message);
                return null;
            }
        }

        public bool UpdateTableBodyParameters(BodyParameters bodyParameters)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "BMDataBase.db")))
                {
                    connection.Query<BodyParameters>("UPDATE BodyParameters set Date=?,Name=?,Amount=?,Unit=? WHERE Id=?",
                        bodyParameters.Date, bodyParameters.Name, bodyParameters.Amount, bodyParameters.Unit, bodyParameters.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx UpdateTableBodyParameters", ex.Message);
                return false;
            }
        }

        public bool DeleteTableBodyParameters(BodyParameters bodyParameters)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "BMDataBase.db")))
                {
                    connection.Delete(bodyParameters);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx DeleteTableBodyParameters", ex.Message);
                return false;
            }
        }

        public bool SelectTableBodyParameters(int Id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "BMDataBase.db")))
                {
                    connection.Query<BodyParameters>("SELECT * WHERE Id=?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx SelectTableBodyParameters", ex.Message);
                return false;
            }
        }
        #endregion
        #region ParameterUnit
        public List<ParameterUnit> SelectTableParameterUnit()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "BMDataBase.db")))
                {
                    return connection.Table<ParameterUnit>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx SelectTableParameterUnit", ex.Message);
                return null;
            }
        }
        #endregion
    }
}