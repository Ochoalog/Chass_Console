using board;

namespace chass
{
    class Pawn : Piece
    {
        private ChassMatch match;

        public Pawn(Board board, Color color, ChassMatch match) : base(board, color)
        {
            this.match = match;
        }        
        public override string ToString()
        {
            return "P";
        }

        private bool existEnemy(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p.color == Color.Black;
        }

        private bool free(Position pos)
        {
            return board.piece(pos) == null;
        }

        public override bool[,] possiblesMoviments()
        {
            bool[,] mat = new bool[board.Lines, board.Colums];

            Position pos = new Position(0, 0);

            if(color == Color.White)
            {
                pos.defineValues(position.line - 1, position.colum);
                if(board.positionValid(pos) && free(pos))
                {
                    mat[pos.line, pos.colum] = true;
                }

                pos.defineValues(position.line - 2, position.colum);
                if (board.positionValid(pos) && free(pos) && qtyMoviments == 0)
                {
                    mat[pos.line, pos.colum] = true;
                }

                pos.defineValues(position.line - 1, position.colum - 1);
                if (board.positionValid(pos) && existEnemy(pos))
                {
                    mat[pos.line, pos.colum] = true;
                }

                pos.defineValues(position.line - 1, position.colum + 1);
                if (board.positionValid(pos) && existEnemy(pos))
                {
                    mat[pos.line, pos.colum] = true;
                }

                //#jogadaespecial en passant
                if(position.line == 3)
                {
                    Position left = new Position(position.line, position.colum - 1);
                    if (board.positionValid(left) && existEnemy(left) && board.piece(left) == match.vulnerableEnPassant)
                    {
                        mat[left.line - 1, left.colum] = true;
                    }

                    Position right = new Position(position.line, position.colum + 1);
                    if (board.positionValid(right) && existEnemy(right) && board.piece(right) == match.vulnerableEnPassant)
                    {
                        mat[right.line - 1, right.colum] = true;
                    }
                }
            }

            else
            {
                pos.defineValues(position.line + 1, position.colum);
                if (board.positionValid(pos) && free(pos))
                {
                    mat[pos.line, pos.colum] = true;
                }

                pos.defineValues(position.line + 2, position.colum);
                if (board.positionValid(pos) && free(pos) && qtyMoviments == 0)
                {
                    mat[pos.line, pos.colum] = true;
                }

                pos.defineValues(position.line + 1, position.colum - 1);
                if (board.positionValid(pos) && existEnemy(pos))
                {
                    mat[pos.line, pos.colum] = true;
                }

                pos.defineValues(position.line + 1, position.colum + 1);
                if (board.positionValid(pos) && existEnemy(pos))
                {
                    mat[pos.line, pos.colum] = true;
                }

                if (position.line == 4)
                {
                    Position left = new Position(position.line, position.colum - 1);
                    if (board.positionValid(left) && existEnemy(left) && board.piece(left) == match.vulnerableEnPassant)
                    {
                        mat[left.line + 1, left.colum] = true;
                    }

                    Position right = new Position(position.line, position.colum + 1);
                    if (board.positionValid(right) && existEnemy(right) && board.piece(right) == match.vulnerableEnPassant)
                    {
                        mat[right.line + 1, right.colum] = true;
                    }
                }
            }

            return mat;
        }
    }
}
