using board;
namespace chass
{
    class PositionChass
    {
        public char Colum { get; set; }
        public int Line { get; set; }

        public PositionChass(char colum, int line)
        {
            Colum = colum;
            this.Line = line;
        }

        public Position toPosition()
        {
            return new Position(8 - Line, Colum - 'a');
        }

        public override string ToString()
        {
            return "" + Colum + Line;
        }
    }
}
