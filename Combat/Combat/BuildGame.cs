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
        public int gameTypeToBuild { get; set; }

        private Tank playerTank;
        private Tank otherTank;
        private Tank playerTankPartTwo;
        private Tank otherTankPartTwo;
        private List<Bar> barPlayer;
        private List<Bar> barOther;
        private List<Bullets> playerBullets;
        private List<Bullets> otherBullets;
        private List<ExteriorWalls> exteriorWalls;
        private List<InteriorWalls> interiorWalls;
        private List<IDrawable> drawables;
        private List<ICollidable> collidables;
        private List<IDrawable> drawableTwo;
        private bool isShooting;
        private bool gameOver;

        private Gamepad controller;

        public BuildGame()
        {
            drawables = new List<IDrawable>();
            drawableTwo = new List<IDrawable>();
            exteriorWalls = new List<ExteriorWalls>();
            interiorWalls = new List<InteriorWalls>();
            collidables = new List<ICollidable>();
            playerTank = new Tank(30, 30, 60, 60, 90, 90, Colors.Black);
            otherTank = new Tank(300, 300, 60, 60, 90, 90, Colors.Blue);


            playerTankPartTwo = new Tank(playerTank.X, playerTank.Y + 20, playerTank.Height + 50, playerTank.Width - 40, 90, 90, playerTank.Colors);
            otherTankPartTwo = new Tank(otherTank.X - 50, otherTank.Y + 20, otherTank.Height + 50, otherTank.Width - 40, 90, 90, otherTank.Colors);

            //Looking at the instance of the bullets
            playerBullets = new List<Bullets>();
            otherBullets = new List<Bullets>();

            barPlayer = new List<Bar>();
            barOther = new List<Bar>();

            //var barPlayerPieceOne = new Bar(5, 5, 10, 10, Colors.Black);

            //Boundary of game
            var outsideWall = new ExteriorWalls(20, 20, 1000, 700, Colors.Black);
            var fillInWall = new InteriorWalls(outsideWall.X, outsideWall.Y, outsideWall.Height, outsideWall.Width, Colors.GreenYellow);
            var insideWallLeftSide = new InteriorWalls(100, 100, 100, 50, Colors.Black);
            // var insideWallRightSide = new InteriorWalls(100, 50, 50, 100, Colors.Black);

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

            drawableTwo.Add(playerTank);
            drawableTwo.Add(otherTank);

            drawables.Add(playerTankPartTwo);
            drawables.Add(otherTankPartTwo);

            CreateHealth();
        }


        //Messing with Health bars
        public void CreateHealth()
        {
            var barValuePlayerOnePartOne = new Bar(20, 5, 10, 10, Colors.Black);
            var barValuePlayerOnePartTwo = new Bar(30, 5, 10, 10, Colors.Black);
            var barValuePlayerOnePartThree = new Bar(40, 5, 10, 10, Colors.Black);
            var barValuePlayerOnePartFour = new Bar(50, 5, 10, 10, Colors.Black);
            var barValuePlayerOnePartFive = new Bar(60, 5, 10, 10, Colors.Black);

            barPlayer.Add(barValuePlayerOnePartFive);
            barPlayer.Add(barValuePlayerOnePartFour);
            barPlayer.Add(barValuePlayerOnePartThree);
            barPlayer.Add(barValuePlayerOnePartTwo);
            barPlayer.Add(barValuePlayerOnePartOne);

            drawables.Add(barValuePlayerOnePartFive);
            drawables.Add(barValuePlayerOnePartFour);
            drawables.Add(barValuePlayerOnePartThree);
            drawables.Add(barValuePlayerOnePartTwo);
            drawables.Add(barValuePlayerOnePartOne);
        }

        public void Update()
        {
            if (Gamepad.Gamepads.Count > 0)
            {
                controller = Gamepad.Gamepads.First();

                var controls = controller.GetCurrentReading();

                gameOver = false;

                if (!gameOver)
                {
                    if (!isShooting)
                    {
                        //Trying to have a variable of game buton and link it to the button A

                        playerTank.X += (int)(controls.LeftThumbstickX * 5);
                        playerTank.Y += (int)(controls.LeftThumbstickY * -5);

                        playerTankPartTwo.X += (int)(controls.LeftThumbstickX * 5);
                        playerTankPartTwo.Y += (int)(controls.LeftThumbstickY * -5);

                        //Messing with angular movement
                        playerTank.AngleX += (int)(controls.RightThumbstickX + (Math.Cos(30) * 1));
                        playerTank.AngleY -= (int)(controls.RightThumbstickY + (Math.Sin(30) * 1));

                        otherTank.X += (int)(controls.LeftThumbstickX * 5);
                        otherTank.Y += (int)(controls.LeftThumbstickY * -5);

                        otherTankPartTwo.X += (int)(controls.LeftThumbstickX * 5);
                        otherTankPartTwo.Y += (int)(controls.LeftThumbstickY * -5);
                    }

                    //Removing the entire health bar not each square
                    if (controls.Buttons.HasFlag(GamepadButtons.Y))
                    { 
                        bool hit = true;

                        foreach (var health in barPlayer.ToList())
                        {     
                            if (hit == true)
                            {
                                barPlayer.Remove(health);
                                drawables.Remove(health);
                                hit = false;
                            }
                        }
                    }

                    //Debug mode does not like the A button....using B button for bullet firing
                    if (controls.Buttons.HasFlag(GamepadButtons.B))
                    {
                        //Looking at the instance of the bullets
                        var playerBullet = new Bullets(playerTank.X + 65, playerTank.Y + 25, 10, 10, Colors.Blue);
                        var otherBullet = new Bullets(otherTank.X - 30, otherTank.Y + 25, 10, 10, Colors.Orange);

                        otherBullet.TravelingLeftWard = true;

                        //Include traveling before adding to list
                        playerBullets.Add(playerBullet);
                        otherBullets.Add(otherBullet);

                        drawables.Add(playerBullet);
                        drawables.Add(otherBullet);

                        isShooting = true;
                    }
                    else
                    {
                        isShooting = false;
                    }

                    //Movement update for bullets
                    foreach (var player in playerBullets)
                    {
                        player.Update();
                    }

                    foreach (var other in otherBullets)
                    {
                        other.Update();
                    }

                    if (barPlayer.Count == 0)
                    {
                        gameOver = true;
                    }
                }
                else
                {
                    
                }

                //Testing if bullet collides with wall
                //Issue might arise since foreach can't be used such as "var bullets in player bullets", and if one bullet collides, all of the players bullets might be erased.
                /*foreach (var wall in exteriorWalls)
                {
                    if (wall.Collides(playerBullets.X, playerBullets.Y, 0, 0))
                    {
                        playerBullets.Width = 0;

                    }
                }*/

                //bool isButtonPressed;
            }
        }

        //Draw the game
        public void DrawGame(CanvasDrawingSession canvas)
        {
            //If we want game one, build it like this
            if (gameTypeToBuild == 0)
            {
                foreach (var drawable in drawables)
                {
                    drawable.Draw(canvas);
                }
            }

            //If we want game two, build it like this
            else if (gameTypeToBuild == 1)
            {
                foreach (var drawables in drawableTwo)
                {
                    drawables.Draw(canvas);
                }
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
                throw new NotImplementedException();
            }

            public void Draw(CanvasDrawingSession canvas)
            {
                canvas.FillRectangle(X, Y, Height, Width, Colors);

               // canvas.FillRectangle(X, Y + 20, Height + 50, Width - 40, Colors);

               // canvas.FillCircle(X + 20, Y + 30, Height - 10, Colors);

               // canvas.FillRectangle(X, Y - 20, Height, Width, Colors);
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
                // Want to test if either the X or Y intersect exactly
                return (x == X && x <= Width) || (y == Y && y <= Height);
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
                return (x == X && x <= Width) || (y == Y && y <= Height);
            }

            public void Draw(CanvasDrawingSession canvas)
            {
                canvas.FillRectangle(X, Y, Height, Width, Color);
            }
        }


        //This is the score keeper for the character
        public class Bar : IDrawable
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
            public Color Color { get; set; }

            public Bar(int x, int y, int height, int width, Color color)
            {
                X = x;
                Y = y;
                Height = height;
                Width = width;
                Color = color;
            }

            public void Draw(CanvasDrawingSession canvas)
            {
                canvas.FillRectangle(X, Y, Height, Width, Color);
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
                //return x >= X && x <= Width && y >= Y && y <= Height;// Original return statement
                return (x  <= X || x <= Width) && (y >= Y || y <= Height); //Test statement
            }

            public void Draw(CanvasDrawingSession canvas)
            {
                canvas.FillRectangle(X, Y, Height, Width, Color);
            }
        }

    }
}
