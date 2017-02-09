using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;

namespace BallsAndBars
{
    class GamPanel : SurfaceView, ISurfaceHolderCallback
    {
        MainActivity mainActivity;
        Context context;
        ISurfaceHolder surfaceHolder;
        Paint painter;

        public enum Direction { Poziom, Pion, None };

        public Direction dir = Direction.Poziom;

        MainThread myThread;

        Rect rect;

        MyTextView Level;
        MyTextView Percent;
        Round round;

        Rect Slider;
        Direction DirSlider;

       
        DisplayMetrics dm;

        int percent = 0;
        int Lifes = 5;
        bool gameOver = false;


        List<Ball> balls;
        List<Area> areas;
        Area areaPointer;


        public GamPanel(Context context, MyTextView Level, MyTextView percent, MainActivity mainActiv) : base(context)
        {
            
            Console.WriteLine("konstruktor GamePanel");

            this.mainActivity = mainActiv;
            this.context = context;
            this.Holder.AddCallback(this);
            this.surfaceHolder = Holder;

            painter = new Paint();
            painter.SetARGB(255, 200, 0, 0);
            painter.TextSize = 160;

            myThread = new MainThread(Holder, this);
            // rect = new Rect(10, 10, 10 + 100, 10 + 100);


            // area = new Area(new Rect(0, Constans.START_Y, Constans.SCREEN_WIDTH, Constans.SCREEN_HEIGHT));

            areas = new List<Area>();
            areas.Add(new Area(new Rect(0, Constans.START_Y, Constans.SCREEN_WIDTH, Constans.SCREEN_HEIGHT)));
            areaPointer = null;

           // Console.WriteLine("ROZMIAR: " + areas.Count);

            balls = new List<Ball>();

            this.Level = Level;
            this.Percent = percent;
            round = new Round(Level, percent, balls, mainActiv, areas[0]);
            DirSlider = Direction.None;


            Focusable =true;

            
            

        }



        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
           // throw new NotImplementedException();
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            switch (e.Action) {
                case MotionEventActions.Down:
                // case MotionEvent.ACTION_MOVE:

                if (areaPointer == null)
            {
                int X = (int) e.GetX();
                    int Y = (int) e.GetY();

                    foreach (Area area in areas) {
            if (area.Border.Contains(X, Y))
            {

                areaPointer = area;

                if (dir == Direction.Poziom)
                {

                    if (DirSlider == Direction.None)
                    {
                        DirSlider = Direction.Poziom;
                        Slider = new Rect(X, Y, X, Y + Constans.BAR_WIDTH);
                    }


                }
                else if (dir == Direction.Pion)
                {

                    if (DirSlider == Direction.None)
                    {
                        DirSlider = Direction.Pion;
                        Slider = new Rect(X, Y, X + Constans.BAR_WIDTH, Y);
                    }

                }
            }
        }
        }
                    break;
    }

        return true;
           // return base.OnTouchEvent(e);
        }

        Random rnd = new Random();
        public void SurfaceCreated(ISurfaceHolder holder)
        {

            Console.WriteLine("CREATED");
            myThread = new MainThread(Holder, this);
            myThread.setRunning(true);
            myThread.Start();

          //  if (mainActivity == null) Console.WriteLine("PUSTO");

            this.Level.show(mainActivity, "Poziom 1");
            this.Percent.show(mainActivity, "0");
           

        }

