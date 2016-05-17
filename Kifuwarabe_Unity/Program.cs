using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn800_move____;
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

            //────────────────────────────────────────────────────────────────────────────────
            // 指し手のテストいろいろ
            //────────────────────────────────────────────────────────────────────────────────

            // テスト用の盤面を作ろうぜ☆！（＾ｑ＾）！
            Board board01 = kwThink.CreateBoard(
                    // 何路盤のサイズ（9～19）
                    19,

                    // 手で打ち込んだテーブル☆（manualTable）
                    // 19路盤を作ります。
                    // 枠(3)がない場合、エラーの原因となります。
                    // 枠(3)を付けるので21x21の一次元配列にします。
                    new int[]
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

                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 1, 2, 0, 0,  3,//[10] // コウのテスト用
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 1, 2, 0, 2, 0,  3,//[11] // コウのテスト用
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 1, 2, 0, 0,  3,//[12] // コウのテスト用
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[13]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[14]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 1, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[15] // 自殺手のテスト用
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  1, 0, 1, 0, 0, 0, 0, 0, 0, 0,  3,//[16] // 自殺手のテスト用
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 1, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[17] // 自殺手のテスト用
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[18]
                       3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3,//[19]

                       3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3,//[20]
                    }
                );
            // 盤面オブジェクトのできたてほやほや焼き上がりだぜ☆！（＾▽＾）ほいっ☆！
            // あとは　これを更新していこうぜ☆！（＾▽＾）！


            {
                // 棋譜  [][3]
                //      [手数][0]...座標
                //		[手数][1]...石の色
                //		[手数][2]...消費時間（秒)
                // 手数は 0 から始まり、curTesuu の1つ手前まである。
                int[,] kifu = new int[2048, 3];

                // 現在の手数
                int curTesuu = 0;

                // コミ
                double komi = 0;

                int bestmove = kwThink.DoBestmove(
                    board01,
                    kifu,
                    curTesuu,
                    Color.BLACK,//黒手番
                    komi
                    );

                // 16進4桁表記☆（＾▽＾）
                System.Console.WriteLine(string.Format("bestmove = 0x{0:x4}", bestmove));
                System.Console.ReadKey();
            }

            //*
            // 着手禁止点のテスト    （黒石にぶつかる場合）
            {
                System.Console.WriteLine("着手禁止点のテスト    （黒石にぶつかる場合）");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示


                int bestmoveNode = 0x0101;//黒石にぶつける

                NoMoveReason noMoveReason;
                if (!kwThink.CanMove(
                    Color.BLACK,
                    bestmoveNode,
                    board01,
                    out noMoveReason
                    ))
                {
                    // 着手禁止点（または自分の眼をつぶす手の場合）
                    System.Console.WriteLine(kwThink.CreateErrorMessage(bestmoveNode, noMoveReason));
                }
            }
            //*/

            //*
            // 着手禁止点のテスト    （白石にぶつかる場合）
            {
                System.Console.WriteLine("着手禁止点のテスト    （白石にぶつかる場合）");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示


                int bestmoveNode = 0x0405;//白石にぶつける

                NoMoveReason noMoveReason;
                if (!kwThink.CanMove(
                    Color.BLACK,
                    bestmoveNode,
                    board01,
                    out noMoveReason
                    ))
                {
                    // 着手禁止点（または自分の眼をつぶす手の場合）
                    System.Console.WriteLine(kwThink.CreateErrorMessage(bestmoveNode, noMoveReason));
                }
            }
            //*/

            //*
            // 着手禁止点のテスト    （枠にぶつかる場合）
            {
                System.Console.WriteLine("着手禁止点のテスト    （枠にぶつかる場合）");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示


                int bestmoveNode = 0x0000;//枠にぶつける

                NoMoveReason noMoveReason;
                if (!kwThink.CanMove(
                    Color.BLACK,
                    bestmoveNode,
                    board01,
                    out noMoveReason
                    ))
                {
                    // 着手禁止点（または自分の眼をつぶす手の場合）
                    System.Console.WriteLine(kwThink.CreateErrorMessage(bestmoveNode, noMoveReason));
                }
            }
            //*/

            //*
            // 着手禁止点のテスト    （コウに打ち込む場合）
            {
                System.Console.WriteLine("着手禁止点のテスト    （コウに打ち込む場合）");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示

                // 黒(17,11) と打ち込んだ後は、
                // 白(16,11) とは打ち込めない☆




                int bestmoveNode = 0x0b11;//コウを作る☆
                // 石を置きます。
                kwThink.DropStone(bestmoveNode, Color.BLACK, board01);

                // 次、エラーになる。
                bestmoveNode = 0x0b10;//コウに打ち込もうとする（エラー）☆

                NoMoveReason noMoveReason;
                if (!kwThink.CanMove(
                    Color.WHITE,// 白の手番
                    bestmoveNode,
                    board01,
                    out noMoveReason
                    ))
                {
                    // 着手禁止点（または自分の眼をつぶす手の場合）
                    System.Console.WriteLine(kwThink.CreateErrorMessage(bestmoveNode, noMoveReason));
                }
            }
            //*/


            //*
            // 着手禁止点のテスト    （自殺手の場合）
            {
                System.Console.WriteLine("着手禁止点のテスト    （自殺手の場合）");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示


                int bestmoveNode = 0x100b;//隣接する四方を自分の石に囲まれたところに打つ

                NoMoveReason noMoveReason;
                if (!kwThink.CanMove(
                    Color.BLACK,// 黒の手番
                    bestmoveNode,
                    board01,
                    out noMoveReason
                    ))
                {
                    // 着手禁止点（または自分の眼をつぶす手の場合）
                    System.Console.WriteLine(kwThink.CreateErrorMessage(bestmoveNode, noMoveReason));
                }
            }
            //*/

            //*
            // 終局処理のテスト
            {
                System.Console.WriteLine("終局処理のテスト");

                // 棋譜  [][3]
                //      [手数][0]...座標
                //		[手数][1]...石の色
                //		[手数][2]...消費時間（秒)
                // 手数は 0 から始まり、curTesuu の1つ手前まである。
                int[,] kifu = new int[2048, 3];

                // 現在の手数
                int curTesuu = 0;

                // 処理の結果を代入する。
                int[] endgameBoard = new int[AbstractTable<Color>.ANOMALY_BOARD_MAX];

                kwThink.DoEndGame(
                    board01,
                    kifu,
                    curTesuu,
                    ref endgameBoard
                    );
            }
            //*/

            //*
            // 図形描画のテスト
            {
                System.Console.WriteLine("図形描画のテスト");

                // 棋譜  [][3]
                //      [手数][0]...座標
                //		[手数][1]...石の色
                //		[手数][2]...消費時間（秒)
                // 手数は 0 から始まり、curTesuu の1つ手前まである。
                int[,] kifu = new int[2048, 3];

                // 現在の手数
                int curTesuu = 0;

                // 処理の結果を代入する。
                int[] endgameBoard = new int[AbstractTable<Color>.ANOMALY_BOARD_MAX];

                kwThink.DoDrawFigure(
                    board01,
                    kifu,
                    curTesuu,
                    ref endgameBoard
                    );
            }
            //*/

            //*
            // 数字描画のテスト
            {
                System.Console.WriteLine("数字描画のテスト");

                // 棋譜  [][3]
                //      [手数][0]...座標
                //		[手数][1]...石の色
                //		[手数][2]...消費時間（秒)
                // 手数は 0 から始まり、curTesuu の1つ手前まである。
                int[,] kifu = new int[2048, 3];

                // 現在の手数
                int curTesuu = 0;

                // 処理の結果を代入する。
                int[] endgameBoard = new int[AbstractTable<Color>.ANOMALY_BOARD_MAX];

                kwThink.DoDrawNumber(
                    board01,
                    kifu,
                    curTesuu,
                    ref endgameBoard
                    );
            }
            //*/

            //*
            // 試しにコンピューターに続きから１０手　打たせてみようぜ☆！（＾ｑ＾）
            {
                System.Console.WriteLine("試しにコンピューターに続きから１０手　打たせてみようぜ☆！（＾ｑ＾）");

                // 棋譜  [][3]
                //      [手数][0]...座標
                //		[手数][1]...石の色
                //		[手数][2]...消費時間（秒)
                // 手数は 0 から始まり、curTesuu の1つ手前まである。
                int[,] kifu = new int[2048, 3];

                // 現在の手数
                int curTesuu = 0;

                // コミ
                double komi = 0;


                int bestmoveNode;
                //────────────────────────────────────────
                // 黒１
                //────────────────────────────────────────
                bestmoveNode = kwThink.DoBestmove(
                    board01,
                    kifu,
                    curTesuu,
                    Color.BLACK,
                    komi
                    );
                kwThink.DropStone(bestmoveNode, Color.BLACK, board01);
                System.Console.WriteLine("（＾ｑ＾）黒１  ☆！");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示

                //────────────────────────────────────────
                // 白１
                //────────────────────────────────────────
                bestmoveNode = kwThink.DoBestmove(
                    board01,
                    kifu,
                    curTesuu,
                    Color.WHITE,
                    komi
                    );
                kwThink.DropStone(bestmoveNode, Color.WHITE, board01);
                System.Console.WriteLine("（＾ｑ＾）白１  ☆！");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示

                //────────────────────────────────────────
                // 黒２
                //────────────────────────────────────────
                bestmoveNode = kwThink.DoBestmove(
                    board01,
                    kifu,
                    curTesuu,
                    Color.BLACK,
                    komi
                    );
                kwThink.DropStone(bestmoveNode, Color.BLACK, board01);
                System.Console.WriteLine("（＾ｑ＾）黒２  ☆！");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示

                //────────────────────────────────────────
                // 白２
                //────────────────────────────────────────
                bestmoveNode = kwThink.DoBestmove(
                    board01,
                    kifu,
                    curTesuu,
                    Color.WHITE,
                    komi
                    );
                kwThink.DropStone(bestmoveNode, Color.WHITE, board01);
                System.Console.WriteLine("（＾ｑ＾）白２  ☆！");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示

                //────────────────────────────────────────
                // 黒３
                //────────────────────────────────────────
                bestmoveNode = kwThink.DoBestmove(
                    board01,
                    kifu,
                    curTesuu,
                    Color.BLACK,
                    komi
                    );
                kwThink.DropStone(bestmoveNode, Color.BLACK, board01);
                System.Console.WriteLine("（＾ｑ＾）黒３  ☆！");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示

                //────────────────────────────────────────
                // 白３
                //────────────────────────────────────────
                bestmoveNode = kwThink.DoBestmove(
                    board01,
                    kifu,
                    curTesuu,
                    Color.WHITE,
                    komi
                    );
                kwThink.DropStone(bestmoveNode, Color.WHITE, board01);
                System.Console.WriteLine("（＾ｑ＾）白３  ☆！");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示

                //────────────────────────────────────────
                // 黒４
                //────────────────────────────────────────
                bestmoveNode = kwThink.DoBestmove(
                    board01,
                    kifu,
                    curTesuu,
                    Color.BLACK,
                    komi
                    );
                kwThink.DropStone(bestmoveNode, Color.BLACK, board01);
                System.Console.WriteLine("（＾ｑ＾）黒４  ☆！");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示

                //────────────────────────────────────────
                // 白４
                //────────────────────────────────────────
                bestmoveNode = kwThink.DoBestmove(
                    board01,
                    kifu,
                    curTesuu,
                    Color.WHITE,
                    komi
                    );
                kwThink.DropStone(bestmoveNode, Color.WHITE, board01);
                System.Console.WriteLine("（＾ｑ＾）白４  ☆！");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示

                //────────────────────────────────────────
                // 黒５
                //────────────────────────────────────────
                bestmoveNode = kwThink.DoBestmove(
                    board01,
                    kifu,
                    curTesuu,
                    Color.BLACK,
                    komi
                    );
                kwThink.DropStone(bestmoveNode, Color.BLACK, board01);
                System.Console.WriteLine("（＾ｑ＾）黒５  ☆！");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示

                //────────────────────────────────────────
                // 白５
                //────────────────────────────────────────
                bestmoveNode = kwThink.DoBestmove(
                    board01,
                    kifu,
                    curTesuu,
                    Color.WHITE,
                    komi
                    );
                kwThink.DropStone(bestmoveNode, Color.WHITE, board01);
                System.Console.WriteLine("（＾ｑ＾）白５  ☆！");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示

            }
            //*/



            kwThink.DoEnd();



            System.Console.WriteLine("（＾ｑ＾）おわりだぜ☆ 何かキーを押せだぜ☆");
            System.Console.ReadKey();
            return;
        }
    }
}
