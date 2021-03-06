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

            //勢力情報取得.
            var minePower = gameSystem.GetMinePower(state.Power);
            var antiPower = gameSystem.GetAntiPower(state.Power);

            antiPower.Damage(state.Attack + minePower.CharacterState.Attack);
            minePower.CharacterState.DiffenceUp(state.Deffence);
        }

        public string DetailStr(WordState state)
        {
            var s = "";
            if(state.Attack > 0)
            {
                s += $"相手に{state.Attack}ダメージ ";
            }
            if(state.Deffence > 0)
            {
                s += $"このターン{state.Deffence}ダメージ防ぐ";
            }

            return s;
        }
    }
}
