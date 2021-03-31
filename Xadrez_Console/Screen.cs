using System;
using board;
using chass;

namespace Chass_Console
{
    class Screen
    {
        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Colums; j++)
                {
                    if(board.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    } else
                    {
                        printPiece(board.piece(i, j));
                        Console.Write(" ");
                    }                                        
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static PositionChass readPositionChass()
        {
            String s = Console.ReadLine();
            char colum = s[0];
            int line = int.Parse(s[1] + "");
            return new PositionChass(colum, line);
        }

        public static void printPiece(Piece piece)
        {
            if(piece.color == Color.White)
            {
                Console.Write(piece);
            } 
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}
