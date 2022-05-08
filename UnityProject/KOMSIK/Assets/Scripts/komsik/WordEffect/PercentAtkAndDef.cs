using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public class PercentAtkAndDef : IWordEffect
    {
        public int Percent { get; private set; } = 0;

        public PercentAtkAndDef(int percent)
        {
            Percent = percent;
        }

        public PercentAtkAndDef(PercentAtkAndDef x)
            : this(x.Percent)
        {
        }

        public void DoEffect(WordState state, GameSystem gameSystem)
        {
            Debug.Log($"{state.Word}");

            //���͏��擾.
            var minePower = gameSystem.GetMinePower(state.Power);
            var antiPower = gameSystem.GetAntiPower(state.Power);

            // �_�C�X�U���āA�m���ȉ��Ȃ�~�X.
            var rand = Random.Range(0,100);
            if (rand >= Percent)
            {
                // �Ȃɂ�������Ȃ�.
                Debug.Log($"Effect failed {rand} {Percent}");
                return;
            }

            antiPower.Damage(state.Attack + minePower.CharacterState.Attack);
            minePower.CharacterState.DiffenceUp(state.Deffence);
        }

        public string DetailStr(WordState state)
        {
            var s = $"{Percent}%�̊m���ŁA";
            if (state.Attack > 0)
            {
                s += $"�����{state.Attack}�_���[�W ";
            }
            if (state.Deffence > 0)
            {
                s += $"���̃^�[��{state.Deffence}�_���[�W�h��";
            }

            return s;
        }
    }
}
