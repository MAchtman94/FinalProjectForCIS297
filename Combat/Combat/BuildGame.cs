using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Combat
{
    public class BuildGame
    {
        private Tank tank;
        private Points points;
        private List<Walls> walls;
        private List<IDrawable> drawables;
        private List<ICollidable> collidables;

        public BuildGame()
        {
            drawables = new List<IDrawable>();
            tank = new Tank(100, 100, 50, 50);

            drawables.Add(tank);
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
            bool Collides(int x, int y, int width, int height);
        }

        public interface IDrawableUdnerground
        {
            void Draw(CanvasDrawingSession canvas);
        }

        //Character player
        public class Tank : ICollidable, IDrawable
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }

            public Tank(int x, int y, int height, int width)
            {
                X = x;
                Y = y;
                Height = height;
                Width = width;
            }

            public bool Collides(int x, int y, int width, int height)
            {
                throw new NotImplementedException();
            }

            public void Draw(CanvasDrawingSession canvas)
            {
                canvas.DrawImage(tankIm, )
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

            public Walls(int x, int y)
            {
                X = x;
                Y = y;
            }

            public bool Collides(int x, int y, int width, int height)
            {
                throw new NotImplementedException();
            }

            public void Draw(CanvasDrawingSession canvas)
            {
                throw new NotImplementedException();
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

            public Bullets(int x, int y, int height, int width)
            {
                X = x;
                Y = y;
                Height = height;
                Width = width;
            }

            public bool Collides(int x, int y, int width, int height)
            {
                throw new NotImplementedException();
            }

            public void Draw(CanvasDrawingSession canvas)
            {
                throw new NotImplementedException();
            }
        }

    }
}
