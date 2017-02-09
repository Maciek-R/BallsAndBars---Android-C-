using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BallsAndBars
{
    class Area
    {

        public Rect Border { get; set; }

        Rect LeftRect;
        Rect RightRect;
        Rect TopRect;
        Rect BottomRect;

        public Area()
        {

            Border = null;

            LeftRect = null;
            RightRect = null;
            TopRect = null;
            BottomRect = null;
        }

        public Area(Rect rect):this()
        {

            Border = new Rect(rect);

        }
        public Area(Area area)
        {

            Border = area.Border;
            LeftRect = area.LeftRect;
            RightRect = area.RightRect;
            TopRect = area.TopRect;
            BottomRect = area.BottomRect;
        }

        public int getSurface()
        {
            return ((Border.Right - Border.Left) * (Border.Bottom - Border.Top));
        }

        public void set(Rect rect)
        {
            Border = rect;
        }
    }

}