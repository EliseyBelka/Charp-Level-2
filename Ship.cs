using System.Drawing;
using MyGame;

namespace Level2Les1
{
    class Ship : BaseObject
    {
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(Pos, Size);
        public static event Message MessageDie;
        private int _energy = 1;
        public int Energy => _energy;
        private int _point = 10;
        public int Point => _point;
        public static Bitmap rock = new Bitmap(@"rock1.png");
        public static Bitmap Boom = new Bitmap(@"Blow.png");

        public void Die()
        {
            MessageDie?.Invoke();
            Game.InFile("Корабль уничтожен\n");
        }

        public void EnergyLow(int n)
        {
            _energy -= n;
            Game.InFile("Уровень энергии изменен\n");
        }

        public void PointGrow (int n)
        {
            _point += n;
            Game.InFile("Количество очков изменено\n");

        }

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            if (_energy > 0)
                Game.Buffer.Graphics.DrawImage(rock, Pos.X, Pos.Y, 50, 50);
            else //окончание игры вынес в эту часть игры
            {
                Game.Buffer.Graphics.DrawImage(Boom, Pos.X, Pos.Y, 50, 40);
                Game.timer.Stop();
                Game.Buffer.Graphics.DrawString("GAME OVER", new Font(FontFamily.GenericSansSerif, 60,
               FontStyle.Bold), Brushes.Orange, Game.Height/2, 250);
                Game.InFile("игра окончена поражением\n");
            }
            if (_energy >21 & _point>40)
            {
                Game.timer.Stop();
                Game.Buffer.Graphics.DrawString("You Win", new Font(FontFamily.GenericSansSerif, 60,
               FontStyle.Bold), Brushes.Orange, Game.Height / 2, 250);
                Game.InFile("Игра окончена победой\n");
            }
        }
        public override void Update()
        {
        }
        public void Up()
        {
            if (Pos.Y-10 > 0) Pos.Y = Pos.Y - 10;
            else Pos.Y = 10;

        }
        public void Down()
        {
            if (Pos.Y+70 < Game.Height) Pos.Y = Pos.Y + 10;
            else Pos.Y = Game.Height-70;
        }
        public void Right()
        {
            if (Pos.X+70 < Game.Width) Pos.X = Pos.X + 20;
            else Pos.X = Game.Width-70;

        }
        public void Left()
        {
            if (Pos.X > 0) Pos.X = Pos.X - 20;
            else Pos.X = 0;

        }
    }
}