/********************************************************
 * Author: Chukwudi Ikem
 * Email: godofcollege43@csu.fullerton.edu
 * Course: CPSC 223N
 * Semester: Fall 2019
 * Assignment #: 4
 * Program name: BouncingBallScreen
*****************************************************/
using System;
using System.Windows.Forms;
using System.Drawing;
namespace BouncingBallScreen
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Blistering Biomes!");
            Application.Run(new BouncingBallUI());
        }
    }
}
