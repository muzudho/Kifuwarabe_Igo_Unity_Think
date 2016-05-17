namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___
{ 
    public class LibertyImpl : Liberty
    {
        // 既にこの石を検索した場合は真
        public bool[] checkedBoard = new bool[AbstractTable<Color>.ANOMALY_BOARD_MAX];

        // 連のリバティ（石の呼吸点）の数(再帰関数で使う)
        public int m_liberty_;
        public int GetLiberty()
        {
            return this.m_liberty_;
        }
        public void SetLiberty(int value)
        {
            this.m_liberty_ = value;
        }
        public void IncreaseLiberty()
        {
            this.m_liberty_++;
        }

        /// <summary>
        /// 隣接する（１個あるいは連の）石の数(再帰関数で使う)
        /// </summary>
        public int m_renIshi_;
        public int GetRenIshi()
        {
            return this.m_renIshi_;
        }
        public void SetRenIshi(int value)
        {
            this.m_renIshi_ = value;
        }
        public void IncreaseRenIshi()
        {
            this.m_renIshi_++;
        }


        public LibertyImpl()
        {
            //this.SetLiberty(0);
	        this.SetRenIshi( 0);
        }

        public void Count(int node, Color color, Table<Color> board)
        {
            // 眼に打ち込まないか、口の中に打ち込まないか、の処理のあとに
            if (color == Color.EMPTY || color == Color.WAKU)
            {
                // 空っぽか、枠なら。
                //System.Console.WriteLine( "空っぽか、枠。 \n" );
                goto gt_EndMethod;
            }

            int i;

            this.SetLiberty(0);
            this.SetRenIshi(0);
            for (i = 0; i < AbstractTable<Color>.ANOMALY_BOARD_MAX; i++)
            {
                this.checkedBoard[i] = false;
            }

            this.CountElement(node, color, board);
            //this.CountElement(node, pBoard.ValueOf(node), pBoard);

            gt_EndMethod:
            return;
        }

        public void CountElement(int tNode, Color color, Table<Color> board)
        {

            this.checkedBoard[tNode] = true;    // この石は検索済み	
            this.IncreaseRenIshi();             // 呼吸点を数えている（１個または連の）
                                                // 石の数

            board.ForeachArroundNodes(tNode, (int adjNode, ref bool isBreak)=> {
                if (this.checkedBoard[adjNode])
                {
                    goto gt_Next;
                }
                if (board.ValueOf(adjNode) == 0)
                {                // 空点
                    this.checkedBoard[adjNode] = true;  // この空点は検索済みとする
                    this.IncreaseLiberty();             // リバティの数
                }
                if (board.ValueOf(adjNode) == color)
                {
                    this.CountElement(adjNode, color, board); // 未探索の自分の石
                }
                gt_Next:
                ;
            });
        }
    }
}