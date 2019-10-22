/********************************************************
 * Author: Chukwudi Ikem
 * Email: godofcollege43@csu.fullerton.edu
 * Course: CPSC 223N
 * Semester: Fall 2019
 * Assignment #: 4
 * Program name: BouncingBallMain
*****************************************************/
using System;
using System.Windows.Forms;
using System.Drawing;
//TODO: Create the rectangle, sharp black color for which the ball will bounce
//TODO: Create a refresh rate and an animation clock - create a ball to cut through the middle in a smooth fashion.
namespace BouncingBallScreen
{
    public class BouncingBallUI : Form
    {
        // dimensions for the ball screen
        protected const int from_x = 535;
        protected const int from_y = 115;
        protected const int maxdimensionforx = 850;
        protected const int maxdimensionfory = 850;
        // dimensions for the ball
        protected const int radius = 10; 
        protected double startingpositionx = 960;
        protected double startingpositiony = 540;
        //
        protected double BallSpeed_PerSecond; // pixels per second
        protected double BallSpeed_PerTic; // pixels per tic
        protected static System.Timers.Timer BallClock = new System.Timers.Timer(); // ball clock, this is what will handle the refresh. (by interval)
        protected const double BallSpeed = 60; // setting the refresh to 60 times per second
        protected static System.Timers.Timer GraphicClock = new System.Timers.Timer();
        protected const double GraphicSpeed = 30; // refresh the graphical area 30 times per second.
        protected double Speed { get; set; }

        public BouncingBallUI()
        {
            Size = new Size(1920, 1080);
            Text = "Bouncing Ball in Blistering Biome";
            BallClock.Elapsed += new System.Timers.ElapsedEventHandler(UpdateBallPosition);
            GraphicClock.Elapsed += new System.Timers.ElapsedEventHandler(UpdateBallPosition);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            // creating the bouncing ball area
            Graphics bouncingballarea = e.Graphics;
            bouncingballarea.DrawRectangle(Pens.Black,from_x,from_y,maxdimensionforx,maxdimensionfory);
            Graphics ball = e.Graphics;
            ball.FillEllipse(Brushes.Red, (int)startingpositionx, (int)startingpositiony, radius, radius);
            StartingGraphicClock();
            StartingBallClock();
            base.OnPaint(e);
        }
        protected void StartingGraphicClock()
        {
            GraphicClock.Interval = Convert.ToInt32((1000.0) / GraphicSpeed);
            GraphicClock.Enabled = true;
        }
        protected void StartingBallClock()
        {
            BallClock.Interval = Convert.ToInt32((1000.0 / BallSpeed)); // 1000.0 because it is in milliseconds. 1 Second / 60 refrehes a second. Sets interval to 60.
            BallClock.Enabled = true;
        }
        protected void UpdateBallPosition(object sender, System.Timers.ElapsedEventArgs myevent)
        {
            Invalidate();
            startingpositionx += 1;
        }
    }
}
