//using n190_board___.AbstractBoard;
//using n190_board___.Board;


namespace n190_board___ {


    // 自分の位置を含む石、または連の総呼吸点の数を、各交点に格納したテーブルです。
    public interface MarkingBoard : Board {

	    void Initialize(Board board);
    }
}