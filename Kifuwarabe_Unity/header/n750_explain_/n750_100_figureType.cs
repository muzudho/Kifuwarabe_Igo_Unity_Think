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
        None,            //  0: 何も描かない
        Triangle,        //  1: 三角形
        Square,          //  2: 四角
        Circle,          //  3: 円
        Cross,           //  4: ×
        Question,        //  5: "？"の記号	
        Horizon,         //  6: 横線
        Vertical,        //  7: 縦線
        LineLeftup,     //  8: 斜め、左上から右下
        LineRightup,    //  9: 斜め、左下から右上
        Black = 0x1000,  // 10: 黒で描く（色指定)
        White = 0x2000,  // 11: 白で描く (色指定）
    };
}