using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace BallsAndBars
{
    class MainThread
    {

        Thread thread;

    public const int MAX_FPS = 240;
    private double averageFPS;
    private ISurfaceHolder surfaceHolder;
    private GamPanel gamePanel;
    private bool running;
    public static Canvas canvas;

        long lastTimeNano;

        public MainThread(ISurfaceHolder surfaceHolder, GamPanel gamePanel):base()
    {
           
        this.surfaceHolder = surfaceHolder;
        this.gamePanel = gamePanel;

            lastTimeNano = DateTime.Now.ToFileTime() * 100;
            thread = new Thread(run);
    }

    public void setRunning(bool running)
    {
        this.running = running;
    }

        public void join()
        {
            thread.Join();
        }

    public void Start()
     {
            thread.Start();
     }

        
    public void run()
    {

        long startTime;
        long timeMillis;
        long waitTime;
        int frameCount = 0;
        double totalTime = 0;
        long targetTime = 1000 / MAX_FPS;

            //long lastTimeNano = System.nanoTime();
            
            double time_elapsed_in_sec;

        while (running)
        {
           // startTime = System.nanoTime();
           startTime = DateTime.Now.ToFileTime()*100;

                time_elapsed_in_sec = (double)(startTime - lastTimeNano) / 1000000000;
            //   System.out.println("Minelo " + time_elapsed_in_sec);
            // System.out.println("Przes " + time_elapsed_in_sec*500);


            lastTimeNano = startTime;
            canvas = null;

            try
            {

                    
                    
                        canvas = this.surfaceHolder.LockCanvas();
                        lock (surfaceHolder)
                        {
                            this.gamePanel.update(time_elapsed_in_sec);
                            if (canvas != null)
                                this.gamePanel.Draw(canvas);
                            // System.out.println(this.gamePanel.getBallX());
                        }
                    

            }
            catch (Java.Lang.Exception e) { }
            finally
            {
                if (canvas != null)
                {
                    try
                    {
                        surfaceHolder.UnlockCanvasAndPost(canvas);
                    }
                    catch (Java.Lang.Exception e) { }
                }

            }

           // timeMillis = (System.nanoTime() - startTime) / 1000000;
           timeMillis = (DateTime.Now.ToFileTime()*100 - startTime) / 1000000;
            waitTime = targetTime - timeMillis;

            try
            {
                    if (waitTime > 0)
                        Thread.Sleep((int)waitTime);
                  //  this.Wait(waitTime);
            }
            catch (Java.Lang.Exception e) {  }

           // totalTime += System.nanoTime() - startTime;
           totalTime += DateTime.Now.ToFileTime()*100 - startTime;
            frameCount++;
            if (frameCount == MAX_FPS)
            {
                averageFPS = 1000 / ((totalTime / frameCount) / 1000000);
                frameCount = 0;
                totalTime = 0;
                    Console.WriteLine(averageFPS);
               // System.out.println(averageFPS);
            }

        }

    }
}

}