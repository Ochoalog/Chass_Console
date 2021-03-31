using System;
using board;
namespace chass
{
    class ChassMatch
    {
        public Board board { get; private set; }
        private int Shifit;
        private Color CurrentPlayer;
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

        private void putPieces()
        {
            board.putPiece(new Tower(board, Color.White), new PositionChass('c', 1).toPosition());
            board.putPiece(new Tower(board, Color.White), new PositionChass('c', 2).toPosition());
            board.putPiece(new Tower(board, Color.White), new PositionChass('d', 2).toPosition());
            board.putPiece(new Tower(board, Color.White), new PositionChass('e', 2).toPosition());
            board.putPiece(new Tower(board, Color.White), new PositionChass('e', 1).toPosition());
            board.putPiece(new King(board, Color.White), new PositionChass('d', 1).toPosition());

            board.putPiece(new Tower(board, Color.Black), new PositionChass('c', 7).toPosition());
            board.putPiece(new Tower(board, Color.Black), new PositionChass('c', 8).toPosition());
            board.putPiece(new Tower(board, Color.Black), new PositionChass('d', 7).toPosition());
            board.putPiece(new Tower(board, Color.Black), new PositionChass('e', 7).toPosition());
            board.putPiece(new Tower(board, Color.Black), new PositionChass('e', 8).toPosition());
            board.putPiece(new King(board, Color.Black), new PositionChass('d', 8).toPosition());
        }
    }
}
