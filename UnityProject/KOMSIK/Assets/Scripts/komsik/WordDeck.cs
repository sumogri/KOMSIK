using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace KOMSIK
{
    /// <summary>
    /// 実行時Deck
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

            //初期デッキをセット.
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
        /// 戦闘中、このデックから実行する単語を抽選.
        /// </summary>
        /// <returns></returns>
        public WordState[] ChoseBattleWordSet()
        {
            WordState[] selected = new WordState[3];

            // 一段目.
            var first = UnityEngine.Random.Range(0,1+1);
            selected[0] = Slot[first];

            // 二段目.
            /// 0なら2-3,1なら3-4のどっちか.
            var second = UnityEngine.Random.Range(2 + first, 3 + first + 1);
            selected[1] = Slot[second];

            // 三段目.
            var diff = second - 2; //ゲタをどける. 2-4を0-2範囲に.
            var third = UnityEngine.Random.Range(5 + diff, 6 + diff + 1);
            selected[2] = Slot[third];

            Debug.Log($"ChoseBattleWordSet {first},{second},{third}");
            return selected;
        }
    }
}
