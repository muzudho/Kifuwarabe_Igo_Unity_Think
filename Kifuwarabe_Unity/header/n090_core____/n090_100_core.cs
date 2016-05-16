//# include <windows.h>	// rand() 等を使用するために。
//# include <tchar.h> // Unicode対応の _T() 関数を使用するために。


namespace n090_core____ {

    public interface Core {

        //--------------------------------------------------------------------------------
        // 動作モード
        //--------------------------------------------------------------------------------

        // ランダム打ち専用モードにするぜ☆（＾ｑ＾）
        //#define RANDOM_MOVE_ONLY

        // 攻撃的な評価関数を使うぜ☆（＾ｑ＾）
        const bool ENABLE_MOVE_ATTACK = true;

        // 異常な指し手の場合、打ち手のやり直しを可能にするぜ☆（＾ｑ＾）
        cosnt bool ENABLE_MOVE_RETRY = true;

        // コンソールに、評価値のログを出すかだぜ☆（＾ｑ＾）
        //#define EVAL_LOG

        // コンソールに、動作確認用のログを出すかだぜ☆（＾ｑ＾）
        //#define CHECK_LOG

        const int PRT_LEN_MAX = 256;            // 最大256文字まで出力可


        public HANDLE hConsoleWindow;

	    // printf()の代用関数。コンソールに出力。
	    public void PRT(
    
            const _TCHAR* format,
		    ...
	    );

        // 一時的にWindowsに制御を渡します。
        // 思考中にこの関数を呼ぶと思考中断ボタンが有効になります。
        // 毎秒30回以上呼ばれるようにするとスムーズに中断できます。
        public static void YieldWindowsSystem();
    };
}