using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public class PowerState 
    {
        public WordDeck WordDeck => wordDeck;
        public CharacterState CharacterState => charState;
        public WordState[] ChosenWordStateChache => chosenWordStateChache;
        public Power Power => power;
        public WordState CutinWordState => cutinWord;

        private WordDeck wordDeck;
        private CharacterState charState;
        private WordState[] chosenWordStateChache;
        private WordState cutinWord;
        private Power power;

        public PowerState(
            WordDeck wordDeck,
            CharacterState charState,
            WordState[] chosenWordStateChache,
            Power power
            )
        {
            this.wordDeck = wordDeck;
            this.charState = charState;
            this.chosenWordStateChache = chosenWordStateChache;
            this.power = power;
            cutinWord = new WordState();
            cutinWord.Power = power;
        }

        public void SetChosenChache(WordState[] chosenChache)
        {
            chosenWordStateChache = chosenChache;
        }

        public void Damage(int damage)
        {
            //まずキャラにダメージ入れて
            //次にカードにダメージ入る
            damage = charState.Damage(damage);

            foreach(var w in chosenWordStateChache)
            {
                w.Damaged(damage);
            }
        }
    }
}
