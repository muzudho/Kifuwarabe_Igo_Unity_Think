using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn800_move____;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn800_best____;//GameType
using Grayscale.Kifuwarabe_Igo_Unity_Think.n950_main____;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: テストを書きたいぜ☆！（＾ｑ＾）！

            KifuwarabeThink kwThink = new KifuwarabeThinkImpl();
            kwThink.DoBegin();

            // 指し手のテスト
            {
                // 盤面のサイズ
                int boardSize = 19;

                //Board board = new BoardImpl();
                //board.SetSize(boardSize);

                // 初期盤面（置碁の場合は、ここに置石が入る）
                Color[] initBoard = new Color[AbstractTable<Color>.BOARD_MAX];
                for (int i = 0; i < AbstractTable<Color>.BOARD_MAX; i++)
                {
                    initBoard[i] = Color.EMPTY;//空点
                }
                /*
                // 枠を 3 に初期化。
                board.ForeachAllNodesOfWaku((int node, ref bool isBreak) => {
                    // 呼吸点の数を覚えておく碁盤です。
                    board.SetValue(node, BoardImpl.WAKU);
                });
                */


                // 棋譜  [][3]
                //      [手数][0]...座標
                //		[手数][1]...石の色
                //		[手数][2]...消費時間（秒)
                // 手数は 0 から始まり、curTesuu の1つ手前まである。
                int[,] kifu = new int[2048, 3];

                // 現在の手数
                int curTesuu = 0;

                // 黒手番フラグ
                bool isBlackTurn = true;


                // コミ
                double komi = 0;

                // 0...通常の思考、1...終局処理、2...図形を表示、3...数値を表示。
                GameType endgameType = GameType.GAME_MOVE;

                // 終局処理の結果を代入する。
                int[] endgameBoard = new int[AbstractTable<Color>.BOARD_MAX];

                int bestmove = kwThink.DoBestmove(
                    initBoard,
                    kifu,
                    curTesuu,
                    isBlackTurn,
                    boardSize,
                    komi,
                    endgameType,
                    ref endgameBoard
                    );

                // 16進4桁表記☆（＾▽＾）
                System.Console.WriteLine(string.Format("bestmove = 0x{0:x4}", bestmove));
                System.Console.ReadKey();
            }

            //*
            // 着手禁止点のテスト
            {
                int bestmoveNode = 0x0303;
                Color color = Color.BLACK;
                // 盤面
                Color[] boardArr = new Color[AbstractTable<Color>.BOARD_MAX];
                for (int i = 0; i < AbstractTable<Color>.BOARD_MAX; i++)
                {
                    boardArr[i] = 0;//空点
                }
                // 入れなおす☆
                Table<Color> board = new BoardImpl();
                board.Initialize(boardArr);
                board.SetValue(bestmoveNode, Color.BLACK);//黒石を置いておくぜ☆

                NoMoveReason noMoveReason;
                if (!UtilMove.CanMove(
                    color,
                    bestmoveNode,
                    board,
                    out noMoveReason
                    ))
                {
                    // 着手禁止点（または自分の眼をつぶす手の場合）

                    int x, y;
                    AbstractTable<Color>.ConvertToXy(out x, out y, bestmoveNode);

                    switch (noMoveReason)
                    {
                        case NoMoveReason.ExistsStone:
                            // 石があるなら
                            System.Console.WriteLine(string.Format("({0:D},{1:D})　石がある。\n", x, y));
                            break;
                        case NoMoveReason.OutOfBoard:
                            // 枠なら
                            System.Console.WriteLine(string.Format("({0:D},{1:D})　枠だった。\n", x, y));
                            break;
                        case NoMoveReason.Kou:
                            // コウになる位置なら
                            System.Console.WriteLine(string.Format("({0:D},{1:D})　コウになる。\n", x, y));
                            break;
                        case NoMoveReason.Suicide:
                            // 自殺手になるなら
                            System.Console.WriteLine(string.Format("({0:D},{1:D})　自殺手になる。\n", x, y));
                            break;
                        case NoMoveReason.OwnEye:
                            // 自分の眼になるなら
                            System.Console.WriteLine(string.Format("({0:D},{1:D})　自分の眼に打ち込む。\n", x, y));
                            break;
                    }
                }
            }
            //*/



            kwThink.DoEnd();



            System.Console.WriteLine("（＾ｑ＾）おわりだぜ☆ 何かキーを押せだぜ☆");
            System.Console.ReadKey();
            return;
        }
    }
}
