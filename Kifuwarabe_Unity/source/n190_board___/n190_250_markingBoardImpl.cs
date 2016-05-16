namespace n190_board___
{
    public class MarkingBoardImpl : AbstractBoard, MarkingBoard
    {
        public void Initialize(Board board)
        {
            this.SetSize(board.GetSize());

            // 枠と、枠内全てを 0 に初期化。
            this.ForeachAllNodesWithWaku((int node, ref bool isBreak) =>{
                this.SetValue(node, 0);
            });
        }
    }
}