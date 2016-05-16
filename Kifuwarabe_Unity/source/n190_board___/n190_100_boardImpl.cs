using System.Collections.Generic;//# include <vector>

//using n190_board___;//.Board;


namespace n190_board___
{

    public class BoardImpl : AbstractBoard
    {
        // 碁石
        public const int EMPTY = 0;      // 空きスペース
        public const int BLACK = 1;
        public const int WHITE = 2;
        public const int WAKU = 3;		// 盤外
        public static int INVCLR(int x){
            return 3-x;// 石の色を反転させる
        }

        public BoardImpl()
        {

            this.kouNodeForUndo = 0;
	        this.moveNodeForUndo = 0;
	        this.kouNode = 0;		// コウになる位置。

	        this.hama[0] = 0;	
	        this.hama[BoardImpl.BLACK] = 0;	// 取った石の数
	        this.hama[BoardImpl.WHITE] = 0;
        }

        ~BoardImpl()
        {
        }


        // vector→
        public override List<int> GetOpenNodesOfStone(int node, int size123)
        {
            std::vector<int> openNodes;

            // 上側 → 右側 → 下側 → 左側
            this.ForeachArroundNodes(node,[this, size123, &openNodes](int adjNode, bool & isBreak) {

                if (this.ValueOf(adjNode) == BoardImpl.EMPTY && adjNode != this.kouNode)
                {
                    // 空きスペースで、コウにならない位置なら。
                    openNodes.push_back(adjNode);

                    if (openNodes.size() == size123)
                    {
                        // 計算を打ち切り。
                        isBreak = true;
                        goto gt_Next;
                    }
                }

                gt_Next:
                ;
            });

            gt_EndMethod:
            return openNodes;
        }

        public override void DeleteRenStones(
            int tNode,
            int color
            )
        {
            // 指定した位置の石を削除。
            this.SetValue(tNode, 0);

            // ４方向の石にも同じ処理を行います。
            this.ForeachArroundNodes(tNode, [this, color](int adjNode, bool & isBreak) {
                if (this.ValueOf(adjNode) == color)
                {
                    this.DeleteRenStones(adjNode, color);
                }
            });
        }
    }
}