using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;//.Liberty;.LibertyOfNodes;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn400_tactics_.nnn200_hit_____
{
    public class HitNobiSaverImpl : HitNobiSaver
    {
        public int Evaluate(
            Color color,
            int node,
            Board board,
            LibertyOfNodes libertyOfNodes
        ){
            int score = 0;

    //# ifndef RANDOM_MOVE_ONLY

            // 石を置く前の、上、右、下、左　にある自分の石（または連）の呼吸点の数の合計。
            int currentAdjLibertySum = 0;
            libertyOfNodes.ForeachArroundNodes(node, (int adjNode, ref bool isBreak)=> {
                if (board.ValueOf(adjNode) == color)//自分の石
                {
                    currentAdjLibertySum += libertyOfNodes.ValueOf(adjNode);
                }
            });

            //----------------------------------------
            // 危機でも無いのに伸ばしてしまうということがないよう、呼吸点が 1～3 のときを危機と定義します。
            //----------------------------------------
            if (!(0 < currentAdjLibertySum && currentAdjLibertySum < 4))
            {
                // 危機じゃなかった。
                goto gt_EndMethod;
            }
            // これで0による除算も回避☆

            // 呼吸点に自分の石を置いたと考えて、石を置いた局面のその自分の石（または連）の呼吸点を数えます。
            Liberty futureLiberty = new LibertyImpl();
            futureLiberty.Count(node, color, board);

            // 評価値計算
            if (futureLiberty.GetLiberty() <= currentAdjLibertySum)
            {
                // ツケて　呼吸点が増えていないようでは話しになりません。
                //score += 0;
            }
            else
            {
                // ツケて　呼吸点が増えているので、どれだけ増えたかを数えます。
                int upLiberty = futureLiberty.GetLiberty() - currentAdjLibertySum;

                //score += 40  // 40を基本に。
                score += 1  // 1 を基本に。
                    +
                    (upLiberty - 1) * 50    // 呼吸点が２以上増えるなら、
                                            // 呼吸点が１増えるたびに 50 点のボーナス。
                    /
                    (currentAdjLibertySum * currentAdjLibertySum * currentAdjLibertySum)
                    // ツケる前の呼吸点の数が大きいほど、
                    // スコアが減る（緊急の関心を薄れさせる）仕掛け。
                    // 呼吸点 1 = 1／1 点
                    // 呼吸点 2 = 1／8 点
                    // 呼吸点 3 = 1／27 点
                    // 呼吸点 4 = 1／64 点
                    ;
            }

            //----------------------------------------
            // 効き目に倍率を掛けます。
            //----------------------------------------
            score *= 80 / 10;

            gt_EndMethod:

    //#endif

            return score;
        }
    }
}