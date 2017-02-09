using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace BallsAndBars
{
    class Constans
    {
        public static int SCREEN_WIDTH;
        public static int SCREEN_HEIGHT;

        public static int BUTTON_WIDTH;
        public static int BUTTON_HEIGHT;

        public static int START_Y;
        public static int START_Y_LIFES;
        public static int START_X_LIFES;

        public static int BAR_WIDTH;

        public static int SPEED;

        public static int LIFE_SPACE;
        public static int LIFE_WIDTH;

        public static float GAME_OVER_SIZE;
        public static int GAME_OVER_WIDTH;

        public static void init(DisplayMetrics dm)
        {
            SCREEN_WIDTH = dm.WidthPixels;
            SCREEN_HEIGHT = dm.HeightPixels;

            BUTTON_WIDTH = (20 * SCREEN_WIDTH) / 100;
            BUTTON_HEIGHT = (10 * SCREEN_HEIGHT) / 100;

            START_Y = (12 * SCREEN_HEIGHT) / 100;
            START_Y_LIFES = (1 * SCREEN_HEIGHT) / 100;
            START_X_LIFES = (21 * SCREEN_WIDTH) / 100;

            BAR_WIDTH = (1 * SCREEN_HEIGHT) / 100;

            SPEED = (70 * SCREEN_WIDTH) / 100;

            LIFE_SPACE = (5 * SCREEN_WIDTH) / 100;
            LIFE_WIDTH = (4 * SCREEN_WIDTH) / 100;
        }
    }
}