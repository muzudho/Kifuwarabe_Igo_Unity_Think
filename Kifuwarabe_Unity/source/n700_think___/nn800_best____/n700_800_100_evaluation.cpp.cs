using n090_core____;///n090_100_core.h"
using n190_board___;///n190_100_board.h"
using n190_board___;///n190_200_libertyOfNodes.h"
using n400_robotArm;///n400_100_move.h"

using n700_think___.nn400_tactics_.nnn100_noHit___;///n700_400_100_050_noHitSuicide.h"
using n700_think___.nn400_tactics_.nnn100_noHit___;///n700_400_100_100_noHitOwnEye.h"
using n700_think___.nn400_tactics_.nnn100_noHit___;///n700_400_100_110_noHitMouth.h"
using n700_think___.nn400_tactics_.nnn100_noHit___;///n700_400_100_120_noHitHasinohoBocchi.h"

using n700_think___.nn400_tactics_.nnn200_hit_____;///n700_400_200_050_hitRandom.h"
using n700_think___.nn400_tactics_.nnn200_hit_____;///n700_400_200_100_hitTuke.h"
using n700_think___.nn400_tactics_.nnn200_hit_____;///n700_400_200_150_hitAte.h"
using n700_think___.nn400_tactics_.nnn200_hit_____;///n700_400_200_200_hitNobiSaver.h"
using n700_think___.nn400_tactics_.nnn200_hit_____;///n700_400_200_250_hitGnugo12Random.h"

using n700_think___.nn800_best____;//n700_800_100_evaluation.h"


namespace n700_think___.nn800_best____
{

    int Evaluation::EvaluateAtNode(
    Core core,
    int&			flgAbort        ,
    int color,
    int node,
    Board* pBoard,
    LibertyOfNodes* pLibertyOfNodes
)
    {
        int invColor = INVCLR(color);               //白黒反転
        NoHitSuicide noHitSuicide;      // 自殺手を打たないようにする仕組み。
        NoHitOwnEye noHitOwnEye;        // 自分の眼に打たない仕組み。
        NoHitMouth noHitMouth;          // 相手の口に打たない仕組み。
        NoHitHasinohoBocchi noHitHasinoho;      // 端の方には、ぼっち石　を、あまり打たないようにする仕組み。
        HitRandom hitRandom;            // 手をばらけさせる仕組み。
        HitTuke hitTuke;            // 相手の石に積極的にツケるようにする仕組み。
        HitAte hitAte;              // アタリに積極的にアテるようにする仕組み。
        HitNobiSaver hitNoviServer;     // 助けられる石を積極的にノビるようにする仕組み。
        HitGnugo12Random hitGnugo12Random;  // Gnugo1.2を参考にしたランダム。
        int score = 0;                  // 読んでいる手の評価値


        if (pBoard->ValueOf(node) == BLACK || pBoard->ValueOf(node) == WHITE) {
            // 石があるなら
# ifdef CHECK_LOG
            core.PRT(_T("石がある。"));
            core.PRT(_T("\n"));
#endif
            flgAbort = 1;
            goto gt_EndMethod;
        } else if (pBoard->ValueOf(node) == WAKU) {
            // 枠なら
# ifdef CHECK_LOG
            core.PRT(_T("枠。"));
            core.PRT(_T("\n"));
#endif
            flgAbort = 1;
            goto gt_EndMethod;
        } else if (node == pBoard->kouNode) {
            // コウになる位置なら
# ifdef CHECK_LOG
            core.PRT(_T("コウ。 "));
            core.PRT(_T("\n"));
#endif
            flgAbort = 1;
            goto gt_EndMethod;
        }

        int x, y;
        AbstractBoard::ConvertToXy(x, y, node);
        int libertyOfRen = pLibertyOfNodes->ValueOf(node);



        noHitMouth.Research(color, node, pBoard);       // 相手の口に石を打ち込む状況でないか調査。


        Liberty liberties[4];// 上隣 → 右隣 → 下隣 → 左隣
        pBoard->ForeachArroundDirAndNodes(node, [&pBoard, &liberties](int iDir, int adjNode, bool & isBreak) {
            int adjColor = pBoard->ValueOf(adjNode);            // 上下左右隣(adjacent)の石の色
            liberties[iDir].Count(adjNode, adjColor, pBoard);   // 隣の石（または連）の呼吸点　の数を数えます。
        });

        // ツケるかどうかを評価
        int nTuke = hitTuke.Evaluate(invColor, node, liberties, pBoard);

        // アテるかどうかを評価
        int nAte = hitAte.Evaluate(core, color, node, pBoard, pLibertyOfNodes);

        if (noHitOwnEye.IsThis(color, node, liberties, pBoard)) {// 自分の眼に打ち込む状況か調査
# ifdef CHECK_LOG
            core.PRT(_T("自分の眼に打ち込むのを回避。"));
            core.PRT(_T("\n"));
#endif
            flgAbort = 1;
            goto gt_EndMethod;
        }

        if (noHitSuicide.IsThis(core, color, node, liberties, pBoard)) {// 自殺手になる状況でないか調査。
# ifdef CHECK_LOG
            core.PRT(_T("自殺手を回避。"));
            core.PRT(_T("\n"));
#endif
            flgAbort = 1;
            goto gt_EndMethod;
        }

# ifdef EVAL_LOG
        core.PRT(_T("$(%d,%d) "), x, y);
        core.PRT(_T("LibRen=%d スコア="), libertyOfRen);
#endif

        int nHitRandom = hitRandom.Evaluate(); // 0 ～ 99 のランダムな評価値を与える。

        //----------------------------------------
        // 自分の眼を埋める、自殺手を打つ、のチェック終了後にする処理
        //----------------------------------------

        int nNoHitMouth = noHitMouth.Evaluate(noHitSuicide.flgCapture);

        noHitHasinoho.Research(node, pBoard);
        int nNoHitHasinoho = noHitHasinoho.Evaluate();

        // ノビるかどうかを評価
        int nNobiSaver = hitNoviServer.Evaluate(core, color, node, pBoard, pLibertyOfNodes);

        // Gnugo1.2みたいに打ちたい
        int nHitGnugo12Random = hitGnugo12Random.Evaluate(color, node, pBoard);

        //----------------------------------------
        // 集計
        //----------------------------------------

        // ばらしたい
# ifdef EVAL_LOG
        core.PRT(_T("b%d,"), nHitRandom);
#endif
        score += nHitRandom;

        // マウスに打ちたくない
# ifdef EVAL_LOG
        core.PRT(_T("m%d,"), nNoHitMouth);
#endif
        score += nNoHitMouth;

        // ツケたい
# ifdef EVAL_LOG
        core.PRT(_T("t%d,"), nTuke);
#endif
        score += nTuke;

        // アテたい
# ifdef EVAL_LOG
        core.PRT(_T("a%d,"), nAte);
#endif
        score += nAte;

        // ノビたい
# ifdef EVAL_LOG
        core.PRT(_T("n%d,"), nNobiSaver);
#endif
        score += nNobiSaver;

        // 端の方に打ちたくない
# ifdef EVAL_LOG
        core.PRT(_T("h%d,"), nNoHitHasinoho);
#endif
        score += nNoHitHasinoho;

        // Gnugo1.2みたいに打ちたい
# ifdef EVAL_LOG
        core.PRT(_T("g%d,"), nHitGnugo12Random);
#endif
        score += nHitGnugo12Random;

# ifdef EVAL_LOG
        core.PRT(_T("[%d] \n"), score);
#endif

        gt_EndMethod:
        return score;
    }


}