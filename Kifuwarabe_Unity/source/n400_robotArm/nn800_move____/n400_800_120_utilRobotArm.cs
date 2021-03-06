﻿using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn800_move____
{
    public abstract class UtilRobotArm {

        /// <summary>
        /// 石を置きます。
        /// </summary>
        /// <param name="node"></param>
        /// <param name="color"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public static DroppedResult DropStone(
            int node,
            Color color,
            Board board
        ){
            //----------------------------------------
            // Undo用に記憶
            //----------------------------------------
            board.SetKouNodeForUndo( board.GetKouNode());       // コウの位置を退避
            board.SetMoveNodeForUndo( node);             // 石を置いた位置を記憶

            //----------------------------------------

            int sum;
            int delNode = 0;
            int tottaIshi = 0;              // 取った石の合計
            Color invClr = ConvColor.INVCLR(color); // 相手の石の色

            //----------------------------------------
            // パスの場合
            //----------------------------------------
            if (node == 0)
            {
                // 操作を受け付けます。
                board.SetKouNodeForUndo( board.GetKouNode());
                board.SetKouNode(0);
                return DroppedResult.Success;
            }

            //----------------------------------------
            // コウに置こうとした場合
            //----------------------------------------
            if (node == board.GetKouNode())
            {
                System.Console.WriteLine(string.Format("move() Err: コウ！z={0:x4}\n", node));
                // 操作を弾きます。
                return DroppedResult.Kou;
            }

            //----------------------------------------
            // 空点でないところに置こうとした場合
            //----------------------------------------
            if (board.ValueOf(node) != 0)
            {
                System.Console.WriteLine(string.Format("move() Err: 空点ではない！z={0:x4}\n", node));
                // 操作を弾きます。
                return DroppedResult.ExistsStone;
            }

            board.SetValue(node, color);  // とりあえず置いてみる

            // ここから下は、石を置いたあとの盤面です。

            // 自殺手になるかどうか判定するために、
            // また、新しい　コウ　を作るかどうか判定するために、
            // 相手の石を取るところまで進めます。
            board.ForeachArroundNodes(node, (int adjNode, ref bool isBreak) =>{
                Liberty liberty1 = new LibertyImpl();

                Color adjColor = board.ValueOf(adjNode);
                if (adjColor != invClr)
                {
                    // 隣接する石が　相手の石　でないなら無視。
                    goto gt_Next1;
                }

                //----------------------------------------
                // 相手の石が取れるか判定します。
                //----------------------------------------

                // 隣接する石（連）の呼吸点を数えます。
                liberty1.Count(adjNode, adjColor, board);

                if (liberty1.GetLiberty() == 0)
                {
                    // 呼吸点がないようなら、石（連）は取れます。

                    // 囲んだ石の数を　ハマに加点。
                    board.AddHama(color, liberty1.GetRenIshi());
                    tottaIshi += liberty1.GetRenIshi();
                    delNode = adjNode;  // 取られた石の座標。コウの判定で使う。

                    // 処理が被らないように、囲まれている相手の石（計算済み）を消します。
                    BoardImpl.DeleteRenStones(board, adjNode, invClr);
                }

                gt_Next1:
                ;
            });

            //----------------------------------------
            // 自殺手になるかを判定
            //----------------------------------------
            Liberty liberty = new LibertyImpl();
            liberty.Count(node, color, board);

            if (liberty.GetLiberty() == 0)
            {
                // 置いた石に呼吸点がない場合。

                // 操作を弾きます。
                //System.Console.WriteLine(string.Format("move() Err: 自殺手! z={0:x4}\n", node));
                board.SetValue(node, 0);
                return DroppedResult.Suicide;
            }

            //----------------------------------------
            // 次にコウになる位置を判定しておく。
            //----------------------------------------


            // コウになるのは、石を1つだけ取った場合です。
            if (tottaIshi == 1)
            {
                // 取られた石の4方向に、自分の呼吸点が1個の連が1つだけある場合、その位置はコウ。
                sum = 0;
                board.ForeachArroundNodes(delNode, (int adjNode, ref bool isBreak) =>{

                    Liberty liberty2 = new LibertyImpl();

                    Color adjColor = board.ValueOf(adjNode);
                    if (adjColor != color)
                    {
                        goto gt_Next2;
                    }

                    liberty2.Count(adjNode, adjColor, board);
                    if (liberty2.GetLiberty() == 1 && liberty2.GetRenIshi() == 1)
                    {
                        sum++;
                    }

                    gt_Next2:
                    ;
                });

                if (sum >= 2)
                {
                    System.Console.WriteLine(string.Format(
                        "１つ取られて、コウの位置へ打つと、１つの石を2つ以上取れる？node={0:x4}\n", node));
                    // これはエラー。

                    // 操作を弾きます。
                    return DroppedResult.Fatal;
                }

                if (sum == 0)
                {
                    board.SetKouNodeForUndo( board.GetKouNode());
                    board.SetKouNode( 0);    // コウにはならない。
                }
                else
                {
                    board.SetKouNodeForUndo( board.GetKouNode());
                    board.SetKouNode( delNode);  // 取り合えず取られた石の場所をコウの位置とする
                }
            }
            else
            {
                // コウではない
                board.SetKouNodeForUndo( board.GetKouNode());
                board.SetKouNode(0);
            }

            //----------------------------------------
            // 置くことに成功☆（＾▽＾）！
            //----------------------------------------

            // 操作を受け入れます。
            return DroppedResult.Success;
        }

        /// <summary>
        /// 石を置く前の状態に戻します。置いた石に対して１回のみ使用可能です。
        /// </summary>
        /// <param name="board"></param>
        public static void UndropStoneOnce(Board board)
        {
            // 石を置く前の状態に戻します。
            board.SetKouNode( board.GetKouNodeForUndo());           // コウの位置を元に戻します。
            board.SetValue(board.GetMoveNodeForUndo(), 0);       // 置いた石を消します。

            board.SetKouNodeForUndo(0);
            board.SetMoveNodeForUndo( 0);
        }
    }
}