//#include <windows.h> // コンソールへの出力等
using n190_board___;///n190_100_board.h"
using n190_board___;///n190_150_liberty.h"
using n400_robotArm;///n400_100_move.h"
using n700_think___/nn400_tactics_/nnn100_noHit___;///n700_400_100_050_noHitSuicide.h"


namespace n700_think___.nn400_tactics_.nnn100_noHit___
{ 

NoHitSuicide::NoHitSuicide() {

    this->flgCapture = 0;
}




    // 自殺手になる状況でないか調査。
    bool NoHitSuicide::IsThis(
        Core core,
        int color,
        int node,
        Liberty liberties[4],
        Board* pBoard
    )
    {
        bool result = false;
        int invColor = INVCLR(color);   //白黒反転

        pBoard->ForeachArroundDirAndNodes(node, [this, &pBoard, &liberties, invColor](int iDir, int adjNode, bool & isBreak) {
            int adjColor = pBoard->ValueOf(adjNode);        // 上下左右隣(adjacent)の石の色

            // 隣に、呼吸点が 1 個の相手の石があれば、それは取ることができます。
            if (adjColor == invColor && liberties[iDir].liberty == 1) {
                //PRT(_T("敵石を取った。 \n"));
                this->flgCapture = 1;   // 敵石を、取ったフラグ。
            }
        });

        int flgMove;    // 移動結果の種類

        if (this->flgCapture == 0) {                    // 石が取れない場合
                                                        // 実際に置いてみて　自殺手かどうか判定
            Move move;
            flgMove = move.MoveOne(core, node, color, pBoard);      // 石を置きます。コウの位置が変わるかも。

            // 石を置く前の状態に戻します。
            move.UndoOnce(core, pBoard);

            if (flgMove == MOVE_SUICIDE) {      // 自殺手なら
                                                //PRT(_T("自殺手は打たない。 \n"));
                                                // ベストムーブにはなりえない
                result = true;
                goto gt_EndMethod;
            }
        }

        gt_EndMethod:
        return result;
    }

}