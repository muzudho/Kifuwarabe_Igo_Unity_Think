using n700_think___.nn400_tactics_.nnn200_hit_____;//.HitRandom;


namespace n700_think___.nn400_tactics_.nnn200_hit_____
{ 
    public class HitRandomImpl : HitRandom
    {
        public HitRandomImpl()
        {
        }

        public int Evaluate(
        ){
            System.Random rand = new System.Random(1000);//FIXME:
            return rand.Next() % 100; // 0 ～ 99 のランダムな評価値を与える。
        }
    }
}