using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp2Lesson2
{
    #region интерфейс
    interface ISortArray
    {
        void SortArray(string[] arr);
    }
    #endregion
    #region классы
    /// <summary>
    /// Абстрактный класс сотрудники
    /// </summary>
    abstract class BaseClassEmployee
    {
        public string Surname { get; set; }
        public int HourlyPay { get; set; }
        public int FixMonthPay { get; set; }
        public abstract double PermanentStaffPay();
        public abstract double HiredStaffPay();
    }
    /// <summary>
    /// класс штатников наследник абстрактного класс сотрудники
    /// </summary>
    class PermStaff : BaseClassEmployee
    {

        /// <summary>
        /// Расчет среднемесячной заработной платы штатника
        /// </summary>
        /// <returns></returns>
        public override double PermanentStaffPay()
        {
            double AverageMonthlyWage;
           return AverageMonthlyWage = FixMonthPay;
        }
        //заглушеный абстрактный метод для наёмного у штатника
        //.т.к. обязан быть согласно правила объявления в базовом классе
        public override double HiredStaffPay()
        { return 0; }
      

    }
    /// <summary>
    /// класс наёмников наследник абстрактного класс сотрудники
    /// </summary>
    class HiredStaff : BaseClassEmployee
    {
        /// <summary>
        /// Расчет среднемесячной заработной платы по найму
        /// </summary>
        /// <returns></returns>
        public override double HiredStaffPay()
        {
            double AverageMonthlyWage;
            return AverageMonthlyWage = 20.8 * 8 * HourlyPay;
        }
        //заглушеный абстрактный метод для штатника у наёмного
        //.т.к. обязан быть согласно правила объявления в базовом классе
        public override double PermanentStaffPay()
        { return 0; }
    }
    /// <summary>
    /// Сортировка одномерного массива через SortArray сформир с учетом интерфейса
    /// </summary>
    class SortingStaff : ISortArray
    {   
        public void SortArray(string[] arr)
        {
            Array.Sort(arr);
        }
    }
    /// <summary>
    /// Сохранение массива и вывод на экран через  foreach
    /// </summary>
    class PrintArray
    {
        public static void Print(string[] arr)
        {
            string[] temp = arr;
            foreach (string i in temp)
            {
                Console.WriteLine(i);
            }
        }
    }
    #endregion

    class Program
    {
        /// <summary>
         /// Ведикая пауза
         /// </summary>
        static void Pause()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.WriteLine("Press ESC to exit");
            while (Console.ReadKey().Key != ConsoleKey.Escape) ;
        }
        static void Main(string[] args)
        {
            int NumberStaff;
            string StaffType;
            PermStaff PS = new PermStaff();
            HiredStaff HS = new HiredStaff();
            SortingStaff SS = new SortingStaff();
            Console.WriteLine("How much staff will be filled?");
            NumberStaff=int.Parse(Console.ReadLine());
            string[,] staff=new string[NumberStaff,2]; //Фамилия отдельно/ЗП отдельно
            string[] StaffForSort = new string[NumberStaff];
            #region// Заполнение массива
            for (int i = 0; i < NumberStaff; i++)
            {
                //Hired (по найму) Permanent (псотоянный)
                Console.WriteLine("Specify the employee type.(Hire/Perm)?");
                StaffType = (Console.ReadLine().ToLower());
                //условие для постоянного работнкиа
                if ((StaffType == "perm") | (StaffType == "p"))
                {
                    Console.WriteLine("Enter the name of the person:");
                    PS.Surname = (Console.ReadLine()+" PS");
                    Console.WriteLine("Gets how much per month?");
                    PS.FixMonthPay = int.Parse(Console.ReadLine());
                    staff[i, 0] = PS.Surname;                           //0 - фамилия
                    staff[i, 1] = (PS.PermanentStaffPay().ToString());  //1 - среднемесячная заработная плата
                    StaffForSort[i] = PS.Surname + " " + PS.PermanentStaffPay().ToString();
                }
                //условия для временного работника
                if ((StaffType == "hire") | (StaffType == "h"))
                {
                    Console.WriteLine("Enter the name of the person:");
                    HS.Surname = (Console.ReadLine()+" HS");
                    Console.WriteLine("Gets how much per hour?");
                    HS.HourlyPay = int.Parse(Console.ReadLine());
                    staff[i, 0] = HS.Surname;                       //0 - фамилия
                    staff[i, 1] = (HS.HiredStaffPay().ToString());  //1 - среднемесячная заработная плата
                    StaffForSort[i] = HS.Surname + " " + HS.HiredStaffPay().ToString();
                }
                if ((StaffType != "perm") & (StaffType != "hire") & (StaffType != "p") & (StaffType != "h"))
                    Console.WriteLine("You have entered incorrect data.\nThe program will not give accurate results.");
            }
            #endregion
            #region//Сортировка массива (use interface)
            SS.SortArray(StaffForSort);
            #endregion
            #region//Вывод двумерного массива содержащего [список][зарплаты]
            Console.WriteLine("The state has the following employees(monthly salary)");
            for (int i = 0; i < NumberStaff; i++)
            Console.WriteLine(staff[i , 0]+" (" +staff[i, 1] + ")");
            #endregion
            #region//Вывод сортированного массива [список + тип + зарплаты]
            Console.WriteLine("Sorted array of staff");
            for (int i = 0; i < NumberStaff; i++)
            Console.WriteLine(StaffForSort[i]);
            #endregion
            #region//Вывод массив через foreach
            Console.WriteLine("Output array via foreach");
            PrintArray.Print(StaffForSort);
            #endregion

            Pause();
        }
    }
}
