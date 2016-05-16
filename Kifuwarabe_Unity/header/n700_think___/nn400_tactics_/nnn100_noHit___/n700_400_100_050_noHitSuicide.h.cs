//# include <windows.h> // コンソールへの出力等
using n090_core____;///n090_100_core.h"
using n190_board___;///n190_150_liberty.h"
using n700_think___.nn400_tactics_.nnn100_noHit___;///n700_400_100_000_noHit.h"


namespace n700_think___.nn400_tactics_.nnn100_noHit___
{

    //--------------------------------------------------------------------------------
    // クラス
    //--------------------------------------------------------------------------------

    //
    // 自殺手を指さない仕組み。
    //
    public class NoHitSuicide : NoHit
    {

        // 敵石を取ったフラグ
        public int flgCapture;


	        public NoHitSuicide();

        // 自殺手になる状況でないか調査。
        public bool IsThis(
            Core core,
            int color,
            int node,
            Liberty liberties[4],
            Board* pBoard
        );
    };


}