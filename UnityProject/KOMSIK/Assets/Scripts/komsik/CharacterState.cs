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
        public int CustomDeckID => customDeckID.Value;
        public Power Power => power;
        public int Diffence => diffence.Value;
        public int Attack => attack.Value;

        public IObservable<int> OnChangeHP => hp;
        public IObservable<int> OnChangeDiffence => diffence;
        public IObservable<int> OnChangeCustomDeckID => customDeckID;

        private ReactiveProperty<int> hp = new ReactiveProperty<int>();
        private ReactiveProperty<int> customDeckID = new ReactiveProperty<int>(0);
        private Power power = Power.None;
        private ReactiveProperty<int> diffence = new ReactiveProperty<int>();
        private int occuerMultipleNum = 1; //多重発動回数
        private ReactiveProperty<int> attack = new ReactiveProperty<int>(0); //攻撃低下.

        public CharacterState()
        {

        }

        public CharacterState(int hp,int customDeckID,Power power)
        {
            this.hp.Value = hp;
            this.customDeckID.Value = customDeckID;
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
            this.customDeckID.Value = origin.CustomDeckID;
            this.power = origin.Power;
            this.diffence.Value = 0;
            this.attack.Value = 0;
            this.occuerMultipleNum = 1;
        }

        public int Damage(int damage)
        {
            damage = DamageAttenuation(damage);

            hp.Value -= damage;

            return damage;
        }
        public int Cure(int cure)
        {
            hp.Value += cure;

            return cure;
        }

        /// <summary>
        /// 次の効果をnum回発生させる状態をセット.
        /// </summary>
        /// <param name="num"></param>
        public void SetNextWordEffectOccuerMultiple(int num)
        {
            occuerMultipleNum = num;
        }
        public int PopNextWordEffectOccuerMultiple()
        {
            // 多重発動ステートは使ったら消える.
            var ret = occuerMultipleNum;
            occuerMultipleNum = 1;
            return ret;
        }

        public void AttackDown(int less)
        {
            // 重複しない,最新の値を適用.
            attack.Value = -less;
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
