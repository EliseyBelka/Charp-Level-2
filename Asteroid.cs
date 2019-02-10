using System;
using MyGame;
using System.Drawing;


namespace Level2Les1
{
    class Asteroid : BaseObject, ICollision
    {
        public bool Collision(ICollision o) =>  o.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(Pos, Size);
        public static Bitmap aster = new Bitmap(@"astr.png");
        public int Power { get; set; }
        public int NumSpaceBody { get; set; }//закрепляет за астероидом номер его создания
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }
        public override void Draw()
        {
         Game.Buffer.Graphics.DrawImage(aster, new Rectangle(Pos.X, Pos.Y, 45, 30));
        }
        public override void Update()
        {
            Pos.X = Pos.X - 2;
            if (Pos.X < 0) Pos.X = Game.Width;
            //сложное движение астероидов
            //Random Rnd = new Random();
            //Pos.Y = Pos.Y - Rnd.Next(-10, 10);
            //if (Pos.Y+30 < 0) Pos.Y = 30;
            //if (Pos.Y-30 > Game.Height) Pos.Y = Game.Height-30;
        }
     
    }
}
