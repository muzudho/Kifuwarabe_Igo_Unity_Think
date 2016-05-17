using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;//.Board .BoardImpl .LibertyOfNodes;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn800_move____;//.Move;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn800_best____;//.ThinkImpl.GameType;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n750_explain_;//FigureType
using Grayscale.Kifuwarabe_Igo_Unity_Think.n800_scene___;//.EndgameImpl;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n950_main____;//.CgfThink;


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
        public void DoBegin() {
            System.Console.Title = "CgfgobanDLL Infomation Window";
            System.Console.WriteLine(string.Format("デバッグ用の窓だぜ☆（＾ｑ＾）　PRT()関数で出力できるんだぜ☆\n"));

            // この下に、メモリの確保など必要な場合のコードを記述してください。
        }


        //────────────────────────────────────────────────────────────────────────────────
        // ベストムーブ呼び出し
        //────────────────────────────────────────────────────────────────────────────────
        // GUI は、コンピューターの手番で呼び出してください。
        // その際、現在の局面情報を渡してください。
        //
        // 次の1手の座標を返す。PASSの場合0。
        // 終局処理時に呼び出した場合は、終局判断の結果を返す。
        public int DoBestmove(
                int[]       initBoard	,   // 初期盤面（置碁の場合は、ここに置石が入る）
                int[,]     kifu        ,   // 棋譜  [2048][3]
                                            //      [手数][0]...座標
                                            //		[手数][1]...石の色
                                            //		[手数][2]...消費時間（秒)
                                            // 手数は 0 から始まり、curTesuu の1つ手前まである。
                int         curTesuu,       // 現在の手数
                bool        isBlackTurn,   // 黒手番フラグ(黒番...1、白番...0)。ここだけ定数と違ってややこしい。
                int         boardSize   ,   // 盤面のサイズ
                double      komi        ,   // コミ
                GameType    endgameType ,   // 0...通常の思考、1...終局処理、2...図形を表示、3...数値を表示。
            ref int[]       endgameBoard	// 終局処理の結果を代入する。
        ){
            int bestmoveNode = 0;   // コンピューターが打つ交点。

            //--------------------
            // 何路盤
            //--------------------
            Board board = new BoardImpl();
            board.SetSize(boardSize);

            // 現在局面を棋譜と初期盤面から作る
            board.Initialize(initBoard);


            //--------------------
            // 初期化
            //--------------------
            int node;           // 囲碁盤上の交点（将棋盤でいうマス目）
            int color;          // 石の色
            int time;           // 消費時間
            int iTesuu;

            // 累計思考時間 [0]先手 [1]後手。置碁の場合、白の先手なので、常に黒が先手とは限らない。
            int[] thoughtTime = { 0, 0 };  // 配列[2]

            // 棋譜を進めていくぜ☆
            for (iTesuu = 0; iTesuu < curTesuu; iTesuu++) {
                node = kifu[iTesuu,0]; // 座標、y*256 + x の形で入っている
                color = kifu[iTesuu,1];    // 石の色
                time = kifu[iTesuu,2]; // 消費時間
                thoughtTime[iTesuu & 1] += time; // 手数の下1桁を見て [0]先手、[1]後手。
                Move move = new MoveImpl();
                if (move.MoveOne(node, color, board) != MoveResult.MOVE_SUCCESS) {
                    // 動かせなければそこで止める。（エラーがあった？？）
                    System.Console.WriteLine(string.Format("棋譜を進められなかったので止めた☆ \n"));
                    break;
                }
            }

            // モード別対応
            switch (endgameType)
            {
                // 「終局処理」なら
                case GameType.GAME_END_STATUS:
                    {
                        // FIXME: 2度手間なのをなんとかしたいぜ☆（＞＿＜）！  ref 配列は、ラムダ式の中に入れられないぜ☆！（＾ｑ＾）
                        GtpStatusType[] endgameBoard2 = new GtpStatusType[AbstractBoard.BOARD_MAX];
                        int result = EndgameImpl.EndgameStatus(endgameBoard2, board);

                        for(int i=0;i< AbstractBoard.BOARD_MAX; i++)
                        {
                            endgameBoard[i] = (int)endgameBoard2[i];
                        }

                        return result;
                    }

                // 「図形を描く」なら
                case GameType.GAME_DRAW_FIGURE:
                    {
                        // FIXME: 2度手間なのをなんとかしたいぜ☆（＞＿＜）！  ref 配列は、ラムダ式の中に入れられないぜ☆！（＾ｑ＾）
                        FigureType[] endgameBoard2 = new FigureType[AbstractBoard.BOARD_MAX];
                        int result = EndgameImpl.EndgameDrawFigure(endgameBoard2, board);

                        for (int i = 0; i < AbstractBoard.BOARD_MAX; i++)
                        {
                            endgameBoard[i] = (int)endgameBoard2[i];
                        }

                        return result;
                    }

                // 「数値を書く」なら
                case GameType.GAME_DRAW_NUMBER:
                    {
                        return EndgameImpl.EndgameDrawNumber(endgameBoard, board);
                    }

                // 通常の指し手
                default:
                    break;
            }

            //--------------------------------------------------------------------------------
            // 思考ルーチン
            //--------------------------------------------------------------------------------

            if (isBlackTurn) {
                color = BoardImpl.BLACK;
            } else {
                color = BoardImpl.WHITE;
            }

            // 石（または連）の呼吸点を数えて、各交点に格納しておきます。
            LibertyOfNodes libertyOfNodes = new LibertyOfNodesImpl();
            libertyOfNodes.Initialize(board);

            // １手指します。
            bestmoveNode = ThinkImpl.Bestmove(color, board, libertyOfNodes);

            System.Console.WriteLine(
                string.Format(
                    "先手{0:D4}秒　　　後手{1:D4}秒　　　着手({2:D2},{3:D2})\n",
                    thoughtTime[0],
                    thoughtTime[1],
                    (bestmoveNode & 0xff),
                    (bestmoveNode >> 8)
                )
            );
            //System.Console.WriteLine(string.Format("思考時間：先手={0:D}秒、後手={1:D}秒\n", thoughtTime[0], thoughtTime[1]));
            //System.Console.WriteLine(string.Format("着手=({0:D2},{1:2D})({2:x4}), 手数={3:D},手番={4:D},盤size={5:D},komi={6:f1}\n",(bestmoveNode&0xff),(bestmoveNode>>8),bestmoveNode, curTesuu,flgBlackTurn,boardSize,komi));

            //BoardViewImpl.PrintBoard(g_hConsoleWindow, &board);

            return bestmoveNode;
        }

        //────────────────────────────────────────────────────────────────────────────────
        // 終了
        //────────────────────────────────────────────────────────────────────────────────
        //
        // GUIは、対局終了時に一度だけ呼びだしてください。
        // 思考部は、メモリの解放などが必要な場合にここに記述してください。
        public void DoEnd() {
            //System.Console.Clear();
            // この下に、メモリの解放など必要な場合のコードを記述してください。
        }
    }
}