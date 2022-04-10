using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace Lesson6_TaskManager
{
    internal class Program
    {
        //метод: вывод id и имен (названий) всех запущенных процессов
        static void PrintProcess()
        {
            Console.WriteLine("---- Список запущенных процессов ----");
            foreach (Process process in Process.GetProcesses())
            {
                Console.WriteLine("ID: {0,-6} | Name: {1,-20}", process.Id, process.ProcessName);
            }
        }

        //метод: завершить процесс по имени
        static void KillProcessByName()
        {
            try
            {
                //считать имя завершаемого процесса
                Console.Write("Введите имя (название) завершаемого процесса : ");
                string sNameKillProcess = Console.ReadLine();

                //список процессов с введенным именем завершаемого процесса
                Process[] vKillProcesses = Process.GetProcessesByName(sNameKillProcess);

                //проверка наличия завершаемых процессов  
                if (vKillProcesses.Length > 0) //есть завершаемые процессы
                {
                    //переменная для отслеживания завершения всех процессов с именем sNameKillProcess
                    bool bAllProcessKill = true;

                    //массив для хранения id завершаемых процессов
                    List<int> vID = new List<int>();

                    //
                    Console.WriteLine("-- Список завершаемых процессов --");
                    foreach (Process process in vKillProcesses)
                    {
                        //вывод ID и имени текущего процесса
                        Console.WriteLine("ID: {0,-6} | Name: {1,-20}", process.Id, process.ProcessName);

                        //зполнение массива vID
                        vID.Add(process.Id);

                        //остановка текущего процесса
                        process.Kill();

                        //проверка завершения текущего процесса
                        if (!process.HasExited) { bAllProcessKill = false; break; }
                    }

                    //проверка завершения всех процессов с именем sNameKillProcess
                    if (bAllProcessKill) //все процессы остановлены
                    {
                        Console.WriteLine($"Процесс c именем {sNameKillProcess} (ID = {string.Join(" ", vID)}) завершен!");
                    }
                    else
                    {
                        Console.WriteLine($"Процесс c именем {sNameKillProcess} не завершен!");
                    }
                }
                else
                {
                    Console.WriteLine($"Завершаемый процесс с именем {sNameKillProcess} отсутствует");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникло исключение: {ex.Message}");
            }
        }

        //метод: завершить процесс по id
        static void KillProcessByID()
        {
            try
            {
                //считать id завершаемого процесса
                Console.Write("Введите ID завершаемого процесса : ");
                int idKillProcess = Convert.ToInt32(Console.ReadLine());

                //проверка наличия введенного id в процессах
                bool bExistIdProcess = false;
                foreach (Process process in Process.GetProcesses())
                {
                    if (process.Id == idKillProcess) { bExistIdProcess = true; break; }
                }

                if (bExistIdProcess) //введенный id в процессах присутствует 
                {
                    //получаем процесс по id и завершаем его
                    Process killProcess = Process.GetProcessById(idKillProcess);
                    killProcess.Kill();

                    //проверка завершения процесса
                    string sStatusProcess = (killProcess.HasExited) ? "завершен" : "не завершен";

                    //вывод результата
                    Console.WriteLine($"Процесс c именем {killProcess.ProcessName} {sStatusProcess} " +
                                      $"по ID = {killProcess.Id}");
                }
                else
                {
                    Console.WriteLine($"Процесс с ID = {idKillProcess} отсутствует");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникло исключение: {ex.Message}");
            }
        }

        //вывести меню
        static void ShowMenu()
        {
            Console.WriteLine("---------------- М Е Н Ю ----------------");
            Console.WriteLine("1 - Вывести список всех запущенных прроцессов");
            Console.WriteLine("2 - Завершить процесс по имени");
            Console.WriteLine("3 - Завершить процесс по ID");
            Console.WriteLine("4 - Выход из программы");
        }

        //получить корректный номер пункта меню
        static int GetNumberMenu()
        {
            int iMenu;

            //
            do
            {
                Console.Write("Введите номер меню от 1 до 4 : ");
                iMenu = Convert.ToInt32(Console.ReadLine());
                if (iMenu < 1 || iMenu > 12)
                    Console.WriteLine("Ошибка: введите число от 1 до 4");
                else
                    break;
            }
            while (true);

            //
            return iMenu;
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Урок 6, домашнее задание");

            //
            int iMenu;
            do
            {
                //работа с меню: вывод пунктов, выбор пункта
                ShowMenu();
                iMenu = GetNumberMenu();

                //выполнение действия
                switch (iMenu)
                {
                    case 1: PrintProcess(); break;
                    case 2: KillProcessByName(); break;
                    case 3: KillProcessByID(); break;
                    case 4: Console.WriteLine("Good Bye!!!"); break;
                }
            }
            while (iMenu != 4);

            //
            Console.ReadLine();
        }
    }
}