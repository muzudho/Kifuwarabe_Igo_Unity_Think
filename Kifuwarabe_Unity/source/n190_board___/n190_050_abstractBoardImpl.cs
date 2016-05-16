using n190_board___.AbstractBoard;



namespace n190_board___
{
    public class AbstractBoardImpl : AbstractBoard
    {
        public int ConvertToNode(int x, int y)
        {
            return y * 256 + x;
        }

        public void ConvertToXy(int& x, int& y, int node)
        {
            y = node / 256;
            x = node % 256;
        }

        public AbstractBoard()
        {

            this->size = 0;
        }

        ~AbstractBoard()
        {
        }

        public int NorthOf(int node)
        {
            return this->ValueOf(node + this->dir4[0]);
        }

        public int EastOf(int node)
        {
            return this->ValueOf(node + this->dir4[1]);
        }

        public int SouthOf(int node)
        {
            return this->ValueOf(node + this->dir4[2]);
        }

        public int WestOf(int node)
        {
            return this->ValueOf(node + this->dir4[3]);
        }

        public void SetSize(int size)
        {
            this->size = size;
        }

        public int GetSize()
        {
            return this->size;
        }

        public void SetValue(int node, int value)
        {
            this->table[node] = value;
        }

        public int ValueOf(int node)
        {
            return this->table[node];
        }
    }
}