//# include <vector>
using n090_core____;///n090_100_core.h"
using n190_board___;///n190_050_abstractBoard.h"


namespace n190_board___
{



    //--------------------------------------------------------------------------------
    // 定義
    //--------------------------------------------------------------------------------

    // 碁石
#define EMPTY 0		// 空きスペース
#define BLACK 1
#define WHITE 2
#define WAKU  3		// 盤外

#define INVCLR(x) (3-(x))		// 石の色を反転させる


    public class Board : AbstractBoard{

	// 1手進めたことで消えたコウの場所を覚えておくものです。（戻せるのは１回だけです）
	private int kouNodeForUndo;
        // 石を置いた位置を覚えておくものです。（戻せるのは１回だけです）
        private int moveNodeForUndo;
        // 次にコウになる位置。無ければ 0。
        private int kouNode;

        // ハマ。取った石の数のこと。[0]...空き。[1]... 黒が取った石の数, [2]...白が取った石の数
        private int hama[3];


        public void Initialize( int initBoard[]);

        public Board();
        public ~Board();

        // 指定したnode（石）に隣接している空きスペース（1以上3以下）を配列に入れて返します。
        public std::vector<int> GetOpenNodesOfStone(
		Core	core	,
		int		node	,
		int		size123		// 1～3 のいずれかを指定してください。
		);

        // 連になっている石を消す。１個の石でも消す。
        public void DeleteRenStones(
		int tNode,
		int color
	);

};

}