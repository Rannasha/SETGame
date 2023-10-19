using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETLib
{
    internal class Utils
    {
        public static bool IsSet(Card c1, Card c2, Card c3) 
        {            
            for (int i = 0; i < 4; i++)
            {
                if (!IsPropertyOK(c1, c2, c3, i))
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsPropertyOK(Card c1, Card c2, Card c3, int propIx) 
        { 
            if (
                (c1.Properties[propIx] == c2.Properties[propIx] && c1.Properties[propIx] == c3.Properties[propIx]) // All properties the same
                ||
                (c1.Properties[propIx] != c2.Properties[propIx] && c1.Properties[propIx] != c3.Properties[propIx] && c2.Properties[propIx] != c3.Properties[propIx]) // All properties different
                )
            {
                return true;
            }
            return false;
        }
    }
}
