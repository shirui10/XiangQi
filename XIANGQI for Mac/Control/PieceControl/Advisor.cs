using System;
using Model;

namespace Control
{
    class Advisor
    {
        public bool Shi(int X, int Y, int chozenX, int chozenY, Chess[,] Matrix)
        {
            ProCon con = new ProCon();

            if (Matrix[chozenX, chozenY].side == Chess.Player.blue)
            {
                if (Y < 6 || Y > 10 || X > 4)
                {
                    return false;
                }
            }
            else
            {
                if (Y < 6 || Y > 10 || X < 14)
                {
                    return false;
                }
            }

            if (Math.Abs(X - chozenX) != 2 || Math.Abs(chozenY - Y) != 2)
            {
                return false;
            }

            if (Matrix[chozenX, chozenY].side == Matrix[X, Y].side)
            {
                return false;
            }

            con.Setmove(X, Y, chozenX, chozenY, Matrix);

            return true;
        }
    }
}