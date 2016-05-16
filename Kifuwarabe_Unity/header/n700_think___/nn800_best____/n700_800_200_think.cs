using n190_board___;//.Board;.LibertyOfNodes;


namespace n700_think___.nn800_best____
{
    //--------------------------------------------------------------------------------
    // enum
    //--------------------------------------------------------------------------------

    // 現在局面で何をするか、を指定
    public enum GameType {
        GAME_MOVE,              // 通常の手
        GAME_END_STATUS,        // 終局処理
        GAME_DRAW_FIGURE,       // 図形を描く
        GAME_DRAW_NUMBER        // 数値を書く
    }
}
