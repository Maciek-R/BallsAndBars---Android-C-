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
    class MyButton : Button, View.IOnClickListener
    {

    public GamPanel gamePanel;

    public MyButton(Context context, GamPanel gamePanel):base(context)
    {

        this.gamePanel = gamePanel;

            SetTextSize(Android.Util.ComplexUnitType.Dip, 10);
            // SetTextSize(10);
            Text = "POZIOM";
     //   SetText("POZIOM");

        SetOnClickListener(this);
    }


    
    public void OnClick(View v)
    {

     //   System.out.println("ONCLICK");
        if (gamePanel.dir == GamPanel.Direction.Poziom)
        {
                Text = "PION";
                // SetText("PION", 0, 4);
                gamePanel.dir = GamPanel.Direction.Pion;
        }
        else
        {
                Text = "POZIOM";
                // SetText("POZIOM");
                gamePanel.dir = GamPanel.Direction.Poziom;
        }
    }
}
}