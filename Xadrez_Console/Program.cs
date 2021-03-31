using System;
using board;
using chass;

namespace Chass_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                PositionChass pos = new PositionChass('c', 7);

                Console.WriteLine(pos);

                Console.WriteLine(pos.toPosition());
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            } 
            Console.ReadLine();
            
        }
    }
}
