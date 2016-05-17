using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;//n190_100_board.h" /n190_150_liberty.h"
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn800_move____;///n400_100_move.h"
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn400_tactics_.nnn100_noHit___;///n700_400_100_050_noHitSuicide.h"


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn400_tactics_.nnn100_noHit___
{ 
    public class NoHitSuicideImpl : NoHitSuicide
    {
        public NoHitSuicideImpl() {
            //this.m_flgCapture_ = 0;
        }

        /// <summary>
        /// 敵石を取ったフラグ
        /// </summary>
        private bool m_isCapture_;
        public bool IsCapture()
        {
            return this.m_isCapture_;
        }
        public void SetCapture(bool value)
        {
            this.m_isCapture_ = value;
        }



        // 自殺手になる状況でないか調査。
        public bool IsThis(
            int         color,
            int         node,
            Liberty[]   liberties,//[4]
            Board       board
        ){
            bool result = false;
            int invColor = BoardImpl.INVCLR(color);   //白黒反転

            board.ForeachArroundDirAndNodes(node, (int iDir, int adjNode,ref bool isBreak) =>{
                int adjColor = board.ValueOf(adjNode);        // 上下左右隣(adjacent)の石の色

                // 隣に、呼吸点が 1 個の相手の石があれば、それは取ることができます。
                if (adjColor == invColor && liberties[iDir].GetLiberty() == 1)
                {
                    //System.Console.WriteLine(string.Format("敵石を取った。 \n"));
                    this.SetCapture(true);   // 敵石を、取ったフラグ。
                }
            });

            MoveResult flgMove;    // 移動結果の種類

            if (!this.IsCapture())
            {                    // 石が取れない場合
                                 // 実際に置いてみて　自殺手かどうか判定
                Move move = new MoveImpl();
                flgMove = move.MoveOne(node, color, board);      // 石を置きます。コウの位置が変わるかも。

                // 石を置く前の状態に戻します。
                move.UndoOnce(board);

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