using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;//.Board
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn400_tactics_.nnn100_noHit___;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn800_move____
{
    /// <summary>
    /// 石を置けない理由。
    /// </summary>
    public enum NoMoveReason
    {

        /// <summary>
        /// 石は置ける。
        /// </summary>
        None,

        /// <summary>
        /// 石があるから。
        /// </summary>
        ExistsStone,

        /// <summary>
        /// 枠だから。
        /// </summary>
        OutOfBoard,

        /// <summary>
        /// コウだから。
        /// </summary>
        Kou,

        /// <summary>
        /// 自殺手だから。
        /// </summary>
        Suicide,

        /// <summary>
        /// 自分の眼だから。（着手禁止点ではないが）
        /// </summary>
        OwnEye
    }

    public class UtilMove
    {
        /// <summary>
        /// 石を置けるか調べます。
        /// </summary>
        /// <returns></returns>
        public static bool CanMove(
                int color,
                int node,
                Board board,
            out NoMoveReason noMoveReason // 理由
            )
        {
            bool canMove;

            int x, y;
            AbstractBoard.ConvertToXy(out x, out y, node);

            if (board.ValueOf(node) == BoardImpl.BLACK || board.ValueOf(node) == BoardImpl.WHITE)
            {
                // 石があるなら
                canMove = false;
                noMoveReason = NoMoveReason.ExistsStone;
                goto gt_EndMethod;
            }
            else if (board.ValueOf(node) == BoardImpl.WAKU)
            {
                // 枠なら
                canMove = false;
                noMoveReason = NoMoveReason.OutOfBoard;
                goto gt_EndMethod;
            }
            else if (node == board.GetKouNode())
            {
                // コウになる位置なら
                canMove = false;
                noMoveReason = NoMoveReason.Kou;
                goto gt_EndMethod;
            }


            Liberty[] liberties = new Liberty[4]{// 上隣 → 右隣 → 下隣 → 左隣
                new LibertyImpl(),
                new LibertyImpl(),
                new LibertyImpl(),
                new LibertyImpl(),
            };
            board.ForeachArroundDirAndNodes(node, (int iDir, int adjNode, ref bool isBreak) => {

                int adjColor = board.ValueOf(adjNode);            // 上下左右隣(adjacent)の石の色

                liberties[iDir].Count(adjNode, adjColor, board);   // 隣の石（または連）の呼吸点　の数を数えます。

            });

            // 自殺手チェック
            {
                NoHitSuicide noHitSuicide = new NoHitSuicideImpl();      // 自殺手を打たないようにする仕組み。

                if (noHitSuicide.IsThis(color, node, liberties, board))
                {
                    // 自殺手になる状況でないか調査。
                    canMove = false;
                    noMoveReason = NoMoveReason.Suicide;
                    goto gt_EndMethod;
                }
            }

            // 自分の眼潰しチェック
            {
                NoHitOwnEye noHitOwnEye = new NoHitOwnEyeImpl();        // 自分の眼に打たない仕組み。
                if (noHitOwnEye.IsThis(color, node, liberties, board))
                {
                    // 自分の眼に打ち込む状況か調査
                    canMove = false;
                    noMoveReason = NoMoveReason.OwnEye;
                    goto gt_EndMethod;
                }
            }

            canMove = true;
            noMoveReason = NoMoveReason.None;

            gt_EndMethod:
            return canMove;
        }
    }
}