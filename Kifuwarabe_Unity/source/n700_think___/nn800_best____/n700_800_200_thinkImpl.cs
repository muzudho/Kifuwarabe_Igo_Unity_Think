using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;//.Board;.LibertyOfNodes;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn400_tactics_.nnn100_noHit___;//.NoHitSuicide;.NoHitOwnEye;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn800_move____;//.Move;
//using Grayscale.Kifuwarabe_Igo_Unity_Think.n800_scene___;//.EndgameImpl;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn800_best____
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
            Color color,
            Board board,
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
                        System.Console.WriteLine(string.Format("({0:D},{1:D})　石がある。　リトライ☆！　[{2:D}]\n", x, y, retry));
                        break;
                    case NoMoveReason.OutOfBoard:
                        // 枠なら
                        System.Console.WriteLine(string.Format("({0:D},{1:D})　枠だった。　リトライ☆！　[{2:D}]\n", x, y, retry));
                        break;
                    case NoMoveReason.Kou:
                        // コウになる位置なら
                        System.Console.WriteLine(string.Format("({0:D},{1:D})　コウになる。　リトライ☆！　[{2:D}]\n", x, y, retry));
                        break;
                    case NoMoveReason.Suicide:
                        // 自殺手になるなら
                        System.Console.WriteLine(string.Format("({0:D},{1:D})　自殺手になる。　リトライ☆！　[{2:D}]\n", x, y, retry));
                        break;
                    case NoMoveReason.OwnEye:
                        // 自分の眼になるなら
                        System.Console.WriteLine(string.Format("({0:D},{1:D})　自分の眼に打ち込む。　リトライ☆！　[{2:D}]\n", x, y, retry));
                        break;
                }

                // 石を置けないなら☆
                retry++;
                goto gt_Retry;
            }
//#endif

            gt_EndMethod:
            return bestmoveNode;
        }
    }
}
