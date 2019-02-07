using MyGame;
using System;
using System.Drawing;
using System.Windows.Forms;
using Level2Les1;

namespace Level2Les1
{
    class SpaceShip : BaseObject, ICollision
    {
        public static Bitmap rock = new Bitmap(@"rock.png");
        public SpaceShip (Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(rock, Pos.X, Pos.Y, 50,50);
        }
        public override void Update()
        {
            
            Pos.Y= Form.MousePosition.Y-70;
            Pos.X = Pos.X - 10;
            if (Pos.X <0) Pos.X = Game.Width;

        }

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(Pos, Size);
    }
}


