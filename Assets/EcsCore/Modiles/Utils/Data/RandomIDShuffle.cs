namespace Modules.Utils
{
    public class RandomIDShuffle
    {
        private int Current;
        private int[] IDS;

        public RandomIDShuffle(int amount) 
        {
            IDS = new int[amount];
            for (int i = 0; i < IDS.Length; i++)
            {
                IDS[i] = i;
            }
            Shuffle();
            Current = 0;
        }

        public RandomIDShuffle(int[] ids) 
        {
            IDS = new int[ids.Length];
            for (int i = 0; i < IDS.Length; i++)
            {
                IDS[i] = ids[i];
            }
            Shuffle();
            Current = 0;
        }

        public int Next() 
        {
            // if on last pos, reshuffle
            if(Current >= IDS.Length-1) 
            {
                Shuffle();
                return IDS[0];
            }
            Current += 1;
            return IDS[Current];
        }

        /// <summary>
        /// simple random swap shuffle
        /// </summary>
        public void Shuffle() 
        {
            int prev;
            int shufflePos;
            for (int i = 0; i < IDS.Length; i++)
            {
                shufflePos = UnityEngine.Random.Range(0, IDS.Length);
                prev = IDS[shufflePos];
                IDS[shufflePos] = IDS[i];
                IDS[i] = prev;
            }
            Current = 0;
        }
    }
}
