namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___
{

    public class BoardImpl : AbstractBoard
    {
        // 碁石
        public const int EMPTY = 0;      // 空きスペース
        public const int BLACK = 1;
        public const int WHITE = 2;
        public const int WAKU = 3;		// 盤外
        public static int INVCLR(int x){
            return 3-x;// 石の色を反転させる
        }

        public BoardImpl()
        {

            this.SetKouNodeForUndo( 0);
	        this.SetMoveNodeForUndo( 0);
	        this.SetKouNode(0);		// コウになる位置。

	        this.SetHama(0, 0);
	        this.SetHama(BoardImpl.BLACK, 0);	// 取った石の数
	        this.SetHama(BoardImpl.WHITE, 0);
        }

        ~BoardImpl()
        {
        }
    }
}