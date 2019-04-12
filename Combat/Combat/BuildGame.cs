﻿using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace Combat
{
    public class BuildGame
    {
        private Tank playerTank;
        private Tank otherTank;
        private Points pointsPlayer;
        private Points pointsOther;
        private Bullets playerBullets;
        private Bullets otherBullets;
        private List<Walls> walls;
        private List<IDrawable> drawables;
        private List<ICollidable> collidables;

        public BuildGame()
        {
            drawables = new List<IDrawable>();
            walls = new List<Walls>();
            collidables = new List<ICollidable>();
            playerTank = new Tank(30, 30, 60, 60, Colors.Black);
            otherTank = new Tank(300, 300, 60, 60, Colors.Blue);
            pointsPlayer = new Points();
            pointsOther = new Points();
            playerBullets = new Bullets(playerTank.X + 65, playerTank.Y + 25, 5, 5, Colors.Blue);

            //Automatically placing the bullets to move towards the right on X-Axis
            playerBullets.TravelingLeftWard = false;
            playerBullets.TravelingUpward = false;
            playerBullets.TravelingDownward = false;

            otherBullets = new Bullets(otherTank.X + 65, otherTank.Y + 25, 5, 5, Colors.Orange);

            //Automatically placing the bullets to move towards the left on X-Axis
            otherBullets.TravelingLeftWard = false;
            otherBullets.TravelingUpward = false;
            otherBullets.TravelingDownward = false;

            //Boundary of game
            var outsideWall = new Walls(10,10,1000,700, Colors.Black);

            //Bullets, need to be animated to move along X-Axis

            //Adding outside wall
            drawables.Add(outsideWall);
            walls.Add(outsideWall);

            //Adding bullets
            drawables.Add(playerBullets);
            drawables.Add(otherBullets);



            drawables.Add(playerTank);
            drawables.Add(otherTank);
        }

        public void Update()
        {
            playerBullets.Update();
            otherBullets.Update();
        }

        public void DrawGame(CanvasDrawingSession canvas)
        {
            foreach (var drawable in drawables)
            {
                drawable.Draw(canvas);
            }
        }

        public interface IDrawable
        {
            void Draw(CanvasDrawingSession canvas);
        }

        public interface ICollidable
        {
            bool Collides(int x, int y, int height, int width);
        }

        //Character player
        public class Tank : ICollidable, IDrawable
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
            public Color Colors { get; set; }

            //Determine front of tank position
            bool TankUpward { get; set; }
            bool TankDownward { get; set; }
            bool TankLeftward { get; set; }
            bool TankRightward { get; set; }

            public Tank(int x, int y, int height, int width, Color color)
            {
                X = x;
                Y = y;
                Height = height;
                Width = width;
                Colors = color;
            }

            public bool Collides(int x, int y, int height, int width)
            {
                throw new NotImplementedException();
            }

            public void Draw(CanvasDrawingSession canvas)
            {
                canvas.FillRectangle(X, Y, Height, Width, Colors);

                canvas.FillRectangle(X, Y + 20, Height + 50, Width - 40, Colors);
            }
        }

        public class Plane : IDrawable
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }

            public Plane(int x, int y, int height, int width)
            {
                X = x;
                Y = y;
                Height = height;
                Width = width;
            }

            public void Draw(CanvasDrawingSession canvas)
            {
                throw new NotImplementedException();
            }
        }

        public class Walls : ICollidable, IDrawable
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
            public Color Color { get; set; }

            public Walls(int x, int y, int height, int width, Color color)
            {
                X = x;
                Y = y;
                Height = height;
                Width = width;
                Color = color;
            }

            public bool Collides(int x, int y, int height, int width)
            {
                throw new NotImplementedException();
            }

            public void Draw(CanvasDrawingSession canvas)
            {
                canvas.DrawRectangle(X, Y, Height, Width, Color);
            }
        }

        /* public class WallsBoundary : ICollidable, IDrawable
         {

         } */

        //This is the score keeper for the character
        public class Points : IDrawable
        {
            public void Draw(CanvasDrawingSession canvas)
            {
                throw new NotImplementedException();
            }
        }

        public class Bullets : IDrawable, ICollidable
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
            public Color Color { get; set; }

            public bool TravelingDownward { get; set; }
            public bool TravelingLeftWard { get; set; }
            public bool TravelingUpward { get; set; }

            //Looking at a diagonal view, only use in Y-Axis change views
            public bool DiagnolTravelRight { get; set; }
            public bool DiagnolTravelLeft { get; set; }

            public Bullets(int x, int y, int height, int width, Color color)
            {
                X = x;
                Y = y;
                Height = height;
                Width = width;
                Color = color;
            }

            public void Update()
            {
                //Determine what position user is going.  Bullets can only go in one direction
                if (TravelingDownward)
                {
                    if (DiagnolTravelLeft)
                    {
                        Y += 1;
                        X -= 1;
                    }
                    else if (DiagnolTravelRight)
                    {
                        Y += 1;
                        X += 1;
                    }
                    Y += 1;
                }
                else if (TravelingUpward)
                {
                    if (DiagnolTravelLeft)
                    {
                        Y -= 1;
                        X -= 1;
       
                    }
                    else if (DiagnolTravelRight)
                    {
                        Y -= 1;
                        X += 1;
                    }
                    Y -= 1;
                }
                else if (TravelingLeftWard)
                {
                    X -= 1;
                }
                else
                {
                    X += 1;
                }
            }

            public bool Collides(int x, int y, int height, int width)
            {
                return x >= X && x <= Width && y >= Y && y <= Height;
            }

            public void Draw(CanvasDrawingSession canvas)
            {
                canvas.FillRectangle(X, Y, Height, Width, Color);
            }
        }

    }
}
