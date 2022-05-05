using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public static class WordEffectFactory
    {
        public static IWordEffect CopyMake(IWordEffect copyFrom)
        {
            switch (copyFrom)
            {
                case NormalAtkAndDef x:
                    {
                        return new NormalAtkAndDef(x);
                    }
            }

            return null;
        }
    }
}
