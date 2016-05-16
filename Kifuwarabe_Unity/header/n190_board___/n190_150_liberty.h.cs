using n190_board___;///n190_100_board.h"

namespace n190_board___
{





    public class Liberty {

        // 既にこの石を検索した場合は1
        public int checkedBoard[BOARD_MAX];

        // 連のリバティ（石の呼吸点）の数(再帰関数で使う)
        public int liberty;

        // 隣接する（１個あるいは連の）石の数(再帰関数で使う)
        public int renIshi;


	public Liberty();


        // 位置 node におけるリバティ（石の呼吸点）の数と石の数を計算。結果は liberty 変数に格納。
        public void Count(
            int node,
            int color,
            Board* pBoard
        );

        // リバティ（石の呼吸点）と石の数える再帰関数
        // 4方向を調べて、空白だったら+1、自分の石なら再帰で。相手の石、壁ならそのまま。
        public void CountElement(
            int tNode,
            int color,
            Board* pBoard
        );
    };


}