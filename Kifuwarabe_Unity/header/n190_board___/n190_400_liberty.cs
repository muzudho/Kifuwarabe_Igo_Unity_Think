namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___
{
    public interface Liberty {

        // 位置 node におけるリバティ（石の呼吸点）の数と石の数を計算。結果は liberty 変数に格納。
        void Count(
            int node,
            Color color,
            Board board
        );

        // リバティ（石の呼吸点）と石の数える再帰関数
        // 4方向を調べて、空白だったら+1、自分の石なら再帰で。相手の石、壁ならそのまま。
        void CountElement(
            int tNode,
            Color color,
            Board board
        );

        /// <summary>
        /// ────────────────────────────────────────────────────────────────────────────────
        /// 呼吸点
        /// ────────────────────────────────────────────────────────────────────────────────
        /// </summary>
        /// <returns></returns>
        int GetLiberty();
        void SetLiberty(int value);
        void IncreaseLiberty();

        /// <summary>
        /// ────────────────────────────────────────────────────────────────────────────────
        /// 隣接する（１個あるいは連の）石の数(再帰関数で使う)
        /// ────────────────────────────────────────────────────────────────────────────────
        /// </summary>
        int GetRenIshi();
        void SetRenIshi(int value);

    }
}