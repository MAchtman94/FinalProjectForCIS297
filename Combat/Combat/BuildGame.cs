using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using System.Numerics;
using Microsoft.Graphics.Canvas.Geometry;
//using TiledSharp;

namespace Combat
{
    public class BuildGame
    {
        public int gameTypeToBuild { get; set; }
        public string gameOverText { get; set; }

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
        private bool isShooting;
        private bool gameOver;
        bool hitPlayer;
        bool hitOther;
        //public int sound { get; set; }

        private Gamepad controller;

        public BuildGame()
        {
            drawables = new List<IDrawable>();
            exteriorWalls = new List<ExteriorWalls>();
            interiorWalls = new List<InteriorWalls>();
            collidables = new List<ICollidable>();

            //Looking at the instance of the bullets
            playerBullets = new List<Bullets>();
            otherBullets = new List<Bullets>();

            barPlayer = new List<Bar>();
            barOther = new List<Bar>();

            playerTank = new Tank(30, 300, 60, 60, 90, 90, Colors.Orange);
            playerTankPartTwo = new Tank(playerTank.X, playerTank.Y + 20, playerTank.Height + 50, playerTank.Width - 40, 90, 90, playerTank.Colors);

            otherTank = new Tank(930, 300, 60, 60, 90, 90, Colors.Blue);
            otherTankPartTwo = new Tank(otherTank.X - 50, otherTank.Y + 20, otherTank.Height + 50, otherTank.Width - 40, 90, 90, otherTank.Colors);

            //Boundary of game

            //var insideWallLeftSide = new InteriorWalls(100, 100, 100, 50, Colors.Black);
            // var insideWallRightSide = new InteriorWalls(100, 50, 50, 100, Colors.Black);

            //Adding outside wall

            //interiorWalls.Add(fillInWall);
            //drawables.Add(insideWallLeftSide);
            //interiorWalls.Add(insideWallLeftSide);
            // drawables.Add(insideWallRightSide);
            // interiorWalls.Add(insideWallRightSide);
            //sound = 0;

            CreateHealth();
            CreateHealthForOther();
        }

