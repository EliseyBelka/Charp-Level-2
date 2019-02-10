using System;
using System.Windows.Forms;
using System.Drawing;
// Создаем шаблон приложения, где подключаем модули
namespace MyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            form.Width = Screen.PrimaryScreen.Bounds.Size.Width;
            form.Height = Screen.PrimaryScreen.Bounds.Size.Height;
            Game.Init(form);
            form.Show();
            Game.Draw();
            Application.Run(form);
        }
    }
}