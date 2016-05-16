using n190_board___.Board;
using n190_board___.Liberty;
using n700_think___.nn400_tactics_.nnn200_hit_____.Hit;


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
    public interface HitTuke : Hit {

        HitTuke();

        // 評価値を出します。
        int Evaluate(
		    int		invColor,
		    int		node,
		    Liberty liberties[4],
		    Board*	pBoard
		);
    }
}
