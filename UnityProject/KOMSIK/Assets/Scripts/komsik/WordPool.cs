using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public static class WordPool
    {
        public static WordStateOrigin HolySaber { get { return WordStateOrigins[90]; } }

        public static WordStateOrigin[] WordStateOrigins { get; private set; }

        public static WordStateOrigin[][] OriginDecks { get; private set; }

        public static WordStateOrigin[][] CustomDecks { get; private set; }

        static WordPool()
        {
            WordStateOrigins = new WordStateOrigin[100];

            WordStateOrigins[0] = new WordStateOrigin("", 0, 0, 0, 0);

            //�J�[�h�v�[���쐬.
            #region �O����.
            WordStateOrigins[1] = new WordStateOrigin("������", 200, 0, 10, 1);
            WordStateOrigins[1].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[2] = new WordStateOrigin("���ɔC����", 200, 0, 20, 2);
            WordStateOrigins[2].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[3] = new WordStateOrigin("��ɍs��", 200, 0, 30, 6);
            WordStateOrigins[3].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[4] = new WordStateOrigin("���O��", 200, 30, 10, 3);
            WordStateOrigins[4].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[5] = new WordStateOrigin("�w�ɂ�", 200, 30, 20, 4);
            WordStateOrigins[5].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[6] = new WordStateOrigin("��������", 200, 30, 30, 7);
            WordStateOrigins[6].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[7] = new WordStateOrigin("���̐g", 100, 0, 10, 3);
            WordStateOrigins[7].Effects.Add(new NormalAtkAndDef());
            WordStateOrigins[7].Effects.Add(new CPGenerate(1));

            WordStateOrigins[8] = new WordStateOrigin("�l��", 100, 0, 0, 3);
            WordStateOrigins[8].Effects.Add(new AfterEfectTwice(Power.Own,2));

            WordStateOrigins[9] = new WordStateOrigin("���ƒm��", 10, 0, 90, 9);
            WordStateOrigins[9].Effects.Add(new NormalAtkAndDef());
            #endregion

            #region �X�J�C
            WordStateOrigins[10] = new WordStateOrigin("�ʂ�", 150, 50, 0, 1);
            WordStateOrigins[10].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[11] = new WordStateOrigin("�|����", 150, 60, 0, 2);
            WordStateOrigins[11].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[12] = new WordStateOrigin("�����񂾂�", 150, 70, 0, 6);
            WordStateOrigins[12].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[13] = new WordStateOrigin("����", 150, 50, 10, 3);
            WordStateOrigins[13].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[14] = new WordStateOrigin("���`", 150, 60, 10, 4);
            WordStateOrigins[14].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[15] = new WordStateOrigin("�󂯂Ă݂�", 150, 70, 10, 7);
            WordStateOrigins[15].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[16] = new WordStateOrigin("������", 100, 10, 0, 3);
            WordStateOrigins[16].Effects.Add(new NormalAtkAndDef());
            WordStateOrigins[16].Effects.Add(new CPGenerate(1));

            WordStateOrigins[17] = new WordStateOrigin("�ȊO��", 100, 0, 100, 5);
            WordStateOrigins[17].Effects.Add(new PercentAtkAndDef(50));

            WordStateOrigins[18] = new WordStateOrigin("��������", 10, 90, 0, 9);
            WordStateOrigins[18].Effects.Add(new NormalAtkAndDef());
            #endregion

            #region �V�B
            WordStateOrigins[20] = new WordStateOrigin("������", 150, 40, 0, 1);
            WordStateOrigins[20].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[21] = new WordStateOrigin("�|����", 150, 50, 0, 2);
            WordStateOrigins[21].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[22] = new WordStateOrigin("�ǂ�����", 150, 60, 0, 6);
            WordStateOrigins[22].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[23] = new WordStateOrigin("���ɂ�", 150, 0, 10, 1);
            WordStateOrigins[23].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[24] = new WordStateOrigin("�����ʗ��R", 150, 0, 20, 2);
            WordStateOrigins[24].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[25] = new WordStateOrigin("����̂���", 150, 0, 30, 6);
            WordStateOrigins[25].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[26] = new WordStateOrigin("���", 100, 10, 10, 3);
            WordStateOrigins[26].Effects.Add(new NormalAtkAndDef());
            WordStateOrigins[26].Effects.Add(new CPGenerate(1));

            WordStateOrigins[27] = new WordStateOrigin("��ւ�", 100, 0, 0, 3);
            WordStateOrigins[27].Effects.Add(new AttackChangeEffect(-10,Power.Enemy));

            WordStateOrigins[28] = new WordStateOrigin("�s�����Ȃ�", 50, 0, 0, 3);
            WordStateOrigins[28].Effects.Add(new HPRecoverEffect(20));
            #endregion

            #region ���[�V��
            WordStateOrigins[30] = new WordStateOrigin("�l��", 150, 40, 0, 1);
            WordStateOrigins[30].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[31] = new WordStateOrigin("�C����", 150, 50, 0, 2);
            WordStateOrigins[31].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[32] = new WordStateOrigin("���ꂽ����", 150, 60, 0, 6);
            WordStateOrigins[32].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[33] = new WordStateOrigin("���ׂĂ�", 150, 0, 10, 1);
            WordStateOrigins[33].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[34] = new WordStateOrigin("������", 150, 0, 20, 2);
            WordStateOrigins[34].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[35] = new WordStateOrigin("�������߂�", 150, 0, 30, 6);
            WordStateOrigins[35].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[36] = new WordStateOrigin("����", 100, 10, 10, 3);
            WordStateOrigins[36].Effects.Add(new NormalAtkAndDef());
            WordStateOrigins[36].Effects.Add(new CPGenerate(1));

            WordStateOrigins[37] = new WordStateOrigin("������", 100, 0, 0, 6);
            WordStateOrigins[37].Effects.Add(new AfterEfectTwice(Power.Enemy,0));

            WordStateOrigins[38] = new WordStateOrigin("�񂭂���", 100, 100, 0, 6);
            WordStateOrigins[38].Effects.Add(new PercentAtkAndDef(50));

            #endregion

            #region ����
            WordStateOrigins[60] = new WordStateOrigin("�v���m��", 700, 50, 0, 99);
            WordStateOrigins[60].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[61] = new WordStateOrigin("���", 500, 60, 0, 99);
            WordStateOrigins[61].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[62] = new WordStateOrigin("�{���", 100, 100, 0, 99);
            WordStateOrigins[62].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[63] = new WordStateOrigin("�ɂ݂�", 400, 70, 0, 99);
            WordStateOrigins[63].Effects.Add(new NormalAtkAndDef());

            WordStateOrigins[64] = new WordStateOrigin("�߂��݂�", 700, 30, 30, 99);
            WordStateOrigins[64].Effects.Add(new NormalAtkAndDef());
            #endregion

            #region ����(���b�h)

            #endregion

            WordStateOrigins[90] = new WordStateOrigin("������", 9999, 9999, 0, 1);
            WordStateOrigins[90].Effects.Add(new HolySaber());
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

            #region �����f�b�L
            //����
            OriginDecks[0][0] = WordStateOrigins[60];
            OriginDecks[0][1] = WordStateOrigins[60];

            OriginDecks[0][2] = WordStateOrigins[61];
            OriginDecks[0][3] = WordStateOrigins[61];
            OriginDecks[0][4] = WordStateOrigins[60];

            OriginDecks[0][5] = WordStateOrigins[62];
            OriginDecks[0][6] = WordStateOrigins[64];
            OriginDecks[0][7] = WordStateOrigins[63];
            OriginDecks[0][8] = WordStateOrigins[62];

            for (int i = 0; i < 9; i++)
            {
                //OriginDecks[0][i] = WordStateOrigins[99]; //�f�o�b�O.�ʂ����낷�f�b�N.
                CustomDecks[0][i] = WordStateOrigins[99];
            }
            #endregion

            #region �O�����f�b�L
            for (int i = 0; i < 9; i++)
            {
                CustomDecks[1][i] = WordStateOrigins[i+1];
            }
            //�����f�b�L
            OriginDecks[1][0] = WordStateOrigins[1];
            OriginDecks[1][1] = WordStateOrigins[4];

            OriginDecks[1][2] = WordStateOrigins[2];
            OriginDecks[1][3] = WordStateOrigins[1];
            OriginDecks[1][4] = WordStateOrigins[5];

            OriginDecks[1][5] = WordStateOrigins[3];
            OriginDecks[1][6] = WordStateOrigins[1];
            OriginDecks[1][7] = WordStateOrigins[1];
            OriginDecks[1][8] = WordStateOrigins[6];

            #endregion

            #region �X�J�C�f�b�L
            for (int i = 0; i < 9; i++)
            {
                CustomDecks[2][i] = WordStateOrigins[i + 10];
            }
            OriginDecks[2][0] = WordStateOrigins[10];
            OriginDecks[2][1] = WordStateOrigins[13];

            OriginDecks[2][2] = WordStateOrigins[11];
            OriginDecks[2][3] = WordStateOrigins[10];
            OriginDecks[2][4] = WordStateOrigins[14];

            OriginDecks[2][5] = WordStateOrigins[12];
            OriginDecks[2][6] = WordStateOrigins[10];
            OriginDecks[2][7] = WordStateOrigins[10];
            OriginDecks[2][8] = WordStateOrigins[15];

            #endregion

            #region �V�B�f�b�L
            for (int i = 0; i < 9; i++)
            {
                CustomDecks[3][i] = WordStateOrigins[i + 20];
            }
            OriginDecks[3][0] = WordStateOrigins[20];
            OriginDecks[3][1] = WordStateOrigins[23];

            OriginDecks[3][2] = WordStateOrigins[21];
            OriginDecks[3][3] = WordStateOrigins[20];
            OriginDecks[3][4] = WordStateOrigins[24];

            OriginDecks[3][5] = WordStateOrigins[22];
            OriginDecks[3][6] = WordStateOrigins[20];
            OriginDecks[3][7] = WordStateOrigins[20];
            OriginDecks[3][8] = WordStateOrigins[25];
            #endregion

            //�E��
            for (int i = 0; i < 9; i++)
            {
                CustomDecks[4][i] = WordStateOrigins[i + 30];
            }
            OriginDecks[4][0] = WordStateOrigins[30];
            OriginDecks[4][1] = WordStateOrigins[33];

            OriginDecks[4][2] = WordStateOrigins[31];
            OriginDecks[4][3] = WordStateOrigins[30];
            OriginDecks[4][4] = WordStateOrigins[34];

            OriginDecks[4][5] = WordStateOrigins[32];
            OriginDecks[4][6] = WordStateOrigins[30];
            OriginDecks[4][7] = WordStateOrigins[30];
            OriginDecks[4][8] = WordStateOrigins[35];
        }
    }
}
