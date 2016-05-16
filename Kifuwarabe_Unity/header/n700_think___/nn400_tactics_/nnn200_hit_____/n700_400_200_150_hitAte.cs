using n090_core____.Core;
using n190_board___.Board;
using n190_board___.LibertyOfNodes;
using n700_think___.nn400_tactics_.nnn200_hit_____.Hit;


namespace n700_think___.nn400_tactics_.nnn200_noHit___
{

    // アテようとします。
    //
    // _____
    // __1__
    // _xox_
    // __x__
    // _____
    //
    // 上図 o を相手の石、 x を自分の石とし、
    // 1 に自分の石を置く評価が高くなる仕組み。
    //
    // ただし、1 の地点がコウになる場合は置きません。
    //
    // _____
    // __o__
    // _o1o_
    // _xox_
    // __x__
    // _____
    public interface HitAte : Hit {

        // 評価値を出します。
        int Evaluate(
		    Core			core,
		    int				color,
		    int				node,
		    Board*			pBoard,
		    LibertyOfNodes*	pLibertyOfNodes
		);
    }
}
