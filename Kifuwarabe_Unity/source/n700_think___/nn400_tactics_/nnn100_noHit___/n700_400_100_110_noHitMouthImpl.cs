using n190_board___;//.Board;
using n700_think___.nn400_tactics_.nnn100_noHit___;//.NoHitMouth;


namespace n700_think___.nn400_tactics_.nnn100_noHit___
{ 
    public class NoHitMouthImpl : NoHitMouth
    {
        public NoHitMouth() {
            //this.m_adjOppo_ = 0;
        }

        /// <summary>
        /// 上下左右に隣接(adjacent)する相手(opponent)の石の数。
        /// </summary>
        private int m_adjOppo_;
        public int GetAdjOppo()
        {
            return this.m_adjOppo_;
        }
        public void SetAdjOppo(int value)
        {
            this.m_adjOppo_ = value;
        }


        public void Research(
                int color,
                int node,
                Board* pBoard
            )
        {
            int invColor = BoardImpl.INVCLR(color);   //白黒反転

            pBoard.ForeachArroundNodes(node, [this, &pBoard, invColor](int adjNode, bool & isBreak) {
                int adjColor = pBoard.ValueOf(adjNode);        // 上下左右隣(adjacent)の石の色

                // 2016-03-12 16:45 Add
                // 隣が相手の石、または枠ならカウントアップ。
                if (adjColor == invColor || adjColor == BoardImpl.WAKU)
                {
                    this.adjOppo++;
                }
            });
        }

        public int Evaluate(int flgCapture)
        {
            int score = 0;

# ifndef RANDOM_MOVE_ONLY

            // 2016-03-12 16:45 Add
            if (this.adjOppo == 3 && !flgCapture)
            {
                // 3方向が相手の石のところで
                // 駒も取れないところには、打ち込みたくない。
            }
            else
            {
                // それ以外の点を、大幅に加点。
                //score += 50;
                score += 100;
            }

#endif

            return score;
        }
    }
}