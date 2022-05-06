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
            WordStateOrigins[1] = new WordStateOrigin("������", 3, 0, 10, 1);
            WordStateOrigins[1].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[2] = new WordStateOrigin("���ɔC����", 3, 0, 30, 2);
            WordStateOrigins[2].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[3] = new WordStateOrigin("��ɍs��", 3, 20, 0, 1);
            WordStateOrigins[3].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[99] = new WordStateOrigin("����",10000, 100, 0, 0);
            WordStateOrigins[99].Effects.Add(new NormalAtkAndDef());

            // �����f�b�L�ƃJ�X�^���f�b�L���\�z.
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

            //����
            /// ��.
            for (int i = 0; i < 9; i++)
            {
                OriginDecks[0][i] = WordStateOrigins[99];
                CustomDecks[0][i] = WordStateOrigins[99];
            }

            //��
            /// ��.
            for (int i = 0; i < 9; i++)
            {
                OriginDecks[1][i] = WordStateOrigins[3];
                CustomDecks[1][i] = WordStateOrigins[3];
            }

            //��
            /// ��.
            for (int i = 0; i < 9; i++)
            {
                OriginDecks[2][i] = WordStateOrigins[1];
                CustomDecks[2][i] = WordStateOrigins[1];
            }

            //�C
            /// ��.
            for (int i = 0; i < 9; i++)
            {
                OriginDecks[3][i] = WordStateOrigins[1];
                CustomDecks[3][i] = WordStateOrigins[1];
            }

            //�E��
            /// ��.
            for (int i = 0; i < 9; i++)
            {
                OriginDecks[4][i] = WordStateOrigins[1];
                CustomDecks[4][i] = WordStateOrigins[1];
            }
        }
    }
}
