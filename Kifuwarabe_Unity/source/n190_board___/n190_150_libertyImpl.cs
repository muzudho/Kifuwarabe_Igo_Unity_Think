using n190_board___.Liberty;


namespace n190_board___
{ 
    public class LibertyImpl : Liberty
    {
        public Liberty()
        {
            this->liberty = 0;
	        this->renIshi = 0;
        }

        public void Count(int node, int color, Board* pBoard)
        {
            // 眼に打ち込まないか、口の中に打ち込まないか、の処理のあとに
            if (color == EMPTY || color == WAKU)
            {
                // 空っぽか、枠なら。
                //PRT(_T("空っぽか、枠。 \n"));
                goto gt_EndMethod;
            }

            int i;

            this->liberty = 0;
            this->renIshi = 0;
            for (i = 0; i < BOARD_MAX; i++)
            {
                this->checkedBoard[i] = 0;
            }

            this->CountElement(node, color, pBoard);
            //this->CountElement(node, pBoard->ValueOf(node), pBoard);

            gt_EndMethod:
            return;
        }

        public void CountElement(int tNode, int color, Board* pBoard)
        {

            this->checkedBoard[tNode] = 1;                  // この石は検索済み	
            this->renIshi++;                                // 呼吸点を数えている（１個または連の）
                                                            // 石の数

            pBoard->ForeachArroundNodes(tNode, [this, color, &pBoard](int adjNode, bool & isBreak) {
                if (this->checkedBoard[adjNode])
                {
                    goto gt_Next;
                }
                if (pBoard->ValueOf(adjNode) == 0)
                {                // 空点
                    this->checkedBoard[adjNode] = 1;            // この空点は検索済みとする
                    this->liberty++;                            // リバティの数
                }
                if (pBoard->ValueOf(adjNode) == color)
                {
                    this->CountElement(adjNode, color, pBoard); // 未探索の自分の石
                }
                gt_Next:
                ;
            });
        }
    }
}