using System;
using System.Windows.Forms;
using System.Drawing;
using Level2Les1;

namespace MyGame
{
    static class Game
    {
        
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static BaseObject[] _objs;
        public static Bitmap fon = new Bitmap(@"star.gif");
        public static Bitmap rock = new Bitmap(@"rock.png");
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        static Game()
        {
        }
       
        public static void Init(Form form)
        {
            
            // Графическое устройство для вывода графики            
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            Timer timer = new Timer { Interval = 75 };
            timer.Start();
            timer.Tick += Timer_Tick;
            
        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            Game.Buffer.Graphics.DrawImage(fon, new Rectangle(0, 0, fon.Height, fon.Width / 3));
            Game.Buffer.Graphics.DrawImage(rock, new Rectangle(250, 250, 50, 50));
            foreach (BaseObject obj in _objs)
                obj.Draw();
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }
        public static void Load()
        {
            Random rnd = new Random();
            _objs = new BaseObject[30];
            for (int i = 0; i < _objs.Length; i++)
                _objs[i] = new Star(new Point(rnd.Next(700), i * 20), new Point(rnd.Next(50), 0), new Size(15, 5));
        }
    }
}