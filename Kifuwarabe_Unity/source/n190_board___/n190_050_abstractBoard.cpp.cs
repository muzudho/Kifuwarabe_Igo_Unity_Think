using n190_board___;//\n190_050_abstractBoard.h"



namespace n190_board___
{

    int AbstractBoard::ConvertToNode(int x, int y)
    {
        return y * 256 + x;
    }

    void AbstractBoard::ConvertToXy(int& x, int& y, int node)
    {
        y = node / 256;
        x = node % 256;
    }

    AbstractBoard::AbstractBoard()
{

    this->size = 0;
}

    AbstractBoard::~AbstractBoard()
    {
    }

    int AbstractBoard::NorthOf(int node)
    {
        return this->ValueOf(node + this->dir4[0]);
    }

    int AbstractBoard::EastOf(int node)
    {
        return this->ValueOf(node + this->dir4[1]);
    }

    int AbstractBoard::SouthOf(int node)
    {
        return this->ValueOf(node + this->dir4[2]);
    }

    int AbstractBoard::WestOf(int node)
    {
        return this->ValueOf(node + this->dir4[3]);
    }

    void AbstractBoard::SetSize(int size)
    {
        this->size = size;
    }

    int AbstractBoard::GetSize()
    {
        return this->size;
    }

    void AbstractBoard::SetValue(int node, int value)
    {
        this->table[node] = value;
    }

    int AbstractBoard::ValueOf(int node)
    {
        return this->table[node];
    }


}