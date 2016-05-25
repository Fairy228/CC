using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Text;
using Android.Views.InputMethods;
using Android.Graphics;

namespace App2
{
    [Activity(Label = "Calculator", MainLauncher = false, Icon = "@drawable/AppIcon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class FoodInfo:Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            Window.DecorView.SetBackgroundColor(Color.Purple);

            int d = Resources.DisplayMetrics.HeightPixels;

            Button button = FindViewById<Button>(Resource.Id.button1);
            button.SetHeight(d / MainActivity.BUTTON_COEFF);

            EditText editText1 = FindViewById<EditText>(Resource.Id.editText1);
            editText1.SetHeight(d / MainActivity.EDIT_TEXT_COEFF);
            editText1.SetBackgroundColor(Color.BlueViolet);

            EditText editText2 = FindViewById<EditText>(Resource.Id.editText2);
            editText2.SetHeight(d / MainActivity.EDIT_TEXT_COEFF);
            editText2.SetBackgroundColor(Color.BlueViolet);

            TextView view1 = FindViewById<TextView>(Resource.Id.textView1);
            view1.SetHeight(d / MainActivity.TEXTVIEW_COEFF);

            TextView view2 = FindViewById<TextView>(Resource.Id.textView2);
            view2.SetHeight(d / MainActivity.TEXTVIEW_COEFF);

            TextView view3 = FindViewById<TextView>(Resource.Id.textView3);
            view3.SetHeight(d / MainActivity.TEXTVIEW_COEFF);

            TextView view4 = FindViewById<TextView>(Resource.Id.textView4);
            view4.SetHeight(d / MainActivity.TEXTVIEW_COEFF);

            TextView view5 = FindViewById<TextView>(Resource.Id.textView5);
            view5.SetHeight(d / MainActivity.TEXTVIEW_COEFF);

            TextView view6 = FindViewById<TextView>(Resource.Id.textView6);
            view6.SetHeight(d / MainActivity.TEXTVIEW_COEFF);

            TextView Space = FindViewById<TextView>(Resource.Id.textView7);
            Space.SetHeight(d / MainActivity.TEXTVIEW_COEFF / 5);

            InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);

            view1.Touch += delegate
            {
                inputManager.HideSoftInputFromWindow(editText1.WindowToken, 0);
                inputManager.HideSoftInputFromWindow(editText2.WindowToken, 0);
            };

            view2.Touch += delegate
            {
                inputManager.HideSoftInputFromWindow(editText1.WindowToken, 0);
                inputManager.HideSoftInputFromWindow(editText2.WindowToken, 0);
            };

            view3.Touch += delegate
            {
                inputManager.HideSoftInputFromWindow(editText1.WindowToken, 0);
                inputManager.HideSoftInputFromWindow(editText2.WindowToken, 0);
            };

            view4.Touch += delegate
            {
                inputManager.HideSoftInputFromWindow(editText1.WindowToken, 0);
                inputManager.HideSoftInputFromWindow(editText2.WindowToken, 0);
            };

            view5.Touch += delegate
            {
                inputManager.HideSoftInputFromWindow(editText1.WindowToken, 0);
                inputManager.HideSoftInputFromWindow(editText2.WindowToken, 0);
            };

            view6.Touch += delegate
            {
                inputManager.HideSoftInputFromWindow(editText1.WindowToken, 0);
                inputManager.HideSoftInputFromWindow(editText2.WindowToken, 0);
            };

            editText1.Click += delegate
              {
                  editText1.Text = null;
                  double f = 0;
                  double.TryParse(editText2.Text, out f);
                  if (f != 0)
                      editText2.Text = "";
              };

            editText2.Click += delegate
            {
                editText2.Text = null;
            };

            button.Click += delegate
            {
                double q = 0;
                string fName = null;
                string fValue = null;
                string protValue = null;
                string fatValue = null;
                string uglValue = null;
                int FValue = 0;
                StreamReader str = new StreamReader(Assets.Open("Food.txt"));
                int count = 0;
                try                                    
                {
                    while (!str.EndOfStream)
                    {
                        count = 0;
                        fName = null;
                        fValue = "";
                        protValue = null;
                        fatValue = null;
                        uglValue = null;
                        string line = str.ReadLine();
                        for (int i = 0; i < line.Length; i++)
                            if (line[i] == '-')
                                count = i;
                        for (int i = 0; i < count; i++)
                            fName += line[i];
                        if (editText1.Text.ToLower() == fName)
                        {
                            for (int i = count + 1; line[i] != ','; i++)
                            {
                                fValue += line[i];
                                count++;
                            }
                            for (int i = count + 2; line[i] != '?'; i++)
                            {
                                protValue += line[i];
                                count++;
                            }
                            for (int i = count + 3; line[i] != '!'; i++)
                            {
                                fatValue += line[i];
                                count++;
                            }
                            for (int i = count + 4; i < line.Length; i++)
                            {
                                uglValue += line[i];
                                count++;
                            }
                            break;
                        }
                    }
                    FValue = int.Parse(fValue);
                    view2.Text = "Proteins: " + protValue.ToString();
                    view3.Text = "Fats: " + fatValue.ToString();
                    view4.Text = "carbohydrates: " + uglValue.ToString();
                    view5.Text = "Calories: " + FValue.ToString();
                    double f = 0;
                    double.TryParse(editText2.Text, out f);
                    if (f == 0)
                        view6.Text = "Total calories: 0";
                    else
                        view6.Text = "Total calories: " + FValue * (double.Parse(editText2.Text) / 100);
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

