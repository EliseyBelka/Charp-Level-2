using System;
using System.Windows.Forms;
using System.Drawing; //доступ к функциональным возможностям графического интерфейса GDI+.
using Level2Les1;
using System.IO;
using MyGame;

namespace MyGame
{/// <summary>
/// Делегат отвечающий за вызов метода Message
/// </summary>
/// <param name="str"></param>
    delegate void StrToFile(string str);
    /// <summary>
    /// Интерфей столкновение объектов
    /// </summary>
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }  
}


    static class Game
 {  /// <summary>
    /// Работ с методом запись в файл и вывод в консоль через делегат
    /// </summary>
    /// <param name="str"></param>
    public static void InFile(string str)
    {
        using (FileStream fstream = new FileStream(@"note.txt", FileMode.Append))
        {
            byte[] array = System.Text.Encoding.Default.GetBytes(str);
            fstream.Write(array, 0, array.Length);
            Console.WriteLine(str+"");
        }
    }
        //переменная делегата
        public static StrToFile WToFile=InFile;
        //параметры
        public static Bitmap fon = new Bitmap(@"star.gif");
        public static int reload;
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static Timer timer = new Timer() { Interval = 20};
        public static Random Rnd = new Random();
        public static int Width { get; set; }
        public static int Height { get; set; }
        private static BaseObject[] _objs;
        public static Bullet _bullet;
        private static Asteroid[] _asteroids;
        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));
        private static AID[] _aid;

        /// <summary>
        /// Инициализация объектов
        /// </summary>
        /// <param name="form"></param>
        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            Load();
            timer.Start();
            timer.Tick += Timer_Tick;
            form.KeyDown += Form_KeyDown;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
        }
        /// <summary>
    /// обработка клавищ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey & _ship.Energy > 0 & reload == 0)
            {
               _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1));
                reload = 1; //reload
                _ship.PointGrow(-1); //плата за снаряд
            InFile("Выполнен выстрел + оплачен снаряд\n");
            }
        if (e.KeyCode == Keys.Up & _ship.Energy > 0)
        {
            _ship.Up();
           // InFile("Перемещение вверх\n");
        }

        if (e.KeyCode == Keys.Down & _ship.Energy > 0)
        {
            _ship.Down();
          //  InFile("Перемещение вниз\n");
        }

        if (e.KeyCode == Keys.Right & _ship.Energy > 0)
        {
            _ship.Right();
           // InFile("Перемещение вправо\n");
        }

        if (e.KeyCode == Keys.Left & _ship.Energy > 0)
        {
            _ship.Left();
           // InFile("Перемещение влево\n");
        } 
        }
        /// <summary>
    /// Таймер
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        /// <summary>
        /// Рисование объектов и вывод достижений
        /// </summary>
        public static void Draw()
        {
            //"затирка" объектов
            Buffer.Graphics.Clear(Color.Black);
            //Фон
           Game.Buffer.Graphics.DrawImage(fon, new Rectangle(0, 0, Height * 2, Width));
            //Звезды
            foreach (BaseObject obj in _objs)
                obj.Draw();
            //Астероиды
            foreach (Asteroid a in _asteroids)
            {
                a?.Draw();
            }
            //Пуля
            _bullet?.Draw();
            //Корабль
            _ship?.Draw();
            //аптечки
            foreach (AID obj in _aid)
                obj?.Draw();

            if (_ship != null)
                //Вывод инф о заряде
            Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.Red, 0, 0);
            // вывод информации об очках
            Buffer.Graphics.DrawString("Point:" + _ship.Point, SystemFonts.DialogFont, Brushes.Red, 0, 20);
            
            Buffer.Render();
        }
        /// <summary>
        /// Прогрузка объектов
        /// </summary>
        public static void Load()
        {
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
                _objs[i] = new Star(new Point(Rnd.Next(Width), i * 15), new Point(Rnd.Next(5,20), 0), new Size(0, 0));
        #endregion
        InFile("Созданы объекты звезды\n");
            #region ASTER
        _asteroids = new Asteroid[5];
            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = Rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(Game.Width-Rnd.Next(25), Rnd.Next(Game.Height-25)), new Point(-r / 5, r), new Size(r, r));
                _asteroids[i].NumSpaceBody = i;
            }
        #endregion
        InFile("Созданы объекты астероиды\n");
            #region AID
        _aid = new AID[3];
            for (var i = 0; i < _aid.Length; i++)
            {
                int r = Rnd.Next(5, 50);
                _aid[i] = new AID(new Point(Rnd.Next(Game.Width/2, Game.Width), Rnd.Next(0, Game.Height)), new Point(-r / 5, r), new
                    Size(r, r));
            }
        #endregion
        InFile("Созданы объекты помощи\n");
    }
        /// <summary>
        /// Прггрузка изенений объектов
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs) obj.Update();
            for (var i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                if (_bullet != null && _bullet.Collision(_asteroids[i]))
                {
                  _ship.PointGrow(10);
                    //_bullet.MessageDestroyed();
                    System.Media.SystemSounds.Hand.Play();
                    _asteroids[i] = null;
                    _bullet = null;
                    Game.reload = 0;
                InFile("Столкновение астероида и пули\n");
                    continue;
                }
                if (!_ship.Collision(_asteroids[i])) continue;
                InFile("Столкновение коробля и астероида\n");
                _ship?.EnergyLow(Rnd.Next(1, 10));
                InFile("Уровень заряда коробля понижен\n");
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy < 0) _ship?.Die();
            }
            _bullet?.Update();

            for (var i = 0; i < _aid.Length; i++)
            {
                if (_aid[i] == null) continue;
                { _aid[i].Update(); }
                if (!_ship.Collision(_aid[i])) continue;
                {
                    _ship?.EnergyLow(-10);
                    System.Media.SystemSounds.Asterisk.Play();
                    _aid[i] = null;
                }
            }
        }
        /// <summary>
        /// Окончание игры
        /// </summary>
        public static void Finish()
        {
            timer.Stop();
            InFile("Конец игры\n");
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, 
                FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
       }
    }