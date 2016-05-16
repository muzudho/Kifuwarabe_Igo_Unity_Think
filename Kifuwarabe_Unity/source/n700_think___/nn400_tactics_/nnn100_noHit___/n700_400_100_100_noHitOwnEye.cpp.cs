using n190_board___;///n190_100_board.h"
using n190_board___;///n190_150_liberty.h"
using n700_think___.nn400_tactics_.nnn100_noHit___;///n700_400_100_100_noHitOwnEye.h"


namespace n700_think___.nn400_tactics_.nnn100_noHit___
{ 

NoHitOwnEye::NoHitOwnEye()
{

    this->safe = 0;
};




    bool NoHitOwnEye::IsThis(
        int color,
        int node,
        Liberty liberties[4]    ,
        Board* pBoard
    )
    {
        bool result = false;

        pBoard->ForeachArroundDirAndNodes(node, [this, &pBoard, &liberties, color](int iDir, int adjNode, bool & isBreak) {
            int adjColor = pBoard->ValueOf(adjNode);        // 上下左右隣(adjacent)の石の色

            // 次の２つは　安全なつながり方です。
            // (１)枠につなげる。
            // (２)呼吸点が 2 以上ある（＝石を置いても呼吸点が 1 以上残る、
            //     自殺手にはならない）味方につながる。
            if (
                adjColor == WAKU
                ||
                (adjColor == color && 2 <= liberties[iDir].liberty)
                ) {
                //PRT(_T("安全な隣接。 \n"));
                this->safe++;
            }
        });


        if (this->safe == 4) { // 四方が　自分の石や、壁に　囲まれている場所（眼）になるなら
                               //PRT(_T("眼には打たない。 \n"));
                               // 眼には打たない。
            result = 1;
            goto gt_EndMethod;
        }

        gt_EndMethod:
        return result;
    }
}