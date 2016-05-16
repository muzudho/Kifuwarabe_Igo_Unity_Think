//# include <tchar.h>		// Unicode対応の _T() 関数を使用するために。
using n090_core____;///n090_100_core.h"
using n930_print___;///n930_520_libertyOfNodesView.h"


namespace n930_view____
{

    void LibertyOfNodesView::PrintBoard(Core core, LibertyOfNodes* pLibertyOfNodes)
    {
        pLibertyOfNodes->ForeachAllXyWithWaku([&core, &pLibertyOfNodes](int x, int y, bool & isBreak) {
            int node = Board::ConvertToNode(x, y);

            core.PRT(_T("%2d"), pLibertyOfNodes->ValueOf(node));

            if (x == pLibertyOfNodes->GetSize() + 1) {
                core.PRT(_T("\n"));
            }
        });
    }

}