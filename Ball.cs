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
    class Ball
    {

        public double X { get; set; }
        public double Y { get; set; }
        public double Angle { get; set; }

        public static int SizeX = (4 * Constans.SCREEN_WIDTH) / 100;
         public static int SizeY = (4 * Constans.SCREEN_WIDTH) / 100;

        public static Random rand;

        static Ball(){

            rand = new Random();

            }

      

        private Area areaPoint;

        public Ball(Area area)
        {
      
            X = rand.Next(Constans.SCREEN_WIDTH - SizeX);
            Y = rand.Next(Constans.SCREEN_HEIGHT - Constans.START_Y - SizeY) + Constans.START_Y;
            Angle = rand.Next(360);
            areaPoint = area;
           
        }

        public void setAreaPoint(Area areaPoint)
        {
            this.areaPoint = areaPoint;
        }
        public Area getAreaPoint()
        {
            return areaPoint;
        }
        public Rect getRect()
        {
            return new Rect((int)X, (int)Y, (int)X + SizeX, (int)Y + SizeY);
        }
    }

}