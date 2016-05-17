using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n720_kifu____
{
    public class KifuElementImpl : KifuElement
    {
        public KifuElementImpl(
            int node,
            Color color,
            int elapsedSecond
            )
        {
            this.SetNode(node);
            this.SetColor(color);
            this.SetElapsedSecond(elapsedSecond);
        }

        /// <summary>
        /// 座標
        /// </summary>
        private int m_node_;
        public int GetNode()
        {
            return this.m_node_;
        }
        public void SetNode(int value)
        {
            this.m_node_ = value;
        }

        /// <summary>
        /// 石の色
        /// </summary>
        private Color m_color_;
        public Color GetColor()
        {
            return this.m_color_;
        }
        public void SetColor(Color value)
        {
            this.m_color_ = value;
        }

        /// <summary>
        /// 消費時間（秒)
        /// </summary>
        private int m_elapsedSecond_;
        public int GetElapsedSecond()
        {
            return this.m_elapsedSecond_;
        }
        public void SetElapsedSecond(int value)
        {
            this.m_elapsedSecond_ = value;
        }
    }
}
