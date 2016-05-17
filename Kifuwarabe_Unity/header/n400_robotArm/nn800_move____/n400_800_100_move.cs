using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn800_move____
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


    public interface Move {

        // 1手進める。
        MoveResult MoveOne(
            int node,   // 座標
            Color color,      // 石の色
            Table<Color> board
        );

        // 1手戻す。（戻せるのは１回だけです）
        void UndoOnce(Table<Color> board);
    };


}