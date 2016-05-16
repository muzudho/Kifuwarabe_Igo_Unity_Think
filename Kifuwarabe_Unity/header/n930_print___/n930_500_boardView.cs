//using <windows.h>
using n190_board___.Board;


namespace n930_print___
{
    public interface BoardView {

        // 現在の盤面を表示
        static void PrintBoard(Core core, Board* pBoard);
    };

}