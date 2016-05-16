using n190_board___;///n190_100_board.h"
using n700_think___.nn400_tactics_.nnn100_noHit___;///n700_400_100_000_noHit.h"


namespace n700_think___.nn400_tactics_.nnn100_noHit___
{
    // 盤上の端の方に　ぼっちの石（隣接する石がない石）　を打たない仕組み。
    // 呼吸点が塞いでしまって　得をしにくい。
    public class NoHitHasinohoBocchi : NoHit {

        // ぼっち石なら真。
        private bool isBocchi;
        // 盤外なら真。
        private bool isSoto;
        // 枠に隣接しているなら真。
        private bool isEdge;
        // 角なら真。
        private bool isCorner;


    public NoHitHasinohoBocchi();

        // どのような状況か調査。
        public void Research(
            int node,
            Board* pBoard
        );

        // 評価値を出します。
        public int Evaluate(
        );
    };

}