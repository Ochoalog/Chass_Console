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
                ChassMatch match = new ChassMatch();
                
                while (!match.Finish)
                {
                    Console.Clear();
                    Screen.printBoard(match.Board);

                    Console.WriteLine("==========================================");
                    Console.Write("Origem: ");
                    Position origin = Screen.readPositionChass().toPosition();
                    Console.Write("Destino: ");
                    Position arrived = Screen.readPositionChass().toPosition();


                    match.executeMoviment(origin, arrived);
                }

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            } 
            Console.ReadLine();
            
        }
    }
}
