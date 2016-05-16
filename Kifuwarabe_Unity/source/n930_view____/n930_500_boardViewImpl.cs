//# include <tchar.h>		// Unicode対応の _T() 関数を使用するために。
using n090_core____.Core;
using n930_print___.BoardView;


namespace n930_view____
{
    public class BoardViewImpl : BoardView
    {
        public void PrintBoard(Core core, Board* pBoard)
        {
            _TCHAR* str[4] = {
        _T("・"),	// 空き
		_T("●"),	// 黒石
		_T("○"),	// 白石
		_T("＋")		// 枠
	};

            pBoard->ForeachAllXyWithWaku([&pBoard, &core, &str](int x, int y, bool & isBreak) {
                int node = Board::ConvertToNode(x, y);
                core.PRT(_T("%s"), str[pBoard->ValueOf(node)]);
                if (x == pBoard->GetSize() + 1)
                {
                    core.PRT(_T("\n"));
                }
            });
        }
    }
}