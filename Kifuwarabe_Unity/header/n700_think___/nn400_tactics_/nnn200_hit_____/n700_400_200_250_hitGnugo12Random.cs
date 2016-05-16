using n190_board___.Board;
using n700_think___.nn400_tactics_.nnn200_hit_____.Hit;


namespace n700_think___.nn400_tactics_.nnn200_noHit___
{
    // Gnugo1.2 を参考にしたランダムな評価を返します。
    public interface HitGnugo12Random : Hit {

        // 評価値を出します。
        int Evaluate(
            int color,
            int node,
            Board* pBoard
        );
    }
}