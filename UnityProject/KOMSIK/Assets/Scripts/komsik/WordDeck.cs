using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace KOMSIK
{
    /// <summary>
    /// ���s��Deck
    /// </summary>
    public class WordDeck 
    {
        public WordState[] Slot { get; } = new WordState[9];
        public Power Power => power;

        private Power power = Power.None;

        public WordDeck()
        {
            for(int i = 0; i < Slot.Length; i++)
            {
                Slot[i] = new WordState();
            }
        }

        public void SetDeckFromCharacter(CharacterState character)
        {
            var decID = character.CustomDeckID;
            var originDeck = WordPool.OriginDecks[decID];

            //�����f�b�L���Z�b�g.
            for(int i = 0; i < originDeck.Length; i++)
            {
                Slot[i].SetFromOrigin(originDeck[i]);
                Slot[i].Power = character.Power;
            }

            this.power = character.Power;
        }

        public void SetWord(int i, WordStateOrigin state)
        {
            Slot[i].SetFromOrigin(state);
        }

        /// <summary>
        /// �퓬���A���̃f�b�N������s����P��𒊑I.
        /// </summary>
        /// <returns></returns>
        public WordState[] ChoseBattleWordSet()
        {
            WordState[] selected = new WordState[3];

            // ��i��.
            var first = UnityEngine.Random.Range(0,1+1);
            selected[0] = Slot[first];

            // ��i��.
            /// 0�Ȃ�2-3,1�Ȃ�3-4�̂ǂ�����.
            var second = UnityEngine.Random.Range(2 + first, 3 + first + 1);
            selected[1] = Slot[second];

            // �O�i��.
            var diff = second - 2; //�Q�^���ǂ���. 2-4��0-2�͈͂�.
            var third = UnityEngine.Random.Range(5 + diff, 6 + diff + 1);
            selected[2] = Slot[third];

            Debug.Log($"ChoseBattleWordSet {first},{second},{third}");
            return selected;
        }
    }
}
