using n090_core____.Core;
using n190_board___.Board;
using n190_board___.LibertyOfNodes;
using n700_think___.nn400_tactics_.nnn200_hit_____.Hit;


namespace n700_think___.nn400_tactics_.nnn200_noHit___
{

    // 助けられそうな石を　ノビ　させようとします。
    //
    // _____
    // __o__
    // __1__
    // _xox_
    // __x__
    // _____
    //
    // 上図 o を相手の石、 x を自分の石とし、
    // 地点 1 に o を置くことで、
    // 呼吸点 1～4 の自分の石の　呼吸点　を増やすことで助けられるよう、
    // 評価が高くなる仕組み。
    //
    // 呼吸点 2以上の石は　あまり救出しようとしない。
    public interface HitNobiSaver : Hit {

        // 評価値を出します。
        int Evaluate(
            Core core,
            int color,
            int node,
            Board* pBoard,
            LibertyOfNodes* pLibertyOfNodes
        );
    }
}