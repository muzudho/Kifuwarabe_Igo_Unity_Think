using System.Collections.Generic;


namespace n190_board___
{


    // 碁盤を想定した、枠付きのテーブルです。
    public abstract class AbstractBoard : Board
    {
        /// <summary>
        ///  19路盤を最大サイズとする
        /// </summary>
        public const int BOARD_MAX = (19 + 2) * 256;


        //public abstract void Initialize(int[] initBoard);
        public void Initialize(int[] initBoard)
        {
            // 現在局面を棋譜と初期盤面から作る
            for (int iNode = 0; iNode < AbstractBoard.BOARD_MAX; iNode++)
            {
                this.SetValue(iNode, initBoard[iNode]);    // 初期盤面をコピー
            }
        }

        public abstract List<int> GetOpenNodesOfStone(
            int node,
            int size123     // 1～3 のいずれかを指定してください。
        );

        public abstract void DeleteRenStones(
            int tNode,
            int color
        );



        // 1手進めたことで消えたコウの場所を覚えておくものです。（戻せるのは１回だけです）
        protected int kouNodeForUndo;
        // 石を置いた位置を覚えておくものです。（戻せるのは１回だけです）
        protected int moveNodeForUndo;

        /// <summary>
        /// 次にコウになる位置。無ければ 0。
        /// </summary>
        protected int m_kouNode_;
        public int GetKouNode()
        {
            return this.m_kouNode_;
        }
        public void SetKouNode(int value)
        {
            this.m_kouNode_ = value;
        }

        // ハマ。取った石の数のこと。[0]...空き。[1]... 黒が取った石の数, [2]...白が取った石の数
        protected int[] hama = new int[3];

        /// <summary>
        /// 何路盤
        /// </summary>
        protected int m_size_;
        public int GetSize()
        {
            return this.m_size_;
        }
        public void SetSize(int size)
        {
            this.m_size_ = size;
        }

        // 盤上の石の色。
        private int[] table = new int[AbstractBoard.BOARD_MAX];


        /// <summary>
        /// コンストラクター☆
        /// </summary>
        public AbstractBoard()
        {
            this.m_size_ = 0;
        }

        ~AbstractBoard()
        {
        }




        // 上、右、下、左　に移動するのに使う加減値
        private int[] dir4 = new int[4]{
            -0x100,	// 上
		    +0x001,	// 右
		    +0x100,	// 下
		    -0x001	// 左
	    };// オリジナルのcgfthinkでは右、左、下、上の順だった。


	    // (x,y)を1つの座標に変換
	    public static int ConvertToNode(
            int x,
            int y
        ){
            return y * 256 + x;
        }

        // (node)を(x,y)座標に変換
        public static void ConvertToXy(
            out int x,
            out int y,
            int node
        ){
            y = node / 256;
            x = node % 256;
        }

        // 上側に隣接している位置
        public int NorthOf(int node)
        {
            return this.ValueOf(node + this.dir4[0]);
        }

        // 右側に隣接している位置
        public int EastOf(int node)
        {
            return this.ValueOf(node + this.dir4[1]);
        }

        // 下側に隣接している位置
        public int SouthOf(int node)
        {
            return this.ValueOf(node + this.dir4[2]);
        }

        // 左側に隣接している位置
        public int WestOf(int node)
        {
            return this.ValueOf(node + this.dir4[3]);
        }


        // セルの値
        public void SetValue(int node, int value)
        {
            this.table[node] = value;
        }

        public int ValueOf(int node)
        {
            return this.table[node];
        }


        public void ForeachAllNodesOfWaku(Func06 func)
        {
            // 上辺（最後の手前まで）
            {
                int y = 0;
                for (int x = 0; x < this.m_size_ + 2 - 1; x++)
                {
                    int node = AbstractBoard.ConvertToNode(x, y);

                    bool isBreak = false;
                    func(node, ref isBreak);
                    if (isBreak)
                    {
                        break;
                    }
                }
            }

            // 右辺（最後の手前まで）
            {
                int x = this.m_size_ + 2 - 1;
                for (int y = 0; y < this.m_size_ + 2 - 1; y++)
                {
                    int node = AbstractBoard.ConvertToNode(x, y);

                    bool isBreak = false;
                    func(node, ref isBreak);
                    if (isBreak)
                    {
                        break;
                    }
                }
            }

            // 下辺（最後の手前まで）
            {
                int y = this.m_size_ + 2 - 1;
                for (int x = this.m_size_ + 2 - 1; 0 < x; x--)
                {
                    int node = AbstractBoard.ConvertToNode(x, y);

                    bool isBreak = false;
                    func(node, ref isBreak);
                    if (isBreak)
                    {
                        break;
                    }
                }
            }

            // 左辺（最後の手前まで）
            {
                int x = 0;
                for (int y = this.m_size_ + 2 - 1; 0 < y; y--)
                {
                    int node = AbstractBoard.ConvertToNode(x, y);

                    bool isBreak = false;
                    func(node, ref isBreak);
                    if (isBreak)
                    {
                        break;
                    }
                }
            }
        }

        public void ForeachAllNodesWithWaku(Func05 func)
        {
            for (int y = 0; y < this.m_size_ + 2; y++)
            {
                for (int x = 0; x < this.m_size_ + 2; x++)
                {
                    int node = AbstractBoard.ConvertToNode(x, y);

                    bool isBreak = false;
                    func(node, ref isBreak);
                    if (isBreak)
                    {
                        break;
                    }
                }
            }
        }

        public void ForeachAllXyWithWaku(Func04 func)
        {
            for (int y = 0; y < this.m_size_ + 2; y++)
            {
                for (int x = 0; x < this.m_size_ + 2; x++)
                {
                    bool isBreak = false;
                    func(x, y, ref isBreak);
                    if (isBreak)
                    {
                        break;
                    }
                }
            }
        }

        public void ForeachAllNodesWithoutWaku(Func03 func)
        {
            for (int y = 1; y < this.m_size_ + 1; y++)
            {
                for (int x = 1; x < this.m_size_ + 1; x++)
                {
                    int node = AbstractBoard.ConvertToNode(x, y);

                    bool isBreak = false;
                    func(node,ref isBreak);
                    if (isBreak)
                    {
                        break;
                    }
                }
            }
        }

        public void ForeachArroundNodes(int node, Func02 func)
        {
            for (int iDir = 0; iDir < 4; iDir++) {
                bool isBreak = false;
                func(node + this.dir4[iDir], ref isBreak);
                if (isBreak)
                {
                    break;
                }
            }
        }

        public void ForeachArroundDirAndNodes(int node, Func01 func)
        {
            for (int iDir = 0; iDir < 4; iDir++) {
                bool isBreak = false;
                func(iDir, node + this.dir4[iDir], ref isBreak);
                if (isBreak)
                {
                    break;
                }
            }
        }
    };

}