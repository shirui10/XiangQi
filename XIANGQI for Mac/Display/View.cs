using System;
using Model;
using Control;

namespace View
{
    public class ProgramView
    {
        static void Main(string[] args)
        {
            ProCon con = new ProCon();
            ProMod mod = new ProMod();
            ProgramView view = new ProgramView();
            Chess[,] Matrix = mod.Resetground();
            Chess[,] road = mod.Setroad();
            bool result = true;
            bool turn;
            int player = 0;

            while (result == true)
            {
                string[,] Board = mod.Piece(Matrix);
                view.Displaying(Matrix, road);
                view.Start(player);

                try
                {
                    Console.Write("============================================\n");
                    Console.Write("                   X = ");
                    int chozenX = Convert.ToInt32(Console.ReadLine());
                    Console.Write("                   Y = ");
                    int chozenY1 = Convert.ToInt32(Console.ReadLine());
                    int chozenY = chozenY1 * 2;
                    int checkpiece = con.Checkpiece(chozenX * 2, chozenY, Matrix);

                    if (checkpiece == 2)//检测是否有棋子
                    {
                        road = con.Road(chozenX * 2, chozenY, Matrix);
                        view.Displaying(Matrix, road);
                        road = mod.Setroad();
                        view.Check(checkpiece, Board, chozenX, chozenY);
                        Console.Write("                   X = ");
                        int X = Convert.ToInt32(Console.ReadLine());
                        Console.Write("                   Y = ");
                        int Y = Convert.ToInt32(Console.ReadLine());
                        turn = con.Turn(X * 2, Y * 2, chozenX * 2, chozenY, Matrix);
                        player = view.Move(turn, player);
                        result = con.Result(Matrix);
                    }
                    else if (checkpiece == 0)
                    {
                        view.Check(checkpiece, Board, chozenX, chozenY);
                    }
                    else
                    {
                        view.Check(checkpiece, Board, chozenX, chozenY);
                    }
                }

                catch (Exception)
                {
                    view.Wrong();
                }
            }

            view.Displaying(Matrix, road);
            view.Win(player);
            Console.ReadKey();
        }


        public void Start(int start)
        {
            if (start % 2 == 0)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n");
                Console.Write("                   Hello!                   \n");
                Console.Write("                    RED                     \n");
                Console.Write("       Please set your position(X & Y):     \n");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\n");
                Console.Write("                  Hello!                     \n");
                Console.Write("                  BLACK                      \n");
                Console.Write("       Please set your position(X & Y):      \n");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }


        public int Move(bool turn, int player)
        {
            if (turn == false)
            {
                Console.Write(" You cannot move there and set again please! ");
                Console.ReadLine();
            }
            else
            {
                Console.Write("               Move successly!               ");
                Console.ReadLine();
                player++;
            }

            return player;
        }


        public void Check(int checkpiece, string[,] Board, int chozenX, int chozenY)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;

            if (checkpiece == 2)
            {
                Console.Write("\n        You have the piece: " + Board[chozenX * 2, chozenY] + "(" + chozenX + "," + chozenY / 2 + ").\n");
                Console.Write("  Please set your position of moving(X & Y):\n");
                Console.Write("============================================\n");
            }
            else if (checkpiece == 0)
            {
                Console.Write("              There is no piece.            \n");
                Console.ReadLine();
            }
            else
            {
                Console.Write("             You cannot choose it.          \n");
                Console.ReadLine();
            }
        }


        public void Wrong()
        {
            Console.Write("  You cannot set this and set again please! ");
            Console.ReadLine();
        }


        public void Win(int player)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;

            if (player % 2 == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n============================================\n");
                Console.Write("                  RED WIN!!                   ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\n============================================\n");
                Console.Write("                 BLACK WIN!!                  ");
            }
        }


        public void Displaying(Chess[,] Matrix, Chess[,] road)
        {
            ProMod Mod = new ProMod();
            string[,] Board = Mod.Piece(Matrix);
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" X\\Y ");

            for (int j = 0; j <= 17; j++)
            {
                if (j == 0)
                {
                    Console.Write(j + "   ");
                }
                else if (j == 16)
                {
                    Console.Write(j / 2 + "      ");
                }
                else if (j % 2 == 0 && j > 0)
                {
                    Console.Write(j / 2 + "   ");
                }
            }

            Console.Write("\n");

            for (int i = 0; i <= 18; i++)//把棋盘和辅助坐标打印出来
            {
                //Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkGray;

                if (i % 2 == 0)
                {
                    Console.Write("  " + i / 2 + "  ");
                }
                else
                {
                    Console.Write("     ");
                }
                //Console.BackgroundColor = ConsoleColor.Yellow;

                for (int j = 0; j < 17; j++)
                {
                    if (Matrix[i, j].side == Chess.Player.blue)
                    {
                        if (road[i, j].path == Chess.Piecepath.yes)
                        {
                            Colorgreen(Board, i, j);
                            road[i, j].path = Chess.Piecepath.not;
                        }
                        else
                        {
                            Coloryellow(Matrix, Board, i, j);
                        }
                    }
                    else if (Matrix[i, j].side == Chess.Player.red)
                    {
                        if (road[i, j].path == Chess.Piecepath.yes)
                        {
                            Colorgreen(Board, i, j);
                            road[i, j].path = Chess.Piecepath.not;
                        }
                        else
                        {
                            Coloryellow(Matrix, Board, i, j);
                        }
                    }
                    else
                    {
                        if (road[i, j].path == Chess.Piecepath.yes)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write(Board[i, j]);
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            road[i, j].path = Chess.Piecepath.not;
                        }
                        else
                        {
                            Console.Write(Board[i, j]);
                        }
                    }
                }
               // Console.BackgroundColor = ConsoleColor.Black;
                if (i % 2 == 0)
                {
                    Console.Write("  " + i / 2 + "  ");
                }
                else
                {
                    Console.Write("     ");
                }
                Console.Write("\n");
            }

            //Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("     ");

            for (int j = 0; j <= 17; j++)
            {
                if (j == 0)
                {
                    Console.Write(j + "   ");
                }
                else if (j == 16)
                {
                    Console.Write(j / 2 + "      ");
                }
                else if (j % 2 == 0 && j > 0)
                {
                    Console.Write(j / 2 + "   ");
                }
            }
        }


        public void Coloryellow(Chess[,] Matrix, string[,] Board, int i, int j)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;

            if (Matrix[i, j].side == Chess.Player.red)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (Matrix[i, j].side == Chess.Player.blue)
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.Write(Board[i, j]);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            //Console.BackgroundColor = ConsoleColor.Yellow;
        }


        public void Colorgreen(string[,] Board, int i, int j)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(Board[i, j]);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.Yellow;
        }

    }
}
