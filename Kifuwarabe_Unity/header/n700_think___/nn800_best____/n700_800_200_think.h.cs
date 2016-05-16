//# include <tchar.h> // Unicode対応の _T() 関数を使用するために。
using n190_board___;///n190_100_board.h"
using n190_board___;///n190_200_libertyOfNodes.h"


namespace n700_think___.nn800_best____
{
    //--------------------------------------------------------------------------------
    // enum
    //--------------------------------------------------------------------------------

    // 現在局面で何をするか、を指定
    public enum GameType {
        GAME_MOVE,              // 通常の手
        GAME_END_STATUS,        // 終局処理
        GAME_DRAW_FIGURE,       // 図形を描く
        GAME_DRAW_NUMBER        // 数値を書く
    };


    public class Think {

        // 乱数に近い評価関数。少し石を取りに行くように。
        public static int Bestmove(
        Core core,
        int coler,      // 石の色
        Board* pBoard,
        LibertyOfNodes* pLibertyOfNodes
        );
    };


}
