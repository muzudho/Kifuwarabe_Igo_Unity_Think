using n090_core____.Core;
using n190_board___.Board;
using n700_think___.nn400_tactics_.nnn100_noHit___.NoHitHasinohoBocchi;


namespace n700_think___.nn400_tactics_.nnn100_noHit___
{ 
    public class NoHitHasinohoBocchiImpl : NoHitHasinohoBocchi
    {
        public NoHitHasinohoBocchi()
        {
            this->isBocchi = false;
            this->isSoto = false;
            this->isEdge = false;
            this->isCorner = false;
        }

        public void Research(
            int node,
            Board* pBoard
        ){

            this->isSoto = false;
            this->isEdge = false;
            this->isCorner = false;



            this->isBocchi = true;
            pBoard->ForeachArroundNodes(node, [this, &pBoard](int adjNode, bool & isBreak) {
                int adjColor = pBoard->ValueOf(adjNode);        // その色

                if (adjColor == BLACK || adjColor == WHITE)
                {
                    // ぼっちではない。
                    this->isBocchi = false;
                    isBreak = true;
                    goto gt_Next;
                }

                gt_Next:
                ;
            });


            int x, y;
            Board::ConvertToXy(x, y, node);

            if (x < 1 || pBoard->GetSize() < x ||
                y < 1 || pBoard->GetSize() < y
                )
            {
                // 盤外
                //PRT(_T("(%d,%d) ban=%d ; Soto \n"), x, y, boardSize);
                this->isSoto = true;
                goto gt_EndMethod;
            }

            if (x == 1 || pBoard->GetSize() == x ||
                y == 1 || pBoard->GetSize() == y
            )
            {
                // 辺
                //PRT(_T("(%d,%d) ban=%d ; EDGE \n"), x, y, boardSize);
                this->isEdge = true;
            }
            else
            {
                //PRT(_T("(%d,%d) ban=%d ; ------ \n"), x, y, boardSize);
                goto gt_EndMethod;
            }

            if ((x == 1 || pBoard->GetSize() == x) &&
                (y == 1 || pBoard->GetSize() == y)
            )
            {
                // 角
                //PRT(_T("(%d,%d) ban=%d ; CORNER \n"), x, y, boardSize);
                this->isCorner = true;
            }

            gt_EndMethod:
            ;
        }

        // 評価値を出します。
        public int Evaluate(
            )
        {
            int score = 100;

# ifndef RANDOM_MOVE_ONLY

            if (!this->isBocchi)
            {
                // ぼっち石でなければ、気にせず　端でも角でも置きます。
                goto gt_EndMethod;
            }

            if (this->isCorner)
            {
                // 角に　ぼっち石　を置きたくない。
                score -= 50;
                goto gt_EndMethod;
            }

            if (this->isEdge)
            {
                // 辺に　ぼっち石　を置きたくない。
                score -= 33;
                goto gt_EndMethod;
            }

            gt_EndMethod:

#endif

            return score;
        }
    }
}