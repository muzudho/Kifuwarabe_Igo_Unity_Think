using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn800_move____
{
    // dropStone()関数 で手を進めた時の結果
    public enum DroppedResult {

        // 操作を受け入れる１種類。
        Success,           // 成功

        // 操作を弾く４種類。
        Suicide,           // 自殺手

        Kou,               // コウ

        ExistsStone,             // 既に石が存在

        Fatal              // エラーが起こっていて、対応しないときなど。

    };

}