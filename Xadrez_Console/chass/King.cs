using board;

namespace chass
{
    class King : Piece
    {
        private ChassMatch match;
        
        public King(Board board, Color color, ChassMatch match) : base(board, color)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        private bool testTowerRoque(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p is Tower && p.color == color && p.qtyMoviments == 0;
        }

        public override bool[,] possiblesMoviments()
        {
            bool[,] mat = new bool[board.Lines, board.Colums];

            Position pos = new Position(0, 0);

            //acima
            pos.defineValues(position.line - 1, position.colum);
            if(board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            
            //nordeste
            pos.defineValues(position.line - 1, position.colum + 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            //direita
            pos.defineValues(position.line, position.colum + 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            //sudeste
            pos.defineValues(position.line + 1, position.colum + 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            //abaixo
            pos.defineValues(position.line + 1, position.colum);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            //sudoeste
            pos.defineValues(position.line + 1, position.colum - 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            //esquerda
            pos.defineValues(position.line, position.colum - 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            //noroeste
            pos.defineValues(position.line - 1, position.colum - 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            //#jogadaespecial roque
            if(qtyMoviments == 0 && !match.xeque)
            {
                //roque pequeno
                Position posT1 = new Position(position.line, position.colum + 3);
                if (testTowerRoque(posT1))
                {
                    Position p1 = new Position(position.line, position.colum + 1);
                    Position p2 = new Position(position.line, position.colum + 2);
                    if(board.piece(p1) == null && board.piece(p2) == null)
                    {
                        mat[position.line, position.colum + 2] = true;
                    }
                }

                //roque grande
                Position posT2 = new Position(position.line, position.colum - 4);
                if (testTowerRoque(posT2))
                {
                    Position p1 = new Position(position.line, position.colum - 1);
                    Position p2 = new Position(position.line, position.colum - 2);
                    Position p3 = new Position(position.line, position.colum - 3);
                    if (board.piece(p1) == null && board.piece(p2) == null && board.piece(p3) == null)
                    {
                        mat[position.line, position.colum + 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}
