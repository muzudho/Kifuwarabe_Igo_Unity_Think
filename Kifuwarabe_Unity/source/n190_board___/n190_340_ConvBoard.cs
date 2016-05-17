using System.Linq;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn800_move____;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___
{
    public abstract class ConvBoard
    {
        /// <summary>
        /// intの配列を、colorの配列に変換します。
        /// </summary>
        /// <param name="manualTable"></param>
        /// <param name="size">19路盤なら19。</param>
        /// <param name="board"></param>
        public static void IntToColor(int[] manualTable, int boardSize, out Board board)
        {
            // 手で打ち込んだテーブル（manualTable） は　例えば 19路盤の場合 21x21 サイズで渡される。
            // 内部的なテーブルは 横幅が 256 ある。
            // Board の内部的なテーブルも 横幅が 256 ある。
            board = new BoardImpl();
            board.SetBoardSize(boardSize);

            // 手で打ち込んだテーブルのサイズは、枠があるので何路盤のサイズよりも 2 大きいぜ☆（＾▽＾）！
            int manualtableSize = boardSize + 2;

            // 手で打ち込んだテーブルを優先してループを回す☆！（＾▽＾）！
            for (int my=0; my< manualtableSize; my++)
            {
                for (int mx = 0; mx < manualtableSize; mx++)
                {
                    int srcNode = my * manualtableSize + mx;
                    // 内部的なテーブルの座標に変換するぜ☆！（＾▽＾）！
                    int dstNode = AbstractTable<Color>.ConvertToNode(mx, my);

                    board.SetValue(dstNode, ConvColor.FromNumber(manualTable[srcNode]));
                }
            }
        }

        public static string ToErrorMessage(int node, NoMoveReason noMoveReason)
        {
            string errorMessage;

            int x, y;
            AbstractTable<Color>.ConvertToXy(out x, out y, node);

            switch (noMoveReason)
            {
                case NoMoveReason.ExistsStone:
                    // 石があるなら
                    errorMessage = string.Format("({0:D},{1:D})　石がある。\n", x, y);
                    break;
                case NoMoveReason.OutOfBoard:
                    // 枠なら
                    errorMessage = string.Format("({0:D},{1:D})　枠だった。\n", x, y);
                    break;
                case NoMoveReason.Kou:
                    // コウになる位置なら
                    errorMessage = string.Format("({0:D},{1:D})　コウになる。\n", x, y);
                    break;
                case NoMoveReason.Suicide:
                    // 自殺手になるなら
                    errorMessage = string.Format("({0:D},{1:D})　自殺手になる。\n", x, y);
                    break;
                case NoMoveReason.OwnEye:
                    // 自分の眼になるなら
                    errorMessage = string.Format("({0:D},{1:D})　自分の眼に打ち込む。\n", x, y);
                    break;
                default:
                    errorMessage = "";
                    break;
            }

            return errorMessage;
        }
    }

}
