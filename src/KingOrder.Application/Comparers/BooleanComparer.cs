namespace KingOrder.Application.Comparers
{
    public class BooleanComparer : IComparer<bool>
    {
        public int Compare(bool x, bool y)
        {
            int p = x ? 1 : 0;
            int q = y ? 1 : 0;
            return p - q;
        }
    }
}
