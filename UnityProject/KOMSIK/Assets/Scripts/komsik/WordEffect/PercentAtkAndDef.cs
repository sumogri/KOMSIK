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

            //勢力情報取得.
            var minePower = gameSystem.GetMinePower(state.Power);
            var antiPower = gameSystem.GetAntiPower(state.Power);

            // ダイス振って、確率以下ならミス.
            var rand = Random.Range(0,100);
            if (rand >= Percent)
            {
                // なにもおこらない.
                Debug.Log($"Effect failed {rand} {Percent}");
                return;
            }

            antiPower.Damage(state.Attack + minePower.CharacterState.Attack);
            minePower.CharacterState.DiffenceUp(state.Deffence);
        }

        public string DetailStr(WordState state)
        {
            var s = $"{Percent}%の確率で、";
            if (state.Attack > 0)
            {
                s += $"相手に{state.Attack}ダメージ ";
            }
            if (state.Deffence > 0)
            {
                s += $"このターン{state.Deffence}ダメージ防ぐ";
            }

            return s;
        }
    }
}
