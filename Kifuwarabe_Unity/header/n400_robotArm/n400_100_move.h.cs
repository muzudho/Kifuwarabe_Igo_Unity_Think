//# include <windows.h> // コンソールへの出力等
using n090_core____;///n090_100_core.h"


namespace n400_robotArm
{


    //--------------------------------------------------------------------------------
    // enum
    //--------------------------------------------------------------------------------

    // move()関数で手を進めた時の結果
    public enum MoveResult {
        // 操作を受け入れる１種類。
        MOVE_SUCCESS,           // 成功

        // 操作を弾く４種類。
        MOVE_SUICIDE,           // 自殺手
        MOVE_KOU,               // コウ
        MOVE_EXIST,             // 既に石が存在
        MOVE_FATAL              // エラーなど。
    };


    public class Move {

        public Move();
        public ~Move();

        // 1手進める。
        public int MoveOne(
            Core core,
            int node,   // 座標
            int color,      // 石の色
            Board* pBoard
            );

        // 1手戻す。（戻せるのは１回だけです）
        public void UndoOnce(Core core, Board* pBoard);
    };


}