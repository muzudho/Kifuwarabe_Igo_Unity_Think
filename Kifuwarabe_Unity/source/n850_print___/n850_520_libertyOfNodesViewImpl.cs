﻿using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n850_print___
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
                int node = AbstractTable<Color>.ConvertToNode(x, y);

                System.Console.WriteLine(string.Format("{0:D2}", libertyOfNodes.ValueOf(node)));

                if (x == libertyOfNodes.GetTableSize() - 1)
                {
                    System.Console.WriteLine(string.Format("\n"));
                }
            });
        }
    }
}