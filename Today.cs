using System;
using System.Collections.Generic;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace App2
{
    [Activity(Label = "Today")]
    public class Today : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Today);

            Window.DecorView.SetBackgroundColor(Color.Purple);

            ISharedPreferences Time = Application.Context.GetSharedPreferences("Time", FileCreationMode.Private);
            ISharedPreferencesEditor ETime = Time.Edit();

            string CurrentDay = DateTime.Now.Day.ToString();

            ISharedPreferences userInfo = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            ISharedPreferencesEditor edit = userInfo.Edit();           

            EditText PrNAme = FindViewById<EditText>(Resource.Id.editText1);
            PrNAme.SetHeight(MainActivity.GetHeightPixels / MainActivity.EDIT_TEXT_COEFF);
            PrNAme.SetBackgroundColor(Color.BlueViolet);

            EditText PrMass = FindViewById<EditText>(Resource.Id.editText2);
            PrMass.SetHeight(MainActivity.GetHeightPixels / MainActivity.EDIT_TEXT_COEFF);
            PrMass.SetBackgroundColor(Color.BlueViolet);

            TextView dayCalories = FindViewById<TextView>(Resource.Id.textView1);
            dayCalories.SetHeight(MainActivity.GetHeightPixels / MainActivity.TEXTVIEW_COEFF);
            dayCalories.SetTextColor(Color.Black);
            if (CurrentDay != Time.GetString("Day", null)) 
            {
                edit.Remove("DayCalories");
                edit.PutString("DayCalories", "0");
                edit.Apply();
                ETime.Remove("Day");
                ETime.PutString("Day", CurrentDay);
                ETime.Apply();
            }
            dayCalories.Text = userInfo.GetString("DayCalories", "0");

            TextView DailyRate = FindViewById<TextView>(Resource.Id.textView2);
            DailyRate.SetHeight(MainActivity.GetHeightPixels / MainActivity.TEXTVIEW_COEFF);
            DailyRate.SetTextColor(Color.Black);
            DailyRate.Text = "Daily rate: " + userInfo.GetString("DailyRate", "0");

            TextView space = FindViewById<TextView>(Resource.Id.textView3);
            space.SetHeight(MainActivity.GetHeightPixels / MainActivity.TEXTVIEW_COEFF / 5);

            Button Add = FindViewById<Button>(Resource.Id.button1);
            Add.SetHeight(MainActivity.GetHeightPixels / MainActivity.BUTTON_COEFF);

            Add.Click += delegate
              {
                  double currentCalories = 0;
                  double newCurrentCalories = 0;
                  string fName = null;
                  string fValue = null;
                  double FValue = 0;
                  StreamReader str = new StreamReader(Assets.Open("Food.txt"));
                  int count = 0;
                  try                                    //start try
                  {
                      while (!str.EndOfStream)
                      {
                          count = 0;
                          fName = null;
                          fValue = "";
                          string line = str.ReadLine();
                          for (int i = 0; i < line.Length; i++)
                              if (line[i] == '-')
                                  count = i;
                          for (int i = 0; i < count; i++)
                              fName += line[i];
                          if (PrNAme.Text.ToLower() == fName)
                          {
                              for (int i = count + 1; line[i] != ','; i++)
                              {
                                  fValue += line[i];
                                  count++;
                              }
                              break;
                          }
                      }
                      FValue = double.Parse(fValue);
                      double ProductMass;
                      ProductMass = double.Parse(PrMass.Text) / 100;
                      currentCalories = Convert.ToDouble(userInfo.GetString("DayCalories", "0"));
                      newCurrentCalories = currentCalories + (FValue * ProductMass);
                      edit.Remove("DayCalories");
                      edit.PutString("DayCalories", newCurrentCalories.ToString());
                      edit.Apply();
                      dayCalories.Text = userInfo.GetString("DayCalories", "0");

                      str.Close();
                  }

                  catch (FormatException)
                  {
                      var dialog = new ProgressDialog.Builder(this);
                      dialog.SetMessage("Error");
                      dialog.SetPositiveButton("OK", delegate { });
                      dialog.Show();
                  }
                  catch (IndexOutOfRangeException)
                  {
                      var dialog = new ProgressDialog.Builder(this);
                      dialog.SetMessage("Error");
                      dialog.SetNegativeButton("OK", delegate { });
                      dialog.Show();
                  }
                  catch (FileNotFoundException)
                  {
                      var dialog = new ProgressDialog.Builder(this);
                      dialog.SetMessage("Error");
                      dialog.SetNegativeButton("OK", delegate { });
                      dialog.Show();
                  }
              };


        }
    }
}