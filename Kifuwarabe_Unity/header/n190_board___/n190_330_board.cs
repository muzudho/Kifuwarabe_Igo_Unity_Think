using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___
{
    public interface Board : Table<Color>
    {

        /// <summary>
        /// 何路盤のサイズ。
        /// テーブルサイズより２小さい。
        /// </summary>
        int GetBoardSize();
        void SetBoardSize(int value);

    }
}
