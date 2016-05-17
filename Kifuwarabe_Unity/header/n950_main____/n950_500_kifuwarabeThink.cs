using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn800_move____;//NoMoveReason
using Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn800_best____;//.ThinkImpl.GameType;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n950_main____
{

    // Unity／スマートフォン・アプリと連携するぜ☆
    public interface KifuwarabeThink {

        //────────────────────────────────────────────────────────────────────────────────
        // 思考中断フラグ。
        //────────────────────────────────────────────────────────────────────────────────
        // GUI（CgfGoban）の「思考中断ボタン」を押された場合に true になります。
        bool IsPause();
        void SetPause(bool isPause);

        //────────────────────────────────────────────────────────────────────────────────
        // 初期化
        //────────────────────────────────────────────────────────────────────────────────
        // GUI は、対局開始時に一度だけ呼びだしてください。
        void DoBegin();

        /// <summary>
        /// ────────────────────────────────────────────────────────────────────────────────
        /// 終局処理
        /// ────────────────────────────────────────────────────────────────────────────────
        /// </summary>
        /// <param name="initBoard"></param>
        /// <param name="kifu"></param>
        /// <param name="curTesuu"></param>
        /// <param name="endgameBoard"></param>
        void DoEndGame(
                Board initBoard,   // 初期盤面（置碁の場合は、ここに置石が入る）
                int[,] kifu,   // 棋譜  [2048][3]
                               //      [手数][0]...座標
                               //		[手数][1]...石の色
                               //		[手数][2]...消費時間（秒)
                               // 手数は 0 から始まり、curTesuu の1つ手前まである。
                int curTesuu,       // 現在の手数
            ref int[] endgameBoard	// 終局処理の結果を代入する。
            );

        /// <summary>
        /// ────────────────────────────────────────────────────────────────────────────────
        /// 図形描画
        /// ────────────────────────────────────────────────────────────────────────────────
        /// </summary>
        /// <param name="initBoard"></param>
        /// <param name="kifu"></param>
        /// <param name="curTesuu"></param>
        /// <param name="endgameBoard"></param>
        void DoDrawFigure(
                Board initBoard,   // 初期盤面（置碁の場合は、ここに置石が入る）
                int[,] kifu,   // 棋譜  [2048][3]
                               //      [手数][0]...座標
                               //		[手数][1]...石の色
                               //		[手数][2]...消費時間（秒)
                               // 手数は 0 から始まり、curTesuu の1つ手前まである。
                int curTesuu,       // 現在の手数
            ref int[] endgameBoard	// 終局処理の結果を代入する。
            );

        /// <summary>
        /// ────────────────────────────────────────────────────────────────────────────────
        /// 数字描画
        /// ────────────────────────────────────────────────────────────────────────────────
        /// </summary>
        /// <param name="initBoard"></param>
        /// <param name="kifu"></param>
        /// <param name="curTesuu"></param>
        /// <param name="endgameBoard"></param>
        void DoDrawNumber(
                Board initBoard,   // 初期盤面（置碁の場合は、ここに置石が入る）
                int[,] kifu,   // 棋譜  [2048][3]
                               //      [手数][0]...座標
                               //		[手数][1]...石の色
                               //		[手数][2]...消費時間（秒)
                               // 手数は 0 から始まり、curTesuu の1つ手前まである。
                int curTesuu,       // 現在の手数
            ref int[] endgameBoard	// 終局処理の結果を代入する。
            );

        /// <summary>
        ///────────────────────────────────────────────────────────────────────────────────
        /// ベストムーブ呼び出し
        ///────────────────────────────────────────────────────────────────────────────────
        /// GUI は、コンピューターの手番で呼び出してください。
        /// その際、現在の局面情報を渡してください。
        ///
        /// 次の1手の座標を返す。PASSの場合0。
        /// 終局処理時に呼び出した場合は、終局判断の結果を返す。
        /// </summary>
        /// <param name="initBoard">初期盤面（置碁の場合は、ここに置石が入る）</param>
        /// <param name="kifu">棋譜  [2048][3]。[手数][0]...座標。[手数][1]...石の色。[手数][2]...消費時間（秒)。手数は 0 から始まり、curTesuu の1つ手前まである。</param>
        /// <param name="curTesuu">現在の手数</param>
        /// <param name="isBlackTurn">手番。(黒か、白のみ)</param>
        /// <param name="komi">コミ</param>
        /// <returns></returns>
        int DoBestmove(
                Board   initBoard,
                int[,]  kifu,
                int     curTesuu,
                Color   color,
                double  komi
        );


        //────────────────────────────────────────────────────────────────────────────────
        // 終了
        //────────────────────────────────────────────────────────────────────────────────
        //
        // GUIは、対局終了時に一度だけ呼びだしてください。
        // 思考部は、メモリの解放などが必要な場合にここに記述してください。
        void DoEnd();



        /// <summary>
        /// Board を作成します。
        /// </summary>
        /// <param name="boardSize">何路盤のサイズ。9～19。</param>
        /// <param name="manualTable">手で打ち込んだテーブル。要説明書参照。</param>
        /// <returns>盤面オブジェクト。</returns>
        Board CreateBoard(
            int boardSize,
            int[] manualTable
        );

        /// <summary>
        /// 盤面を表示するぜ☆！（＾▽＾）
        /// </summary>
        /// <param name="board"></param>
        void PrintBoard(Board board);

        /// <summary>
        /// 石を置けるか調べます。
        /// </summary>
        /// <returns></returns>
        bool CanMove(
                Color color,
                int node,
                Board board,
            out NoMoveReason noMoveReason // 理由
            );

        /// <summary>
        /// エラーメッセージ作成。
        /// </summary>
        /// <param name="node"></param>
        /// <param name="noMoveReason"></param>
        /// <returns></returns>
        string CreateErrorMessage(int node, NoMoveReason noMoveReason);

        /// <summary>
        /// 石を置きます。
        /// </summary>
        /// <param name="node"></param>
        /// <param name="color"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        DroppedResult DropStone(
            int node,
            Color color,
            Board board
        );

        /// <summary>
        /// 石を置く前の状態に戻します。
        /// </summary>
        /// <param name="board"></param>
        void UndropStoneOnce(Board board);

    }
}