        private void checkProgress()
        {

            Slider = null;
            DirSlider = Direction.None;
            areaPointer = null;

            int WholeSurface = Constans.SCREEN_WIDTH * (Constans.SCREEN_HEIGHT - Constans.START_Y);

            int NotUsedSurface = 0;

            foreach (Area area in areas)
            {
                NotUsedSurface += area.getSurface();
            }

            int UsedSurface = WholeSurface - NotUsedSurface;

            double percent = (((double)(UsedSurface) / WholeSurface) * 100);
            this.percent = (int)percent;

            
            Percent.show(mainActivity, percent.ToString());



            if (percent > 80)
            {
               


                areas.Clear();
                areas.Add(new Area(new Rect(0, Constans.START_Y, Constans.SCREEN_WIDTH, Constans.SCREEN_HEIGHT)));
                areaPointer = null;

                round.nextRound(balls, areas[0]);
                this.percent = 0;

            }
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public void update(double time_elapsed_in_sec)
        {
            if (gameOver) return;

            double przes = ((time_elapsed_in_sec)) * Constans.SPEED;

            foreach (Ball ball in balls)
            {
                double przesX = (Math.Cos(DegreeToRadian(ball.Angle)) * przes);
                double przesY = (Math.Sin(DegreeToRadian(ball.Angle)) * przes);

                ball.X = ball.X + przesX;
                ball.Y = ball.Y + przesY;
            }

            checkCollisions();

            if (DirSlider != Direction.None)
            {

                int l = Slider.Left;
                int t = Slider.Top;
                int b = Slider.Bottom;
                int r = Slider.Right;

                if (DirSlider == Direction.Pion)
                {
                    Slider.Set(l, t - (int)przes, r, b + (int)przes);

                    if (Slider.Top < areaPointer.Border.Top && Slider.Bottom > areaPointer.Border.Bottom)
                    {

                        int X = Slider.Left;

                        if (!isBallBetween(areaPointer.Border.Left, areaPointer.Border.Top, X, areaPointer.Border.Bottom))
                        {
                            areaPointer.Border.Left = X + Constans.BAR_WIDTH;
                            // System.out.println("1");
                        }

                        else if (!isBallBetween(X, areaPointer.Border.Top, areaPointer.Border.Right, areaPointer.Border.Bottom))
                        {
                            areaPointer.Border.Right = X;
                            //System.out.println("2");
                        }
                        else
                        {
                          //  System.out.println("3");
                           // System.out.println((X + Constans.BAR_WIDTH) + " " + areaPointer.border.top + " " + areaPointer.border.right + " " + areaPointer.border.bottom);
                          //  System.out.println(areaPointer.border.left + " " + areaPointer.border.top + " " + X + " " + areaPointer.border.bottom);

                            areas.Add(new Area(new Rect(X + Constans.BAR_WIDTH, areaPointer.Border.Top, areaPointer.Border.Right, areaPointer.Border.Bottom)));
                            areaPointer.set((new Rect(areaPointer.Border.Left, areaPointer.Border.Top, X, areaPointer.Border.Bottom)));

                            setBallsToArea(Direction.Pion);
                        }

                        checkProgress();

                    }
                    else if (Slider.Top < areaPointer.Border.Top)
                    {
                        Slider.Set(Slider.Left, areaPointer.Border.Top, Slider.Right, Slider.Bottom);
                    }
                    else if (Slider.Bottom > areaPointer.Border.Bottom)
                    {
                        Slider.Set(Slider.Left, Slider.Top, Slider.Right, areaPointer.Border.Bottom);
                    }

                }
                else if (DirSlider == Direction.Poziom)
                {
                    Slider.Set(l - (int)przes, t, r + (int)przes, b);

                    if (Slider.Left < areaPointer.Border.Left && Slider.Right > areaPointer.Border.Right)
                    {

                        int Y = Slider.Top;

                        if (!isBallBetween(areaPointer.Border.Left, areaPointer.Border.Top, areaPointer.Border.Right, Y))
                        {

                            areaPointer.Border.Top = Y + Constans.BAR_WIDTH;
                           // System.out.println("4");
                        }

                        else if (!isBallBetween(areaPointer.Border.Left, Y, areaPointer.Border.Right, areaPointer.Border.Bottom))
                        {

                            areaPointer.Border.Bottom = Y;
                          //  System.out.println("5");

                        }
                        else
                        {
                           // System.out.println("6");
                           // System.out.println(areaPointer.border.left + " " + Y + Constans.BAR_WIDTH + " " + areaPointer.border.right + " " + areaPointer.border.bottom);
                           // System.out.println(areaPointer.border.left + " " + areaPointer.border.top + " " + areaPointer.border.right + " " + Y);

                            areas.Add(new Area(new Rect(areaPointer.Border.Left, Y + Constans.BAR_WIDTH, areaPointer.Border.Right, areaPointer.Border.Bottom)));
                            areaPointer.set((new Rect(areaPointer.Border.Left, areaPointer.Border.Top, areaPointer.Border.Right, Y)));

                            setBallsToArea(Direction.Poziom);
                        }




                        checkProgress();

                    }
                    else if (Slider.Left < areaPointer.Border.Left)
                    {
                        Slider.Set(areaPointer.Border.Left, Slider.Top, Slider.Right, Slider.Bottom);
                    }
                    else if (Slider.Right > areaPointer.Border.Right)
                    {
                        Slider.Set(Slider.Left, Slider.Top, areaPointer.Border.Right, Slider.Bottom);
                    }
                }
            }
        }

        private void checkCollisions()
        {
            foreach (Ball b in balls)
            {

                double x = b.X;
                double y = b.Y;
                double angle = b.Angle;
                Area ar = b.getAreaPoint();

                if (x < ar.Border.Left)
                {
                    angle = (180 - angle) % 360;
                    b.Angle = angle;
                    b.X = ar.Border.Left;
                }
                else if (x + Ball.SizeX >= ar.Border.Right)
                {
                    angle = (180 - angle) % 360;
                    b.Angle =angle;
                    b.X = ar.Border.Right - Ball.SizeX;
                }

                if (y < ar.Border.Top)
                {
                    angle = (-angle) % 360;
                    b.Angle = angle;
                    b.Y = ar.Border.Top;
                }
                else if (y + Ball.SizeY >= ar.Border.Bottom)
                {
                    angle = (-angle) % 360;
                    b.Angle = angle;
                    b.Y = ar.Border.Bottom - Ball.SizeY;
                }

                if (Slider != null)
                {
                    if (Slider.Intersect((int)x, (int)y, (int)x + Ball.SizeX, (int)y + Ball.SizeY))
                    {
                        areaPointer = null;
                        DirSlider = Direction.None;
                        Slider = null;
                        Lifes--;
                        if (Lifes == 0)
                        {
                            gameOver = true;


                          //  MyFileReader Fr = new MyFileReader(context);
                           // Integer stage = Fr.readNextLine();
                           // Integer percent = Fr.readNextLine();


                            

                          /*  if ((stage == null && percent == null) || (round.getStage() > stage.intValue()) ||
                                    ((round.getStage() >= stage.intValue()) && (this.percent > percent.intValue())))
                            {

                                MyFileWriter Fw = new MyFileWriter(context);
                                Fw.write(String.valueOf(round.getStage() + "\n"));
                                Fw.write(String.valueOf(this.percent));
                                Fw.close();

                            }*/


                        }
                    }
                }
            }


        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            //  super.draw(canvas);


            canvas.DrawColor(Color.White);



            Paint paint = new Paint();
            paint.SetStyle(Paint.Style.Fill);


            paint.Color = Color.Red;
            canvas.DrawRect(new Rect(0, Constans.START_Y - Constans.BAR_WIDTH, Constans.SCREEN_WIDTH, Constans.SCREEN_HEIGHT), paint);
            paint.Color =Color.Yellow;

            foreach (Area area in areas)
            {
                canvas.DrawRect(area.Border, paint);
            }
            paint.Color = Color.Green;
            foreach (Ball ball in balls)
            {
                int x = (int)ball.X;
                int y = (int)ball.Y;
                canvas.DrawRect(new Rect(x, y, x + Ball.SizeX, y + Ball.SizeY), paint);
            }

            drawSlider(canvas);
            drawLifes(canvas);

            if (gameOver)
            {

                drawGameOver(canvas);
            }

        }

        private bool isBallBetween(int x1, int y1, int x2, int y2)
        {
            int SizeX = Ball.SizeX;
            int SizeY = Ball.SizeY;

            foreach (Ball ball in balls)
            {
                Rect rect = new Rect((int)ball.X, (int)ball.Y, (int)ball.X + SizeX, (int)ball.Y + SizeY);

                if (rect.Intersect(new Rect(x1, y1, x2, y2)))
                    return true;
            }
            return false;
        }
        private void setBallsToArea(Direction dir)
        {

            if (dir == Direction.Pion)
            {
                foreach (Ball ball in balls)
                {
                    if (ball.getAreaPoint() == areaPointer)
                    {
                        if (ball.X >= areaPointer.Border.Right + Constans.BAR_WIDTH)
                        {
                            ball.setAreaPoint(areas[areas.Count - 1]);
                        }
                    }
                }
            }
            else if (dir == Direction.Poziom)
            {

                foreach (Ball ball in balls)
                {
                    if (ball.getAreaPoint() == areaPointer)
                    {
                        if (ball.Y >= areaPointer.Border.Bottom + Constans.BAR_WIDTH)
                        {
                            ball.setAreaPoint(areas[areas.Count - 1]);
                        }
                    }
                }
            }
        }

        private void drawLifes(Canvas canvas)
        {
            Paint paint = new Paint();
            paint.SetStyle(Paint.Style.Fill);
            paint.Color = Color.Red;

            int X = Constans.START_X_LIFES;

            for (int i = 0; i < Lifes; ++i)
            {
                canvas.DrawRect(new Rect(X + i * Constans.LIFE_SPACE, Constans.START_Y_LIFES, X + Constans.LIFE_WIDTH + i * Constans.LIFE_SPACE, Constans.START_Y_LIFES + Constans.LIFE_WIDTH), paint);
            }

        }
        private void drawGameOver(Canvas canvas)
        {
            Paint painter = new Paint();
            
            

            painter.SetStyle(Paint.Style.Fill);
            painter.Color = Color.Blue;


            /*int w = (int) paint.measureText("GAME OVER");
            while(w<Constans.SCREEN_WIDTH/2){
                paint.setTextSize((float)(paint.getTextSize()+0.5));
                w = (int) paint.measureText("GAME OVER");
            }*/
            painter.TextSize = Constans.GAME_OVER_SIZE;

           

            // System.out.println(paint.getTextSize());
            canvas.DrawText("GAME OVER", Constans.SCREEN_WIDTH / 2 - Constans.GAME_OVER_WIDTH / 2, Constans.SCREEN_HEIGHT / 2, painter);

            // paint.setTextSize(Constans.AUTHOR_SIZE);

            //canvas.drawText(Constans.AUTHOR, Constans.SCREEN_WIDTH - Constans.AUTHOR_WIDTH, Constans.SCREEN_HEIGHT, paint);

        }

        private void drawSlider(Canvas canvas)
        {
            Paint paint = new Paint();
            paint.SetStyle(Paint.Style.Fill);

            paint.Color = Color.Red;
            if (Slider != null)
                canvas.DrawRect(Slider, paint);
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            bool retry = true;
            while (retry)
            {
                try
                {
                    
                    myThread.setRunning(false);
                    myThread.join();
                    balls.Clear();
                    retry = false;
                }
                catch (Exception e)
                {
                   // e.printStackTrace();
                }
               

            }
        }
    }
}