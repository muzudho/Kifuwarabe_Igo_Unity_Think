using System.Collections.Generic;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___
{

    /// <summary>
    /// 碁盤を想定したテーブルです。
    /// 
    /// 碁盤の要請：枠付き。
    /// </summary>
    public class BoardImpl : AbstractTable<Color>, Board
    {
        public BoardImpl()
        {

            this.SetKouNodeForUndo( 0);
	        this.SetMoveNodeForUndo( 0);
	        this.SetKouNode(0);		// コウになる位置。

	        this.SetHama(0, 0);
	        this.SetHama(Color.BLACK, 0);	// 取った石の数
	        this.SetHama(Color.WHITE, 0);
        }

        ~BoardImpl()
        {
        }


        /// <summary>
        /// 何路盤のサイズは、テーブルサイズより２小さい。
        /// </summary>
        public int GetBoardSize()
        {
            return this.m_tableSize_ - 2;
        }
        public void SetBoardSize(int value)
        {
            this.m_tableSize_ = value + 2;
        }


        /// <summary>
        /// 指定したnode（石）に隣接している空きスペース（1以上3以下）を配列に入れて返します。
        /// </summary>
        /// <param name="node"></param>
        /// <param name="size123"></param>
        /// <returns></returns>
        public static List<int> GetOpenNodesOfStone(
            Table<Color> board,
            int node,
            int size123     // 1～3 のいずれかを指定してください。
        )
        {
            List<int> openNodes = new List<int>();

            // 上側 → 右側 → 下側 → 左側
            board.ForeachArroundNodes(node, (int adjNode, ref bool isBreak) => {

                if (board.ValueOf(adjNode) == Color.EMPTY && adjNode != board.GetKouNode())
                {
                    // 空きスペースで、コウにならない位置なら。
                    openNodes.Add(adjNode);

                    if (openNodes.Count == size123)
                    {
                        // 計算を打ち切り。
                        isBreak = true;
                        goto gt_Next;
                    }
                }

                gt_Next:
                ;
            });

            //gt_EndMethod:
            return openNodes;
        }

        /// <summary>
        /// 連になっている石を消す。１個の石でも消す。
        /// </summary>
        /// <param name="tNode"></param>
        /// <param name="color"></param>
        public static void DeleteRenStones(
            Table<Color> board,
            int tNode,
            Color color
        )
        {
            // 指定した位置の石を削除。
            board.SetValue(tNode, 0);

            // ４方向の石にも同じ処理を行います。
            board.ForeachArroundNodes(tNode, (int adjNode, ref bool isBreak) => {
                if (board.ValueOf(adjNode) == color)
                {
                    BoardImpl.DeleteRenStones(board, adjNode, color);
                }
            });
        }

    }
}