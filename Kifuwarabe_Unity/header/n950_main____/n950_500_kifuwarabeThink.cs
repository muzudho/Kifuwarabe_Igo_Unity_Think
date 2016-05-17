using System.Collections.Generic;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn800_move____;//NoMoveReason
using Grayscale.Kifuwarabe_Igo_Unity_Think.n720_kifu____;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n950_main____
{

    /// <summary>
    /// Unity／スマートフォン・アプリと連携するぜ☆
    /// </summary>
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
        /// <param name="initBoard">初期盤面（置碁の場合は、ここに置石が入る）</param>
        /// <param name="kifu">棋譜。座標、石の色、消費時間（秒)。要素は、手数 0 から始まっており、curTesuu の1つ手前まである。</param>
        /// <param name="curTesuu">現在の手数</param>
        /// <param name="endgameBoard">終局処理の結果を代入する。</param>
        void DoEndGame(
                Board initBoard,
                List<KifuElement> kifu,
                int curTesuu,
            ref int[] endgameBoard
            );

        /// <summary>
        /// ────────────────────────────────────────────────────────────────────────────────
        /// 図形描画
        /// ────────────────────────────────────────────────────────────────────────────────
        /// </summary>
        /// <param name="initBoard">初期盤面（置碁の場合は、ここに置石が入る）</param>
        /// <param name="kifu">棋譜。座標、石の色、消費時間（秒)。要素は、手数 0 から始まっており、curTesuu の1つ手前まである。</param>
        /// <param name="curTesuu">現在の手数</param>
        /// <param name="endgameBoard">終局処理の結果を代入する。</param>
        void DoDrawFigure(
                Board initBoard,
                List<KifuElement> kifu,
                int curTesuu,
            ref int[] endgameBoard
            );

        /// <summary>
        /// ────────────────────────────────────────────────────────────────────────────────
        /// 数字描画
        /// ────────────────────────────────────────────────────────────────────────────────
        /// </summary>
        /// <param name="initBoard">初期盤面（置碁の場合は、ここに置石が入る）</param>
        /// <param name="kifu">棋譜。座標、石の色、消費時間（秒)。要素は、手数 0 から始まっており、curTesuu の1つ手前まである。</param>
        /// <param name="curTesuu">現在の手数</param>
        /// <param name="endgameBoard">終局処理の結果を代入する。</param>
        void DoDrawNumber(
                Board initBoard,
                List<KifuElement> kifu,
                int curTesuu,
            ref int[] endgameBoard
            );

        void PrintBestmove(
            int bestmoveNode,
            int[] thoughtTime
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
        /// <param name="kifu">棋譜。座標、石の色、消費時間（秒)。要素は、手数 0 から始まっており、curTesuu の1つ手前まである。</param>
        /// <param name="color">手番。(黒か、白のみ)</param>
        /// <param name="komi">コミ</param>
        /// <returns></returns>
        int DoBestmove(
                Board               initBoard,
                List<KifuElement>   kifu,
                Color               color,
                double              komi
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
        /// 棋譜汚しも、石を置けないと判定してしまいます。
        /// NoMoveReason 列挙型で内容を確認してください。
        /// </summary>
        /// <param name="color"></param>
        /// <param name="node"></param>
        /// <param name="board"></param>
        /// <param name="noMoveReason">理由</param>
        /// <returns></returns>
        bool CanMove(
                Color           color,
                int             node,
                Board           board,
            out NoMoveReason    noMoveReason
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
            int     node,
            Color   color,
            Board   board
        );

        /// <summary>
        /// 石を置く前の状態に戻します。
        /// </summary>
        /// <param name="board"></param>
        void UndropStoneOnce(Board board);

        /// <summary>
        /// 棋譜を進めるぜ☆（＾▽＾）
        /// </summary>
        /// <param name="initBoard">変更が加えられる盤面。</param>
        /// <param name="kifu">棋譜。</param>
        /// <param name="susumeruTesuu">棋譜を何手再生するか。</param>
        /// <param name="thoughtTime"></param>
        void PlayKifu(
            Board initBoard,
            List<KifuElement> kifu,
            int susumeruTesuu,
            out int[] thoughtTime
            );

        /// <summary>
        /// 棋譜はいじらず、盤面だけ更新します。
        /// </summary>
        /// <param name="bestmoveNode"></param>
        /// <param name="color"></param>
        /// <param name="board"></param>
        void DropStone_UpdateBoard(
            int bestmoveNode,
            Color color,
            Board board
            );

    }
}