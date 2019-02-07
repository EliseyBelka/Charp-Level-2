using System;
using System.Windows.Forms;
using System.Drawing; //доступ к функциональным возможностям графического интерфейса GDI+.
using Level2Les1;

namespace MyGame
{
    /// <summary>
    /// закладывает поведение двух объектов(поддерживающие его) и определяет, столкнулись ли они.
    /// </summary>
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }
    /// <summary>
    /// Это основной класс, где происходят все действия игры.
    /// </summary>
    static class Game
    {
       
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        private static Bullet _bullet;
        private static Asteroid[] _asteroids;
        private static BaseObject[] _objs;
        private static SpaceShip Spaceship;

        /// <summary>
        /// фон игры в корне
        /// </summary>
        public static Bitmap fon = new Bitmap(@"star.gif");
        /// <summary>
        /// рокета в корне
        /// </summary>
        

        public static int Width { get; set; }
        public static int Height { get; set; }
        static Game()
        {
        }
       
        public static void Init(Form form)
        {    
            Graphics g;                                 //http://c-sharp.pro/?p=47
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();                  //(поверхность рисования)
            try
            {
                Height = form.ClientSize.Height;
                if ((Height > 1000) | (Height < 0)) throw new ArgumentOutOfRangeException();
              
                Width = form.ClientSize.Width;
                if ((Width > 1000) | (Width < 0)) throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Dimensions exceeded");
            }
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height)); //связи буфера и графики

            Load();

            Timer timer = new Timer { Interval = 75 };
            timer.Start();//Запускаем таймер    
            timer.Tick += Timer_Tick;//Обработчик таймера (вызывается с заданной периодичностью)
          //  timer.Stop();//Останавливаем таймер ;
        }
        
        /// <summary>
        /// вывод объектов на экран GAME
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            //!!! разобраться почему фон по ширине нужно *2 ниже
            Game.Buffer.Graphics.DrawImage(fon, new Rectangle(0, 0, Height*2, Width));
            foreach (BaseObject obj in _objs)
                obj.Draw();
            _bullet.Draw();
            Spaceship.Draw();
            foreach (BaseObject astr in _asteroids)
                astr.Draw();
            Buffer.Render();
        }
        /// <summary>
        /// Измененеие состояния объекта GAME
        /// </summary>
        public static void Update()
        {

            Random rnd = new Random();
            foreach (BaseObject obj in _objs)
                obj.Update();
            _bullet.Update();
            Spaceship.Update();
            foreach (Asteroid astr in _asteroids)
            {
                astr.Update();
                if (astr.Collision(_bullet))
                { System.Media.SystemSounds.Hand.Play();
                _bullet = new Bullet(new Point(0, 200), new Point(0, 0), new Size(8, 2));
                    int r = rnd.Next(5, 50);
                    _asteroids[astr.NumSpaceBody] = new Asteroid(new Point((Width), rnd.Next(Height)), new Point(-r / 5, r), new Size(r, r));
                ///!!! КАК УЗНАТЬ НОМЕР АСТЕРОИДА С КОТОРЫМ ПРОИЗОШЛО СТОЛКНОВЕНИЕ???
                }
            }

        }
        /// <summary>
        /// инициализация начального объекта GAME
        /// </summary>
        public static void Load()
        {
            Random rnd = new Random();
            #region STAR
            _objs = new BaseObject[Width / 30];
            //Width/Num  - количство звезд зависит от ширины экрана пользователя
            for (int i = 0; i < _objs.Length; i++)
                #region FAQ
                //rnd.Next(Width) - начальное распределение звезд по горизонтали на форме
                //i*15 - распределение звезд по вертикали, связано с[Width/30] как 2к1 ("чем меньше числа тем больше звезд")
                //(rnd.Next(50), 0) - первый параметр задает скорость звезд rnd - для каждой звезды свою в диапазоне 0-50. 0 - движение по оси Y (в нашем слу не важно)
                //Size(0,0) - не имеет значения т.к. стар в BasOb построены c фикисрованными длинами сторон
                #endregion
                _objs[i] = new Star(new Point(rnd.Next(Width), i * 15), new Point(rnd.Next(25), 0), new Size(0, 0));
            #endregion
            #region BULL and SHIP
            _bullet = new Bullet(new Point(200, 200), new Point(0, 0), new Size(8, 2));
            Spaceship = new SpaceShip(new Point(200, 200), new Point(0, 0), new Size(8, 2));
            #endregion
            #region ASTER
            _asteroids = new Asteroid[5];
            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(100+rnd.Next(Width), rnd.Next(Height)), new Point(-r / 5, r), new Size(r, r));
                _asteroids[i].NumSpaceBody = i;
            }
            #endregion
        }
        /// <summary>
        /// Таймер GAME
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
    }
}