using n190_board___;///n190_150_liberty.h"
using n700_think___.nn400_tactics_.nnn100_noHit___;///n700_400_100_000_noHit.h"


namespace n700_think___.nn400_tactics_.nnn100_noHit___
{

    // _____
    // __o__
    // _o1o_
    // __o__
    // _____
    //
    // 上図 o を自分の石とし、
    // 1 を　眼　に見立てて、
    //
    // 自分の眼に打たない仕組み。
    public class NoHitOwnEye : NoHit {

        // ４方向に隣接する、呼吸点が増える　つなげられる味方の石が　いくつか。0～4。
        public int safe;


	public NoHitOwnEye();

        // 自分の眼に打ち込む状況か調査
        public bool IsThis(
            int color,
            int node,
            Liberty liberties[4],
            Board* pBoard
        );
    };

}