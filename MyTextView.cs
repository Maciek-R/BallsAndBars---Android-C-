using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace BallsAndBars
{
    public class MyTextView: TextView
    {

    private int MyWidth;
    private int MyHeight;


    public MyTextView(Context context):base(context)
    {
        



    }

    public int getMyHeight()
    {
        return MyHeight;
    }

    public int getMyWidth()
    {
        return MyWidth;
    }

    public void setAsPercent()
    {
            // setText("00");
            // Text = "000";
            char[] tex = { '0', '0', '0' };
            SetText(tex, 0, 2);
            SetTextColor(Color.Red);
        Measure(0, 0);
            //        setText("0");
        //    Text = "0";
        
        MyWidth = MeasuredWidth;
        MyHeight = MeasuredHeight;

        SetX(Constans.SCREEN_WIDTH - getMyWidth());
        SetY(MyHeight);
        //setBackgroundColor(Color.GREEN);
    }

        public void setAsLevel()
        {
              Text = "Poziom 10";
          
            
        SetTextColor(Color.Red);
        Measure(0, 0);
        MyWidth = MeasuredWidth;
        MyHeight = MeasuredHeight;


        SetX(Constans.SCREEN_WIDTH - MyWidth);


        SetY(0);

        //setBackgroundColor(Color.GREEN);
        // setText("Poziom 1");
    }

    public void show(MainActivity mainActivity, string text)
    {

            //  mainActivity.RunOnUiThread(run(text));

            mainActivity.RunOnUiThread(() => run(text));
        }

        private object run(string text)
        {
            
            Text = text;
            return text;
            //throw new NotImplementedException();
        }

        
    }

}

