using Microsoft.Graphics.Canvas;
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
        private Points pointsComputer;
        private Bullets playerBullets;
        private Bullets computerBullets;
        private List<Walls> walls;
        private List<IDrawable> drawables;
        private List<ICollidable> collidables;

        public BuildGame()
        {
            drawables = new List<IDrawable>();
            walls = new List<Walls>();
            collidables = new List<ICollidable>();
            playerTank = new Tank(5, 5, 5, 5, Colors.Black);
            otherTank = new Tank(5, 6, 6, 6, Colors.Blue);
            pointsPlayer = new Points();
            pointsComputer = new Points();
            playerBullets = new Bullets(20, 20, 5, 5, Colors.Blue);

            //Automatically placing the bullets to move towards the right on X-Axis
            playerBullets.TravelingLeftWard = false;
            playerBullets.TravelingUpward = false;
            playerBullets.TravelingDownward = false;

            computerBullets = new Bullets(900, 600, 5, 5, Colors.Orange);

            //Automatically placing the bullets to move towards the left on X-Axis
            computerBullets.TravelingLeftWard = true;
            computerBullets.TravelingUpward = false;
            computerBullets.TravelingDownward = false;

            //playerTank = new Tank(100, 100, 50, 50, Colors.Blue);

            //Boundary of game
            var outsideWall = new Walls(10,10,1000,700, Colors.Black);

            //Bullets, need to be animated to move along X-Axis

            //Adding outside wall
            drawables.Add(outsideWall);
            walls.Add(outsideWall);

            //Adding bullets
            drawables.Add(playerBullets);
            drawables.Add(computerBullets);



            //drawables.Add(playerTank);
        }

        public void Update()
        {
            playerBullets.Update();
            computerBullets.Update();
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

            public Tank(int x, int y, int height, int width, Color color)
            {
                X = x;
                Y = y;
                Height = height;
                Width = width;
                color = Colors;
            }

            public bool Collides(int x, int y, int height, int width)
            {
                throw new NotImplementedException();
            }

            public void Draw(CanvasDrawingSession canvas)
            {
                //canvas.DrawEllipse(X, Y, Height, Width, Colors, 3);
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
                    Y += 1;
                }
                else if (TravelingUpward)
                {
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
                //return x >= X && x <= Width && y >= Y && y <= Height;
            }

            public void Draw(CanvasDrawingSession canvas)
            {
                canvas.DrawRectangle(X, Y, Height, Width, Color);
            }
        }

    }
}
