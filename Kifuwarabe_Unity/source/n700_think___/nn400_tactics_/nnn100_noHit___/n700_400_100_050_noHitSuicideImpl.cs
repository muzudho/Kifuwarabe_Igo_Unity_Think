using n190_board___;//n190_100_board.h" /n190_150_liberty.h"
using n400_robotArm;///n400_100_move.h"
using n700_think___.nn400_tactics_.nnn100_noHit___;///n700_400_100_050_noHitSuicide.h"


namespace n700_think___.nn400_tactics_.nnn100_noHit___
{ 
    public class NoHitSuicideImpl : NoHitSuicide
    {
        public NoHitSuicide() {
            //this.m_flgCapture_ = 0;
        }

        /// <summary>
        /// 敵石を取ったフラグ
        /// </summary>
        private int m_flgCapture_;
        public int GetFlgCapture()
        {
            return this.m_flgCapture_;
        }
        public void SetFlgCapture(int value)
        {
            this.m_flgCapture_ = value;
        }



        // 自殺手になる状況でないか調査。
        public bool IsThis(
            int color,
            int node,
            Liberty liberties[4],
            Board* pBoard
        ){
            bool result = false;
            int invColor = BoardImpl.INVCLR(color);   //白黒反転

            pBoard.ForeachArroundDirAndNodes(node, [this, &pBoard, &liberties, invColor](int iDir, int adjNode, bool & isBreak) {
                int adjColor = pBoard.ValueOf(adjNode);        // 上下左右隣(adjacent)の石の色

                // 隣に、呼吸点が 1 個の相手の石があれば、それは取ることができます。
                if (adjColor == invColor && liberties[iDir].liberty == 1)
                {
                    //System.Console.WriteLine(string.Format("敵石を取った。 \n"));
                    this.flgCapture = 1;   // 敵石を、取ったフラグ。
                }
            });

            MoveResult flgMove;    // 移動結果の種類

            if (this.flgCapture == 0)
            {                    // 石が取れない場合
                                 // 実際に置いてみて　自殺手かどうか判定
                Move move;
                flgMove = move.MoveOne(node, color, pBoard);      // 石を置きます。コウの位置が変わるかも。

                // 石を置く前の状態に戻します。
                move.UndoOnce(pBoard);

                if (flgMove == MoveResult.MOVE_SUICIDE)
                {      // 自殺手なら
                       //System.Console.WriteLine(string.Format("自殺手は打たない。 \n"));
                       // ベストムーブにはなりえない
                    result = true;
                    goto gt_EndMethod;
                }
            }

            gt_EndMethod:
            return result;
        }
    }
}