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
            Form form = new Form()
            {
                Width = Screen.PrimaryScreen.Bounds.Width,
                Height = Screen.PrimaryScreen.Bounds.Height
            }; 
            //размер формы по размеру экрана пользователя
            //form.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width;
            //form.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Height;
            Game.Init(form);
            form.Show();
            Game.Draw();
            Application.Run(form);
        }
    }
}