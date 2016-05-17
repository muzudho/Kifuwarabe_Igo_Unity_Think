using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n720_kifu____
{
    public interface KifuElement
    {
        /// <summary>
        /// 座標
        /// </summary>
        int GetNode();
        void SetNode(int value);

        /// <summary>
        /// 石の色
        /// </summary>
        Color GetColor();
        void SetColor(Color value);

        /// <summary>
        /// 消費時間（秒)
        /// </summary>
        int GetElapsedSecond();
        void SetElapsedSecond(int value);

    }
}
