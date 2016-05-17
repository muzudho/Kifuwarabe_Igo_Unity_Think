using Grayscale.Kifuwarabe_Igo_Unity_Think.n090_core____;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;//.Board;.Liberty;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n750_explain_;//.FigureType;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n800_scene___
{

    public class EndgameImpl
    {

        /// <summary>
        /// 終局処理（サンプルでは簡単な判断で死石と地の判定をしています）
        /// </summary>
        /// <param name="arr_endgameBoard"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public static void EndgameStatus(
            GtpStatusType[] arr_endgameBoard,
            Board board
        ){
            board.ForeachAllNodesWithoutWaku((int node, ref bool isBreak) =>{

                Color color = board.ValueOf(node);
                if (color == Color.Empty)
                {
                    arr_endgameBoard[node] = GtpStatusType.GTP_DAME;
                    Color sum = 0;
                    board.ForeachArroundNodes(node, (int adjNode, ref bool isBreak2)=> {
                        Color adjColor;   // 隣接(adjacent)する石の色
                        adjColor = board.ValueOf(adjNode);
                        if (adjColor == Color.Waku)
                        {
                            goto gt_Next;
                        }
                        sum |= adjColor;

                        gt_Next:
                        ;
                    });

                    if (sum == Color.Black)
                    {
                        // 黒字☆
                        arr_endgameBoard[node] = GtpStatusType.GTP_BLACK_TERRITORY;
                    }
                    if (sum == Color.White)
                    {
                        // 白地☆
                        arr_endgameBoard[node] = GtpStatusType.GTP_WHITE_TERRITORY;
                    }
                }
                else {
                    arr_endgameBoard[node] = GtpStatusType.GTP_ALIVE;

                    Liberty liberty = new LibertyImpl();

                    liberty.Count(node, color, board);
                    //	System.Console.WriteLine(string.Format( "({0:D2},{1:D2}),ishi=%2d,dame={2:D2}\n",z&0xff,z>>8,ishi,dame));

                    if (liberty.GetLiberty() <= 1)
                    {
                        // 石は死石☆
                        arr_endgameBoard[node] = GtpStatusType.GTP_DEAD;
                    }
                }
            });
        }

        /// <summary>
        /// 図形を描く。
        /// FIXME: なんか適当に返してないか☆？（＾～＾）？
        /// </summary>
        /// <param name="arr_endgameBoard"></param>
        /// <param name="board"></param>
        public static void EndgameDrawFigure(
            FigureType[] arr_endgameBoard, Board board)
        {
            int x;
            int y;
            int node;

            for (y = 1; y < board.GetBoardSize() + 1; y++)
            {
                for (x = 1; x < board.GetBoardSize() + 1; x++)
                {
                    node = AbstractTable<Color>.ConvertToNode(x, y);
                    if ((Core.GetRandom() % 2) == 0)
                    {
                        arr_endgameBoard[node] = FigureType.None;
                    }
                    else {
                        if (Core.GetRandom() % 2 !=0)
                        {
                            arr_endgameBoard[node] = FigureType.Black;
                        }
                        else {
                            arr_endgameBoard[node] = FigureType.White;
                        }

                        FigureType randamFt;
                        switch ((Core.GetRandom() % 9) + 1)
                        {
                            case 1: randamFt = FigureType.Triangle; break;
                            case 2:
                                randamFt = FigureType.Square;
                                break;
                            case 3:
                                randamFt = FigureType.Circle;
                                break;
                            case 4:
                                randamFt = FigureType.Cross;
                                break;
                            case 5:
                                randamFt = FigureType.Question;
                                break;
                            case 6:
                                randamFt = FigureType.Horizon;
                                break;
                            case 7:
                                randamFt = FigureType.Vertical;
                                break;
                            case 8:
                                randamFt = FigureType.LineLeftup;
                                break;
                            case 9:
                                randamFt = FigureType.LineRightup;
                                break;
                            default:
                                // エラー☆
                                randamFt = FigureType.None;
                                break;
                        }

                        arr_endgameBoard[node] = arr_endgameBoard[node]
                            |
                            randamFt;
                    }
                }
            }
        }

        /// <summary>
        /// 数値を書く(0は表示されない)
        /// FIXME: なんかこれも適当に返してないか☆？（＾～＾）？
        /// </summary>
        /// <param name="arr_endgameBoard"></param>
        /// <param name="board"></param>
        public static void EndgameDrawNumber(
            int[] arr_endgameBoard, Board board)
        {
            int x;
            int y;
            int node;

            for (y = 1; y < board.GetBoardSize() + 1; y++)
            {
                for (x = 1; x < board.GetBoardSize() + 1; x++)
                {
                    node = AbstractTable<Color>.ConvertToNode(x, y);
                    arr_endgameBoard[node]  = (Core.GetRandom() % 110) - 55;
                }
            }
        }
    }
}