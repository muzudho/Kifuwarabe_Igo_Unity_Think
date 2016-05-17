using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;//.Board;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn400_tactics_.nnn100_noHit___;//.NoHitMouth;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn400_tactics_.nnn100_noHit___
{ 
    public class NoHitMouthImpl : NoHitMouth
    {
        public NoHitMouthImpl() {
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
        public void IncreaseAdjOppo()
        {
            this.m_adjOppo_++;
        }


        public void Research(
                Color color,
                int node,
                Board board
            )
        {
            Color invColor = ConvColor.INVCLR(color);   //白黒反転

            board.ForeachArroundNodes(node, (int adjNode, ref bool isBreak) =>{
                Color adjColor = board.ValueOf(adjNode);        // 上下左右隣(adjacent)の石の色

                // 2016-03-12 16:45 Add
                // 隣が相手の石、または枠ならカウントアップ。
                if (adjColor == invColor || adjColor == Color.Waku)
                {
                    this.IncreaseAdjOppo();
                }
            });
        }

        public int Evaluate(bool isCapture)
        {
            int score = 0;

//# ifndef RANDOM_MOVE_ONLY

            // 2016-03-12 16:45 Add
            if (this.GetAdjOppo() == 3 && !isCapture)
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

//#endif

            return score;
        }
    }
}