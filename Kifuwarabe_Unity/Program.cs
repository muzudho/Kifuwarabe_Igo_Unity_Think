using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn800_move____;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn800_best____;//GameType
using Grayscale.Kifuwarabe_Igo_Unity_Think.n930_view____;
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
            Board initBoard;
            {
                // 盤面のサイズ
                int boardSize = 19;

                // 手で打ち込んだテーブル☆（manualTable）
                // 19路盤を作ります。
                // 枠(3)がない場合、エラーの原因となります。
                // 枠(3)を付けるので21x21の一次元配列にします。
                int[] manualtable = new int[]
                {
                    // テスト用の盤面を作ります。
                    // 0: 空点
                    // 1: 黒石
                    // 2: 白石
                    // 3: 枠
                    // 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,12,13,14,15,16,17,18,19, 20
                       3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3,//[ 0]
                       3, 1, 2, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[ 1] //黒石と白石を適当に置いた☆
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[ 2]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[ 3]
                       3, 0, 0, 0, 1, 2, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[ 4] //黒石と白石を適当に置いた☆
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[ 5]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[ 6]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[ 7]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[ 8]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[ 9]

                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[10]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[11]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[12]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[13]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[14]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[15]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[16]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[17]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[18]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[19]

                       3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3,//[20]
                };
                ConvBoard.IntToColor(manualtable, boardSize, out initBoard);


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
                int[] endgameBoard = new int[AbstractTable<Color>.ANOMALY_BOARD_MAX];

                int bestmove = kwThink.DoBestmove(
                    initBoard,
                    kifu,
                    curTesuu,
                    isBlackTurn,
                    komi,
                    endgameType,
                    ref endgameBoard
                    );

                // 16進4桁表記☆（＾▽＾）
                System.Console.WriteLine(string.Format("bestmove = 0x{0:x4}", bestmove));
                System.Console.ReadKey();
            }

            //*
            // 着手禁止点のテスト    （黒石にぶつかる場合）
            {
                // 碁盤表示
                BoardViewImpl.PrintBoard(initBoard);


                int bestmoveNode = 0x0101;//黒石にぶつける

                NoMoveReason noMoveReason;
                if (!UtilMove.CanMove(
                    Color.BLACK,
                    bestmoveNode,
                    initBoard,
                    out noMoveReason
                    ))
                {
                    // 着手禁止点（または自分の眼をつぶす手の場合）

                    string errorMessage = ConvBoard.ToErrorMessage(bestmoveNode, noMoveReason);
                    System.Console.WriteLine(errorMessage);
                }
            }
            //*/

            //*
            // 着手禁止点のテスト    （白石にぶつかる場合）
            {
                // 碁盤表示
                BoardViewImpl.PrintBoard(initBoard);


                int bestmoveNode = 0x0405;//白石にぶつける

                NoMoveReason noMoveReason;
                if (!UtilMove.CanMove(
                    Color.BLACK,
                    bestmoveNode,
                    initBoard,
                    out noMoveReason
                    ))
                {
                    // 着手禁止点（または自分の眼をつぶす手の場合）

                    string errorMessage = ConvBoard.ToErrorMessage(bestmoveNode, noMoveReason);
                    System.Console.WriteLine(errorMessage);
                }
            }
            //*/

            //*
            // 着手禁止点のテスト    （枠にぶつかる場合）
            {
                // 碁盤表示
                BoardViewImpl.PrintBoard(initBoard);


                int bestmoveNode = 0x0000;//枠にぶつける

                NoMoveReason noMoveReason;
                if (!UtilMove.CanMove(
                    Color.BLACK,
                    bestmoveNode,
                    initBoard,
                    out noMoveReason
                    ))
                {
                    // 着手禁止点（または自分の眼をつぶす手の場合）

                    string errorMessage = ConvBoard.ToErrorMessage(bestmoveNode, noMoveReason);
                    System.Console.WriteLine(errorMessage);
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
