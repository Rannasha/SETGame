namespace SETLib
{
    public class Card
    {
        public int[] Properties;

        public int Index()
        {
            return 27 * Properties[0] + 9 * Properties[1] + 3 * Properties[2] + Properties[3];
        }

        public Card(int p0, int p1, int p2, int p3) 
        { 
            Properties = new int[4];
            Properties[0] = p0;
            Properties[1] = p1;
            Properties[2] = p2;
            Properties[3] = p3;
        }
    }
}