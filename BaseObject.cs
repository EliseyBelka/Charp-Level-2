using System;
using System.Drawing;

namespace MyGame
{
    class BaseObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        public static Bitmap rock = new Bitmap(@"rock.png");
        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + 4, Pos.Y + 2);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 4, Pos.Y + 2, Pos.X + 6, Pos.Y + 6);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 6, Pos.Y + 6, Pos.X + 8, Pos.Y + 2);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 8, Pos.Y + 2, Pos.X + 12, Pos.Y);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 12, Pos.Y, Pos.X + 8, Pos.Y - 2);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 8, Pos.Y - 2, Pos.X + 6, Pos.Y - 6);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 6, Pos.Y - 6, Pos.X + 4, Pos.Y - 2);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 4, Pos.Y - 2, Pos.X, Pos.Y);
           
        }
        public void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X > Game.Width) Pos.X = 0;
            if (Pos.X < 0) Pos.X = 500;
        }
    }
}