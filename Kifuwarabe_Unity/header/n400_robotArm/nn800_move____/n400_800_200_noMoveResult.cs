using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n400_robotArm.nn800_move____
{
    /// <summary>
    /// 石を置けない理由。
    /// </summary>
    public enum NoMoveReason
    {

        /// <summary>
        /// 石は置ける。
        /// </summary>
        None,

        /// <summary>
        /// 石があるから。
        /// </summary>
        ExistsStone,

        /// <summary>
        /// 枠だから。
        /// </summary>
        OutOfBoard,

        /// <summary>
        /// コウだから。
        /// </summary>
        Kou,

        /// <summary>
        /// 自殺手だから。
        /// </summary>
        Suicide,

        /// <summary>
        /// 自分の眼だから。（着手禁止点ではないが）
        /// </summary>
        OwnEye
    }
}
