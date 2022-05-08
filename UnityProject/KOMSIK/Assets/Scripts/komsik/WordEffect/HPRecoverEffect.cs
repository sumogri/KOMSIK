using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public class HPRecoverEffect : IWordEffect
    {
        public int Cure { get; private set; }

        public HPRecoverEffect(int cure)
        {
            Cure = cure;
        }

        public HPRecoverEffect(HPRecoverEffect x)
            : this(x.Cure)
        {

        }

        public void DoEffect(WordState state, GameSystem gameSystem)
        {
            Debug.Log($"{state.Word}");

            //���͏��擾.
            var minePower = gameSystem.GetMinePower(state.Power);

            minePower.CharacterState.Cure(Cure);
        }

        public string DetailStr(WordState state)
        {
            return $"���g��HP��{Cure}��";
        }
    }
}
