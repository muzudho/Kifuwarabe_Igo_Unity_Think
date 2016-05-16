using n190_board___;///n190_100_board.h"
using n700_think___.nn400_tactics_.nnn200_hit_____;///n700_400_200_000_hit.h"


namespace n700_think___.nn400_tactics_.nnn200_noHit___
{
    // Gnugo1.2 を参考にしたランダムな評価を返します。
    public class HitGnugo12Random : Hit {

        // 評価値を出します。
        public int Evaluate(
        int color,
        int node,
        Board* pBoard
        );
    };

}