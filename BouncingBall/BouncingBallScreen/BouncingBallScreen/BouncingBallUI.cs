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

namespace BouncingBallScreen
{
    public class BouncingBallUI : Form
    {
        // declaration of RenderButtons
        Button buttonStart, buttonQuit, readysetgo, buttonNew;
        Label speedboxTitle, directionboxTitle, _new, viewcoordinateX, viewcoordinateY;
        TextBox SpeedBox, DirectionalBox;
        // position for the ball screen
        protected const int from_x = 535;
        protected const int from_y = 115;
        protected const int maxdimensionforx = 850;
        protected const int maxdimensionfory = 850;
        protected const double balldefault_xposition = 960;
        protected const double balldefault_yposition = 540;
        // position during ricochet
        protected const double rightsidehand_x = 1385;
        protected const double leftsidehand_x = 535;
        protected const double upperwall_y = 115;
        protected const double lowerwall_y = 965;
        // dimensions for the ball
        protected const int radius = 10;
        protected double startingpositionx = 960;
        protected double startingpositiony = 540;
        // fields dedicated to change in x and y
        protected double ball_deltax, ball_deltay;
        // balls frames per second along with the refresh rate
        protected double BallSpeed_PerSecond; // pixels per second
        protected double BallSpeed_PerTic; // pixels per tic
        protected static System.Timers.Timer BallClock = new System.Timers.Timer(); // ball clock, this is what will handle the refresh. (by interval)
        protected static System.Timers.Timer GraphicClock = new System.Timers.Timer();
        protected static System.Timers.Timer TextClock = new System.Timers.Timer();
        protected double BallSpeed = 60; // setting the refresh to 60 times per second
        protected double GraphicSpeed = 45; //DEFAULT: refresh the graphical area 30 times per second.
        protected static double BallDirection = 240; //DEFAULT: Works
        // misc
        protected double directioninradians;
        protected double Speed { get; set; }
        protected string SpeedBoxText { get; set; }
        protected bool isOn; //the boolean switch that will allow for an update in the ball position
        public BouncingBallUI()
        {
            Size = new Size(1920, 1080);
            Text = "Bouncing Ball in Blistering Biome";
            BallClock.Elapsed += new System.Timers.ElapsedEventHandler(UpdateBallPosition);
            GraphicClock.Elapsed += new System.Timers.ElapsedEventHandler(UpdateBallPosition);
            TextClock.Elapsed += new System.Timers.ElapsedEventHandler(UpdateXYClock);
            RenderButtons();
        }
        public void RenderButtons()
        {
            // Buttons
            buttonStart = new Button { Text = "Start", Location = new Point(355, 540), Size = new Size(50, 50) };
            buttonQuit = new Button { Text = "Quit", Location = new Point(1625, 540), Size = new Size(50, 50) };
            buttonNew = new Button { Text = "New", Location = new Point(1612, 400), AutoSize = true };
            readysetgo = new Button { Text = "Set", Location = new Point(1612, 500), AutoSize = true };
            viewcoordinateX = new Label { Text = GetTextX(), Location = new Point(500, 85), Size = new Size(42,20), BackColor = Color.GhostWhite };
            viewcoordinateY = new Label { Text = GetTextY(), Location = new Point(1350, 85), Size = new Size(42,20), BackColor = Color.GhostWhite };
            Controls.Add(viewcoordinateX);
            Controls.Add(viewcoordinateY);

            buttonStart.Click += UpdateBallPosition;
            buttonStart.Click += UpdateXYClock;
            buttonQuit.Click += EndApplication;
            buttonNew.Click += NewButtonClick;

            Controls.Add(buttonStart);
            Controls.Add(buttonQuit);
            Controls.Add(buttonNew);
            // Label: X - Coord, Y - Coord
            // Titles / Labels
            _new = new Label { Text = "Enter the speed (left side) and angle (right side) ", Location = new Point(1510, 350), AutoSize = true }; // creates instruction for the user
            speedboxTitle = new Label { Text = "Enter speed (1-300)", Location = new Point(1450, 380), AutoSize = true };
            directionboxTitle = new Label { Text = "Enter the degree", Location = new Point(1750, 380), AutoSize = true };

            // Textboxs / Input Properties
            SpeedBox = new TextBox { Location = new Point(1450, 400), Enabled = true, Text = string.Empty };
            DirectionalBox = new TextBox { Location = new Point(1750, 400), Enabled = true, Text = string.Empty };
            readysetgo.Click += GetValues;

        }

