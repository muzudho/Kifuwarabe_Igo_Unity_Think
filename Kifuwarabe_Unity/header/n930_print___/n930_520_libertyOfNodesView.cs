//# include <windows.h>
using n190_board___.Board;
using n190_board___.LibertyOfNodes;


namespace n930_print___
{
    public interface LibertyOfNodesView {

        // 現在の盤面を表示
        static void PrintBoard(Core core, LibertyOfNodes* pLibertyOfNodes);
    };

}