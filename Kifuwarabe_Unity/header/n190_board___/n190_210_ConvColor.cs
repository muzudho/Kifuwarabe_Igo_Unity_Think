using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___
{
    public abstract class ConvColor
    {
        public static Color INVCLR(Color color)
        {
            return (Color)(3 - (int)color);// 石の色を反転させる
        }

        public static Color FromNumber(int colorIndex)
        {
            Color color;

            switch (colorIndex)
            {
                case 1: color = Color.BLACK; break;
                case 2: color = Color.WHITE; break;
                case 3: color = Color.WAKU; break;
                case 0://thru
                default: color = Color.EMPTY; break;
            }

            return color;
        }
    }
}
