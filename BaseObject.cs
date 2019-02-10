using System;
using System.Drawing;

namespace MyGame
{
    public delegate void Message();

    abstract class BaseObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public abstract void Draw();
        public abstract void Update();
        
    }
}