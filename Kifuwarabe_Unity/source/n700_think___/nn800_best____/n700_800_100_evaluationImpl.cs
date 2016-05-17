using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;//.Board;.LibertyOfNodes;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn400_tactics_.nnn100_noHit___;//.Move;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn400_tactics_.nnn100_noHit___;//.NoHitSuicide;.NoHitOwnEye;.NoHitMouth;.NoHitHasinohoBocchi;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn400_tactics_.nnn200_hit_____;//.HitRandom;.HitTuke;.HitAte;.HitNobiSaver;.HitGnugo12Random;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn800_best____
{
    public class EvaluationImpl
    {
        // 指定局面の評価値を求めます。
        public static int EvaluateAtNode(
            ref bool		    isAbort         ,// 解なしなら 真。
                int             color           ,// 手番の色
                int             node            ,// 石を置く位置
                Board           board           ,
                LibertyOfNodes  libertyOfNodes
        )
        {
            int invColor = BoardImpl.INVCLR(color); //白黒反転
            NoHitSuicide        noHitSuicide    =new NoHitSuicideImpl()         ;   // 自殺手を打たないようにする仕組み。
            NoHitOwnEye         noHitOwnEye     =new NoHitOwnEyeImpl()          ;   // 自分の眼に打たない仕組み。
            NoHitMouth          noHitMouth      =new NoHitMouthImpl()           ;   // 相手の口に打たない仕組み。
            NoHitHasinohoBocchi noHitHasinoho   =new NoHitHasinohoBocchiImpl()  ;   // 端の方には、ぼっち石　を、あまり打たないようにする仕組み。
            HitRandom           hitRandom       =new HitRandomImpl()            ;   // 手をばらけさせる仕組み。
            HitTuke             hitTuke         =new HitTukeImpl()              ;   // 相手の石に積極的にツケるようにする仕組み。
            HitAte              hitAte          =new HitAteImpl()               ;   // アタリに積極的にアテるようにする仕組み。
            HitNobiSaver        hitNoviServer   =new HitNobiSaverImpl()         ;   // 助けられる石を積極的にノビるようにする仕組み。
            HitGnugo12Random    hitGnugo12Random=new HitGnugo12RandomImpl()     ;   // Gnugo1.2を参考にしたランダム。
            int score = 0;                          // 読んでいる手の評価値


            if (board.ValueOf(node) == BoardImpl.BLACK || board.ValueOf(node) == BoardImpl.WHITE)
            {
                // 石があるなら
                //# ifdef CHECK_LOG
                //System.Console.WriteLine(string.Format("石がある。"));
                //System.Console.WriteLine(string.Format("\n"));
                //#endif
                isAbort = true;
                goto gt_EndMethod;
            }
            else if (board.ValueOf(node) == BoardImpl.WAKU)
            {
                // 枠なら
                //# ifdef CHECK_LOG
                //System.Console.WriteLine(string.Format("枠。"));
                //System.Console.WriteLine(string.Format("\n"));
                //#endif
                isAbort = true;
                goto gt_EndMethod;
            }
            else if (node == board.GetKouNode())
            {
                // コウになる位置なら
                //# ifdef CHECK_LOG
                //System.Console.WriteLine(string.Format("コウ。 "));
                //System.Console.WriteLine(string.Format("\n"));
                //#endif
                isAbort = true;
                goto gt_EndMethod;
            }

            int x, y;
            AbstractBoard.ConvertToXy(out x, out y, node);
            int libertyOfRen = libertyOfNodes.ValueOf(node);



            noHitMouth.Research(color, node, board);       // 相手の口に石を打ち込む状況でないか調査。


            Liberty[] liberties = new Liberty[4] {// 上隣 → 右隣 → 下隣 → 左隣
                new LibertyImpl(),
                new LibertyImpl(),
                new LibertyImpl(),
                new LibertyImpl(),
            };
            board.ForeachArroundDirAndNodes(node, (int iDir, int adjNode, ref bool isBreak) =>{
                int adjColor = board.ValueOf(adjNode);            // 上下左右隣(adjacent)の石の色
                liberties[iDir].Count(adjNode, adjColor, board);   // 隣の石（または連）の呼吸点　の数を数えます。
            });

            // ツケるかどうかを評価
            int nTuke = hitTuke.Evaluate(invColor, node, liberties, board);

            // アテるかどうかを評価
            int nAte = hitAte.Evaluate( color, node, board, libertyOfNodes);

            if (noHitOwnEye.IsThis(color, node, liberties, board))
            {
                // 自分の眼に打ち込む状況か調査
                //# ifdef CHECK_LOG
                //System.Console.WriteLine(string.Format("自分の眼に打ち込むのを回避。"));
                //System.Console.WriteLine(string.Format("\n"));
                //#endif
                isAbort = true;
                goto gt_EndMethod;
            }

            if (noHitSuicide.IsThis( color, node, liberties, board))
            {
                // 自殺手になる状況でないか調査。
                 //# ifdef CHECK_LOG
                 //System.Console.WriteLine(string.Format("自殺手を回避。"));
                 //System.Console.WriteLine(string.Format("\n"));
                 //#endif
                isAbort = true;
                goto gt_EndMethod;
            }

            //# ifdef EVAL_LOG
            //System.Console.WriteLine(string.Format("$({0:D},{1:D}) ", x, y));
            //System.Console.WriteLine(string.Format("LibRen={0:D} スコア=", libertyOfRen));
            //#endif

            int nHitRandom = hitRandom.Evaluate(); // 0 ～ 99 のランダムな評価値を与える。

            //----------------------------------------
            // 自分の眼を埋める、自殺手を打つ、のチェック終了後にする処理
            //----------------------------------------

            int nNoHitMouth = noHitMouth.Evaluate(noHitSuicide.IsCapture());

            noHitHasinoho.Research(node, board);
            int nNoHitHasinoho = noHitHasinoho.Evaluate();

            // ノビるかどうかを評価
            int nNobiSaver = hitNoviServer.Evaluate( color, node, board, libertyOfNodes);

            // Gnugo1.2みたいに打ちたい
            int nHitGnugo12Random = hitGnugo12Random.Evaluate(color, node, board);

            //----------------------------------------
            // 集計
            //----------------------------------------

            // ばらしたい
            //# ifdef EVAL_LOG
            //System.Console.WriteLine(string.Format("b{0:D},", nHitRandom));
            //#endif
            score += nHitRandom;

            // マウスに打ちたくない
            //# ifdef EVAL_LOG
            //System.Console.WriteLine(string.Format("m{0:D},", nNoHitMouth));
            //#endif
            score += nNoHitMouth;

            // ツケたい
            //# ifdef EVAL_LOG
            //System.Console.WriteLine(string.Format("t{0:D},", nTuke));
            //#endif
            score += nTuke;

            // アテたい
            //# ifdef EVAL_LOG
            //System.Console.WriteLine(string.Format("a{0:D},", nAte));
            //#endif
            score += nAte;

            // ノビたい
            //# ifdef EVAL_LOG
            //System.Console.WriteLine(string.Format("n{0:D},", nNobiSaver));
            //#endif
            score += nNobiSaver;

            // 端の方に打ちたくない
            //# ifdef EVAL_LOG
            //System.Console.WriteLine(string.Format("h{0:D},", nNoHitHasinoho));
            //#endif
            score += nNoHitHasinoho;

            // Gnugo1.2みたいに打ちたい
            //# ifdef EVAL_LOG
            //System.Console.WriteLine(string.Format("g{0:D},", nHitGnugo12Random));
            //#endif
            score += nHitGnugo12Random;

            //# ifdef EVAL_LOG
            //            System.Console.WriteLine(string.Format("[{0:D}] \n", score));
            //#endif

            gt_EndMethod:
            return score;
        }
    }
}