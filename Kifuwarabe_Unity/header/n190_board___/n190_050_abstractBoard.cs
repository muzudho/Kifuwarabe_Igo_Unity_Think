#define BOARD_MAX ((19+2)*256)	// 19路盤を最大サイズとする

namespace n190_board___ {


    // 碁盤を想定した、枠付きのテーブルです。
    public interface AbstractBoard {

	// 盤上の石の色。
	private int table[BOARD_MAX];

        // 上、右、下、左　に移動するのに使う加減値
        private int dir4[4] = {
        -0x100,	// 上
		+0x001,	// 右
		+0x100,	// 下
		-0x001	// 左
	};// オリジナルのcgfthinkでは右、左、下、上の順だった。

        // 何路盤
        private int size;

	// (x,y)を1つの座標に変換
	public static int ConvertToNode(
        int x,
        int y
    );

        // (node)を(x,y)座標に変換
        public static void ConvertToXy(
            int& x,
            int& y,
            int node
        );

        public AbstractBoard();
        public ~AbstractBoard();

        // 上側に隣接している位置
        public int NorthOf(int node);

        // 右側に隣接している位置
        public int EastOf(int node);

        // 下側に隣接している位置
        public int SouthOf(int node);

        // 左側に隣接している位置
        public int WestOf(int node);

        public void SetSize(int size);
        public int GetSize();

        // セルの値
        public void SetValue(int node, int value);
        public int ValueOf(int node);


        // 碁盤の枠を全走査。左上角から時計回り。
        // .cpp に本体を書くとなんかエラーが出たので、.h に書いているんだぜ☆（＾ｑ＾）
        //template<typename Func>
        public void ForeachAllNodesOfWaku(Func func)
        {
            // 上辺（最後の手前まで）
            {
                int y = 0;
                for (int x = 0; x < this->size + 2 - 1; x++)
                {
                    int node = AbstractBoard::ConvertToNode(x, y);

                    bool isBreak = false;
                    func(node, isBreak);
                    if (isBreak)
                    {
                        break;
                    }
                }
            }

            // 右辺（最後の手前まで）
            {
                int x = this->size + 2 - 1;
                for (int y = 0; y < this->size + 2 - 1; y++)
                {
                    int node = AbstractBoard::ConvertToNode(x, y);

                    bool isBreak = false;
                    func(node, isBreak);
                    if (isBreak)
                    {
                        break;
                    }
                }
            }

            // 下辺（最後の手前まで）
            {
                int y = this->size + 2 - 1;
                for (int x = this->size + 2 - 1; 0 < x; x--)
                {
                    int node = AbstractBoard::ConvertToNode(x, y);

                    bool isBreak = false;
                    func(node, isBreak);
                    if (isBreak)
                    {
                        break;
                    }
                }
            }

            // 左辺（最後の手前まで）
            {
                int x = 0;
                for (int y = this->size + 2 - 1; 0 < y; y--)
                {
                    int node = AbstractBoard::ConvertToNode(x, y);

                    bool isBreak = false;
                    func(node, isBreak);
                    if (isBreak)
                    {
                        break;
                    }
                }
            }
        }

        // 枠も含めて碁盤を全走査。左上から右上へ、端で改行して次の行の先頭から。
        // .cpp に本体を書くとなんかエラーが出たので、.h に書いているんだぜ☆（＾ｑ＾）
        //template<typename Func>
        public void ForeachAllNodesWithWaku(Func func)
        {
            for (int y = 0; y < this->size + 2; y++)
            {
                for (int x = 0; x < this->size + 2; x++)
                {
                    int node = AbstractBoard::ConvertToNode(x, y);

                    bool isBreak = false;
                    func(node, isBreak);
                    if (isBreak)
                    {
                        break;
                    }
                }
            }
        }

        // 枠も含めて碁盤を全走査。左上から右上へ、端で改行して次の行の先頭から。
        // .cpp に本体を書くとなんかエラーが出たので、.h に書いているんだぜ☆（＾ｑ＾）
        //template<typename Func>
        public void ForeachAllXyWithWaku(Func func)
        {
            for (int y = 0; y < this->size + 2; y++)
            {
                for (int x = 0; x < this->size + 2; x++)
                {
                    bool isBreak = false;
                    func(x, y, isBreak);
                    if (isBreak)
                    {
                        break;
                    }
                }
            }
        }

        // 枠を含めない碁盤を全走査。左上から右上へ、端で改行して次の行の先頭から。
        // .cpp に本体を書くとなんかエラーが出たので、.h に書いているんだぜ☆（＾ｑ＾）
        //template<typename Func>
        public void ForeachAllNodesWithoutWaku(Func func)
        {
            for (int y = 1; y < this->size + 1; y++)
            {
                for (int x = 1; x < this->size + 1; x++)
                {
                    int node = AbstractBoard::ConvertToNode(x, y);

                    bool isBreak = false;
                    func(node, isBreak);
                    if (isBreak)
                    {
                        break;
                    }
                }
            }
        }

        // 指定のノードに隣接する上、右、下、左のノードを走査。
        //template<typename Func>
        public void ForeachArroundNodes(int node, Func func)
        {
            for (int iDir = 0; iDir < 4; iDir++) {
                bool isBreak = false;
                func(node + this->dir4[iDir], isBreak);
                if (isBreak)
                {
                    break;
                }
            }
        }

        // 指定のノードに隣接する上、右、下、左のノードを走査。
        //template<typename Func>
        public void ForeachArroundDirAndNodes(int node, Func func)
        {
            for (int iDir = 0; iDir < 4; iDir++) {
                bool isBreak = false;
                func(iDir, node + this->dir4[iDir], isBreak);
                if (isBreak)
                {
                    break;
                }
            }
        }
    };

}