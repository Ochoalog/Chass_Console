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
        public bool xeque { get; private set; }

        public ChassMatch()
        {
            board = new Board(8, 8);
            Shifit = 1;
            CurrentPlayer = Color.White;
            Finish = false;
            xeque = false;
            pieces = new HashSet<Piece>();
            captureds = new HashSet<Piece>();
            putPieces();
        }

        public Piece executeMoviment(Position origin, Position arrived)
        {
            
            Piece p = board.removePiece(origin);
            p.incrementQtyMoviments();
            Piece pieceCaptured = board.removePiece(arrived);
            board.putPiece(p, arrived);
            if(pieceCaptured != null)
            {
                captureds.Add(pieceCaptured);
            }
            return pieceCaptured;
        }

        public void unmakeMoviment(Position origin, Position arrived, Piece pieceCaptured)
        {
            Piece p = board.removePiece(arrived);
            p.decrementQtyMoviment();
            if(pieceCaptured != null)
            {
                board.putPiece(pieceCaptured, arrived);
                captureds.Remove(pieceCaptured);
            }
            board.putPiece(p, origin);
        }
        
        
        public void Move(Position origin, Position arrived)
        {
            Piece pieceCaptured = executeMoviment(origin, arrived);

            if (isXeque(CurrentPlayer))
            {
                unmakeMoviment(origin, arrived, pieceCaptured);
                throw new BoardException("Você não pode se colocar em xeque!");
            }

            if (isXeque(adversary(CurrentPlayer)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

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

        private Color adversary(Color color)
        {
            if(color == Color.White)
            {
                return Color.Black;
            } else
            {
                return Color.White;
            }
        }

        private Piece king(Color color)
        {
            foreach (Piece x in piecesInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool isXeque(Color color)
        {
            Piece K = king(color);
            if(K == null)
            {
                throw new BoardException("Não tem rei da cor" + color + " no tabuleiro!");
            }
            foreach(Piece x in piecesInGame(adversary(color)))
            {
                bool[,] mat = x.possiblesMoviments();
                if(mat[K.position.line, K.position.colum])
                {
                    return true;
                }
            }
            return false;
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
