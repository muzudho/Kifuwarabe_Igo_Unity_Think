//# include <tchar.h>		// Unicode対応の _T() 関数を使用するために。
using n090_core____.Core;
using n930_print___.LibertyOfNodesView;


namespace n930_view____
{
    public class LibertyOfNodesViewImpl : LibertyOfNodesView
    {
        void PrintBoard(Core core, LibertyOfNodes* pLibertyOfNodes)
        {
            pLibertyOfNodes->ForeachAllXyWithWaku([&core, &pLibertyOfNodes](int x, int y, bool & isBreak) {
                int node = Board::ConvertToNode(x, y);

                core.PRT(_T("%2d"), pLibertyOfNodes->ValueOf(node));

                if (x == pLibertyOfNodes->GetSize() + 1)
                {
                    core.PRT(_T("\n"));
                }
            });
        }
    }
}