using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;//.Board;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn400_tactics_.nnn100_noHit___;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn400_tactics_.nnn100_noHit___
{
    // 盤上の端の方に　ぼっちの石（隣接する石がない石）　を打たない仕組み。
    // 呼吸点が塞いでしまって　得をしにくい。
    public interface NoHitHasinohoBocchi : NoHit {

        /// <summary>
        /// ぼっち石なら真。
        /// </summary>
        bool IsBocchi();
        void SetBocchi(bool value);

        /// <summary>
        /// 盤外なら真。
        /// </summary>
        bool IsSoto();
        void SetSoto(bool value);

        /// <summary>
        /// 枠に隣接しているなら真。
        /// </summary>
        bool IsEdge();
        void SetEdge(bool value);

        /// <summary>
        /// 角なら真。
        /// </summary>
        bool IsCorner();
        void SetCorner(bool value);


        // どのような状況か調査。
        void Research(
            int node,
            Table<Color> board
        );

        // 評価値を出します。
        int Evaluate(
        );
    };

}