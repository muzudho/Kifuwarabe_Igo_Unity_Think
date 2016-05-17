using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n930_view____
{
    public class LibertyOfNodesViewImpl
    {
        /// <summary>
        /// 現在の盤面を表示
        /// </summary>
        /// <param name="libertyOfNodes"></param>
        public static void PrintBoard(LibertyOfNodes libertyOfNodes)
        {
            libertyOfNodes.ForeachAllXyWithWaku((int x, int y, ref bool isBreak) =>{
                int node = AbstractBoard.ConvertToNode(x, y);

                System.Console.WriteLine(string.Format("{0:D2}", libertyOfNodes.ValueOf(node)));

                if (x == libertyOfNodes.GetSize() + 1)
                {
                    System.Console.WriteLine(string.Format("\n"));
                }
            });
        }
    }
}