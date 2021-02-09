using System;

namespace Lecture7
{
    class Program
    {
        static void Main(string[] args)
        {
            bool forever = true;
            do
            {
                Menu();
                int choice = Int32.Parse(Console.ReadLine());
                int level = 1;
                switch (choice)
                {

                    case 1:
                        {
                            P1Play(level);
                            break;
                        }
                    case 2:
                        {
                            level = Settings();
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Автор - Пётр Григорук");
                            forever = AskToGo(); 
                            break;
                        }
                    case 4:
                        {
                            forever = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Неправильный ввод");
                            break;
                        }
                }
            }
            while (forever);

        }

        static public void MapGen(int rows, int cols, char[,] Table, int x_p, int y_p)
        {
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {

                for (int colIndex = 0; colIndex < cols; colIndex++)
                {

                    Table[rowIndex, colIndex] = '_';

                    Table[x_p, y_p] = 'P';

                    if (rowIndex == rows - 1 && colIndex == cols - 1)
                    {
                        Table[rowIndex, colIndex] = 'F';
                    }
                }
            }
        }

        static public void BombGen(int rows, int cols, char[,] Bombs, char[,] Table, int level)
        {
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < cols; colIndex++)
                {
                    if (Table[rowIndex, colIndex] != 'P' && Table[rowIndex, colIndex] != 'F')
                    {
                        var random_num = new Random();
                        int diapason = 100;
                        if(level == 1)
                        {
                            diapason = 100;
                        }
                        else if(level == 2)
                        {
                            diapason = 90;
                        }
                        else if(level == 3)
                        {
                            diapason = 80;
                        }
                        int decision = random_num.Next(0, diapason);
                        if (decision < 3)
                        {
                            Bombs[rowIndex, colIndex] = '*';
                        }
                        else if (decision >= 3)
                        {
                            Bombs[rowIndex, colIndex] = '_';
                        }
                    }
                }
            }
        }

        static public void Print(char[,] Table, int rows, int cols)
        {
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {

                for (int colIndex = 0; colIndex < cols; colIndex++)
                {
                    Console.Write(Table[rowIndex, colIndex]);
                }
                Console.WriteLine();
            }

        }

        static public void P1Play(int level)
        {
            Console.WriteLine("rows:");
            int rows = int.Parse(Console.ReadLine());
            Console.WriteLine("cols:");
            int cols = int.Parse(Console.ReadLine());


            char[,] Table = new char[rows, cols];
            char[,] Bombs = new char[rows, cols];
            int x_p = 0;
            int y_p = 0;

            ///START POSITION
            MapGen(rows, cols, Table, x_p, y_p);


            ///Bomb location
            BombGen(rows, cols, Bombs, Table, level);

            //map print
            Print(Table, rows, cols);
            Console.WriteLine();

            //bomb print
            Print(Bombs, rows, cols);

            //key read
            ConsoleKeyInfo key;
            do
            {
                if (x_p == cols - 1 && y_p == rows - 1)
                {
                    Console.WriteLine("You WIN!!!");
                    break;
                }


                key = Console.ReadKey();
                if ((int)key.Key == 37)
                {
                    if (x_p > 0)
                    {
                        x_p--;

                    }
                }
                else if ((int)key.Key == 39)
                {
                    if (x_p < cols - 1)
                    {
                        x_p++;
                    }
                }
                else if ((int)key.Key == 38)
                {
                    if (y_p > 0)
                    {
                        y_p--;
                    }
                }
                else if ((int)key.Key == 40)
                {
                    if (y_p < rows - 1)
                    {
                        y_p++;
                    }
                }

                Console.Clear();

                for (int rowIndex = 0; rowIndex < rows; rowIndex++)
                {

                    for (int colIndex = 0; colIndex < cols; colIndex++)
                    {

                        Table[rowIndex, colIndex] = '_';

                        Table[y_p, x_p] = 'P';

                        if (rowIndex == rows - 1 && colIndex == cols - 1)
                        {
                            Table[rowIndex, colIndex] = 'F';
                        }

                        if (Bombs[rowIndex, colIndex] == '*' && Table[rowIndex, colIndex] == 'P')
                        {
                            Table[y_p, x_p] = '_';
                            Table[rowIndex, colIndex] = '*';
                            Console.WriteLine("You loose");

                            break;
                        }
                    }
                }
                Print(Table, rows, cols);
            }
            while (key.Key != ConsoleKey.Escape);

        }

        static public int Settings()
        {
            Console.WriteLine("Выбери уровень сложности 1-Easy 2-Medium 3-Hard");

            int level = Int32.Parse(Console.ReadLine());
            if (level == 1 || level == 2 || level == 3)
            {
                return level;
            }
            else
            {
                Settings();
            }
            return 1;
        }
        static public void Menu()
        {
            Console.WriteLine("1.Играть");
            Console.WriteLine("2.Настройки");
            Console.WriteLine("3.О программе");
            Console.WriteLine("4.Выход");
        }

        static public bool AskToGo()
        {
            Console.WriteLine("Go? 1. Дальше 0. Выход из программы");
            int go = Int32.Parse(Console.ReadLine());
            if(go == 1)
            {
                return true;
            }
            return false;
        }
    }
}
