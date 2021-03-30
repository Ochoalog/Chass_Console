using System;
using board;

namespace Chass_Console
{
    class Screen
    {
        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                for (int j = 0; j < board.Colums; j++)
                {
                    if(board.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    } else
                    {
                        Console.Write(board.piece(i, j) + " ");
                    }                                        
                }
                Console.WriteLine();
            }
        }
    }
}
