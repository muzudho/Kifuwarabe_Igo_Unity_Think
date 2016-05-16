using n090_core____.Core;
using n190_board___.Board;
using n190_board___.Liberty;
using n400_robotArm.Move;


namespace n400_robotArm
{
    public class MoveImpl : Move {
        public MoveImpl()
        {
        }

        ~MoveImpl()
        {
        }

        public int MoveOne(
            Core core,
            int node,
            int color,
            Board* pBoard
        ){
            //----------------------------------------
            // Undo用に記憶
            //----------------------------------------
            pBoard->kouNodeForUndo = pBoard->kouNode;       // コウの位置を退避
            pBoard->moveNodeForUndo = node;             // 石を置いた位置を記憶

            //----------------------------------------

            int sum;
            int delNode = 0;
            int tottaIshi = 0;              // 取った石の合計
            int invClr = INVCLR(color); // 相手の石の色

            //----------------------------------------
            // パスの場合
            //----------------------------------------
            if (node == 0)
            {
                // 操作を受け付けます。
                pBoard->kouNodeForUndo = pBoard->kouNode;
                pBoard->kouNode = 0;
                return MOVE_SUCCESS;
            }

            //----------------------------------------
            // コウに置こうとした場合
            //----------------------------------------
            if (node == pBoard->kouNode)
            {
                core.PRT(_T("move() Err: コウ！z=%04x\n"), node);
                // 操作を弾きます。
                return MOVE_KOU;
            }

            //----------------------------------------
            // 空点でないところに置こうとした場合
            //----------------------------------------
            if (pBoard->ValueOf(node) != 0)
            {
                core.PRT(_T("move() Err: 空点ではない！z=%04x\n"), node);
                // 操作を弾きます。
                return MOVE_EXIST;
            }

            pBoard->SetValue(node, color);  // とりあえず置いてみる

            // ここから下は、石を置いたあとの盤面です。

            // 自殺手になるかどうか判定するために、
            // また、新しい　コウ　を作るかどうか判定するために、
            // 相手の石を取るところまで進めます。
            pBoard->ForeachArroundNodes(node, [&pBoard, &tottaIshi, &delNode, color, invClr](int adjNode, bool & isBreak) {
                Liberty liberty1;

                int adjColor = pBoard->ValueOf(adjNode);
                if (adjColor != invClr)
                {
                    // 隣接する石が　相手の石　でないなら無視。
                    goto gt_Next1;
                }

                //----------------------------------------
                // 相手の石が取れるか判定します。
                //----------------------------------------

                // 隣接する石（連）の呼吸点を数えます。
                liberty1.Count(adjNode, adjColor, pBoard);

                if (liberty1.liberty == 0)
                {
                    // 呼吸点がないようなら、石（連）は取れます。

                    // 囲んだ石の数を　ハマに加点。
                    pBoard->hama[color] += liberty1.renIshi;
                    tottaIshi += liberty1.renIshi;
                    delNode = adjNode;  // 取られた石の座標。コウの判定で使う。

                    // 処理が被らないように、囲まれている相手の石（計算済み）を消します。
                    pBoard->DeleteRenStones(adjNode, invClr);
                }

                gt_Next1:
                ;
            });

            //----------------------------------------
            // 自殺手になるかを判定
            //----------------------------------------
            Liberty liberty;
            liberty.Count(node, color, pBoard);

            if (liberty.liberty == 0)
            {
                // 置いた石に呼吸点がない場合。

                // 操作を弾きます。
                //core.PRT(_T("move() Err: 自殺手! z=%04x\n"), node);
                pBoard->SetValue(node, 0);
                return MOVE_SUICIDE;
            }

            //----------------------------------------
            // 次にコウになる位置を判定しておく。
            //----------------------------------------


            // コウになるのは、石を1つだけ取った場合です。
            if (tottaIshi == 1)
            {
                // 取られた石の4方向に、自分の呼吸点が1個の連が1つだけある場合、その位置はコウ。
                sum = 0;
                pBoard->ForeachArroundNodes(delNode, [&pBoard, &sum, color](int adjNode, bool & isBreak) {
                    Liberty liberty2;

                    int adjColor = pBoard->ValueOf(adjNode);
                    if (adjColor != color)
                    {
                        goto gt_Next2;
                    }

                    liberty2.Count(adjNode, adjColor, pBoard);
                    if (liberty2.liberty == 1 && liberty2.renIshi == 1)
                    {
                        sum++;
                    }

                    gt_Next2:
                    ;
                });

                if (sum >= 2)
                {
                    core.PRT(_T("１つ取られて、コウの位置へ打つと、１つの石を2つ以上取れる？node=%04x\n"), node);
                    // これはエラー。

                    // 操作を弾きます。
                    return MOVE_FATAL;
                }

                if (sum == 0)
                {
                    pBoard->kouNodeForUndo = pBoard->kouNode;
                    pBoard->kouNode = 0;    // コウにはならない。
                }
                else
                {
                    pBoard->kouNodeForUndo = pBoard->kouNode;
                    pBoard->kouNode = delNode;  // 取り合えず取られた石の場所をコウの位置とする
                }
            }
            else
            {
                // コウではない
                pBoard->kouNodeForUndo = pBoard->kouNode;
                pBoard->kouNode = 0;
            }

            //----------------------------------------
            // 置くことに成功☆（＾▽＾）！
            //----------------------------------------

            // 操作を受け入れます。
            return MOVE_SUCCESS;
        }

        public void UndoOnce(Core core, Board* pBoard)
        {
            // 石を置く前の状態に戻します。
            pBoard->kouNode = pBoard->kouNodeForUndo;           // コウの位置を元に戻します。
            pBoard->SetValue(pBoard->moveNodeForUndo, 0);       // 置いた石を消します。

            pBoard->kouNodeForUndo = 0;
            pBoard->moveNodeForUndo = 0;
        }
    }
}