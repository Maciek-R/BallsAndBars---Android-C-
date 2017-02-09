using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using System;

namespace BallsAndBars
{
    [Activity(Label = "BallsAndBars", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 0;

        private MyTextView Level;
        private MyTextView Percent;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            


            this.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;

            this.Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            this.RequestWindowFeature(Android.Views.WindowFeatures.NoTitle);


            DisplayMetrics dm = new DisplayMetrics();

            WindowManager.DefaultDisplay.GetMetrics(dm);

            Constans.init(dm);

            // FrameLayout frameLayout = new FrameLayout(this);


            

            Level = new MyTextView(this);
            Level.setAsLevel();


            Percent = new MyTextView(this);
            Percent.setAsPercent();

            GamPanel gamePanel = new GamPanel(this, Level, Percent, this);

            MyButton myButton = new MyButton(this, gamePanel);


            //     Button button = FindViewById<Button>(Resource.Id.button1);



            //  ViewGroup.LayoutParams l = new ViewGroup.LayoutParams(100, 100);
            // Log.Debug("asdadss", "asdasdas");

            //   frameLayout.AddView(gamePanel);
            SetContentView(gamePanel);

            //  AddContentView(button, lay);

            RelativeLayout.LayoutParams lay = new RelativeLayout.LayoutParams(Constans.BUTTON_WIDTH, Constans.BUTTON_HEIGHT);
            RelativeLayout.LayoutParams lay2 = new RelativeLayout.LayoutParams(Level.getMyWidth(), Level.getMyHeight());
            RelativeLayout.LayoutParams lay3 = new RelativeLayout.LayoutParams(Percent.getMyWidth(), Percent.getMyHeight());

            AddContentView(myButton, lay);
            AddContentView(Level, lay2);
            AddContentView(Percent, lay3);
            

          


        //    SetContentView(gamePanel);


            // Set our view from the "main" layout resource
            /*  SetContentView (Resource.Layout.Main);

              Button button = FindViewById<Button>(Resource.Id.button1);
              button.Click += delegate
              {
                  button.Text = string.Format("{0} clicks!", count++);
              };
              */



        }

       
    }

   

}