        //Creating player one health bar
        public void CreateHealth()
        {
            var barValuePlayerOnePartOne = new Bar(20, 5, 10, 10, playerTank.Colors);
            var barValuePlayerOnePartTwo = new Bar(30, 5, 10, 10, playerTank.Colors);
            var barValuePlayerOnePartThree = new Bar(40, 5, 10, 10, playerTank.Colors);
            var barValuePlayerOnePartFour = new Bar(50, 5, 10, 10, playerTank.Colors);
            var barValuePlayerOnePartFive = new Bar(60, 5, 10, 10, playerTank.Colors);

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

		//Creating player two health bar
        public void CreateHealthForOther()
        {
            var barValueOtherOnePartOne = new Bar(940, 5, 10, 10, otherTank.Colors);
            var barValueOtherOnePartTwo = new Bar(950, 5, 10, 10, otherTank.Colors);
            var barValueOtherOnePartThree = new Bar(960, 5, 10, 10, otherTank.Colors);
            var barValueOtherOnePartFour = new Bar(970, 5, 10, 10, otherTank.Colors);
            var barValueOtherOnePartFive = new Bar(980, 5, 10, 10, otherTank.Colors);

            barOther.Add(barValueOtherOnePartOne);
            barOther.Add(barValueOtherOnePartTwo);
            barOther.Add(barValueOtherOnePartThree);
            barOther.Add(barValueOtherOnePartFour);
            barOther.Add(barValueOtherOnePartFive);

            drawables.Add(barValueOtherOnePartOne);
            drawables.Add(barValueOtherOnePartTwo);
            drawables.Add(barValueOtherOnePartThree);
            drawables.Add(barValueOtherOnePartFour);
            drawables.Add(barValueOtherOnePartFive);
        }

        

        
		public void KeyDown(char input)
        {
            switch(input)
            {
                case 'W':
                    playerTank.IsUp = true;
                    playerTank.IsDown = false;
                    break;
                case 'A':
                    playerTank.IsLeft = true;
                    playerTank.IsRight = false;
                    break;
                case 'S':
                    playerTank.IsUp = false;
                    playerTank.IsDown = true;
                    break;
                case 'D':
                    playerTank.IsLeft = false;
                    playerTank.IsRight = true;
                    break;
                case 'V': playerTank.TankIsShooting = true;
                    break;

                case 'I':
                    otherTank.IsUp = true;
                    otherTank.IsDown = false;
                    break;
                case 'J':
                    otherTank.IsLeft = true;
                    otherTank.IsRight = false;
                    break;
                case 'K':
                    otherTank.IsUp = false;
                    otherTank.IsDown = true;
                    break;
                case 'L':
                    otherTank.IsLeft = false;
                    otherTank.IsRight = true;
                    break;
                case 'B':
                    otherTank.TankIsShooting = true;
                    break;
            }
        }

        public void KeyUp(char input)
        {
            switch (input)
            {
                case 'W':
                    playerTank.IsUp = false;
                    break;
                case 'A':
                    playerTank.IsLeft = false;
                    break;
                case 'S':
                    playerTank.IsDown = false;
                    break;
                case 'D':
                    playerTank.IsRight = false;
                    break;

                case 'I':
                    otherTank.IsUp = false;
                    break;
                case 'J':
                    otherTank.IsLeft = false;
                    break;
                case 'K':
                    otherTank.IsDown = false;
                    break;
                case 'L':
                    otherTank.IsRight = false;
                    break;
            }
        }
        //Draw the game
        public void DrawGame(CanvasDrawingSession canvas)
        {
            //If we want game one, build it like this
            if (gameTypeToBuild == 1)
            {
                var outsideWall = new ExteriorWalls(20, 20, 1000, 700, Colors.Black);
                var fillInWall = new InteriorWalls(outsideWall.X, outsideWall.Y, outsideWall.Height, outsideWall.Width, Colors.GreenYellow, 1);

                drawables.Add(outsideWall);
                exteriorWalls.Add(outsideWall);
                drawables.Add(fillInWall);
                interiorWalls.Add(fillInWall);

                drawables.Add(playerTank);
                drawables.Add(otherTank);

                drawables.Add(playerTankPartTwo);
                drawables.Add(otherTankPartTwo); 

                foreach (var drawable in drawables)
                {
                    drawable.Draw(canvas);
                }
            }

            //If we want game two, build it like this
            else if (gameTypeToBuild == 2)
            {
                var outsideWall = new ExteriorWalls(20, 20, 1000, 700, Colors.Black);
                var fillInWall = new InteriorWalls(outsideWall.X, outsideWall.Y, outsideWall.Height, outsideWall.Width, Colors.GreenYellow, 2);

                drawables.Add(outsideWall);
                exteriorWalls.Add(outsideWall);
                drawables.Add(fillInWall);
                interiorWalls.Add(fillInWall);

                drawables.Add(playerTank);
                drawables.Add(otherTank);

                drawables.Add(playerTankPartTwo);
                drawables.Add(otherTankPartTwo); 

                foreach (var drawables in drawables)
                {
                    drawables.Draw(canvas);
                }
            }

            //If we want to build game three
            else
            {
                var outsideWall = new ExteriorWalls(20, 20, 1000, 700, Colors.Black);
                var fillInWall = new InteriorWalls(outsideWall.X, outsideWall.Y, outsideWall.Height, outsideWall.Width, Colors.GreenYellow, 3);

                drawables.Add(outsideWall);
                exteriorWalls.Add(outsideWall);
                drawables.Add(fillInWall);
                interiorWalls.Add(fillInWall);

                drawables.Add(playerTank);
                drawables.Add(otherTank);

                drawables.Add(playerTankPartTwo);
                drawables.Add(otherTankPartTwo);

                foreach (var drawables in drawables)
                {
                    drawables.Draw(canvas);
                }
            }
        }

        public void Update()
        {
            //********************GAMEPAD CONTROL**************************/
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

                    //Removing health bar behavior
                    
                        foreach (var health in barPlayer.ToList())
                        {
                            //Only the first instance in the list will be removed
                            if (hitPlayer == true)
                            {
                                barPlayer.Remove(health);
                                drawables.Remove(health);
                                hitPlayer = false;
                            }
                        }
                    
                    //Removing health bar for other player
                    
                       

                        foreach (var health in barOther.ToList())
                        {
                            if (hitOther == true)
                            {
                                barOther.Remove(health);
                                drawables.Remove(health);
                                hitOther = false;
                            }
                        }
                    

                    //Debug mode does not like the A button....using B button for bullet firing
                    if (controls.Buttons.HasFlag(GamepadButtons.B))
                    {
                        //Looking at the instance of the bullets
                        var playerBullet = new Bullets(playerTank.X + 65, playerTank.Y + 25, 10, 10, playerTank.Colors);
                        var otherBullet = new Bullets(otherTank.X - 30, otherTank.Y + 25, 10, 10, otherTank.Colors);

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

                    if (barPlayer.Count == 0 || barOther.Count == 0)
                    {
                        gameOver = true;
                    }

                    if (gameOver)
                    {
                        return;
                    }
                }

                //----------------------Collision-----------------------
                foreach (var bullet in playerBullets)
                {
                    
                  /*  foreach (var inWall in interiorWalls)
                    {
                        if (bullet.inWallCollides(inWall.X, inWall.Y, inWall.Height, inWall.Width))
                        {
                            bullet.removeBullet(bullet);
                        }
                    } */
                    
                    foreach (var exWall in exteriorWalls)
                    {
                        if (exWall.Collides(bullet.X, bullet.Y, exWall.Height, exWall.Width))
                        {
                            bullet.removeBullet(bullet);
                            
                        }
                    }
                    
                    if (bullet.tankCollides(otherTank.X, otherTank.Y, otherTank.Height, otherTank.Width) || bullet.tankCollides(otherTankPartTwo.X, otherTankPartTwo.Y, otherTankPartTwo.Height, otherTankPartTwo.Width))
                    {
                        hitOther = true;
                        bullet.removeBullet(bullet);
                        
                    }
                    
                }
               
                foreach (var bullet in otherBullets)
                {
                    /*
                    foreach (var inWall in interiorWalls)
                    {
                        if (inWall.Collides(bullet.X, bullet.Y, inWall.Height, inWall.Width))
                        {
                            bullet.removeBullet(bullet);
                        }
                    }
                     */
                    foreach (var exWall in exteriorWalls)
                    {
                        if (exWall.Collides(bullet.X, bullet.Y, exWall.Height, exWall.Width))
                        {
                            bullet.removeBullet(bullet);
                        }
                    }
                 
                    
                    if (bullet.tankCollides(playerTank.X, playerTank.Y, playerTank.Height, playerTank.Width) || bullet.tankCollides(playerTankPartTwo.X, playerTankPartTwo.Y, playerTankPartTwo.Height, playerTankPartTwo.Width))
                    {
                        hitPlayer = true;
                        bullet.removeBullet(bullet);
                    }
                    
                }
            foreach (var wall in exteriorWalls)
            {
                    if (playerTank.CollidesTop() || playerTankPartTwo.CollidesTop())
                    {
                        playerTank.Y = playerTank.Y + 15;
                        playerTankPartTwo.Y = playerTankPartTwo.Y + 15;
                    }
                    if (playerTank.CollidesBottom() || playerTankPartTwo.CollidesBottom())
                    {
                        playerTank.Y = playerTank.Y - 15;
                        playerTankPartTwo.Y = playerTankPartTwo.Y - 15;
                    }
                    if (playerTank.CollidesLeft() || playerTankPartTwo.CollidesLeft())
                    {
                        playerTank.X = playerTank.X + 15;
                        playerTankPartTwo.X = playerTankPartTwo.X + 15;
                    }
                    if (playerTank.CollidesRight() || playerTankPartTwo.CollidesRight())
                    {
                        playerTank.X = playerTank.X - 15;
                        playerTankPartTwo.X = playerTankPartTwo.X - 15;
                    }
                }
            foreach (var wall in exteriorWalls)
            {
                if (otherTank.CollidesTop() || otherTankPartTwo.CollidesTop())
                {
                    otherTank.Y = otherTank.Y + 15;
                    otherTankPartTwo.Y = otherTankPartTwo.Y + 15;
                }
                if (otherTank.CollidesBottom() || otherTankPartTwo.CollidesBottom())
                {
                    otherTank.Y = otherTank.Y - 15;
                    otherTankPartTwo.Y = otherTankPartTwo.Y - 15;
                }
                if (otherTank.CollidesLeft() || otherTankPartTwo.CollidesLeft())
                {
                    otherTank.X = otherTank.X + 15;
                    otherTankPartTwo.X = otherTankPartTwo.X + 15;
                }
                if (otherTank.CollidesRight() || otherTankPartTwo.CollidesRight())
                {
                    otherTank.X = otherTank.X - 15;
                    otherTankPartTwo.X = otherTankPartTwo.X - 15;
                }
            }


            /*
            if (playerTank.Collides(otherTank.X, otherTank.Y, playerTank.Height, playerTank.Width) || otherTank.Collides(playerTank.X, playerTank.X, playerTank.Height, playerTank.Width))
                {
                    playerTank.X = playerTank.X - 15;
                    playerTank.Y = playerTank.Y -15;

                    otherTank.X = otherTank.X - 15;
                    otherTank.Y = otherTank.Y + 15;
                }
                */
                
                //----------------------^^^Collision^^^--------------------------------

                //bool isButtonPressed;
            }

            //***************KEYBOARD CONTROL**************/
            else
            {
                gameOver = false;

                if (!gameOver)
                {
                    if (!isShooting)
                    {
                        //Player Tank
                        if(playerTank.IsUp == true)
                        {
                            playerTank.Y -= 5;
                            playerTankPartTwo.Y -= 5;
                        }
                        if (playerTank.IsLeft == true)
                        {
                            playerTank.X -= 5;
                            playerTankPartTwo.X -= 5;
                        }
                        if (playerTank.IsDown == true)
                        {
                            playerTank.Y += 5;
                            playerTankPartTwo.Y += 5;
                        }
                        if (playerTank.IsRight == true)
                        {
                            playerTank.X += 5;
                            playerTankPartTwo.X += 5;
                        }

                        //Other Tank
                        if (otherTank.IsUp == true)
                        {
                            otherTank.Y -= 5;
                            otherTankPartTwo.Y -= 5;
                        }
                        if (otherTank.IsLeft == true)
                        {
                            otherTank.X -= 5;
                            otherTankPartTwo.X -= 5;
                        }
                        if (otherTank.IsDown == true)
                        {
                            otherTank.Y += 5;
                            otherTankPartTwo.Y += 5;
                        }
                        if (otherTank.IsRight == true)
                        {
                            otherTank.X += 5;
                            otherTankPartTwo.X += 5;
                        }
                    }
                    

                    if (playerTank.TankIsShooting == true)
                    {
                        if (!(playerBullets.Count() > 3))
                        {
                            //Looking at the instance of the bullets
                            var playerBullet = new Bullets(playerTank.X + 65, playerTank.Y + 25, 10, 10, playerTank.Colors);

                            if (playerTank.IsUp)
                            {
                                playerBullet.TravelingUpward = true;
                                if (playerTank.IsLeft) { playerBullet.DiagnolTravelLeft = true; }
                                if (playerTank.IsRight) { playerBullet.DiagnolTravelRight = true; }
                            }
                            else if (playerTank.IsDown)
                            {
                                playerBullet.TravelingDownward = true;
                                if (playerTank.IsLeft) { playerBullet.DiagnolTravelLeft = true; }
                                if (playerTank.IsRight) { playerBullet.DiagnolTravelRight = true; }
                            }
                            else if (playerTank.IsLeft) { playerBullet.TravelingLeftWard = true; }


                            //Include traveling before adding to list
                            playerBullets.Add(playerBullet);

                            drawables.Add(playerBullet);

                            playerTank.TankIsShooting = false;
                        }
                    }

                    if (otherTank.TankIsShooting == true)
                    {
                        if (!(otherBullets.Count() > 3))
                        {
                            //Looking at the instance of the bullets
                            var otherBullet = new Bullets(otherTank.X - 30, otherTank.Y + 25, 10, 10, otherTank.Colors);

                            if (otherTank.IsUp)
                            {
                                otherBullet.TravelingUpward = true;
                                if (otherTank.IsLeft) { otherBullet.DiagnolTravelLeft = true; }
                                if (otherTank.IsRight) { otherBullet.DiagnolTravelRight = true; }
                            }
                            else if (otherTank.IsDown)
                            {
                                otherBullet.TravelingDownward = true;
                                if (otherTank.IsLeft) { otherBullet.DiagnolTravelLeft = true; }
                                if (otherTank.IsRight) { otherBullet.DiagnolTravelRight = true; }
                            }
                            else if (otherTank.IsLeft) { otherBullet.TravelingLeftWard = true; }


                            //Include traveling before adding to list
                            otherBullets.Add(otherBullet);

                            drawables.Add(otherBullet);
                            otherTank.TankIsShooting = false;
                        }
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

                    if (barPlayer.Count == 0 || barOther.Count == 0)
                    {
                        gameOver = true;
                    }
                }
                List<Bullets> BulletsToRemove = new List<Bullets>();
                //----------------------Keyboard Collision--------------------------------
                foreach (var bullet in playerBullets)
                {
                  /*  foreach (var inWall in interiorWalls)
                    {
                        if (inWall.CollidesBullet(bullet))
                        {
                            BulletsToRemove.Add(bullet);
                        }
                    } */
                    foreach (var exWall in exteriorWalls)
                    {
                        if (exWall.CollidesBullet(bullet))
                        {
                            BulletsToRemove.Add(bullet);
                        }
                    }
                    
                    if (bullet.Collides(otherTank.X, otherTank.Y, bullet.Height, bullet.Width))
                    {
                        BulletsToRemove.Add(bullet);
                    }
                    
                }
                foreach(var rBullet in BulletsToRemove)
                {
                    playerBullets.Remove(rBullet);
                    drawables.Remove(rBullet);
                    rBullet.Dispose();
                }
                BulletsToRemove.Clear();
                
                 foreach (var bullet in otherBullets)
                 {
                     
                   /*  foreach (var inWall in interiorWalls)
                     {
                         if (inWall.CollidesBullet(bullet))
                         {
                            BulletsToRemove.Add(bullet);
                         }
                     } */

                     foreach (var exWall in exteriorWalls)
                     {
                         if (exWall.CollidesBullet(bullet))
                         {
                            BulletsToRemove.Add(bullet);
                         }
                     }
               
                
                    if (bullet.Collides(playerTank.X, playerTank.Y, bullet.Height, bullet.Width))
                    {
                        BulletsToRemove.Add(bullet);
                    }
                 }
                 foreach(var rBullet in BulletsToRemove)
                {
                    otherBullets.Remove(rBullet);
                    drawables.Remove(rBullet);
                    rBullet.Dispose();
                }
                BulletsToRemove.Clear();

                foreach (var wall in exteriorWalls)
                {
                    if (playerTank.CollidesTop() || playerTankPartTwo.CollidesTop())
                    {
                        playerTank.Y = playerTank.Y + 15;
                        playerTankPartTwo.Y = playerTankPartTwo.Y + 15;
                    }
                    if (playerTank.CollidesBottom() || playerTankPartTwo.CollidesBottom())
                    {
                        playerTank.Y = playerTank.Y - 15;
                        playerTankPartTwo.Y = playerTankPartTwo.Y - 15;
                    }
                    if (playerTank.CollidesLeft() || playerTankPartTwo.CollidesLeft())
                    {
                        playerTank.X = playerTank.X + 15;
                        playerTankPartTwo.X = playerTankPartTwo.X + 15;
                    }
                    if (playerTank.CollidesRight() || playerTankPartTwo.CollidesRight())
                    {
                        playerTank.X = playerTank.X - 15;
                        playerTankPartTwo.X = playerTankPartTwo.X - 15;
                    }
                }
                foreach (var wall in exteriorWalls)
                {
                    if (otherTank.CollidesTop() || otherTankPartTwo.CollidesTop())
                    {
                        otherTank.Y = otherTank.Y + 15;
                        otherTankPartTwo.Y = otherTankPartTwo.Y + 15;
                    }
                    if (otherTank.CollidesBottom() || otherTankPartTwo.CollidesBottom())
                    {
                        otherTank.Y = otherTank.Y - 15;
                        otherTankPartTwo.Y = otherTankPartTwo.Y - 15;
                    }
                    if (otherTank.CollidesLeft() || otherTankPartTwo.CollidesLeft())
                    {
                        otherTank.X = otherTank.X + 15;
                        otherTankPartTwo.X = otherTankPartTwo.X + 15;
                    }
                    if (otherTank.CollidesRight() || otherTankPartTwo.CollidesRight())
                    {
                        otherTank.X = otherTank.X - 15;
                        otherTankPartTwo.X = otherTankPartTwo.X - 15;
                    }
                }



                if (playerTank.Collides(otherTank.X, otherTank.Y, playerTank.Height, playerTank.Width) || otherTank.Collides(playerTank.X, playerTank.X, playerTank.Height, playerTank.Width))
                {
                    playerTank.X = playerTank.X;
                    playerTank.Y = playerTank.Y;

                    otherTank.X = otherTank.X;
                    otherTank.Y = otherTank.Y;
                }
            }
            //----------------------^^^Keyboard Collision^^^--------------------------------

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
            //CanvasGeometry myGeometry;

            //Vector2[] Points;

            public bool IsUp { get; set;  } 
            public bool IsDown { get; set; }
            public bool IsLeft { get; set; }
            public bool IsRight { get; set; }
            public bool TankIsShooting { get; set; }

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

                /*Points[0] = new Vector2(0,13);
                Points[1] = new Vector2(15, 13);
                Points[2] = new Vector2(15, 0);
                Points[3] = new Vector2(25, 0);
                Points[4] = new Vector2(25, 13);
                Points[5] = new Vector2(40, 13);
                Points[6] = new Vector2(40, 40);
                Points[7] = new Vector2(0, 40);
                Points[8] = new Vector2(0, 13);*/
            }
            public bool Collides(int x, int y, int height, int width)
            {
                return true;
                //if (x > X && x < X + Width && y > Y && y )
            }
            public bool CollidesTop()
            {
                //(X == x || X == 22) || (
                return ( Y == 20);

                //return x == X && x == Width && y == Y && y == Height; //Test statement MAXX
            }
            public bool CollidesBottom()
            {
                //(X == x || X == 22) || (
                return (Y == 720);
            }
            public bool CollidesLeft()
            {
                //(X == x || X == 22) || (
                return (X == 22);
            }
            public bool CollidesRight()
            {
                //(X == x || X == 22) || (
                return (X == 1018);
            }
            public void Draw(CanvasDrawingSession canvas)
            {
                canvas.FillRectangle(X, Y, Height, Width, Colors);
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
                return ((x == 1018 || x == 22) || (y == 720 || y == 20));

                //return x == X && height == Height; //|| y == Height; //Test statement MAXX

            }

            public bool CollidesBullet(Bullets bull)
            {
                if (bull.X <= X || bull.Y <= Y || bull.X >= X + Width || bull.Y >= Y + Height)
                {
                    return true;
                }
                return false;
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
            public int mapNum { get; set; }


            public InteriorWalls(int x, int y, int height, int width, Color color, int mapNum)
            {
                X = x;
                Y = y;
                Height = height;
                Width = width;
                Color = color;
                this.mapNum = mapNum;

            }

            public bool Collides(int x, int y, int height, int width)
            {
                // Same idea/concept of exterior wall collision function

                return (x <= X || x <= Height || x <= Width || x <= Y); 

                //return x == X || x == height; //|| y == Height; //Test statement MAXX

            }

            public bool CollidesBullet(Bullets bull)
            {
                if (bull.X >= X && bull.X <= (X + Width) && bull.Y >= Y && bull.Y <= (Y + Height))
                {
                    return true;
                }
                return false;
            }

            public void Draw(CanvasDrawingSession canvas)
            {
                //MAP #1
                Color Blue = Colors.Blue;
                Color Red = Colors.Red;
                Color LimeGrn = Colors.LimeGreen;
                Color Yellow = Colors.Yellow;
                Color Black = Colors.Black;


                if (mapNum == 1)
                {
                    //map color
                    //canvas.FillRectangle(X, Y, Height, Width, Colors.Green);
                    //starts at x = 20, y = 20, height(x) = 1000, width(y) = 700, use Colors.???
                    canvas.FillRectangle(195, 120, 50, 500, Colors.Cyan);  //1
                    canvas.FillRectangle(145, 120, 100, 50, Colors.Cyan);   //2
                    canvas.FillRectangle(145, 570, 75, 50, Colors.Cyan);   //3

                    canvas.FillRectangle(460, 20, 120, 100, Colors.Cyan);   //4
                    canvas.FillRectangle(460, 220, 120, 100, Colors.Cyan);   //5
                    canvas.FillRectangle(460, 420, 120, 100, Colors.Cyan);   //6
                    canvas.FillRectangle(460, 620, 120, 100, Colors.Cyan);   //7


                    canvas.FillRectangle(795, 120, 50, 500, Colors.Cyan);   //8
                    canvas.FillRectangle(820, 120, 75, 50, Colors.Cyan);   //9
                    canvas.FillRectangle(820, 570, 75, 50, Colors.Cyan);   //10 */

                }

                if (mapNum == 2)
                {
                    //map color
                    //canvas.FillRectangle(X, Y, Height, Width, Colors.Black);

                    //starts at x = 20, y = 20, height(x) = 1000, width(y) = 700, use Colors.???

                    canvas.FillRectangle(160, 20, 70,100, Colors.LimeGreen); //1
                    canvas.FillRectangle(160, 245, 70, 250, Colors.LimeGreen); //2
                    canvas.FillRectangle(160, 620, 70, 100, Colors.LimeGreen); //3

                    canvas.FillRectangle(376,120,70,200, Colors.LimeGreen); //4
                    canvas.FillRectangle(376,420,70,200, Colors.LimeGreen); //5

                    canvas.FillRectangle(592,120,70,200, Colors.LimeGreen); //7
                    canvas.FillRectangle(592,420,70,200, Colors.LimeGreen); //8

                    canvas.FillRectangle(810,20,70,100, Colors.LimeGreen); //9
                    canvas.FillRectangle(810,245,70,250, Colors.LimeGreen); //10
                    canvas.FillRectangle(810,620,70,100, Colors.LimeGreen); //11 
                }

                if (mapNum == 3)
                {
                    //map color
                    //canvas.FillRectangle(X, Y, Height, Width, Colors.DarkRed);

                    //starts at x = 20, y = 20, height(x) = 1000, width(y) = 700, use Colors.???

                    canvas.FillRectangle(20, 20, 120,120, Yellow); //1
                    canvas.FillRectangle(20, 600, 120,120, Yellow); //2

                    canvas.FillRectangle(260,170,80,400, Yellow); //3

                    canvas.FillRectangle(700,170,80,400, Yellow); //4

                    canvas.FillRectangle(900, 20, 120,120, Yellow); //5
                    canvas.FillRectangle(900, 600, 120,120, Yellow); //6

                    canvas.FillRectangle(430, 270, 60,60, Yellow); //10
                    canvas.FillRectangle(550, 270, 60,60, Yellow); //10
                    canvas.FillRectangle(380, 390, 40,40, Yellow); //10
                    canvas.FillRectangle(420, 430, 200,40, Yellow); //10
                    canvas.FillRectangle(620, 390, 40,40, Yellow); //10
                }
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

        public class Bullets : IDrawable, ICollidable, IDisposable
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
            public Color Color { get; set; }

            public bool TravelingDownward { get; set; }
            public bool TravelingLeftWard { get; set; }
            public bool TravelingUpward { get; set; }

            public int Speed { get; set; }



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

                TravelingLeftWard = false;
                TravelingDownward = false;
                TravelingUpward = false;
                DiagnolTravelLeft = false;
                DiagnolTravelRight = false;
                Speed = 5;
            }

            ~Bullets()
            {
                
            }

            public void Update()
            {
                //Determine what position user is going.  Bullets can only go in one direction

                if (TravelingDownward)
                {
                    if (DiagnolTravelLeft)
                    {
                        Y += Speed;
                        X -= Speed;
                    }
                    else if (DiagnolTravelRight)
                    {
                        Y += Speed;
                        X += Speed;
                    }
                    Y += Speed;
                }
                else if (TravelingUpward)
                {
                    if (DiagnolTravelLeft)
                    {
                        Y -= Speed;
                        X -= Speed;

                    }
                    else if (DiagnolTravelRight)
                    {
                        Y -= Speed;
                        X += Speed;
                    }
                    Y -= Speed;
                }
                else if (TravelingLeftWard)
                {
                    X -= Speed;
                }
                else
                {
                    X += Speed;
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

                return (x <= X || x <= Width) && (y >= Y || y <= Height); //Test statement

                //return (x  <= X || x <= Width) && (y >= Y || y <= Height); //Test statement MAXX
            }

            public bool tankCollides(int x, int y, int height, int width)
            {
               return (x == X && ((y <= Y) && (Y >= width)));
                
            }
            public bool inWallCollides(int x, int y, int height, int width)
            {
                return ( X <= x || X >= height) || (y >= Y || Y >= width);
           
            }
            public void Draw(CanvasDrawingSession canvas)
            {
                canvas.FillRectangle(X, Y, Height, Width, Color);
            }

            public void Dispose()
            {
            }
        }

    }
}
