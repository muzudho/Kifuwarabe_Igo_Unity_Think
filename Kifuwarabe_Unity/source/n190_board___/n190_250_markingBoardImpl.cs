using n190_board___.Board;
using n190_board___.MarkingBoard;


namespace n190_board___
{
    public class MarkingBoardImpl : MarkingBoard
    {
        public void Initialize(Board* pBoard)
        {
            this->SetSize(pBoard->GetSize());

            // 枠と、枠内全てを 0 に初期化。
            this->ForeachAllNodesWithWaku([this](int node, bool & isBreak) {
                this->SetValue(node, 0);
            });
        }
    }
}