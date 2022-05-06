using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public static class WordPool
    {
        public static WordStateOrigin[] WordStateOrigins { get; private set; }

        public static WordStateOrigin[][] OriginDecks { get; private set; }

        public static WordStateOrigin[][] CustomDecks { get; private set; }

        static WordPool()
        {
            WordStateOrigins = new WordStateOrigin[100];

            WordStateOrigins[0] = new WordStateOrigin("", 0, 0, 0, 0);
            WordStateOrigins[1] = new WordStateOrigin("ここは", 3, 0, 10, 1);
            WordStateOrigins[1].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[2] = new WordStateOrigin("俺に任せて", 3, 0, 30, 2);
            WordStateOrigins[2].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[3] = new WordStateOrigin("先に行け", 3, 20, 0, 1);
            WordStateOrigins[3].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[99] = new WordStateOrigin("死ね",10000, 100, 0, 0);
            WordStateOrigins[99].Effects.Add(new NormalAtkAndDef());

            // 初期デッキとカスタムデッキを構築.
            OriginDecks = new WordStateOrigin[5][];
            for(int i = 0; i < 5; i++)
            {
                OriginDecks[i] = new WordStateOrigin[9];
            }
            CustomDecks = new WordStateOrigin[5][];
            for (int i = 0; i < 5; i++)
            {
                CustomDecks[i] = new WordStateOrigin[9];
            }

            //魔王
            /// 仮.
            for (int i = 0; i < 9; i++)
            {
                OriginDecks[0][i] = WordStateOrigins[99];
                CustomDecks[0][i] = WordStateOrigins[99];
            }

            //陸
            /// 仮.
            for (int i = 0; i < 9; i++)
            {
                OriginDecks[1][i] = WordStateOrigins[3];
                CustomDecks[1][i] = WordStateOrigins[3];
            }

            //空
            /// 仮.
            for (int i = 0; i < 9; i++)
            {
                OriginDecks[2][i] = WordStateOrigins[1];
                CustomDecks[2][i] = WordStateOrigins[1];
            }

            //海
            /// 仮.
            for (int i = 0; i < 9; i++)
            {
                OriginDecks[3][i] = WordStateOrigins[1];
                CustomDecks[3][i] = WordStateOrigins[1];
            }

            //勇者
            /// 仮.
            for (int i = 0; i < 9; i++)
            {
                OriginDecks[4][i] = WordStateOrigins[1];
                CustomDecks[4][i] = WordStateOrigins[1];
            }
        }
    }
}
