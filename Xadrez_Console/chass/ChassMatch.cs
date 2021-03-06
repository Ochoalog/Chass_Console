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
        public Piece vulnerableEnPassant { get; private set; }

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

            //#jogadaespecial roque pequeno
            if(p is King && arrived.colum == origin.colum + 2)
            {
                Position originT = new Position(origin.line, origin.colum + 3);
                Position arrivedT = new Position(origin.line, origin.colum + 1);
                Piece T = board.removePiece(originT);
                T.incrementQtyMoviments();
                board.putPiece(T, arrivedT);
            }

            //#jogadaespecial roque grande
            if (p is King && arrived.colum == origin.colum - 2)
            {
                Position originT = new Position(origin.line, origin.colum - 4);
                Position arrivedT = new Position(origin.line, origin.colum - 1);
                Piece T = board.removePiece(originT);
                T.incrementQtyMoviments();
                board.putPiece(T, arrivedT);
            }

            //#jogadaespecial en passant
            if(p is Pawn)
            {
                if (origin.colum != arrived.colum && pieceCaptured == null)
                {
                    Position posP;
                    if(p.color == Color.White)
                    {
                        posP = new Position(arrived.line + 1, arrived.colum);
                    }
                    else
                    {
                        posP = new Position(arrived.line - 1, arrived.colum);
                    }
                    pieceCaptured = board.removePiece(posP);
                    captureds.Add(pieceCaptured);
                }
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

            //#jogadaespecial roque pequeno
            if (p is King && arrived.colum == origin.colum + 2)
            {
                Position originT = new Position(origin.line, origin.colum + 3);
                Position arrivedT = new Position(origin.line, origin.colum + 1);
                Piece T = board.removePiece(arrivedT);
                T.decrementQtyMoviment();
                board.putPiece(T, originT);
            }

            //#jogadaespecial roque grande
            if (p is King && arrived.colum == origin.colum - 2)
            {
                Position originT = new Position(origin.line, origin.colum - 4);
                Position arrivedT = new Position(origin.line, origin.colum - 1);
                Piece T = board.removePiece(arrivedT);
                T.decrementQtyMoviment();
                board.putPiece(T, originT);
            }

            //#jogadaespecial en passant
            if(p is Pawn)
            {
                if(origin.colum != arrived.colum && pieceCaptured == vulnerableEnPassant)
                {
                    Piece pawn = board.removePiece(arrived);
                    Position posP;
                    if(p.color == Color.White)
                    {
                        posP = new Position(3, arrived.colum);
                    }
                    else
                    {
                        posP = new Position(4, arrived.colum);
                    }
                    board.putPiece(pawn, posP);
                }
            }
        }
        
        
        public void Move(Position origin, Position arrived)
        {
            Piece pieceCaptured = executeMoviment(origin, arrived);

            if (isXeque(CurrentPlayer))
            {
                unmakeMoviment(origin, arrived, pieceCaptured);
                throw new BoardException("Você não pode se colocar em xeque!");
            }

            Piece p = board.piece(arrived);

            //jogadaespecial promocao
            if(p is Pawn)
            {
                if((p.color == Color.White && arrived.line == 0) || (p.color == Color.Black && arrived.line == 7))
                {
                    p.board.removePiece(arrived);
                    pieces.Remove(p);
                    Piece queen = new Queen(board, p.color);
                    board.putPiece(queen, arrived);
                    pieces.Add(queen);
                }
            }

            if (isXeque(adversary(CurrentPlayer)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if (testXequeMate(adversary(CurrentPlayer)))
            {
                Finish = true;
            }
            else
            {
                Shifit++;
                changePlayer();
            }
           
            //#jogadaespecial en passant
            if (p is Pawn && (arrived.line == origin.line - 2 || arrived.line == origin.line + 2))
            {
                vulnerableEnPassant = p;
            }
            else
            {
                vulnerableEnPassant = null;
            }

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

        public bool testXequeMate(Color color)
        {
            if (!isXeque(color))
            {
                return false;
            }
            foreach (Piece x in piecesInGame(color))
            {
                bool[,] mat = x.possiblesMoviments();
                for (int i = 0; i < board.Lines; i++)
                {
                    for (int j = 0; j < board.Colums; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.position;
                            Position arrived = new Position(i, j);
                            Piece pieceCaptured = executeMoviment(origin, arrived);
                            bool testXeque = isXeque(color);
                            unmakeMoviment(origin, arrived, pieceCaptured);
                            if (!testXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void putNewPiece(char colum, int line, Piece piece)
        {
            board.putPiece(piece, new PositionChass(colum, line).toPosition());
            pieces.Add(piece);
        }
        
        private void putPieces()
        {
            putNewPiece('a', 1, new Tower(board, Color.White));
            putNewPiece('b', 1, new Horse(board, Color.White));
            putNewPiece('c', 1, new Bishop(board, Color.White));
            putNewPiece('d', 1, new Queen(board, Color.White));
            putNewPiece('e', 1, new King(board, Color.White, this));
            putNewPiece('f', 1, new Bishop(board, Color.White));
            putNewPiece('g', 1, new Horse(board, Color.White));
            putNewPiece('h', 1, new Tower(board, Color.White));
            putNewPiece('a', 2, new Pawn(board, Color.White, this));
            putNewPiece('b', 2, new Pawn(board, Color.White, this));
            putNewPiece('c', 2, new Pawn(board, Color.White, this));
            putNewPiece('d', 2, new Pawn(board, Color.White, this));
            putNewPiece('e', 2, new Pawn(board, Color.White, this));
            putNewPiece('f', 2, new Pawn(board, Color.White, this));
            putNewPiece('g', 2, new Pawn(board, Color.White, this));
            putNewPiece('h', 2, new Pawn(board, Color.White, this));

            putNewPiece('a', 8, new Tower(board, Color.Black));
            putNewPiece('b', 8, new Horse(board, Color.Black));
            putNewPiece('c', 8, new Bishop(board, Color.Black));
            putNewPiece('d', 8, new Queen(board, Color.Black));
            putNewPiece('e', 8, new King(board, Color.Black, this));
            putNewPiece('f', 8, new Bishop(board, Color.Black));
            putNewPiece('g', 8, new Horse(board, Color.Black));
            putNewPiece('h', 8, new Tower(board, Color.Black));
            putNewPiece('a', 7, new Pawn(board, Color.Black, this));
            putNewPiece('b', 7, new Pawn(board, Color.Black, this));
            putNewPiece('c', 7, new Pawn(board, Color.Black, this));
            putNewPiece('d', 7, new Pawn(board, Color.Black, this));
            putNewPiece('e', 7, new Pawn(board, Color.Black, this));
            putNewPiece('f', 7, new Pawn(board, Color.Black,this));
            putNewPiece('g', 7, new Pawn(board, Color.Black, this));
            putNewPiece('h', 7, new Pawn(board, Color.Black, this));
        }
    }
}