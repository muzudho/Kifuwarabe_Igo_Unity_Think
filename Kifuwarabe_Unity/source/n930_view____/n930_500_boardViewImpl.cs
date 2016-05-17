using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n930_view____
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
                int node = AbstractBoard.ConvertToNode(x, y);
                System.Console.WriteLine(string.Format("{0}", str[board.ValueOf(node)]));
                if (x == board.GetSize() + 1)
                {
                    System.Console.WriteLine(string.Format("\n"));
                }
            });
        }
    }
}