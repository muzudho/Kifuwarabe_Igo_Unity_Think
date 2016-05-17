using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;//.Board;.Liberty;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn400_tactics_.nnn100_noHit___;//.NoHitOwnEye;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn400_tactics_.nnn100_noHit___
{ 
    public class NoHitOwnEyeImpl : NoHitOwnEye
    {
        public NoHitOwnEyeImpl()
        {
            //this.SetSafe( 0);
        }

        /// <summary>
        /// ４方向に隣接する、呼吸点が増える　つなげられる味方の石が　いくつか。0～4。
        /// </summary>
        private int m_safe_;
        public int GetSafe()
        {
            return this.m_safe_;
        }
        public void SetSafe(int value)
        {
            this.m_safe_ = value;
        }
        public void IncreaseSafe()
        {
            this.m_safe_++;
        }


        public bool IsThis(
            Color       color,
            int         node,
            Liberty[]   liberties,//[4]
            Table<Color> board
        ){
            bool result = false;

            board.ForeachArroundDirAndNodes(node, (int iDir, int adjNode, ref bool isBreak) =>{
                Color adjColor = board.ValueOf(adjNode);        // 上下左右隣(adjacent)の石の色

                // 次の２つは　安全なつながり方です。
                // (１)枠につなげる。
                // (２)呼吸点が 2 以上ある（＝石を置いても呼吸点が 1 以上残る、
                //     自殺手にはならない）味方につながる。
                if (
                    adjColor == Color.WAKU
                    ||
                    (adjColor == color && 2 <= liberties[iDir].GetLiberty())
                    )
                {
                    //System.Console.WriteLine(string.Format("安全な隣接。 \n"));
                    this.IncreaseSafe();
                }
            });


            if (this.GetSafe() == 4)
            { // 四方が　自分の石や、壁に　囲まれている場所（眼）になるなら
              //System.Console.WriteLine(string.Format("眼には打たない。 \n"));
              // 眼には打たない。
                result = true;
                goto gt_EndMethod;
            }

            gt_EndMethod:
            return result;
        }
    }
}