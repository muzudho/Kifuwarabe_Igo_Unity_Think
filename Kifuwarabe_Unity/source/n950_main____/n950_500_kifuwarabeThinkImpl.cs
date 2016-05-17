using System.Collections.Generic;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;//.Board .BoardImpl .LibertyOfNodes;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn800_move____;//.Move;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn800_best____;//.ThinkImpl.GameType;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n720_kifu____;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n750_explain_;//FigureType
using Grayscale.Kifuwarabe_Igo_Unity_Think.n800_scene___;//.EndgameImpl;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n850_print___;


//
// コンピューター囲碁ソフト『きふわらべ』の思考エンジン
//
// CgfGoban.exe用の思考ルーチンのサンプル
//
// 『2005/06/04 - 2005/07/15 山下 宏』版を元に改造。
// 乱数で手を返すだけです。
namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n950_main____
{
    //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    // 別のアプリケーションから呼び出される関数をまとめている
    //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    public class KifuwarabeThinkImpl : KifuwarabeThink
    {

        /// <summary>
        /// Board を作成します。
        /// </summary>
        /// <param name="boardSize">何路盤のサイズ。9～19。</param>
        /// <param name="manualTable">手で打ち込んだテーブル。要説明書参照。</param>
        /// <returns>盤面オブジェクト。</returns>
        public Board CreateBoard(
            int     boardSize,
            int[]   manualTable
        )
        {
            Board board01;

            // これで盤面を作るぜ☆！（＾ｑ＾）
            ConvBoard.IntToColor(
                out board01,
                manualTable,
                boardSize
                );

            return board01;
        }

        /// <summary>
        /// 盤面を表示するぜ☆！（＾▽＾）
        /// </summary>
        /// <param name="board"></param>
        public void PrintBoard(Board board)
        {
            BoardViewImpl.PrintBoard(board);
        }

        /// <summary>
        /// 石を置けるか調べます。
        /// </summary>
        /// <returns></returns>
        public bool CanMove(
            Color               color,
            int                 node,
            Board               board,
            out NoMoveReason    noMoveReason // 理由
        )
        {
            return UtilMove.CanMove(
                    color,
                    node,
                    board,
                    out noMoveReason
                    );
        }

        /// <summary>
        /// エラーメッセージ作成。
        /// </summary>
        /// <param name="node"></param>
        /// <param name="noMoveReason"></param>
        /// <returns></returns>
        public string CreateErrorMessage(int node, NoMoveReason noMoveReason)
        {
            return ConvBoard.ToErrorMessage(node, noMoveReason);
        }


        //────────────────────────────────────────────────────────────────────────────────
        // 思考中断フラグ。
        //────────────────────────────────────────────────────────────────────────────────
        public bool isPause;
        public bool IsPause()
        {
            return this.isPause;
        }
        public void SetPause(bool isPause)
        {
            this.isPause = isPause;
        }

        //────────────────────────────────────────────────────────────────────────────────
        // 初期化
        //────────────────────────────────────────────────────────────────────────────────
        // GUI は、対局開始時に一度だけ呼びだしてください。
        public void DoBegin()
        {
            System.Console.Title = "Kifuwarabe_Igo_Unity_Think Infomation Window";
            System.Console.WriteLine(string.Format("デバッグ用の窓だぜ☆（＾ｑ＾）\n"));

            // この下に、メモリの確保など必要な場合のコードを記述してください。
        }

        /// <summary>
        /// 棋譜を進めるぜ☆（＾▽＾）
        /// </summary>
        /// <param name="board">変更が加えられる盤面。</param>
        /// <param name="kifu">棋譜。</param>
        /// <param name="susumeruTesuu">棋譜を何手再生するか。</param>
        /// <param name="thoughtTime"></param>
        public void PlayKifu(
            Board               board,
            List<KifuElement>   kifu,
            int                 susumeruTesuu,
            out int[]           thoughtTime
            )
        {
            int node;           // 囲碁盤上の交点（将棋盤でいうマス目）
            Color color;          // 石の色
            int time;           // 消費時間
            int iTesuu;
            // 累計思考時間 [0]先手 [1]後手。置碁の場合、白の先手なので、常に黒が先手とは限らない。
            thoughtTime = new int[] { 0, 0 };  // 配列[2]
            for (iTesuu = 0; iTesuu < susumeruTesuu; iTesuu++)
            {
                node    = kifu[iTesuu].GetNode(); // 座標、y*256 + x の形で入っている
                color   = kifu[iTesuu].GetColor();    // 石の色
                time    = kifu[iTesuu].GetElapsedSecond(); // 消費時間
                thoughtTime[iTesuu & 1] += time; // 手数の下1桁を見て [0]先手、[1]後手。
                if (UtilRobotArm.DropStone(node, color, board) != DroppedResult.Success)
                {
                    // 動かせなければそこで止める。（エラーがあった？？）
                    System.Console.WriteLine(string.Format("棋譜を進められなかったので止めた☆ \n"));
                    break;
                }
            }
        }

        /// <summary>
        /// ────────────────────────────────────────────────────────────────────────────────
        /// 終局処理
        /// ────────────────────────────────────────────────────────────────────────────────
        /// </summary>
        /// <param name="initBoard"></param>
        /// <param name="kifu"></param>
        /// <param name="curTesuu"></param>
        /// <param name="resultTable"></param>
        public void DoEndGame(
                Board               initBoard,      // 初期盤面（置碁の場合は、ここに置石が入る）
                List<KifuElement>   kifu,           // 棋譜。座標、石の色、消費時間（秒)。
                                                    // 要素は、手数 0 から始まっており、curTesuu の1つ手前まである。
                int curTesuu,       // 現在の手数
            ref int[]               resultTable	    // 終局処理の結果を数字で代入する。
            )
        {
            //────────────────────────────────────────────────────────────────────────────────
            // 棋譜を進めていくぜ☆
            //────────────────────────────────────────────────────────────────────────────────
            int[] thoughtTime;
            this.PlayKifu(initBoard, kifu, curTesuu, out thoughtTime);

            // FIXME: 2度手間なのをなんとかしたいぜ☆（＞＿＜）！  ref 配列は、ラムダ式の中に入れられないぜ☆！（＾ｑ＾）
            GtpStatusType[] endgameBoard2 = new GtpStatusType[AbstractTable<Color>.ANOMALY_BOARD_MAX];
            EndgameImpl.EndgameStatus(endgameBoard2, initBoard);

            for (int i = 0; i < AbstractTable<Color>.ANOMALY_BOARD_MAX; i++)
            {
                resultTable[i] = (int)endgameBoard2[i];
            }
        }

        /// <summary>
        /// ────────────────────────────────────────────────────────────────────────────────
        /// 図形描画
        /// ────────────────────────────────────────────────────────────────────────────────
        /// </summary>
        /// <param name="initBoard"></param>
        /// <param name="kifu"></param>
        /// <param name="curTesuu"></param>
        /// <param name="resultTable"></param>
        public void DoDrawFigure(
                Board               initBoard,      // 初期盤面（置碁の場合は、ここに置石が入る）
                List<KifuElement>   kifu,           // 棋譜。座標、石の色、消費時間（秒)。
                                                    // 要素は、手数 0 から始まっており、curTesuu の1つ手前まである。
                int curTesuu,                       // 現在の手数
            ref int[] resultTable	                // 終局処理の結果を代入する。
            )
        {
            //────────────────────────────────────────────────────────────────────────────────
            // 棋譜を進めていくぜ☆
            //────────────────────────────────────────────────────────────────────────────────
            int[] thoughtTime;
            this.PlayKifu(initBoard, kifu, curTesuu, out thoughtTime);

            // FIXME: 2度手間なのをなんとかしたいぜ☆（＞＿＜）！  ref 配列は、ラムダ式の中に入れられないぜ☆！（＾ｑ＾）
            FigureType[] endgameBoard2 = new FigureType[AbstractTable<Color>.ANOMALY_BOARD_MAX];
            EndgameImpl.EndgameDrawFigure(endgameBoard2, initBoard);

            for (int i = 0; i < AbstractTable<Color>.ANOMALY_BOARD_MAX; i++)
            {
                resultTable[i] = (int)endgameBoard2[i];
            }
        }

        /// <summary>
        /// ────────────────────────────────────────────────────────────────────────────────
        /// 数字描画
        /// ────────────────────────────────────────────────────────────────────────────────
        /// </summary>
        /// <param name="initBoard"></param>
        /// <param name="kifu"></param>
        /// <param name="curTesuu"></param>
        /// <param name="resultTable"></param>
        public void DoDrawNumber(
                Board               initBoard,  // 初期盤面（置碁の場合は、ここに置石が入る）
                List<KifuElement>   kifu,       // 棋譜。座標、石の色、消費時間（秒)。
                                                // 要素は、手数 0 から始まっており、curTesuu の1つ手前まである。
                int curTesuu,                   // 現在の手数
            ref int[] resultTable	            // 終局処理の結果を代入する。
            )
        {
            //────────────────────────────────────────────────────────────────────────────────
            // 棋譜を進めていくぜ☆
            //────────────────────────────────────────────────────────────────────────────────
            int[] thoughtTime;
            this.PlayKifu(initBoard, kifu, curTesuu, out thoughtTime);

            EndgameImpl.EndgameDrawNumber(resultTable, initBoard);
        }

        public void PrintBestmove(
            int     bestmoveNode,
            int[]   thoughtTime
            )
        {
            System.Console.WriteLine(
                string.Format(
                    // カンマで区切って4桁右詰め、コロンで区切って前ゼロ☆
                    "先手{0,4}秒　　　後手{1,4}秒　　　着手({2:D2},{3:D2})\n",
                    thoughtTime[0],
                    thoughtTime[1],
                    (bestmoveNode & 0xff),
                    (bestmoveNode >> 8)
                )
            );
            //System.Console.WriteLine(string.Format("思考時間：先手={0:D}秒、後手={1:D}秒\n", thoughtTime[0], thoughtTime[1]));
            //System.Console.WriteLine(string.Format("着手=({0:D2},{1:2D})({2:x4}), 手数={3:D},手番={4:D},盤size={5:D},komi={6:f1}\n",(bestmoveNode&0xff),(bestmoveNode>>8),bestmoveNode, curTesuu,flgBlackTurn,boardSize,komi));

            //BoardViewImpl.PrintBoard(g_hConsoleWindow, &board);
        }

        public int DoBestmove(
                Board               board,
                List<KifuElement>   kifu,
                Color               color,
                double              komi
        ){
            //--------------------------------------------------------------------------------
            // 思考ルーチン
            //--------------------------------------------------------------------------------
            int bestmoveNode = 0;   // コンピューターが打つ交点。

            // 石（または連）の呼吸点を数えて、各交点に格納しておきます。
            LibertyOfNodes libertyOfNodes = new LibertyOfNodesImpl();
            libertyOfNodes.CopyFrom(board);

            // １手指します。
            bestmoveNode = ThinkImpl.Bestmove(color, board, libertyOfNodes);

            return bestmoveNode;
        }

        //────────────────────────────────────────────────────────────────────────────────
        // 終了
        //────────────────────────────────────────────────────────────────────────────────
        //
        // GUIは、対局終了時に一度だけ呼びだしてください。
        // 思考部は、メモリの解放などが必要な場合にここに記述してください。
        public void DoEnd()
        {
            //System.Console.Clear();
            // この下に、メモリの解放など必要な場合のコードを記述してください。
        }

        /// <summary>
        /// 石を置きます。
        /// </summary>
        /// <param name="node"></param>
        /// <param name="color"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public DroppedResult DropStone(
            int         node,
            Color       color,
            Board       board
        )
        {
            return UtilRobotArm.DropStone(node, color, board);
        }

        /// <summary>
        /// 石を置く前の状態に戻します。置いた石に対して１回のみ使用可能です。
        /// </summary>
        /// <param name="board"></param>
        public void UndropStoneOnce(Board board)
        {
            UtilRobotArm.UndropStoneOnce(board);
        }

    }
}