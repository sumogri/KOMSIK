using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public class AttackChangeEffect : IWordEffect
    {
        public int ChangeAttackNum { get; private set; }
        public Power Target { get; private set; }

        public AttackChangeEffect(int attack,Power target)
        {
            ChangeAttackNum = attack;
            Target = target;
        }

        public AttackChangeEffect(AttackChangeEffect x)
            : this(x.ChangeAttackNum,x.Target)
        {
        }

        public void DoEffect(WordState state, GameSystem gameSystem)
        {
            Debug.Log($"{state.Word}");

            var targetPower = gameSystem.GetMinePower(Target);

            if(ChangeAttackNum < 0)
            {
                targetPower.CharacterState.AttackDown(ChangeAttackNum * -1);
            }
        }

        public string DetailStr(WordState state)
        {
            return $"“G‚Ì—^ƒ_ƒ[ƒW‚ð{ChangeAttackNum}(‰i‘±)";
        }
    }
}
