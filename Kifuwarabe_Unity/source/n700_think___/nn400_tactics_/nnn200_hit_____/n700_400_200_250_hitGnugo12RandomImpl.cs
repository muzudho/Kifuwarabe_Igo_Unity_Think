using Grayscale.Kifuwarabe_Igo_Unity_Think.n090_core____;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;//.Board;.Liberty;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn400_tactics_.nnn200_hit_____
{
    public class HitGnugo12RandomImpl : HitGnugo12Random
    {
        public int Evaluate(
            Color color,
            int         node,
            Table<Color> pBoard
        ){
            int score = 0;

//# ifndef RANDOM_MOVE_ONLY

            int boardSize = pBoard.GetSize();
            if (boardSize < 9)
            {
                // ９路盤より小さいものは対象外。
                goto gt_EndMethod;
            }

            int x, y;
            AbstractTable<Color>.ConvertToXy(out x, out y, node);

            // 置きたくない位置を avoid 点数で表す。
            int avoid = 0;

            int waku = 1;
            int good = 1;
            int bad;
            if (18 < boardSize)
            {
                bad = 3;
            }
            else if (12 < boardSize)
            {
                bad = 2;
            }
            else {
                bad = 1;
            }

            //----------------------------------------
            // x軸について
            //----------------------------------------

            if (x < waku + bad)
            {
                // 端には置きたくない。
                avoid++;
            }
            else if (x < waku + bad + good)
            {
                // 端からちょっと離れたポイントはオッケー
            }
            else if (x < waku + 2 * bad + good)
            {
                // その先はまた嫌。
                avoid++;
            }
            else if (x < waku + 2 * bad + 2 * good)
            {
                // その先はまたオッケー。
            }
            else if ((boardSize - waku - bad) < x)
            {
                // 端には置きたくない。
                avoid++;
            }
            else if ((boardSize - waku - (bad + good)) < x)
            {
                // 端からちょっと離れたポイントはオッケー
            }
            else if ((boardSize - waku - (2 * bad + good)) < x)
            {
                // その先はまた嫌。
                avoid++;
            }
            else if ((boardSize - waku - (2 * bad + 2 * good)) < x)
            {
                // その先はまたオッケー。
            }
            else
            {
                // 中央地帯はいやだ。
                avoid++;
            }


            //----------------------------------------
            // y軸について
            //----------------------------------------

            if (y < waku + bad)
            {
                // 端には置きたくない。
                avoid++;
            }
            else if (y < waku + bad + good)
            {
                // 端からちょっと離れたポイントはオッケー
            }
            else if (y < waku + 2 * bad + good)
            {
                // その先はまた嫌。
                avoid++;
            }
            else if (y < waku + 2 * bad + 2 * good)
            {
                // その先はまたオッケー。
            }
            else if ((boardSize - waku - bad) < y)
            {
                // 端には置きたくない。
                avoid++;
            }
            else if ((boardSize - waku - (bad + good)) < y)
            {
                // 端からちょっと離れたポイントはオッケー
            }
            else if ((boardSize - waku - (2 * bad + good)) < y)
            {
                // その先はまた嫌。
                avoid++;
            }
            else if ((boardSize - waku - (2 * bad + 2 * good)) < 7)
            {
                // その先はまたオッケー。
            }
            else
            {
                // 中央地帯はいやだ。
                avoid++;
            }


            // 避けたい場所
            // 連の呼吸点が 0 ～ 1 しかない場合。
            Liberty liberty = new LibertyImpl();
            liberty.Count(node, color, pBoard);
            if (liberty.GetLiberty() < 2)
            {
                avoid++;
            }

            //----------------------------------------
            // avoid は 4 段階
            //----------------------------------------
            if (2 < avoid)
            {
                score = 6 + Core.GetRandom() % 94;
            }
            else if (1 < avoid)
            {
                score = 12 + Core.GetRandom() % 88;
            }
            else if (0 < avoid)
            {
                score = 25 + Core.GetRandom() % 75;
            }
            else {
                score = 50 + Core.GetRandom() % 50;
            }

            //----------------------------------------
            // 効き目に倍率を掛けます。
            //----------------------------------------
            score *= 80 / 10;

            gt_EndMethod:

//#endif

            return score;
        }
    }
}