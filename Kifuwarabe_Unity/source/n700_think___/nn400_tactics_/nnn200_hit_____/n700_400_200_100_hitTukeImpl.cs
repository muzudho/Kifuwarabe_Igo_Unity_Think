using n190_board___.Board;
using n190_board___.Liberty;
using n700_think___.nn400_tactics_.nnn200_hit_____.HitTuke;


namespace n700_think___.nn400_tactics_.nnn200_hit_____
{ 
    public class HitTukeImpl : HitTuke
    {
        public HitTuke()
        {
        }

        public int Evaluate(
            int invColor,
            int node,
            Liberty liberties[4],
            Board* pBoard
        ){
            int score = 0;

# ifndef RANDOM_MOVE_ONLY
# ifdef ENABLE_MOVE_ATTACK
            // 評価値の計算（４方向分繰り返す）
            pBoard->ForeachArroundDirAndNodes(node, [&pBoard, &liberties, &score, invColor](int iDir, int adjNode, bool & isBreak) {
                int adjColor = pBoard->ValueOf(adjNode);        // その色

                score +=
                    (adjColor == invColor)      // 隣の石
                                                //		相手の石: 1
                                                //		それ以外: 0
                                                //   ×
                    * liberties[iDir].renIshi   // 連の石の数
                                                //   ×
                    * (10 / (liberties[iDir].liberty + 1)); // 連の呼吸点の個数
                                                            //		0個: 10点
                                                            //		1個:  5点
                                                            //		2個:  3.3333...点
                                                            //		3個:  2.5点
                                                            //		4個:  2点
                                                            //		...
            });
#endif
#endif

            return score;
        }
    }
}