namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___
{
    public class MarkingBoardImpl : AbstractTable<int>, MarkingBoard
    {
        public void Initialize(Table<int> board)
        {
            this.SetTableSize(board.GetTableSize());

            // 枠と、枠内全てを 0 に初期化。
            this.ForeachAllNodesWithWaku((int node, ref bool isBreak) =>{
                this.SetValue(node, 0);
            });
        }
    }
}