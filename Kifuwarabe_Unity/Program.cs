using n190_board___;
using n950_main____;
using n700_think___.nn800_best____;//GameType

namespace Kifuwarabe_Unity
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: テストを書きたいぜ☆！（＾ｑ＾）！

            KifuwarabeThink kwThink = new KifuwarabeThinkImpl();
            kwThink.DoBegin();

            // 初期盤面（置碁の場合は、ここに置石が入る）
            int[] initBoard = new int[AbstractBoard.BOARD_MAX];
            for (int i=0;i< AbstractBoard.BOARD_MAX;i++)
            {
                initBoard[i] = 0;//空点
            }

            // 棋譜  [][3]
            //      [手数][0]...座標
            //		[手数][1]...石の色
            //		[手数][2]...消費時間（秒)
            // 手数は 0 から始まり、curTesuu の1つ手前まである。
            int[,] kifu = new int[2048,3];

            // 現在の手数
            int curTesuu = 0;

            // 黒手番フラグ
            bool isBlackTurn = true;

            // 盤面のサイズ
            int boardSize = 19;

            // コミ
            double komi = 0;

            // 0...通常の思考、1...終局処理、2...図形を表示、3...数値を表示。
            GameType endgameType = GameType.GAME_MOVE;

            // 終局処理の結果を代入する。
            int[] endgameBoard = new int[AbstractBoard.BOARD_MAX];

            int bestmove = kwThink.DoBestmove(
                initBoard,
                kifu,
                curTesuu,
                isBlackTurn,
                boardSize,
                komi,
                endgameType,
                ref endgameBoard
                );

            // 16進4桁表記☆（＾▽＾）
            System.Console.WriteLine(string.Format( "bestmove = 0x{0:x4}",bestmove) );
            System.Console.ReadKey();

            kwThink.DoEnd();



            System.Console.WriteLine("（＾ｑ＾）おわりだぜ☆ 何かキーを押せだぜ☆");
            System.Console.ReadKey();
            return;
        }
    }
}
