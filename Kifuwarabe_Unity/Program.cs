using System.Collections.Generic;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn800_move____;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n720_kifu____;
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

            // ハンディキャップを設定しておこうぜ☆（＾～＾）
            // コミ
            double komi = 0;


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
                        // テスト用の初期局面を作ります。
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

            // （＾ｑ＾）もし１手　打ったら棋譜に記録していこうぜ☆！
            List<KifuElement> kifu01 = new List<KifuElement>();

            {
                int bestmove = kwThink.DoBestmove(
                    board01,
                    kifu01,
                    kifu01.Count,// 現在の手数
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
                // 打ち込めるか確認しただけで、打ち込んでいないぜ☆（＾▽＾）
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
                // 打ち込めるか確認しただけで、打ち込んでいないぜ☆（＾▽＾）
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
                // 打ち込めるか確認しただけで、打ち込んでいないぜ☆（＾▽＾）
            }
            //*/

            //*
            // 着手禁止点のテスト    （コウに打ち込む場合）
            {
                System.Console.WriteLine("着手禁止点のテスト    （コウに打ち込む場合）");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示

                int bestmoveNode;
                // 黒(17,11) と打ち込んだ後は、
                // 白(16,11) とは打ち込めない☆

                // 石を置きます。
                bestmoveNode = 0x0b11;//黒番、コウを作る☆
                {
                    Color color = Color.BLACK;
                    kwThink.DropStone(bestmoveNode, color, board01);
                    kifu01.Add(new KifuElementImpl(bestmoveNode, color, 0));// FIXME: とりあえず 0 秒で打ち込んだことにするか☆？
                }

                // 次、エラーになる。
                bestmoveNode = 0x0b10;//白番、コウに打ち込もうとする（エラー）☆
                {
                    Color color = Color.WHITE;

                    NoMoveReason noMoveReason;
                    if (!kwThink.CanMove(
                        color,// 白の手番
                        bestmoveNode,
                        board01,
                        out noMoveReason
                        ))
                    {
                        // 着手禁止点（または自分の眼をつぶす手の場合）
                        System.Console.WriteLine(kwThink.CreateErrorMessage(bestmoveNode, noMoveReason));
                    }
                }

            }
            //*/


            //*
            // 着手禁止点のテスト    （自殺手の場合）
            {
                System.Console.WriteLine("着手禁止点のテスト    （自殺手の場合）");
                kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示


                {
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
                // 打ち込めるか確認しただけで、打ち込んでいないぜ☆（＾▽＾）
            }
            //*/

            //*
            // 終局処理のテスト
            {
                System.Console.WriteLine("終局処理のテスト");

                // 処理の結果を代入する。
                int[] endgameBoard = new int[AbstractTable<Color>.ANOMALY_BOARD_MAX];

                kwThink.DoEndGame(
                    board01,
                    kifu01,
                    kifu01.Count,// 現在の手数
                    ref endgameBoard
                    );
                // 打ち込めるか確認しただけで、打ち込んでいないぜ☆（＾▽＾）
            }
            //*/

            //*
            // 図形描画のテスト
            {
                System.Console.WriteLine("図形描画のテスト");

                // 処理の結果を代入する。
                int[] endgameBoard = new int[AbstractTable<Color>.ANOMALY_BOARD_MAX];

                kwThink.DoDrawFigure(
                    board01,
                    kifu01,// 現在の手数
                    kifu01.Count,
                    ref endgameBoard
                    );
            }
            //*/

            //*
            // 数字描画のテスト
            {
                System.Console.WriteLine("数字描画のテスト");
               
                // 処理の結果を代入する。
                int[] endgameBoard = new int[AbstractTable<Color>.ANOMALY_BOARD_MAX];

                kwThink.DoDrawNumber(
                    board01,
                    kifu01,
                    kifu01.Count,// 現在の手数
                    ref endgameBoard
                    );
            }
            //*/


            System.Console.WriteLine("次は、試しにコンピューターに続きから１０手　打たせてみようぜ☆！（＾ｑ＾）");
            System.Console.ReadKey();

            //*
            // 試しにコンピューターに続きから１０手　打たせてみようぜ☆！（＾ｑ＾）
            {
                int bestmoveNode;
                {
                    //────────────────────────────────────────
                    // 黒１
                    //────────────────────────────────────────
                    Color color = Color.BLACK;//キータイピング・ミスを防ぐために、変数に小分けしようぜ☆（＾▽＾）ｗｗｗ
                    bestmoveNode = kwThink.DoBestmove(
                        board01,
                        kifu01,
                        kifu01.Count,// 現在の手数
                        color,
                        komi
                        );
                    kwThink.DropStone(bestmoveNode, color, board01);
                    kifu01.Add(new KifuElementImpl(bestmoveNode, color, 0));// FIXME: とりあえず 0 秒で打ち込んだことにするか☆？

                    System.Console.WriteLine("（＾ｑ＾）黒１  ☆！");
                    kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示
                }

                {
                    //────────────────────────────────────────
                    // 白１
                    //────────────────────────────────────────
                    Color color = Color.WHITE;
                    bestmoveNode = kwThink.DoBestmove(
                        board01,
                        kifu01,
                        kifu01.Count,// 現在の手数
                        color,
                        komi
                        );
                    kwThink.DropStone(bestmoveNode, color, board01);
                    kifu01.Add(new KifuElementImpl(bestmoveNode, color, 0));// FIXME: とりあえず 0 秒で打ち込んだことにするか☆？

                    System.Console.WriteLine("（＾ｑ＾）白１  ☆！");
                    kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示
                }

                {
                    //────────────────────────────────────────
                    // 黒２
                    //────────────────────────────────────────
                    Color color = Color.BLACK;
                    bestmoveNode = kwThink.DoBestmove(
                        board01,
                        kifu01,
                        kifu01.Count,// 現在の手数
                        color,
                        komi
                        );
                    kwThink.DropStone(bestmoveNode, color, board01);
                    kifu01.Add(new KifuElementImpl(bestmoveNode, color, 0));// FIXME: とりあえず 0 秒で打ち込んだことにするか☆？

                    System.Console.WriteLine("（＾ｑ＾）黒２  ☆！");
                    kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示
                }

                {
                    //────────────────────────────────────────
                    // 白２
                    //────────────────────────────────────────
                    Color color = Color.WHITE;
                    bestmoveNode = kwThink.DoBestmove(
                        board01,
                        kifu01,
                        kifu01.Count,// 現在の手数
                        color,
                        komi
                        );
                    kwThink.DropStone(bestmoveNode, color, board01);
                    kifu01.Add(new KifuElementImpl(bestmoveNode, color, 0));// FIXME: とりあえず 0 秒で打ち込んだことにするか☆？

                    System.Console.WriteLine("（＾ｑ＾）白２  ☆！");
                    kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示
                }

                {
                    //────────────────────────────────────────
                    // 黒３
                    //────────────────────────────────────────
                    Color color = Color.BLACK;
                    bestmoveNode = kwThink.DoBestmove(
                        board01,
                        kifu01,
                        kifu01.Count,// 現在の手数
                        color,
                        komi
                        );
                    kwThink.DropStone(bestmoveNode, color, board01);
                    kifu01.Add(new KifuElementImpl(bestmoveNode, color, 0));// FIXME: とりあえず 0 秒で打ち込んだことにするか☆？

                    System.Console.WriteLine("（＾ｑ＾）黒３  ☆！");
                    kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示
                }

                {
                    //────────────────────────────────────────
                    // 白３
                    //────────────────────────────────────────
                    Color color = Color.WHITE;
                    bestmoveNode = kwThink.DoBestmove(
                        board01,
                        kifu01,
                        kifu01.Count,// 現在の手数
                        color,
                        komi
                        );
                    kwThink.DropStone(bestmoveNode, color, board01);
                    kifu01.Add(new KifuElementImpl(bestmoveNode, color, 0));// FIXME: とりあえず 0 秒で打ち込んだことにするか☆？

                    System.Console.WriteLine("（＾ｑ＾）白３  ☆！");
                    kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示
                }

                {
                    //────────────────────────────────────────
                    // 黒４
                    //────────────────────────────────────────
                    Color color = Color.BLACK;
                    bestmoveNode = kwThink.DoBestmove(
                        board01,
                        kifu01,
                        kifu01.Count,// 現在の手数
                        color,
                        komi
                        );
                    kwThink.DropStone(bestmoveNode, color, board01);
                    kifu01.Add(new KifuElementImpl(bestmoveNode, color, 0));// FIXME: とりあえず 0 秒で打ち込んだことにするか☆？

                    System.Console.WriteLine("（＾ｑ＾）黒４  ☆！");
                    kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示
                }

                {
                    //────────────────────────────────────────
                    // 白４
                    //────────────────────────────────────────
                    Color color = Color.WHITE;
                    bestmoveNode = kwThink.DoBestmove(
                        board01,
                        kifu01,
                        kifu01.Count,// 現在の手数
                        color,
                        komi
                        );
                    kwThink.DropStone(bestmoveNode, color, board01);
                    kifu01.Add(new KifuElementImpl(bestmoveNode, color, 0));// FIXME: とりあえず 0 秒で打ち込んだことにするか☆？

                    System.Console.WriteLine("（＾ｑ＾）白４  ☆！");
                    kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示
                }

                {
                    //────────────────────────────────────────
                    // 黒５
                    //────────────────────────────────────────
                    Color color = Color.BLACK;
                    bestmoveNode = kwThink.DoBestmove(
                        board01,
                        kifu01,
                        kifu01.Count,// 現在の手数
                        color,
                        komi
                        );
                    kwThink.DropStone(bestmoveNode, color, board01);
                    kifu01.Add(new KifuElementImpl(bestmoveNode, color, 0));// FIXME: とりあえず 0 秒で打ち込んだことにするか☆？

                    System.Console.WriteLine("（＾ｑ＾）黒５  ☆！");
                    kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示
                }

                {
                    //────────────────────────────────────────
                    // 白５
                    //────────────────────────────────────────
                    Color color = Color.WHITE;
                    bestmoveNode = kwThink.DoBestmove(
                        board01,
                        kifu01,
                        kifu01.Count,// 現在の手数
                        color,
                        komi
                        );
                    kwThink.DropStone(bestmoveNode, color, board01);
                    kifu01.Add(new KifuElementImpl(bestmoveNode, color, 0));// FIXME: とりあえず 0 秒で打ち込んだことにするか☆？

                    System.Console.WriteLine("（＾ｑ＾）白５  ☆！");
                    kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示
                }
            }
            //*/

            System.Console.WriteLine("最後のテストだぜ☆（＾ｑ＾）　最後まで打たそうぜ☆！＼（＾ｑ＾）／");
            System.Console.WriteLine("両者がパスしたら最後だぜ☆（＾▽＾）");
            System.Console.ReadKey();
            int passCount = 0;
            while(true)//無限ループ☆！
            {
                int bestmoveNode;
                {
                    //────────────────────────────────────────
                    // 黒１
                    //────────────────────────────────────────
                    Color color = Color.BLACK;//キータイピング・ミスを防ぐために、変数に小分けしようぜ☆（＾▽＾）ｗｗｗ
                    bestmoveNode = kwThink.DoBestmove(
                        board01,
                        kifu01,
                        kifu01.Count,// 現在の手数
                        color,
                        komi
                        );
                    kwThink.DropStone(bestmoveNode, color, board01);
                    kifu01.Add(new KifuElementImpl(bestmoveNode, color, 0));// FIXME: とりあえず 0 秒で打ち込んだことにするか☆？

                    System.Console.WriteLine("（＾ｑ＾）黒１  ☆！");
                    kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示
                }

                if(bestmoveNode==0)
                {
                    passCount++;

                    if (2 <= passCount)
                    {
                        break;//ループを抜けるぜ☆
                    }
                }
                else
                {
                    passCount = 0;
                }

                {
                    //────────────────────────────────────────
                    // 白１
                    //────────────────────────────────────────
                    Color color = Color.WHITE;
                    bestmoveNode = kwThink.DoBestmove(
                        board01,
                        kifu01,
                        kifu01.Count,// 現在の手数
                        color,
                        komi
                        );
                    kwThink.DropStone(bestmoveNode, color, board01);
                    kifu01.Add(new KifuElementImpl(bestmoveNode, color, 0));// FIXME: とりあえず 0 秒で打ち込んだことにするか☆？

                    System.Console.WriteLine("（＾ｑ＾）白１  ☆！");
                    kwThink.PrintBoard(board01);// （│●│○│●│○）碁盤表示
                }

                if (bestmoveNode == 0)
                {
                    passCount++;

                    if (2 <= passCount)
                    {
                        break;//ループを抜けるぜ☆
                    }
                }
                else
                {
                    passCount = 0;
                }
            }

            kwThink.DoEnd();



            System.Console.WriteLine("（＾＿＾）おつかれさん☆");
            System.Console.WriteLine("（＾ｑ＾）おわりだぜ☆ 何かキーを押せだぜ☆");
            System.Console.ReadKey();
            return;
        }
    }
}
