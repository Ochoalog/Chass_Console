using System;
using board;
namespace chass
{
    class ChassMatch
    {
        public Board board { get; private set; }
        public int Shifit { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finish { get; private set; }
        public ChassMatch()
        {
            board = new Board(8, 8);
            Shifit = 1;
            CurrentPlayer = Color.White;
            Finish = false;
            putPieces();
        }

        public void executeMoviment(Position origin, Position arrived)
        {
            
            Piece p = board.removePiece(origin);
            p.incrementQtyMoviments();
            Piece pieceCaptured = board.removePiece(arrived);
            board.putPiece(p, arrived);
        }

        public void Move(Position origin, Position arrived)
        {
            executeMoviment(origin, arrived);
            Shifit++;
            changePlayer();
        }

        public void validPositionOrigin(Position pos)
        {
            if(board.piece(pos) == null)
            {
                throw new BoardException("Não existe peça na posição de origem escolhida.");
            }
            if(CurrentPlayer != board.piece(pos).color)
            {
                throw new BoardException("A peça de origem escolhida não é sua!");
            }
            if (!board.piece(pos).existpossiblesMoviments())
            {
                throw new BoardException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        private void changePlayer()
        {
            if(CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        private void putPieces()
        {
            board.putPiece(new Tower(board, Color.White), new PositionChass('a', 1).toPosition());
            board.putPiece(new Tower(board, Color.White), new PositionChass('h', 1).toPosition());
            board.putPiece(new King(board, Color.White), new PositionChass('e', 1).toPosition());


            board.putPiece(new Tower(board, Color.Black), new PositionChass('a', 8).toPosition());
            board.putPiece(new Tower(board, Color.Black), new PositionChass('h', 8).toPosition());
            board.putPiece(new King(board, Color.Black), new PositionChass('e', 8).toPosition());
        }
    }
}
