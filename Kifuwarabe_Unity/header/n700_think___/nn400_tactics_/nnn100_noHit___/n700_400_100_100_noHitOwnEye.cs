using n190_board___;//.Liberty;
//using n700_think___.nn400_tactics_.nnn100_noHit___.NoHit;


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
    public interface NoHitOwnEye : NoHit {

        /// <summary>
        /// ４方向に隣接する、呼吸点が増える　つなげられる味方の石が　いくつか。0～4。
        /// </summary>
        int GetSafe();
        void SetSafe(int value);


        //NoHitOwnEye();

        // 自分の眼に打ち込む状況か調査
        bool IsThis(
            int color,
            int node,
            Liberty[] liberties,//[4]
            Board board
        );
    };

}