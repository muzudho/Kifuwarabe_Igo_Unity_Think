using n700_think___.nn400_tactics_.nnn200_hit_____;//.HitRandom;


namespace n700_think___.nn400_tactics_.nnn200_hit_____
{ 
    public class HitRandomImpl : HitRandom
    {
        public HitRandom()
        {
        }

        public int Evaluate(
        ){
            return rand() % 100; // 0 ～ 99 のランダムな評価値を与える。
        }
    }
}