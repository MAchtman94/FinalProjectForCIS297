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
        private List<Bullets> playerBullets;
        private List<Bullets> otherBullets;
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
            playerTank = new Tank(30, 30, 60, 60, 90, 90, Colors.Black);
            otherTank = new Tank(300, 300, 60, 60, 90, 90, Colors.Blue);
            pointsPlayer = new Points();
            pointsOther = new Points();

            //Looking at the instance of the bullets
            playerBullets = new List<Bullets>();
            otherBullets = new List<Bullets>();

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

                //Messing with angular movement
                playerTank.AngleX += (int)(controls.RightThumbstickX + (Math.Cos(30) * 1));
                playerTank.AngleY -= (int)(controls.RightThumbstickY + (Math.Sin(30) * 1));

                otherTank.X += (int)(controls.LeftThumbstickX * 5);
                otherTank.Y += (int)(controls.LeftThumbstickY * -5);


                //Debug mode does not like the A button....using B button for bullet firing

                if (controls.Buttons.HasFlag(GamepadButtons.B))
                {
                    playerTank.X += (int)(controls.LeftThumbstickX * 0);
                    playerTank.Y += (int)(controls.LeftThumbstickY * 0);

                    //Looking at the instance of the bullets
                    var playerBullet = new Bullets(this.playerTank.X + 65, this.playerTank.Y + 25, 10, 10, Colors.Blue);
                    var otherBullet = new Bullets(this.otherTank.X + 65, this.otherTank.Y + 25, 10, 10, Colors.Orange);

                    //Include traveling before adding to list
                    playerBullets.Add(playerBullet);
                    otherBullets.Add(otherBullet);

                    drawables.Add(playerBullet);
                    drawables.Add(otherBullet);
                }

                //Movement update for bullets
                foreach(var player in playerBullets)
                {
                    player.Update();
                }

                foreach(var other in otherBullets)
                {
                    other.Update();
                }

                //Testing if bullet collides with walls, internal and external
                //Issue might arise since foreach can't be used such as "var bullets in player bullets", and if one bullet collides, all of the players bullets might be erased.
                foreach (var bullet in playerBullets)
                {
                    foreach(var inWall in interiorWalls)
                    {
                        if (inWall.Collides(bullet.X, bullet.Y, inWall.Height,inWall.Width))
                        {
                            bullet.removeBullet(bullet);
                        }
                    }
                    foreach (var exWall in exteriorWalls)
                    {
                        if (exWall.Collides(bullet.X, bullet.Y, exWall.Height, exWall.Width))
                        {
                            bullet.removeBullet(bullet);
                        }
                    }
                    if (bullet.Collides(otherTank.X, otherTank.Y, bullet.Height,bullet.Width))
                    {
                        bullet.removeBullet(bullet);
                    }
                }
                foreach (var bullet in otherBullets)
                {
                    foreach (var inWall in interiorWalls)
                    {
                        if (inWall.Collides(bullet.X, bullet.Y, inWall.Height, inWall.Width))
                        {
                            bullet.removeBullet(bullet);
                        }
                    }
                    foreach (var exWall in exteriorWalls)
                    {
                        if (exWall.Collides(bullet.X, bullet.Y, exWall.Height, exWall.Width))
                        {
                            bullet.removeBullet(bullet);
                        }
                    }
                    if (bullet.Collides(playerTank.X, playerTank.Y, bullet.Height, bullet.Width))
                    {
                        bullet.removeBullet(bullet);
                    }
                }
                // ----> Work in progress: Crude statement to try and lock the players so long as they hit each other; they can't pass through one another. Mainly a placeholder until testing.
                if (playerTank.Collides(otherTank.X, otherTank.Y, playerTank.Height, playerTank.Width) || otherTank.Collides(playerTank.X, playerTank.X, playerTank.Height, playerTank.Width))
                {
                    playerTank.X = playerTank.X;
                    playerTank.Y = playerTank.Y;

                    otherTank.X = otherTank.X;
                    otherTank.Y = otherTank.Y;
                }
                
                //bool isButtonPressed;
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
            public int AngleX { get; set; }
            public int AngleY { get; set; }
            public Color Colors { get; set; }

            //Determine front of tank position
            bool TankUpward { get; set; }
            bool TankDownward { get; set; }
            bool TankLeftward { get; set; }
            bool TankRightward { get; set; }

            public Tank(int x, int y, int height, int width, int angleX, int angleY, Color color)
            {
                X = x;
                Y = y;
                Height = height;
                Width = width;
                AngleX = angleX;
                AngleY = angleY;
                Colors = color;
            }

            public bool Collides(int x, int y, int height, int width)
            {
                return x >= X && x <= Width && y >= Y && y <= Height;
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
                return (x <= X || x <= Width) && (y >= Y || y >= Height);
               
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
                // Same idea/concept of exterior wall collision function

                return x >= X && x <= Width && y >= Y && y <= Height;

            }

            public void Draw(CanvasDrawingSession canvas)
            {
                canvas.FillRectangle(X, Y, Height, Width, Color);
            }
        }


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

            public void removeBullet(Bullets bullet)
            {
                bullet.Width = 0;
                bullet.Height = 0;
                bullet.X = -10;
                bullet.Y = -10;
             
                
            }
            public bool Collides(int x, int y, int height, int width)
            {
                //return (x >= X && x <= Width) && (y >= Y && y <= Height);// Original return statement
              
                return (x  <= X || x <= Width) && (y >= Y || y <= Height); //Test statement
            }

            public void Draw(CanvasDrawingSession canvas)
            {
                canvas.FillRectangle(X, Y, Height, Width, Color);
            }
        }

    }
}
