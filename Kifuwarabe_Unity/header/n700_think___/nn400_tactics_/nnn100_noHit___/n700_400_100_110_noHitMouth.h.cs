using n700_think___.nn400_tactics_.nnn100_noHit___;///n700_400_100_000_noHit.h"


namespace n700_think___.nn400_tactics_.nnn100_noHit___
{

    // _____
    // __x__
    // _x1x_
    // _____
    // _____
    //
    // 上図 x を　口　に見立てて、
    // 1 を口の中に見立て、
    //
    // 相手の石の口の中に打たない仕組み。
    public class NoHitMouth : NoHit {

        public int adjOppo;    // 上下左右に隣接(adjacent)する相手(opponent)の石の数。


        public NoHitMouth();

        // 相手の口に石を打ち込む状況でないか調査。
        public void Research(
            int color,
            int node,
            Board* pBoard
        );

        // 評価値を出します。
        public int Evaluate(
            int flgCapture      // suicide.flgCapture
        );
    };

}