using n190_board___;//n190_100_board.h"
using n190_board___;///n190_200_libertyOfNodes.h"


namespace n700_think___.nn800_best____
{

    public class Evaluation {

        // 指定局面の評価値を求めます。
        public static int EvaluateAtNode(
        Core core,
        int&			flgAbort        ,   // 解なしなら 0 以外。
        int color,  // 手番の色
        int node,   // 石を置く位置
        Board* pBoard,
        LibertyOfNodes* pLibertyOfNodes
        );
    };

}
