using n190_board___;//n190_050_abstractBoard.h"
using n190_board___;///n190_100_board.h"


namespace n190_board___
{ 

// 自分の位置を含む石、または連の総呼吸点の数を、各交点に格納したテーブルです。
public class LibertyOfNodes : AbstractBoard{

    public LibertyOfNodes();
        public ~LibertyOfNodes();

        public void Initialize( Board* pBoard);
};

}