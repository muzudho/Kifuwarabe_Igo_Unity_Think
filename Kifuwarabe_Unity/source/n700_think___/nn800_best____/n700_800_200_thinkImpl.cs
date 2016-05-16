//#include <time.h>		// clock() を使用するために。
using n190_board___;//.Board;.LibertyOfNodes;
using n400_robotArm;//.Move;
using n700_think___.nn400_tactics_.nnn100_noHit___;//.NoHitSuicide;.NoHitOwnEye;
using n800_scene___;//.EndgameImpl;


namespace n700_think___.nn800_best____
{ 
    public class ThinkImpl
    {

        /// <summary>
        /// 乱数に近い評価関数。少し石を取りに行くように。
        /// </summary>
        /// <param name="coler">石の色☆</param>
        /// <param name="board"></param>
        /// <param name="libertyOfNodes"></param>
        /// <returns></returns>
        public static int Bestmove(
            int             color,
            Board           board,
            LibertyOfNodes  libertyOfNodes
        ){

//# ifdef ENABLE_MOVE_RETRY
            int retry = 0;
            // 異常回避☆！　リトライ☆！

            gt_Retry:// 戻ってくるラベル☆（＾▽＾）
            ;
//#endif


            int maxScore;   // 今まで読んだ手で一番高かった評価値
            int bestmoveNode;

            // 実行するたびに違う値が得られるように現在の時刻で乱数を初期化
            //TODO: srand((unsigned)clock());

            //----------------------------------------
            // 石を置く位置１つずつ、その手の評価値を算出します。
            //----------------------------------------
            maxScore = -1;
            bestmoveNode = 0; // 0 ならパス。

            board.ForeachAllNodesWithoutWaku((int node, ref bool isBreak) =>{
                //{
                //int x, y;
                //AbstractBoard::ConvertToXy(x, y, node);
                //System.Console.WriteLine(string.Format("#({0:D},{1:D}) ", x, y));
                //}

                // この局面で、石を置いたときの評価値
                bool isAbort = false;
                int score;      // 読んでいる手の評価値
                score = EvaluationImpl.EvaluateAtNode(
                    ref isAbort, color, node, board, libertyOfNodes);

                if (isAbort)
                {
                    goto gt_Next;
                }

                // ベストムーブを更新します。
                // System.Console.WriteLine( string.Format("x,y=({0:D},{1:D})={2:D}\n",x,y,score));
                if (maxScore < score)
                {
                    maxScore = score;
                    bestmoveNode = node;
                }
                gt_Next:
                ;
            });

//# ifdef ENABLE_MOVE_RETRY

            int x, y;
            AbstractBoard.ConvertToXy(out x, out y, bestmoveNode);

            // 異常回避☆！　リトライ☆！
            if (bestmoveNode == 0)
            {
                // パスは正常です。
                goto gt_EndMethod;
            }
            else if (400 < retry)
            {
                // 諦めてパスにします。
                bestmoveNode = 0;
                goto gt_EndMethod;
            }
            else if (board.ValueOf(bestmoveNode) == BoardImpl.BLACK || board.ValueOf(bestmoveNode) == BoardImpl.WHITE)
            {
                // 石があるなら
                System.Console.WriteLine(string.Format("({0:D},{1:D})　石がある。　リトライ☆！　[{2:D}]\n", x, y, retry));
                retry++;
                goto gt_Retry;
            }
            else if (board.ValueOf(bestmoveNode) == BoardImpl.WAKU)
            {
                // 枠なら
                System.Console.WriteLine(string.Format("({0:D},{1:D})　枠だった。　リトライ☆！　[{2:D}]\n", x, y, retry));
                retry++;
                goto gt_Retry;
            }
            else if (bestmoveNode == board.GetKouNode())
            {
                // コウになる位置なら
                System.Console.WriteLine(string.Format("({0:D},{1:D})　コウになる。　リトライ☆！　[{2:D}]\n", x, y, retry));
                retry++;
                goto gt_Retry;
            }

            Liberty[] liberties = new Liberty[4]{// 上隣 → 右隣 → 下隣 → 左隣
                new LibertyImpl(),
                new LibertyImpl(),
                new LibertyImpl(),
                new LibertyImpl(),
            };
            board.ForeachArroundDirAndNodes(bestmoveNode, (int iDir, int adjNode, ref bool isBreak) =>{
                int adjColor = board.ValueOf(adjNode);            // 上下左右隣(adjacent)の石の色
                liberties[iDir].Count(adjNode, adjColor, board);   // 隣の石（または連）の呼吸点　の数を数えます。
            });

            // 自分の眼潰しチェック
            {
                NoHitOwnEye noHitOwnEye = new NoHitOwnEyeImpl();        // 自分の眼に打たない仕組み。
                if (noHitOwnEye.IsThis(color, bestmoveNode, liberties, board))
                {// 自分の眼に打ち込む状況か調査
                    System.Console.WriteLine(string.Format("({0:D},{1:D})　自分の眼に打ち込む。　リトライ☆！　[{2:D}]\n", x, y, retry));
                    retry++;
                    goto gt_Retry;
                }
            }

            // 自殺手チェック
            {
                NoHitSuicide noHitSuicide = new NoHitSuicideImpl();      // 自殺手を打たないようにする仕組み。

                if (noHitSuicide.IsThis( color, bestmoveNode, liberties, board))
                {// 自殺手になる状況でないか調査。
                    System.Console.WriteLine(string.Format("({0:D},{1:D})　自殺手になる。　リトライ☆！　[{2:D}]\n", x, y, retry));
                    retry++;
                    goto gt_Retry;
                }
            }
//#endif

            gt_EndMethod:
            return bestmoveNode;
        }
    }
}
