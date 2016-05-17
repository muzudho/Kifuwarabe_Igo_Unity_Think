using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;//.Board;.LibertyOfNodes;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn400_tactics_.nnn200_hit_____;//.Hit;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn400_tactics_.nnn200_hit_____
{

    // アテようとします。
    //
    // _____
    // __1__
    // _xox_
    // __x__
    // _____
    //
    // 上図 o を相手の石、 x を自分の石とし、
    // 1 に自分の石を置く評価が高くなる仕組み。
    //
    // ただし、1 の地点がコウになる場合は置きません。
    //
    // _____
    // __o__
    // _o1o_
    // _xox_
    // __x__
    // _____
    public interface HitAte : Hit {

        // 評価値を出します。
        int Evaluate(
            Color color,
		    int				node,
		    Table<Color> board,
		    LibertyOfNodes 	libertyOfNodes
		);
    }
}
