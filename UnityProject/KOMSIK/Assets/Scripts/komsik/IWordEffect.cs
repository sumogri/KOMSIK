using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    /// <summary>
    /// ���[�h����
    /// 
    /// ���̃��[�h��p�̌��ʂ͂��ׂĂ����ŏ���.
    /// �����A�V���O���g���I�ɐU�镑��. Origin�̃I�u�W�F�N�g��State�Ɏg���񂳂��.
    /// �C���X�^���X���~�����Ȃ�ADoEffect�o�R�ōĐ������A�p�����[�^��WordState�ɐ��₷���c�c(�����񂪂����ς�)
    /// </summary>
    public interface IWordEffect
    {
        public void DoEffect(WordState state, GameSystem gameSystem);

        public string DetailStr(WordState state);
    }
}
