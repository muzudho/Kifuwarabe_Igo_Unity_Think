namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n090_core____ {

    public abstract class Core {

        //────────────────────────────────────────────────────────────────────────────────
        // 乱数
        //────────────────────────────────────────────────────────────────────────────────
        private static System.Random m_random_ = new System.Random(System.DateTime.Now.Millisecond);
        public static int GetRandom() { return m_random_.Next(); }



        //--------------------------------------------------------------------------------
        // 動作モード
        //--------------------------------------------------------------------------------

        // ランダム打ち専用モードにするぜ☆（＾ｑ＾）
        //#define RANDOM_MOVE_ONLY

        // 攻撃的な評価関数を使うぜ☆（＾ｑ＾）
        public const bool ENABLE_MOVE_ATTACK = true;

        // 異常な指し手の場合、打ち手のやり直しを可能にするぜ☆（＾ｑ＾）
        public const bool ENABLE_MOVE_RETRY = true;
    }

}