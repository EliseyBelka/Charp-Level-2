using System;
using System.Drawing;
using MyGame;

namespace Level2Les1
{
    class Star : BaseObject
    {
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            //звезда
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + 4, Pos.Y + 2);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 4, Pos.Y + 2, Pos.X + 6, Pos.Y + 6);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 6, Pos.Y + 6, Pos.X + 8, Pos.Y + 2);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 8, Pos.Y + 2, Pos.X + 12, Pos.Y);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 12, Pos.Y, Pos.X + 8, Pos.Y - 2);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 8, Pos.Y - 2, Pos.X + 6, Pos.Y - 6);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 6, Pos.Y - 6, Pos.X + 4, Pos.Y - 2);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 4, Pos.Y - 2, Pos.X, Pos.Y);
        }
        /// <summary>
        /// Измененеие состояния объекта BaseObject
        /// </summary>
        public override void Update()
        {
            //полет вправо
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width;
            //полет влево
            //Pos.X = Pos.X + Dir.X;
            //if (Pos.X > Game.Width) Pos.X = 0;
        }
    }
}
