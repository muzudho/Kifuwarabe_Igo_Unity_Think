﻿namespace n090_core____ {

    public abstract class Core {

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