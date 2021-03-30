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
                Board board = new Board(8, 8);
                board.putPiece(new Tower(board, Color.White), new Position(0, 0));
                board.putPiece(new Tower(board, Color.Black), new Position(1, 3));
                board.putPiece(new King(board, Color.Black), new Position(2, 4));
                board.putPiece(new Tower(board, Color.White), new Position(0, 2));

                Screen.printBoard(board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            } 
            Console.ReadLine();
            
        }
    }
}
