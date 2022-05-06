using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace KOMSIK
{
    public class CharacterState
    {
        public int HP => hp.Value;
        public int CustomDeckID => customDeckID;
        public Power Power => power;
        public int Diffence => diffence.Value;

        public IObservable<int> OnChangeHP => hp;
        public IObservable<int> OnChangeDiffence => diffence;

        private ReactiveProperty<int> hp = new ReactiveProperty<int>();
        private int customDeckID = 0;
        private Power power = Power.None;
        private ReactiveProperty<int> diffence = new ReactiveProperty<int>();

        public CharacterState()
        {

        }

        public CharacterState(int hp,int customDeckID,Power power)
        {
            this.hp.Value = hp;
            this.customDeckID = customDeckID;
            this.power = power;
            this.diffence.Value = 0;
        }

        public CharacterState(CharacterOrigin origin)
        {
            InitFromOring(origin);
        }

        public void InitFromOring(CharacterOrigin origin)
        {
            this.hp.Value = origin.HP;
            this.customDeckID = origin.CustomDeckID;
            this.power = origin.Power;
            this.diffence.Value = 0;
        }

        public int Damage(int damage)
        {
            damage = DamageAttenuation(damage);

            hp.Value -= damage;

            return damage;
        }

        public void DiffenceUp(int diffence)
        {
            this.diffence.Value += diffence;
        }

        public void OnTurnEnd()
        {
            //ボウギョは1ターン限定.もちこさない.
            diffence.Value = 0;
        }

        /// <summary>
        /// ダメージ減衰処理.
        /// </summary>
        /// <param name="damaga"></param>
        /// <returns></returns>
        private int DamageAttenuation(int damage)
        {
            //防御で肩代わり.肩代わりした分、防御ダウン.
            var attenuatedDamage = Math.Max(0,damage - Diffence);
            diffence.Value = Math.Max(0, Diffence - damage);

            return attenuatedDamage;
        }
    }
}
