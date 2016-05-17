using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;//.Board;.Liberty;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn400_tactics_.nnn200_hit_____;//.HitTuke;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn400_tactics_.nnn200_hit_____
{ 
    public class HitTukeImpl : HitTuke
    {
        public HitTukeImpl()
        {
        }

        public int Evaluate(
            Color invColor,
            int         node,
            Liberty[]   liberties,//[4]
            Board pBoard
        ){
            int score = 0;

//# ifndef RANDOM_MOVE_ONLY
//# ifdef ENABLE_MOVE_ATTACK
            // 評価値の計算（４方向分繰り返す）
            pBoard.ForeachArroundDirAndNodes(node, (int iDir, int adjNode, ref bool isBreak) =>{
                Color adjColor = pBoard.ValueOf(adjNode);        // その色

                score +=
                    (adjColor == invColor ? 1 : 0)                      // 隣の石
                                                                //		相手の石: 1
                                                                //		それ以外: 0
                                                                //   ×
                    * liberties[iDir].GetRenIshi()              // 連の石の数
                                                                //   ×
                    * (10 / (liberties[iDir].GetLiberty() + 1));// 連の呼吸点の個数
                                                                //		0個: 10点
                                                                //		1個:  5点
                                                                //		2個:  3.3333...点
                                                                //		3個:  2.5点
                                                                //		4個:  2点
                                                                //		...
            });
//#endif
//#endif

            return score;
        }
    }
}