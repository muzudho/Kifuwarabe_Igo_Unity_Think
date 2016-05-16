using n190_board___;///n190_100_board.h"
using n190_board___;///n190_150_liberty.h"
using n700_think___.nn400_tactics_.nnn200_hit_____;///n700_400_200_000_hit.h"


namespace n700_think___.nn400_tactics_.nnn200_noHit___
{


    // _____
    // _ooo_
    // __o__
    // __1__
    // _____
    //
    // 上図 o を相手の石とし、
    // 1 に自分の石を置いたとき、
    // ○ の数が多く、○ の呼吸点が少ないものほど
    // 1 の評価が高くなる仕組み。
    //
    // ツケようとします。
    public class HitTuke : Hit {

        public HitTuke();

        // 評価値を出します。
        public int Evaluate(
		int		invColor,
		int		node,
		Liberty liberties[4],
		Board*	pBoard
		);

};


}
