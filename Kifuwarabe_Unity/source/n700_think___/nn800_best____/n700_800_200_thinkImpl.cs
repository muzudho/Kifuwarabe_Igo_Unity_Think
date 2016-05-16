// muzudho: ログ出力のために。
//#include <iostream>
//#include <fstream>
//using namespace std;

//#include <time.h>		// clock() を使用するために。
//#include <tchar.h>		// Unicode対応の _T() 関数を使用するために。
using n090_core____.Core;
using n190_board___.Board;
using n190_board___.LibertyOfNodes;
using n400_robotArm.Move;
using n700_think___.nn400_tactics_.nnn100_noHit___.NoHitSuicide;
using n700_think___.nn400_tactics_.nnn100_noHit___.NoHitOwnEye;
using n700_think___.nn800_best____.Evaluation;
using n700_think___.nn800_best____.Think;
using n800_scene___.Endgame;


namespace n700_think___.nn800_best____
{ 
    public class ThinkImpl : Think
    {
        public int Bestmove(
            Core core,
            int color,
            Board* pBoard,
            LibertyOfNodes* pLibertyOfNodes
        ){
# ifdef CHECK_LOG
            core.PRT(_T("Bestmove開始☆！ \n"));
#endif

# ifdef ENABLE_MOVE_RETRY
            int retry = 0;
            // 異常回避☆！　リトライ☆！
            gt_Retry:
            ;
#endif


            int maxScore;   // 今まで読んだ手で一番高かった評価値
            int bestmoveNode;

            // 実行するたびに違う値が得られるように現在の時刻で乱数を初期化
            srand((unsigned)clock());

            //----------------------------------------
            // 石を置く位置１つずつ、その手の評価値を算出します。
            //----------------------------------------
            maxScore = -1;
            bestmoveNode = 0; // 0 ならパス。

            pBoard->ForeachAllNodesWithoutWaku([color, &maxScore, &bestmoveNode, &pBoard, &pLibertyOfNodes, &core](int node, bool & isBreak) {
                //{
                //int x, y;
                //AbstractBoard::ConvertToXy(x, y, node);
                //core.PRT(_T("#(%d,%d) "), x, y);
                //}

                // この局面で、石を置いたときの評価値
                int flgAbort = 0;
                int score;      // 読んでいる手の評価値
                score = Evaluation::EvaluateAtNode(core, flgAbort, color, node, pBoard, pLibertyOfNodes);
                if (flgAbort)
                {
                    goto gt_Next;
                }

                // ベストムーブを更新します。
                // PRT("x,y=(%d,%d)=%d\n",x,y,score);
                if (maxScore < score)
                {
                    maxScore = score;
                    bestmoveNode = node;
                }
                gt_Next:
                ;
            });

# ifdef ENABLE_MOVE_RETRY

            int x, y;
            AbstractBoard::ConvertToXy(x, y, bestmoveNode);

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
            else if (pBoard->ValueOf(bestmoveNode) == BLACK || pBoard->ValueOf(bestmoveNode) == WHITE)
            {
                // 石があるなら
                core.PRT(_T("(%d,%d)　石がある。　リトライ☆！　[%d]\n"), x, y, retry);
                retry++;
                goto gt_Retry;
            }
            else if (pBoard->ValueOf(bestmoveNode) == WAKU)
            {
                // 枠なら
                core.PRT(_T("(%d,%d)　枠だった。　リトライ☆！　[%d]\n"), x, y, retry);
                retry++;
                goto gt_Retry;
            }
            else if (bestmoveNode == pBoard->kouNode)
            {
                // コウになる位置なら
                core.PRT(_T("(%d,%d)　コウになる。　リトライ☆！　[%d]\n"), x, y, retry);
                retry++;
                goto gt_Retry;
            }

            Liberty liberties[4];// 上隣 → 右隣 → 下隣 → 左隣
            pBoard->ForeachArroundDirAndNodes(bestmoveNode, [&pBoard, &liberties](int iDir, int adjNode, bool & isBreak) {
                int adjColor = pBoard->ValueOf(adjNode);            // 上下左右隣(adjacent)の石の色
                liberties[iDir].Count(adjNode, adjColor, pBoard);   // 隣の石（または連）の呼吸点　の数を数えます。
            });

            // 自分の眼潰しチェック
            {
                NoHitOwnEye noHitOwnEye;        // 自分の眼に打たない仕組み。
                if (noHitOwnEye.IsThis(color, bestmoveNode, liberties, pBoard))
                {// 自分の眼に打ち込む状況か調査
                    core.PRT(_T("(%d,%d)　自分の眼に打ち込む。　リトライ☆！　[%d]\n"), x, y, retry);
                    retry++;
                    goto gt_Retry;
                }
            }

            // 自殺手チェック
            {
                NoHitSuicide noHitSuicide;      // 自殺手を打たないようにする仕組み。

                if (noHitSuicide.IsThis(core, color, bestmoveNode, liberties, pBoard))
                {// 自殺手になる状況でないか調査。
                    core.PRT(_T("(%d,%d)　自殺手になる。　リトライ☆！　[%d]\n"), x, y, retry);
                    retry++;
                    goto gt_Retry;
                }
            }
#endif

            gt_EndMethod:
            return bestmoveNode;
        }
    }
}
