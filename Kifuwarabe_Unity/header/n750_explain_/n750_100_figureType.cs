namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n750_explain_
{

    //--------------------------------------------------------------------------------
    // enum まとめ
    //--------------------------------------------------------------------------------

    // コンピューター囲碁ソフト『彩』のスクリーンショットでも見たことのある人もいるかもしれない、
    // 盤面、石の上に描く記号
    // (形 | 色) で指定する。黒で四角を描く場合は (FIGURE_SQUARE | FIGURE_BLACK)
    public enum FigureType
    {
        FIGURE_NONE,            //  0: 何も描かない
        FIGURE_TRIANGLE,        //  1: 三角形
        FIGURE_SQUARE,          //  2: 四角
        FIGURE_CIRCLE,          //  3: 円
        FIGURE_CROSS,           //  4: ×
        FIGURE_QUESTION,        //  5: "？"の記号	
        FIGURE_HORIZON,         //  6: 横線
        FIGURE_VERTICAL,        //  7: 縦線
        FIGURE_LINE_LEFTUP,     //  8: 斜め、左上から右下
        FIGURE_LINE_RIGHTUP,    //  9: 斜め、左下から右上
        FIGURE_BLACK = 0x1000,  // 10: 黒で描く（色指定)
        FIGURE_WHITE = 0x2000,  // 11: 白で描く (色指定）
    };
}