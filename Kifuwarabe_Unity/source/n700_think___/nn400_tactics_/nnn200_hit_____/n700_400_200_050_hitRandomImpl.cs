using n090_core____;
//using n700_think___.nn400_tactics_.nnn200_hit_____;//.HitRandom;


namespace n700_think___.nn400_tactics_.nnn200_hit_____
{ 
    public class HitRandomImpl : HitRandom
    {
        public HitRandomImpl()
        {
        }

        public int Evaluate(
        ){
            return Core.GetRandom() % 100; // 0 ～ 99 のランダムな評価値を与える。
        }
    }
}