        protected string GetTextX() => Convert.ToString(Math.Round(startingpositionx, 0)); // GetText is the function, returning String conversion of x position
        protected string GetTextY() => Convert.ToString(Math.Round(startingpositiony, 0));

        protected void NewButtonClick(object sender, EventArgs events)
        {
            isOn = false;
            BallClock.Enabled = false;
            GraphicClock.Enabled = false;
            Controls.Add(_new); // creates text 
            Controls.Add(speedboxTitle); // creates text
            Controls.Add(directionboxTitle); // creates text
            Controls.Add(SpeedBox);
            Controls.Add(DirectionalBox);
            Controls.Add(readysetgo);
            Controls.Remove(buttonNew);

        }
        protected void GetValues(object sender, EventArgs events)
        {
            BallDirection = double.Parse(DirectionalBox.Text); //occurs before RenderGraphics
            BallSpeed = double.Parse(SpeedBox.Text);
            // need to convert degrees to radians.
            directioninradians = (BallDirection * Math.PI) / 180.0;
            // The speed per tic is the ball speed per refresh divided by refreshes a second
            BallSpeed_PerTic = BallSpeed / GraphicSpeed;
            ball_deltax = BallSpeed_PerTic * Math.Cos(directioninradians);
            ball_deltay = BallSpeed_PerTic * Math.Sin(directioninradians);
            Controls.Remove(readysetgo);
            Controls.Remove(_new);
            Controls.Remove(speedboxTitle);
            Controls.Remove(directionboxTitle);
            Controls.Remove(SpeedBox);
            Controls.Remove(DirectionalBox);
            Controls.Add(buttonNew);
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            // created the bouncing ball area
            Graphics bouncingballarea = e.Graphics;
            bouncingballarea.DrawRectangle(Pens.Black, from_x, from_y, maxdimensionforx, maxdimensionfory);
            Graphics ball = e.Graphics;
            ball.FillEllipse(Brushes.Red, (int)startingpositionx - 5, (int)startingpositiony - 5, radius, radius);
            if (isOn)
            {
                StartingGraphicClock(GraphicSpeed); // pull value from the new function
                StartingBallClock(BallSpeed); // pull value from the new functon
                StartingXYClock();
            }

            base.OnPaint(e);
        }
        protected void StartingGraphicClock(double update_graphic_speed)
        {
            GraphicClock.Interval = Convert.ToInt32((1000.0) / update_graphic_speed);
            GraphicClock.Enabled = true;
        }
        protected void StartingBallClock(double update_ball_speed)
        {
            if (update_ball_speed < 1.0)
            {
                update_ball_speed = 1.0;
            }
            BallClock.Interval = Convert.ToInt32((1000.0 / update_ball_speed)); // 1000.0 because it is in milliseconds. 1 Second / 60 refrehes a second. Sets interval to 60.
            BallClock.Enabled = true;
        }
        protected void StartingXYClock()
        {
            TextClock.Interval = 1.0;
            TextClock.Enabled = true;
        }
        protected void UpdateBallPosition(object sender, EventArgs myevent)
        {
            if (!isOn) { isOn = true; }
            startingpositionx += ball_deltax;
            startingpositiony -= ball_deltay;
            if ((int)Math.Round(startingpositionx + 5) >= rightsidehand_x)
            {
                ball_deltax = -ball_deltax;
            }
            if ((int)Math.Round(startingpositionx - 5) <= leftsidehand_x)
            {
                ball_deltax = -ball_deltax;
            }
            if ((int)Math.Round(startingpositiony + 5) <= lowerwall_y)
            {
                ball_deltay = -ball_deltay;
            }
            if ((int)Math.Round(startingpositiony - 5) >= upperwall_y)
            {
                ball_deltay = -ball_deltay;
            }
            Invalidate();
        }
        protected void UpdateXYClock(object sender, EventArgs myevent)
        {
            viewcoordinateX.Text = GetTextX(); //will retreive the text from the get function, and update the text box accordingly.
            viewcoordinateY.Text = GetTextY();
        }
        protected void EndApplication(object sender, EventArgs myevent) => Close();
    }
}


