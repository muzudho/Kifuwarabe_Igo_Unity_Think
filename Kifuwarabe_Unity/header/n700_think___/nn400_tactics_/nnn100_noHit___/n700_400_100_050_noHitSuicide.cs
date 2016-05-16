//# include <windows.h> // コンソールへの出力等
using n090_core____.Core;
using n190_board___.Liberty;
using n700_think___.nn400_tactics_.nnn100_noHit___.NoHit;


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

        // 敵石を取ったフラグ
        int flgCapture;


	    NoHitSuicide();

        // 自殺手になる状況でないか調査。
        bool IsThis(
            Core core,
            int color,
            int node,
            Liberty liberties[4],
            Board* pBoard
        );
    };


}