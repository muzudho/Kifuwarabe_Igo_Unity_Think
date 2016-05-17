using System.Collections.Generic;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___
{
    //────────────────────────────────────────────────────────────────────────────────
    // ラムダ式用☆（＾▽＾）
    //────────────────────────────────────────────────────────────────────────────────
    public delegate void Func06(int node, ref bool isBreak);


    public delegate void Func05(int node, ref bool isBreak);


    public delegate void Func04(int x, int y, ref bool isBreak);


    public delegate void Func03(int node, ref bool isBreak);


    public delegate void Func02(int adjNode, ref bool isBreak);


    public delegate void Func01(int iDir, int adjNode, ref bool isBreak);


    /// <summary>
    /// Color型にすれば碁盤、int型にすればマーキング・シート☆（＾▽＾）
    /// </summary>
    /// <typeparam name="ELM"></typeparam>
    public interface Table<ELM>
    {
        /// <summary>
        /// テーブルサイズ。
        /// 何路盤のサイズより２大きい。
        /// </summary>
        /// <returns></returns>
        int GetTableSize();
        void SetTableSize(int value);


        /// <summary>
        /// 次にコウになる位置。無ければ 0。
        /// </summary>
        int GetKouNode();
        void SetKouNode(int value);

        /// <summary>
        /// 1手進めたことで消えたコウの場所を覚えておくものです。（戻せるのは１回だけです）
        /// </summary>
        int GetKouNodeForUndo();
        void SetKouNodeForUndo(int value);

        /// <summary>
        /// 石を置いた位置を覚えておくものです。（戻せるのは１回だけです）
        /// </summary>
        int GetMoveNodeForUndo();
        void SetMoveNodeForUndo(int value);

        /// <summary>
        /// セルの値
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        void SetValue(int node, ELM value);
        ELM ValueOf(int node);

        /// <summary>
        /// ハマ。取った石の数のこと。[0]...使わない。[1]... 黒が取った石の数, [2]...白が取った石の数
        /// </summary>
        int GetHama(Color color);
        void SetHama(Color color, int value);
        void AddHama(Color color, int value);



        /// <summary>
        /// 碁盤の枠を全走査。左上角から時計回り。
        /// .cpp に本体を書くとなんかエラーが出たので、.h に書いているんだぜ☆（＾ｑ＾）
        /// </summary>
        /// <param name="func"></param>
        void ForeachAllNodesOfWaku(Func06 func);

        /// <summary>
        /// 枠も含めて碁盤を全走査。左上から右上へ、端で改行して次の行の先頭から。
        /// .cpp に本体を書くとなんかエラーが出たので、.h に書いているんだぜ☆（＾ｑ＾）
        /// </summary>
        /// <param name="func"></param>
        void ForeachAllNodesWithWaku(Func05 func);

        /// <summary>
        /// 枠も含めて碁盤を全走査。左上から右上へ、端で改行して次の行の先頭から。
        /// .cpp に本体を書くとなんかエラーが出たので、.h に書いているんだぜ☆（＾ｑ＾）
        /// </summary>
        /// <param name="func"></param>
        void ForeachAllXyWithWaku(Func04 func);

        /// <summary>
        /// 枠を含めない碁盤を全走査。左上から右上へ、端で改行して次の行の先頭から。
        /// .cpp に本体を書くとなんかエラーが出たので、.h に書いているんだぜ☆（＾ｑ＾）
        /// </summary>
        /// <param name="func"></param>
        void ForeachAllNodesWithoutWaku(Func03 func);


        /// <summary>
        /// 指定のノードに隣接する上、右、下、左のノードを走査。
        /// </summary>
        /// <param name="node"></param>
        /// <param name="func"></param>
        void ForeachArroundNodes(int node, Func02 func);


        /// <summary>
        /// 指定のノードに隣接する上、右、下、左のノードを走査。
        /// </summary>
        /// <param name="node"></param>
        /// <param name="func"></param>
        void ForeachArroundDirAndNodes(int node, Func01 func);


    }
}