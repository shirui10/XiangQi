using System;
using Model;


namespace Control
{
    public class ProCon
    {
        static void Main(string[] args)
        {
        }

        public int turn = 0;

        public bool Turn(int X, int Y, int chozenX, int chozenY, Chess[,] Matrix)//在己方回合中不能选择对方棋子
        {
            if (turn == 0)
            {
                if (Matrix[chozenX, chozenY].side != Chess.Player.red)
                {
                    return false;
                }
                else
                {
                    bool check = Movechess(X, Y, chozenX, chozenY, Matrix);
                    if (check == true)
                    {
                        turn = 1;
                        return true;
                    }
                    else return false;
                }
            }
            else if (turn == 1)
            {
                if (Matrix[chozenX, chozenY].side != Chess.Player.blue)
                {
                    return false;
                }
                else
                {
                    bool check = Movechess(X, Y, chozenX, chozenY, Matrix);
                    if (check == true)
                    {
                        turn = 0;
                        return true;
                    }
                    else return false;
                }
            }
            else return false;
        }


        public bool Result(Chess[,] Matrix)       //判断结果
        {
            int n = 0;
            bool result = true;

            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    if (Matrix[i, j].type == Chess.Piecetype.jiang)
                    {
                        n++;
                    }
                }
            }

            if (n == 2)
            {
                return result;
            }
            else
            {
                result = false;
                return result;
            }
        }


        public int Checkpiece(int chozenX, int chozenY, Chess[,] Matrix)//检测选中的是否棋子
        {
            if (Matrix[chozenX, chozenY].type == Chess.Piecetype.god)
            {
                return 0;
            }
            else if (turn == 0)
            {
                if (Matrix[chozenX, chozenY].side != Chess.Player.red)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            else if (turn == 1)
            {
                if (Matrix[chozenX, chozenY].side != Chess.Player.blue)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }

            return 0;
        }


        public void Setmove(int X, int Y, int chozenX, int chozenY, Chess[,] Matrix)       //基本移动方式
        {
            Matrix[X, Y].side = Matrix[chozenX, chozenY].side;
            Matrix[X, Y].type = Matrix[chozenX, chozenY].type;
            Matrix[chozenX, chozenY].side = Chess.Player.god;
            Matrix[chozenX, chozenY].type = Chess.Piecetype.god;
        }


        public bool Movechess(int X, int Y, int chozenX, int chozenY, Chess[,] Matrix)       //定义每种棋子的移动方式
        {
            Rook che = new Rook();
            Horse ma = new Horse();
            Elephant xiang = new Elephant();
            Advisor shi = new Advisor();
            General jiang = new General();
            Cannon pao = new Cannon();
            Soldier bing = new Soldier();
            bool re;

            switch (Matrix[chozenX, chozenY].type)
            {
                case Chess.Piecetype.che:
                    re = che.Che(X, Y, chozenX, chozenY, Matrix);
                    return re;
                case Chess.Piecetype.ma:
                    re = ma.Ma(X, Y, chozenX, chozenY, Matrix);
                    return re;
                case Chess.Piecetype.xiang:
                    re = xiang.Xiang(X, Y, chozenX, chozenY, Matrix);
                    return re;
                case Chess.Piecetype.shi:
                    re = shi.Shi(X, Y, chozenX, chozenY, Matrix);
                    return re;
                case Chess.Piecetype.jiang:
                    re = jiang.Jiang(X, Y, chozenX, chozenY, Matrix);
                    return re;
                case Chess.Piecetype.pao:
                    re = pao.Pao(X, Y, chozenX, chozenY, Matrix);
                    return re;
                case Chess.Piecetype.bing:
                    re = bing.Bing(X, Y, chozenX, chozenY, Matrix);
                    return re;
            }

            return false;
        }


        public Chess[,] Road(int chozenX, int chozenY, Chess[,] Matrix)      // 使棋子显示可行路径时，确认移动前棋子不移动
        {
            ProMod mod = new ProMod();
            Chess[,] road = mod.Setroad();
            Chess[,] trans = new Chess[19, 17];
            bool cr;

            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    trans[i, j] = new Chess();
                }
            }

            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    if (i % 2 == 0)
                    {
                        trans[i, j].side = Matrix[i, j].side;
                        trans[i, j].type = Matrix[i, j].type;
                        trans[chozenX, chozenY].side = Matrix[chozenX, chozenY].side;
                        trans[chozenX, chozenY].type = Matrix[chozenX, chozenY].type;
                        cr = Movechess(i, j, chozenX, chozenY, Matrix);

                        if (cr == true)
                        {
                            road[i, j].path = Chess.Piecepath.yes;
                        }

                        Matrix[i, j].side = trans[i, j].side;
                        Matrix[i, j].type = trans[i, j].type;
                        Matrix[chozenX, chozenY].side = trans[chozenX, chozenY].side;
                        Matrix[chozenX, chozenY].type = trans[chozenX, chozenY].type;
                    }
                }
            }

            return road;
        }
    }
}