using board;

namespace chass
{
    class Tower : Piece
    {
        public Tower(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "T";
        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] possiblesMoviments()
        {
            bool[,] mat = new bool[board.Lines, board.Colums];

            Position pos = new Position(0, 0);

            //acima
            pos.defineValues(position.line - 1, position.colum);
            while(board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.line = pos.line - 1;
            }

            //abaixo
            pos.defineValues(position.line + 1, position.colum);
            while (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.line = pos.line + 1;
            }

            //direita
            pos.defineValues(position.line, position.colum + 1);
            while (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.colum = pos.colum + 1;
            }

            //esquerda
            pos.defineValues(position.line, position.colum - 1);
            while (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.colum = pos.colum - 1;
            }

            return mat;
        }
    }
}