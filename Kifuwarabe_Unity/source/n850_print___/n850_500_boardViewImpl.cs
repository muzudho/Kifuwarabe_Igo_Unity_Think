using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n850_print___
{
    public class BoardViewImpl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pBoard"></param>
        public static void PrintBoard(Board board)
        {
            string[] str = new string[4]{
                "・",	// 空き
		        "●",	// 黒石
		        "○",	// 白石
		        "＋"		// 枠
	        };

            board.ForeachAllXyWithWaku((int x, int y, ref bool isBreak) =>{

                int node = AbstractTable<Color>.ConvertToNode(x, y);

                System.Console.Write(string.Format("{0}", str[(int)board.ValueOf(node)]));

                if (x == board.GetBoardSize() + 1)
                {
                    System.Console.Write(string.Format("\n"));//改行☆
                }
            });
        }
    }
}