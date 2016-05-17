using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;//.Liberty;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn400_tactics_.nnn100_noHit___
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
        bool IsCapture();
        void SetCapture(bool value);


        //NoHitSuicide();

        // 自殺手になる状況でないか調査。
        bool IsThis(
            Color       color,
            int         node,
            Liberty[]   liberties,//[4]
            Board       board
        );
    };


}