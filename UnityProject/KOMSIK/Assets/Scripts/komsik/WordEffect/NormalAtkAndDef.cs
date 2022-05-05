using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public class NormalAtkAndDef : IWordEffect
    {
        public NormalAtkAndDef()
        {
        }

        public NormalAtkAndDef(NormalAtkAndDef x)
        {
        }

        public void DoEffect(WordState state, GameSystem gameSystem)
        {
            Debug.Log($"{state.Word}");

            //ê®óÕèÓïÒéÊìæ.
            var minePower = gameSystem.GetMinePower(state.Power);
            var antiPower = gameSystem.GetAntiPower(state.Power);

            antiPower.Damage(state.Attack);
            minePower.CharacterState.DiffenceUp(state.Deffence);
        }
    }
}
