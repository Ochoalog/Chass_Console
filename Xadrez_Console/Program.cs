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
                    try
                    {
                        Console.Clear();
                        Screen.printMatch(match);
                        
                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Position origin = Screen.readPositionChass().toPosition();
                        match.validPositionOrigin(origin);

                        bool[,] possiblesPositions = match.board.piece(origin).possiblesMoviments();

                        Console.Clear();
                        Screen.printBoard(match.board, possiblesPositions);

                        Console.WriteLine("+=+=+=+=+=+=+");
                        Console.Write("Destino: ");
                        Position arrived = Screen.readPositionChass().toPosition();
                        match.validPositionArrived(origin, arrived);

                        match.Move(origin, arrived);
                    }
                    catch(BoardException e)
                    {
                        Console.WriteLine(e.Message + " Enter para repetir a jogada.");
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.printMatch(match);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            } 
            Console.ReadLine();
            
        }
    }
}
