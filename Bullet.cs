using MyGame;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Level2Les1
{
    class Bullet : BaseObject, ICollision
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.FillRectangle(Brushes.LawnGreen, Pos.X+ Size.Width/2, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X + 100;
            if (Pos.X > Game.Width) Pos.X = 0;
        }
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(Pos, Size);
    }
}

