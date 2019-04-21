using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

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
        private List<ExteriorWalls> exteriorWalls;
        private List<InteriorWalls> interiorWalls;
        private List<IDrawable> drawables;
        private List<ICollidable> collidables;

                

        //private bool gameOver;

        private Gamepad controller;

        public BuildGame()
        {
            drawables = new List<IDrawable>();
            exteriorWalls = new List<ExteriorWalls>();
            interiorWalls = new List<InteriorWalls>();
            collidables = new List<ICollidable>();
            playerTank = new Tank(30, 30, 60, 60, Colors.Black);
            otherTank = new Tank(300, 300, 60, 60, Colors.Blue);
            pointsPlayer = new Points();
            pointsOther = new Points();

            //Looking at the instance of the bullets
            playerBullets = new Bullets(0,0,0,0, Colors.AliceBlue);
            otherBullets = new Bullets(0,0,0,0, Colors.Orange);

            //Automatically placing the bullets to move towards the right on X-Axis
            playerBullets.TravelingLeftWard = false;
            playerBullets.TravelingUpward = false;
            playerBullets.TravelingDownward = false;

            //Automatically placing the bullets to move towards the left on X-Axis
            otherBullets.TravelingLeftWard = false;
            otherBullets.TravelingUpward = false;
            otherBullets.TravelingDownward = false;

            //Boundary of game
            var outsideWall = new ExteriorWalls(10, 10, 1000, 700, Colors.Black);
            var fillInWall = new InteriorWalls(outsideWall.X, outsideWall.Y, outsideWall.Height, outsideWall.Width, Colors.GreenYellow);
            var insideWallLeftSide = new InteriorWalls(100, 100, 100, 50, Colors.Black);
            // var insideWallRightSide = new InteriorWalls(100, 50, 50, 100, Colors.Black);

            //Bullets, need to be animated to move along X-Axis

            //Adding outside wall
            drawables.Add(outsideWall);
            exteriorWalls.Add(outsideWall);
            drawables.Add(fillInWall);
            interiorWalls.Add(fillInWall);
            drawables.Add(insideWallLeftSide);
            interiorWalls.Add(insideWallLeftSide);
            // drawables.Add(insideWallRightSide);
            // interiorWalls.Add(insideWallRightSide);

            drawables.Add(playerTank);
            drawables.Add(otherTank);
        }

        public void Update()
        {
            if (Gamepad.Gamepads.Count > 0)
            {
                controller = Gamepad.Gamepads.First();

                var controls = controller.GetCurrentReading();

                //Trying to have a variable of game buton and link it to the button A

                playerTank.X += (int)(controls.LeftThumbstickX * 5);
                playerTank.Y += (int)(controls.LeftThumbstickY * -5);

                //Want to look into rotational movement.....
                //playerTank.X += (int)(controls.RightThumbstickX * 5);

                otherTank.X += (int)(controls.LeftThumbstickX * 5);
                otherTank.Y += (int)(controls.LeftThumbstickY * -5);

                //Debug mode does not like the A button....using B button for bullet firing

                if (controls.Buttons.HasFlag(GamepadButtons.B))
                {
                    //Looking at the instance of the bullets
                    playerBullets = new Bullets(playerTank.X + 65, playerTank.Y + 25, 10, 10, Colors.Blue);
                    otherBullets = new Bullets(otherTank.X + 65, otherTank.Y + 25, 10, 10, Colors.Orange);

                    drawables.Add(playerBullets);
                    drawables.Add(otherBullets);
                }

                playerBullets.Update();
                otherBullets.Update();
            }
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

        public class ExteriorWalls : ICollidable, IDrawable
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
            public Color Color { get; set; }

            public ExteriorWalls(int x, int y, int height, int width, Color color)
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

        public class InteriorWalls : ICollidable, IDrawable
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
            public Color Color { get; set; }

            public InteriorWalls(int x, int y, int height, int width, Color color)
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
                canvas.FillRectangle(X, Y, Height, Width, Color);
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
