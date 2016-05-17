using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn400_tactics_.nnn100_noHit___;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn400_tactics_.nnn100_noHit___
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
    public interface NoHitMouth : NoHit {

        /// <summary>
        /// 上下左右に隣接(adjacent)する相手(opponent)の石の数。
        /// </summary>
        int GetAdjOppo();
        void SetAdjOppo(int value);


        //NoHitMouth();

        // 相手の口に石を打ち込む状況でないか調査。
        void Research(
            Color color,
            int node,
            Table<Color> board
        );

        // 評価値を出します。
        int Evaluate(
            bool isCapture      // suicide.flgCapture
        );
    };

}