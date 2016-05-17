namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___
{
    public class LibertyOfNodesImpl : AbstractTable<int>, LibertyOfNodes
    {
        public LibertyOfNodesImpl()
        {
        }

        ~LibertyOfNodesImpl()
        {
        }

        public void CopyFrom(Board board)
        {
            this.SetTableSize(board.GetTableSize());

            // 枠を 0 に初期化。
            this.ForeachAllNodesOfWaku((int node, ref bool isBreak)=> {
                // 呼吸点の数を覚えておく碁盤です。
                this.SetValue(node, 0);
            });

            board.ForeachAllNodesWithoutWaku((int node, ref bool isBreak)=> {

                Liberty liberty = new LibertyImpl();
                liberty.Count(node, board.ValueOf(node), board);

                // 呼吸点の数を覚えておく碁盤です。
                this.SetValue(node, liberty.GetLiberty());
            });
        }
    }
}