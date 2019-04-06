using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combat
{
    public class BuildGame
    {
        private Vehicle character;
        private HealthBar healthBar;
        private List<Walls> walls;
        private List<IDrawable> drawables;
        private List<ICollidable> collidables;

        public BuildGame()
        {
            drawables = new List<IDrawable>();
            character = new Vehicle(100, 100, 0, 0);

            drawables.Add(character);
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
        public class Vehicle : ICollidable, IDrawable
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }

            public Vehicle(int x, int y, int height, int width)
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

        //This the boundary of the form, once touched it will take user to the next form
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

        //This is the energy bar for the character
        public class HealthBar : IDrawable
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
