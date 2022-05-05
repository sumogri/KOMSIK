using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace KOMSIK
{
    /// <summary>
    /// 単語カード 実行時ステート
    /// </summary>
    public class WordState
    {
        public int HP => hp.Value;
        public int Attack => attack.Value;
        public int Deffence => deffence.Value;
        public string Word => word.Value;
        public Power Power { get; set; }
        public List<IWordEffect> Effects => effects;

        public IObservable<int> OnChangeHP => hp;
        public IObservable<int> OnChangeAttack => attack;
        public IObservable<int> OnChangeDeffence => deffence;
        public IObservable<string> OnChangeWord => word;

        private ReactiveProperty<int> hp = new ReactiveProperty<int>();
        private ReactiveProperty<int> attack = new ReactiveProperty<int>();
        private ReactiveProperty<int> deffence = new ReactiveProperty<int>();
        private ReactiveProperty<string> word = new ReactiveProperty<string>();
        private List<IWordEffect> effects = new List<IWordEffect>();

        public WordState()
        {
            this.hp.Value = 0;
            this.attack.Value = 0;
            this.deffence.Value = 0;
            this.word.Value = "";
        }

        public WordState(int hp,int attack,int deffence,string word,Power power)
        {
            this.hp.Value = hp;
            this.attack.Value = attack;
            this.deffence.Value = deffence;
            this.word.Value = word;
            this.Power = power;
        }

        public void Damaged(int subHP)
        {
            hp.Value -= subHP;
        }

        public void DoEffects(GameSystem gameSystem)
        {
            for (var i = 0; i < effects.Count; i++)
            {
                effects[i].DoEffect(this, gameSystem);
            }
        }

        public void SetFromOrigin(WordStateOrigin origin)
        {
            hp.Value = origin.HP;
            attack.Value = origin.Attack;
            deffence.Value = origin.Diffence;
            word.Value = origin.Word;
            effects = origin.Effects;
        }
    }
}
