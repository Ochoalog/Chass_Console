using System;
using board;
using chass;
using System.Collections.Generic;

namespace Chass_Console
{
    class Screen
    {
        
        public static void printMatch(ChassMatch match)
        {
            printBoard(match.board);
            Console.WriteLine();
            printPiecesCaptureds(match);
            Console.WriteLine("+=+=+=+=+=+=+=+=+");
            Console.WriteLine("Turno: " + match.Shifit);
            Console.WriteLine("Aguardando jogada: " + match.CurrentPlayer);
        }

        public static void printPiecesCaptureds(ChassMatch match)
        {
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            printCollection(match.piecesCaptured(Color.White));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            printCollection(match.piecesCaptured(Color.Black));
            Console.ForegroundColor = aux;
        }

        public static void printCollection(HashSet<Piece> collection)
        {
            Console.Write("[");
            foreach (Piece x in collection)
            {
                Console.Write(x + " ");
            }
            Console.WriteLine("]");
        }
        
        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Colums; j++)
                {
                    printPiece(board.piece(i, j));                                                         
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printBoard(Board board, bool[,] possiblesPositions)
        {
            ConsoleColor original = Console.BackgroundColor;
            ConsoleColor altered = ConsoleColor.DarkGray;
            
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Colums; j++)
                {
                    if(possiblesPositions[i, j])
                    {
                        Console.BackgroundColor = altered;
                    }
                    else
                    {
                        Console.BackgroundColor = original;
                    }
                    printPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = original;
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
            if(piece == null)
            {
                Console.Write("- ");
            } else
            { 
                if (piece.color == Color.White)
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
                Console.Write(" ");
            }
        }
    }
}
