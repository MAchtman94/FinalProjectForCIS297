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
        private List<IDrawable> drawablesTwo;
        private List<IDrawable> drawablesThree;
        private bool isShooting;
        private bool gameOver;
        //public int sound { get; set; }

        private Gamepad controller;

        public BuildGame()
        {
            drawables = new List<IDrawable>();
            drawablesTwo = new List<IDrawable>();
            drawablesThree = new List<IDrawable>();
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
            //interiorWalls.Add(fillInWall);
            drawables.Add(insideWallLeftSide);
            interiorWalls.Add(insideWallLeftSide);
            // drawables.Add(insideWallRightSide);
            // interiorWalls.Add(insideWallRightSide);
            //sound = 0;

            drawables.Add(playerTank);
            drawables.Add(otherTank);

            drawablesTwo.Add(playerTank);
            drawablesTwo.Add(otherTank);

            drawables.Add(playerTankPartTwo);
            drawables.Add(otherTankPartTwo);

            drawablesTwo.Add(playerTankPartTwo);
            drawablesTwo.Add(otherTankPartTwo);

            CreateHealth();
            CreateHealthForOther();
        }

        //Creating player one health bar
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

            drawablesTwo.Add(barValuePlayerOnePartFive);
            drawablesTwo.Add(barValuePlayerOnePartFour);
            drawablesTwo.Add(barValuePlayerOnePartThree);
            drawablesTwo.Add(barValuePlayerOnePartTwo);
            drawablesTwo.Add(barValuePlayerOnePartOne);

            drawablesThree.Add(barValuePlayerOnePartFive);
            drawablesThree.Add(barValuePlayerOnePartFour);
            drawablesThree.Add(barValuePlayerOnePartThree);
            drawablesThree.Add(barValuePlayerOnePartTwo);
            drawablesThree.Add(barValuePlayerOnePartOne);
        }

		//Creating player two health bar
        public void CreateHealthForOther()
        {
            var barValueOtherOnePartOne = new Bar(940, 5, 10, 10, Colors.Blue);
            var barValueOtherOnePartTwo = new Bar(950, 5, 10, 10, Colors.Blue);
            var barValueOtherOnePartThree = new Bar(960, 5, 10, 10, Colors.Blue);
            var barValueOtherOnePartFour = new Bar(970, 5, 10, 10, Colors.Blue);
            var barValueOtherOnePartFive = new Bar(980, 5, 10, 10, Colors.Blue);

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

            drawablesTwo.Add(barValueOtherOnePartOne);
            drawablesTwo.Add(barValueOtherOnePartTwo);
            drawablesTwo.Add(barValueOtherOnePartThree);
            drawablesTwo.Add(barValueOtherOnePartFour);
            drawablesTwo.Add(barValueOtherOnePartFive);

            drawablesThree.Add(barValueOtherOnePartOne);
            drawablesThree.Add(barValueOtherOnePartTwo);
            drawablesThree.Add(barValueOtherOnePartThree);
            drawablesThree.Add(barValueOtherOnePartFour);
            drawablesThree.Add(barValueOtherOnePartFive);
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
                case 'E': playerTank.TankIsShooting = true;
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
                case 'O':
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
                    if (controls.Buttons.HasFlag(GamepadButtons.Y))
                    { 
                        bool hit = true;

                        foreach (var health in barPlayer.ToList())
                        {
                            //Only the first instance in the list will be removed
                            if (gameTypeToBuild == 1)
                            {
                                if (hit == true)
                                {
                                    barPlayer.Remove(health);
                                    drawables.Remove(health);
                                    hit = false;
                                }
                            }
                            else if (gameTypeToBuild == 2)
                            {
                                if (hit == true)
                                {
                                    barPlayer.Remove(health);
                                    drawablesTwo.Remove(health);
                                    hit = false;
                                }
                            }
                            else
                            {
                                if (hit == true)
                                {
                                    barPlayer.Remove(health);
                                    drawablesThree.Remove(health);
                                    hit = false;
                                }
                            }
                        }
                    }

                    //Removing health bar for other player
                    if (controls.Buttons.HasFlag(GamepadButtons.X))
                    {
                        bool hit = true;

                        if (gameTypeToBuild == 1)
                        {
                            foreach (var health in barOther.ToList())
                            {
                                if (hit == true)
                                {
                                    barOther.Remove(health);
                                    drawables.Remove(health);
                                    hit = false;
                                }
                            }
                        }

                        else if (gameTypeToBuild == 2)
                        {
                            foreach (var health in barOther.ToList())
                            {
                                if (hit == true)
                                {
                                    barOther.Remove(health);
                                    drawablesTwo.Remove(health);
                                    hit = false;
                                }
                            }
                        }
                        else
                        {
                            foreach (var health in barOther.ToList())
                            {
                                if (hit == true)
                                {
                                    barOther.Remove(health);
                                    drawablesThree.Remove(health);
                                    hit = false;
                                }
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

                        if (gameTypeToBuild == 1)
                        {
                            drawables.Add(playerBullet);
                            drawables.Add(otherBullet);
                        }
                        else if (gameTypeToBuild == 2)
                        {
                            drawablesTwo.Add(playerBullet);
                            drawablesTwo.Add(otherBullet);
                        }
                        else
                        {
                            drawablesThree.Add(playerBullet);
                            drawablesThree.Add(otherBullet);
                        }

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
                }
                else
                {
                    
                }

                //Testing if bullet collides with walls, internal and external
                //Issue might arise since foreach can't be used such as "var bullets in player bullets", and if one bullet collides, all of the players bullets might be erased.

                // if (itCollides)
                // {

                foreach (var bullet in playerBullets)
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
                    /*
                    if (bullet.Collides(otherTank.X, otherTank.Y, bullet.Height, bullet.Width))
                    {
                        bullet.removeBullet(bullet);


                    }
                    */
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
                    /*
                    if (bullet.Collides(playerTank.X, playerTank.Y, bullet.Height, bullet.Width))
                    {
                        bullet.removeBullet(bullet);
                    }
                    */
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

               /*
                if (playerTank.Collides(otherTank.X, otherTank.Y, playerTank.Height, playerTank.Width) || otherTank.Collides(playerTank.X, playerTank.X, playerTank.Height, playerTank.Width))
                {
                    playerTank.X = playerTank.X;
                    playerTank.Y = playerTank.Y;

                    otherTank.X = otherTank.X;
                    otherTank.Y = otherTank.Y;
                }
                */
                
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

                    //Removing the entire health bar not each square
                    /*if (controls.Buttons.HasFlag(GamepadButtons.Y))
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
                    }*/

                    if (playerTank.TankIsShooting == true)
                    {
                        //Looking at the instance of the bullets
                        var playerBullet = new Bullets(playerTank.X + 65, playerTank.Y + 25, 10, 10, Colors.Blue);

                        if (playerTank.IsUp)
                        {
                            playerBullet.TravelingUpward = true;
                            if(playerTank.IsLeft) { playerBullet.DiagnolTravelLeft = true; }
                            if(playerTank.IsRight) { playerBullet.DiagnolTravelRight = true; }
                        }
                        else if (playerTank.IsDown)
                        {
                            playerBullet.TravelingDownward = true;
                            if (playerTank.IsLeft) { playerBullet.DiagnolTravelLeft = true; }
                            if (playerTank.IsRight) { playerBullet.DiagnolTravelRight = true; }
                        }
                        else if(playerTank.IsLeft) {playerBullet.TravelingLeftWard = true;}


                        //Include traveling before adding to list
                        playerBullets.Add(playerBullet);

                        drawables.Add(playerBullet);

                        playerTank.TankIsShooting = false;
                    }

                    if (otherTank.TankIsShooting == true)
                    {
                        //Looking at the instance of the bullets
                        var otherBullet = new Bullets(otherTank.X - 30, otherTank.Y + 25, 10, 10, Colors.Orange);

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

                //Testing if bullet collides with walls, internal and external
                //Issue might arise since foreach can't be used such as "var bullets in player bullets", and if one bullet collides, all of the players bullets might be erased.
                foreach (var bullet in playerBullets)
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
                    if (bullet.Collides(otherTank.X, otherTank.Y, bullet.Height, bullet.Width))
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
               /*if (playerTank.Collides(otherTank.X, otherTank.Y, playerTank.Height, playerTank.Width) || otherTank.Collides(playerTank.X, playerTank.X, playerTank.Height, playerTank.Width))
                {
                    playerTank.X = playerTank.X;
                    playerTank.Y = playerTank.Y;

                    otherTank.X = otherTank.X;
                    otherTank.Y = otherTank.Y;
                }*/

                //bool isButtonPressed;
            }

        }

        //Draw the game
        public void DrawGame(CanvasDrawingSession canvas)
        {
            //If we want game one, build it like this
            if (gameTypeToBuild == 1)
            {
                foreach (var drawable in drawables)
                {
                    drawable.Draw(canvas);
                }
            }

            //If we want game two, build it like this
            else if (gameTypeToBuild == 2)
            {
                foreach (var drawables in drawablesTwo)
                {
                    drawables.Draw(canvas);
                }
            }

            //If we want to build game three
            else
            {
                foreach (var drawables in drawablesThree)
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
        public class Tank :  IDrawable
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
            public int AngleX { get; set; }
            public int AngleY { get; set; }
            public Color Colors { get; set; }

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
            }
            public bool Collides()
            {
                return true;
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

                //return x == X || x == height; //|| y == Height; //Test statement MAXX

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

                TravelingLeftWard = false;
                TravelingDownward = false;
                TravelingUpward = false;
                DiagnolTravelLeft = false;
                DiagnolTravelRight = false;
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

                return (x <= X || x <= Width) && (y >= Y || y <= Height); //Test statement

                //return (x  <= X || x <= Width) && (y >= Y || y <= Height); //Test statement MAXX
            }

            public void Draw(CanvasDrawingSession canvas)
            {
                canvas.FillRectangle(X, Y, Height, Width, Color);
            }
        }

    }
}
