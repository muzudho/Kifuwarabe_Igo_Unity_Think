using n190_board___;///n190_100_board.h"


namespace n800_scene___
{

    //--------------------------------------------------------------------------------
    // enum まとめ
    //--------------------------------------------------------------------------------

    // 死活情報でセットする値
    // その位置の石が「活」か「死」か。不明な場合は「活」で。
    // その位置の点が「黒地」「白地」「ダメ」か。
    public enum GtpStatusType {
        GTP_ALIVE,              // 0: 活
        GTP_DEAD,               // 1: 死
        GTP_ALIVE_IN_SEKI,      // 2: セキで活（未使用、「活」で代用して下さい）
        GTP_WHITE_TERRITORY,    // 3: 白地
        GTP_BLACK_TERRITORY,    // 4: 黒地
        GTP_DAME                // 5: ダメ
    };


    public class Endgame {


        // 終局処理（サンプルでは簡単な判断で死石と地の判定をしています）
        public static int EndgameStatus(
        int arr_endgameBoard[], // 終局図の盤上の石の色
        Board* pBoard
        );

        // 図形を描く
        public static int EndgameDrawFigure(
            int arr_endgameBoard[],
            Board* pBoard
            );

        // 数値を書く(0は表示されない)
        public static int EndgameDrawNumber(
            int arr_endgameBoard[],
            Board* pBoard
            );
    };

}