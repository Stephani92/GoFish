using System.Collections.Generic;

namespace GoFish
{
    class NumberByHight : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x<y)
            {
                return -1;
            }
            if (x>y)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
