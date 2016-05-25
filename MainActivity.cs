using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    [Activity(Label = "Calories calculator", MainLauncher = true, Icon = "@drawable/AppIcon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {

        public const int BUTTON_COEFF = 11;
        public const int EDIT_TEXT_COEFF = 15;
        public const int TEXTVIEW_COEFF = 20;
        static int k;
        static int m;
        
         protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout1);

            LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            layout.SetBackgroundColor(Color.Purple);

            k = Resources.DisplayMetrics.HeightPixels;
            m = Resources.DisplayMetrics.WidthPixels;

            Button button = FindViewById<Button>(Resource.Id.button1);
            button.SetHeight(k / BUTTON_COEFF);

            Button button1 = FindViewById<Button>(Resource.Id.button2);
            button1.SetHeight(k / BUTTON_COEFF);

            Button TodayBtn = FindViewById<Button>(Resource.Id.button3);
            TodayBtn.SetHeight(k / BUTTON_COEFF);

            button.Click += delegate
            {
                Intent main = new Intent(this, typeof(FoodInfo));
                StartActivity(main);
            };

            button1.Click += delegate
            {
                Intent settings = new Intent(this, typeof(SettingsActivity));
                StartActivity(settings);
            };

            TodayBtn.Click += delegate
              {
                  Intent Today = new Intent(this, typeof(Today));
                  StartActivity(Today);
              };
        }

        public static int GetHeightPixels
        {
            get { return k; }
        }

        public static int GetWidthPixels
        {
            get { return m; }
        }
        

    }
}