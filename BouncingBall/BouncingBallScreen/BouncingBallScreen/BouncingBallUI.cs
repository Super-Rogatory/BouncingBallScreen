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
//TODO: Configure New, SpeedBox, Direction Input Box.
namespace BouncingBallScreen
{
    public class BouncingBallUI : Form
    {
        // dimensions for the ball screen
        protected const int from_x = 535;
        protected const int from_y = 115;
        protected const int maxdimensionforx = 850;
        protected const int maxdimensionfory = 850;
        protected const double balldefault_xposition = 960;
        protected const double balldefault_yposition = 540;

        // dimensions for the ball
        protected const int radius = 10; 
        protected double startingpositionx = 960;
        protected double startingpositiony = 540;
        // fields dedicated to change in x and y
        protected double ball_deltax, ball_deltay;
        // balls frames per second along with the refresh rate
        protected double BallSpeed_PerSecond; // pixels per second
        protected double BallSpeed_PerTic; // pixels per tic
        protected double BallDirectionX, BallDirectionY; // TODO: the angle are which to launch the ball is given by the NEW
        protected static System.Timers.Timer BallClock = new System.Timers.Timer(); // ball clock, this is what will handle the refresh. (by interval)
        protected const double BallSpeed = 60; // setting the refresh to 60 times per second
        protected static System.Timers.Timer GraphicClock = new System.Timers.Timer();
        protected const double GraphicSpeed = 45; // refresh the graphical area 30 times per second.
        protected double Speed { get; set; }
        protected string SpeedBoxText { get; set; }
        protected bool isOn; //the boolean switch that will allow for an update in the ball position

        public BouncingBallUI()
        {
            Size = new Size(1920, 1080);
            Text = "Bouncing Ball in Blistering Biome";
            BallClock.Elapsed += new System.Timers.ElapsedEventHandler(UpdateBallPosition);
            GraphicClock.Elapsed += new System.Timers.ElapsedEventHandler(UpdateBallPosition);
            RenderButtons();
            RenderGraphics();
        }
        protected void RenderButtons()
        {
            Label speedboxTitle, directionboxTitle, _new;
            Button buttonStart, buttonQuit;
            buttonStart = new Button { Text = "Start", Location = new Point(355, 540), Size = new Size(50, 50) };
            buttonQuit = new Button { Text = "Quit", Location = new Point(1625, 540), Size = new Size(50, 50) };
            buttonStart.Click += UpdateBallPosition;
            buttonQuit.Click += EndApplication;
            Controls.Add(buttonStart);
            Controls.Add(buttonQuit);
            // TODO: | Label: X - Coord, Y - Coord
            TextBox SpeedBox, DirectionalBox;
            _new = new Label { Text = "Enter the speed (left side) and angle (right side) ", Location = new Point(1510, 350), AutoSize = true }; // creates instruction for the user
            speedboxTitle = new Label { Text = "Enter speed (1-100)", Location = new Point(1450, 380), AutoSize = true };
            SpeedBox = new TextBox {Location = new Point(1450,400)};
            directionboxTitle = new Label { Text = "Enter the degree (x,y)", Location = new Point(1750, 380), AutoSize = true };
            DirectionalBox = new TextBox { Location = new Point(1750, 400) };
            // adding controls
            Controls.Add(_new); // creates text 
            Controls.Add(speedboxTitle); // creates text
            Controls.Add(directionboxTitle); // creates text
            Controls.Add(SpeedBox);
            Controls.Add(DirectionalBox);
        }
        protected void RenderGraphics()
        {
            // TODO: ball speed per second is given by the new function
            BallSpeed_PerTic = BallSpeed_PerSecond / BallSpeed;
            // TODO: set BallDirectionX & BallDirectionY from New,
            double hypotenuse = Math.Sqrt((BallDirectionX * BallDirectionX) + (BallDirectionY * BallDirectionY)); // standard pythagorean theorem
            ball_deltax = (BallSpeed_PerTic * BallDirectionX) / hypotenuse;
            ball_deltay = (BallSpeed_PerTic * BallDirectionY) / hypotenuse;
        }
        protected void SpeedBoxEnter(object sender, EventArgs events)
        {
            // TODO: finish SpeedBox functionality for New Button
            
            
        }
        protected void DegreeBoxEnter(object sender, EventArgs events)
        {
            // TODO: finish DegreeBox function for New Button
        }
       
        protected override void OnPaint(PaintEventArgs e)
        {
            // creating the bouncing ball area
            Graphics bouncingballarea = e.Graphics;
            bouncingballarea.DrawRectangle(Pens.Black,from_x,from_y,maxdimensionforx,maxdimensionfory);
            Graphics ball = e.Graphics;
            ball.FillEllipse(Brushes.Red, (int)startingpositionx, (int)startingpositiony, radius, radius);
            if (isOn)
            {
                StartingGraphicClock(GraphicSpeed); // pull value from the new function
                StartingBallClock(BallSpeed); // pull value from the new functon
            }

            base.OnPaint(e);
        }
        protected void StartingGraphicClock(double update_graphic_speed)
        {
            //keeping a constant speed for now
            update_graphic_speed = GraphicSpeed;
            if(update_graphic_speed < 1.0)
            {
                update_graphic_speed = 1.0;
            }
            GraphicClock.Interval = Convert.ToInt32((1000.0) / update_graphic_speed);
            GraphicClock.Enabled = true;
        }
        protected void StartingBallClock(double update_ball_speed)
        {
            if(update_ball_speed < 1.0)
            {
                update_ball_speed = 1.0;
            }
            BallClock.Interval = Convert.ToInt32((1000.0 / update_ball_speed)); // 1000.0 because it is in milliseconds. 1 Second / 60 refrehes a second. Sets interval to 60.
            BallClock.Enabled = true;
        }
        protected void UpdateBallPosition(object sender, EventArgs myevent)
        {
            isOn = true;
            Invalidate();
            startingpositionx += 1.5;
        }
        protected void EndApplication(object sender, EventArgs myevent)
        {
            Close();
        }
    }
}
