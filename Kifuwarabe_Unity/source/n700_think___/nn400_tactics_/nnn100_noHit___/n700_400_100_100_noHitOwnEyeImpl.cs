using n190_board___;//.Board;.Liberty;
using n700_think___.nn400_tactics_.nnn100_noHit___;//.NoHitOwnEye;


namespace n700_think___.nn400_tactics_.nnn100_noHit___
{ 
    public class NoHitOwnEyeImpl : NoHitOwnEye
    {
        public NoHitOwnEye()
        {
            this.safe = 0;
        }

        public bool IsThis(
            int color,
            int node,
            Liberty liberties[4]    ,
            Board* pBoard
        ){
            bool result = false;

            pBoard.ForeachArroundDirAndNodes(node, [this, &pBoard, &liberties, color](int iDir, int adjNode, bool & isBreak) {
                int adjColor = pBoard.ValueOf(adjNode);        // 上下左右隣(adjacent)の石の色

                // 次の２つは　安全なつながり方です。
                // (１)枠につなげる。
                // (２)呼吸点が 2 以上ある（＝石を置いても呼吸点が 1 以上残る、
                //     自殺手にはならない）味方につながる。
                if (
                    adjColor == BoardImpl.WAKU
                    ||
                    (adjColor == color && 2 <= liberties[iDir].liberty)
                    )
                {
                    //System.Console.WriteLine(string.Format("安全な隣接。 \n"));
                    this.safe++;
                }
            });


            if (this.safe == 4)
            { // 四方が　自分の石や、壁に　囲まれている場所（眼）になるなら
              //System.Console.WriteLine(string.Format("眼には打たない。 \n"));
              // 眼には打たない。
                result = 1;
                goto gt_EndMethod;
            }

            gt_EndMethod:
            return result;
        }
    }
}