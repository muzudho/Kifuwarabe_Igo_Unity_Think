using n190_board___;//.Liberty;
//using n700_think___.nn400_tactics_.nnn100_noHit___.NoHit;


namespace n700_think___.nn400_tactics_.nnn100_noHit___
{

    //--------------------------------------------------------------------------------
    // クラス
    //--------------------------------------------------------------------------------

    //
    // 自殺手を指さない仕組み。
    //
    public interface NoHitSuicide : NoHit
    {

        /// <summary>
        /// 敵石を取ったフラグ
        /// </summary>
        int GetFlgCapture();
        void SetFlgCapture(int value);


        //NoHitSuicide();

        // 自殺手になる状況でないか調査。
        bool IsThis(
            int color,
            int node,
            Liberty[] liberties,//[4]
            Board board
        );
    };


}