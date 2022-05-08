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
        private int occuerMultipleNum = 1; //���d������
        private ReactiveProperty<int> attack = new ReactiveProperty<int>(0); //�U���ቺ.

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
        /// ���̌��ʂ�num�񔭐��������Ԃ��Z�b�g.
        /// </summary>
        /// <param name="num"></param>
        public void SetNextWordEffectOccuerMultiple(int num)
        {
            occuerMultipleNum = num;
        }
        public int PopNextWordEffectOccuerMultiple()
        {
            // ���d�����X�e�[�g�͎g�����������.
            var ret = occuerMultipleNum;
            occuerMultipleNum = 1;
            return ret;
        }

        public void AttackDown(int less)
        {
            // �d�����Ȃ�,�ŐV�̒l��K�p.
            attack.Value = -less;
        }

        public void DiffenceUp(int diffence)
        {
            this.diffence.Value += diffence;
        }

        public void OnTurnEnd()
        {
            //�{�E�M����1�^�[������.���������Ȃ�.
            diffence.Value = 0;
        }

        /// <summary>
        /// �_���[�W��������.
        /// </summary>
        /// <param name="damaga"></param>
        /// <returns></returns>
        private int DamageAttenuation(int damage)
        {
            //�h��Ō�����.�����肵�����A�h��_�E��.
            var attenuatedDamage = Math.Max(0,damage - Diffence);
            diffence.Value = Math.Max(0, Diffence - damage);

            return attenuatedDamage;
        }
    }
}
