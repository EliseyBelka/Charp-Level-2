using MyGame;
using System.Drawing;

namespace Level2Les1
{
    delegate string SendConsoleMessage (string x);

    class Bullet : BaseObject, ICollision
    {
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(Pos, Size);
        public static Bitmap bullet = new Bitmap(@"wave.png");
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(bullet, new Rectangle(Pos.X, Pos.Y, 25, 25));
        }
        public override void Update()
        {
            Pos.X = Pos.X + 10;
            if (Pos.X + 30 > Game.Width)
            {
                Game.reload = 0; //перезарядка разрешена
                Game._bullet = null;
            } 
        }
        
    }
}

