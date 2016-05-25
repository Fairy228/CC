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
using Android.Views.InputMethods;
using Android.Graphics;

namespace App2
{
    [Activity(Label = "Personal Settings", MainLauncher = false, Icon = "@drawable/AppIcon", ScreenOrientation =Android.Content.PM.ScreenOrientation.Portrait)]
    class SettingsActivity : Activity
    {
        EditText EWeight;
        EditText EGrowth;
        EditText EOld;

        int Sex;
        int Check;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Settings);

            Window.DecorView.SetBackgroundColor(Color.Purple);

            int k = MainActivity.GetHeightPixels;
            int m = MainActivity.GetWidthPixels;

            ISharedPreferences Settings = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            string SSex = Settings.GetString("Sex", "0");
            string OOld = Settings.GetString("Old", "0");
            string GGrowth = Settings.GetString("Growth", "0");
            string WWeight = Settings.GetString("Weight", "0");
            string DDailyRate = Settings.GetString("DailyRate", "0");

            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.SetHeight(k / MainActivity.BUTTON_COEFF);
            button1.SetWidth(m / 4);
            button1.SetX(m / 4 - m / 8);

            Button button2 = FindViewById<Button>(Resource.Id.button2);
            button2.SetHeight(k / MainActivity.BUTTON_COEFF);
            button2.SetWidth(m / 4);
            button2.SetX(m  / 2-m/8);
            if (SSex == "0")
            {
                button2.SetBackgroundColor(Color.Blue);
                button1.SetBackgroundColor(Color.Gray);
            }
            else
            {
                button1.SetBackgroundColor(Color.Blue);
                button2.SetBackgroundColor(Color.Gray);
            }

            Button count = FindViewById<Button>(Resource.Id.button3);
            count.SetHeight(k / MainActivity.BUTTON_COEFF);

            TextView weight = FindViewById<TextView>(Resource.Id.textView2);
            weight.SetHeight(k / MainActivity.TEXTVIEW_COEFF);
            weight.SetWidth(m / 3);
            weight.Gravity = GravityFlags.Center;

            TextView Growth = FindViewById<TextView>(Resource.Id.textView3);
            Growth.SetHeight(k / MainActivity.TEXTVIEW_COEFF);
            Growth.SetWidth(m / 3);
            Growth.Gravity = GravityFlags.Center;

            TextView view1 = FindViewById<TextView>(Resource.Id.textView1);
            view1.SetHeight(k / (MainActivity.TEXTVIEW_COEFF));
            view1.Gravity = GravityFlags.Center;

            TextView DayCalories = FindViewById<TextView>(Resource.Id.textView2);
            DayCalories.SetHeight(k / (MainActivity.TEXTVIEW_COEFF));
            DayCalories.Gravity = GravityFlags.Center;

            TextView Old = FindViewById<TextView>(Resource.Id.textView4);
            Old.SetHeight(k / MainActivity.TEXTVIEW_COEFF);
            Old.SetWidth(m / 3);
            Old.Gravity = GravityFlags.Center;

            TextView DailyRate = FindViewById<TextView>(Resource.Id.textView5);
            DailyRate.Text = "Daily rate: " + DDailyRate;

            EWeight = FindViewById<EditText>(Resource.Id.editText1);
            EWeight.SetWidth(m / 3);
            EWeight.SetHeight(k / MainActivity.EDIT_TEXT_COEFF);
            EWeight.Gravity = GravityFlags.Center;
            EWeight.Text = WWeight;
            EWeight.SetBackgroundColor(Color.Black);

            EGrowth = FindViewById<EditText>(Resource.Id.editText2);
            EGrowth.SetWidth(m / 3);
            EGrowth.SetHeight(k / MainActivity.EDIT_TEXT_COEFF);
            EGrowth.Gravity = GravityFlags.Center;
            EGrowth.Text = GGrowth;
            EGrowth.SetBackgroundColor(Color.Black);

            EOld = FindViewById<EditText>(Resource.Id.editText3);
            EOld.SetWidth(m / 3);
            EOld.SetHeight(k / MainActivity.EDIT_TEXT_COEFF);
            EOld.Gravity = GravityFlags.Center;
            EOld.Text = OOld;
            EOld.SetBackgroundColor(Color.Black);

            

            button1.Click += delegate
              {
                  Sex = 1;
                  button1.SetBackgroundColor(Color.Blue);
                  button2.SetBackgroundColor(Color.DarkGray);
                  Save("Sex", "1");
              };

            button2.Click += delegate
              {
                  Sex = 0;
                  button2.SetBackgroundColor(Color.Blue);
                  button1.SetBackgroundColor(Color.DarkGray);
                  Save("Sex", "0");
              };

            count.Click += delegate
              {
                  ISharedPreferencesEditor edit = Settings.Edit();
                  edit.PutString("Old", EOld.Text);
                  edit.PutString("Weight", EWeight.Text);
                  edit.PutString("Growth", EGrowth.Text);
                  edit.Apply();

                  string _sex = Settings.GetString("Sex", "1");
                  string _Old = Settings.GetString("Old", "20");
                  string _Weight = Settings.GetString("Weight", "65");
                  string _Height = Settings.GetString("Growth", "165");

                  if (OOld == "0" || WWeight == "0" || GGrowth == "0")
                      Check = 0;
                  else Check = 1;

                  if (_sex == "1")
                  {
                      string _DailyRate = (Check * (88.36 + (13.4 * Double.Parse(EWeight.Text)) + (4.8 * Double.Parse(EGrowth.Text)) - (5.7 * Double.Parse(EOld.Text)))).ToString();
                      DailyRate.Text = "Daily rate: " + _DailyRate;
                      Save("DailyRate", _DailyRate);
                  }

                  if (_sex=="0")
                  {
                      string _DailyRate = (Check * (447.6 + (9.2 * Double.Parse(EWeight.Text)) + (3.1 * Double.Parse(EGrowth.Text)) - (4.3 * Double.Parse(EOld.Text)))).ToString();
                      DailyRate.Text = "Daily rate: "+_DailyRate;
                      Save("DailyRate", _DailyRate);
                  }

              };


        }

         void Save(string Key, string Value)
        {
            ISharedPreferences Settings = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            ISharedPreferencesEditor ESetting = Settings.Edit();
            ESetting.PutString(Key, Value);
            ESetting.Apply();
        }

    }
}