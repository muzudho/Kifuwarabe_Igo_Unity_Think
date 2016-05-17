using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___
{
    /// <summary>
    /// 碁石。黒と白は、手番としても利用。
    /// </summary>
    public enum Color
    {
        /// <summary>
        /// 空きスペース
        /// </summary>
        EMPTY,

        /// <summary>
        /// 黒石
        /// </summary>
        BLACK,

        /// <summary>
        /// 白石
        /// </summary>
        WHITE,

        /// <summary>
        /// 盤外
        /// </summary>
        WAKU
    }
}
