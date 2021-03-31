using System.Collections.Generic;
using board;
namespace chass
{
    class ChassMatch
    {
        public Board board { get; private set; }
        public int Shifit { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finish { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captureds;
        public ChassMatch()
        {
            board = new Board(8, 8);
            Shifit = 1;
            CurrentPlayer = Color.White;
            Finish = false;
            pieces = new HashSet<Piece>();
            captureds = new HashSet<Piece>();
            putPieces();
        }

        public void executeMoviment(Position origin, Position arrived)
        {
            
            Piece p = board.removePiece(origin);
            p.incrementQtyMoviments();
            Piece pieceCaptured = board.removePiece(arrived);
            board.putPiece(p, arrived);
            if(pieceCaptured != null)
            {
                captureds.Add(pieceCaptured);
            }
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

        public void validPositionArrived(Position origin, Position arrived)
        {
            if (!board.piece(origin).canGo(arrived))
            {
                throw new BoardException("Posição de destino inválida!");
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

        public HashSet<Piece> piecesCaptured(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captureds)
            {
                if(x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> piecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(piecesCaptured(color));
            return aux;
        }

        public void putNewPiece(char colum, int line, Piece piece)
        {
            board.putPiece(piece, new PositionChass(colum, line).toPosition());
            pieces.Add(piece);
        }
        
        private void putPieces()
        {
            putNewPiece('a', 1, new Tower(board, Color.White));
            putNewPiece('h', 1, new Tower(board, Color.White));
            putNewPiece('c', 1, new Tower(board, Color.White));
            putNewPiece('c', 2, new Tower(board, Color.White));
            putNewPiece('d', 2, new Tower(board, Color.White));
            putNewPiece('e', 2, new Tower(board, Color.White));
            putNewPiece('e', 1, new Tower(board, Color.White));
            putNewPiece('d', 1, new King(board, Color.White));

            putNewPiece('a', 8, new Tower(board, Color.Black));
            putNewPiece('h', 8, new Tower(board, Color.Black));
            putNewPiece('c', 8, new Tower(board, Color.Black));
            putNewPiece('c', 7, new Tower(board, Color.Black));
            putNewPiece('d', 7, new Tower(board, Color.Black));
            putNewPiece('e', 7, new Tower(board, Color.Black));
            putNewPiece('e', 8, new Tower(board, Color.Black));
            putNewPiece('d', 8, new King(board, Color.Black));
        }
    }
}
