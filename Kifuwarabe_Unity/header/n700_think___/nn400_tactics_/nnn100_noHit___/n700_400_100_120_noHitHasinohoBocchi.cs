using n190_board___.Board;
using n700_think___.nn400_tactics_.nnn100_noHit___.NoHit;


namespace n700_think___.nn400_tactics_.nnn100_noHit___
{
    // 盤上の端の方に　ぼっちの石（隣接する石がない石）　を打たない仕組み。
    // 呼吸点が塞いでしまって　得をしにくい。
    public interface NoHitHasinohoBocchi : NoHit {

        // ぼっち石なら真。
        private bool isBocchi;
        // 盤外なら真。
        private bool isSoto;
        // 枠に隣接しているなら真。
        private bool isEdge;
        // 角なら真。
        private bool isCorner;


        NoHitHasinohoBocchi();

        // どのような状況か調査。
        void Research(
            int node,
            Board* pBoard
        );

        // 評価値を出します。
        int Evaluate(
        );
    };

}