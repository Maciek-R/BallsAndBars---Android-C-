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

namespace BallsAndBars
{


    class Round
    {

        private int stage;
        private MyTextView level;
        private TextView percent;
        private int BallsCount;
        private MainActivity mainActivity;

        public Round(MyTextView level, TextView percent, List<Ball> balls, MainActivity mainActivity, Area area)
        {

            this.stage = 1;
            this.level = level;
            this.percent = percent;
            this.BallsCount = 1;
            this.mainActivity = mainActivity;
          //  Console.WriteLine("ROZMIARBALLS: " + balls.Count);
            balls.Add(new Ball(area));
        }
        public void nextRound(List<Ball> balls, Area area)
        {
            stage++;
            mainActivity.RunOnUiThread(() => run());
            



            balls.Clear();
            for(int i = 0; i<stage; ++i){
                balls.Add(new Ball(area));
            }
          }



         public int getStage()
         {
            return stage;
         }

        private object run()
        {
            
            
            
            level.Text = "Poziom " + stage.ToString();
            percent.Text="0";
            return null;
            //throw new NotImplementedException();
        }
    }

 

}