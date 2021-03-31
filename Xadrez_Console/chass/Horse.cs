using board;

namespace chass
{
    class Horse : Piece
    {
        public Horse(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "C";
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

            pos.defineValues(position.line - 1, position.colum - 2);
            if(board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            pos.defineValues(position.line - 2, position.colum - 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            pos.defineValues(position.line - 2, position.colum + 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            pos.defineValues(position.line - 1, position.colum + 2);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            pos.defineValues(position.line + 1, position.colum + 2);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            pos.defineValues(position.line +2, position.colum + 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            pos.defineValues(position.line + 2, position.colum - 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            pos.defineValues(position.line + 1, position.colum - 2);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            return mat;
        }
    }
}
