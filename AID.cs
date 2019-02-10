using MyGame;
using System.Drawing;

namespace Level2Les1
{
    class AID : BaseObject, ICollision
    {
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(Pos, Size);
        public static Bitmap aid = new Bitmap(@"aid.png");
        public AID(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(aid, new Rectangle(Pos.X, Pos.Y, 25, 25));
        }
        public override void Update()
        {
            Pos.X = Pos.X - 8;
          
        }
    }

}