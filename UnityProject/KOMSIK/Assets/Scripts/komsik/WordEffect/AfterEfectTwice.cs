using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public class AfterEfectTwice : IWordEffect
    {
        public Power Target { get; private set; }
        public int MultiNum { get; private set; }

        public AfterEfectTwice(Power target, int multileNum)
        {
            this.Target = target;
            this.MultiNum = multileNum;
        }

        public AfterEfectTwice(AfterEfectTwice x)
            : this(x.Target,x.MultiNum)
        {
        }

        public void DoEffect(WordState state, GameSystem gameSystem)
        {
            Debug.Log($"{state.Word}");

            //勢力情報取得.
            var targetPower = gameSystem.GetMinePower(Target);
            targetPower.CharacterState.SetNextWordEffectOccuerMultiple(MultiNum);
        }

        public string DetailStr(WordState state)
        {
            if(MultiNum > 0)
            {
                return $"次に自分が使用するワード効果を{MultiNum}倍する";
            }
            else
            {
                return "次に敵が使用するワード効果を無効にする";
            }
        }
    }
}
