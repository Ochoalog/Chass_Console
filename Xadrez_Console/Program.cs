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

                while(!match.Finish)
                {
                    Console.Clear();
                    Screen.printBoard(match.board);

                    Console.WriteLine("+=+=+=+=+=+=+=+=+");
                    Console.Write("Origem: ");
                    Position origin = Screen.readPositionChass().toPosition();

                    bool[,] possiblesPositions = match.board.piece(origin).possiblesMoviments();

                    Console.Clear();
                    Screen.printBoard(match.board, possiblesPositions);

                    Console.WriteLine("+=+=+=+=+=+=+");
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
