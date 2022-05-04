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

        public IObservable<int> OnChangeHP => hp;

        private ReactiveProperty<int> hp = new ReactiveProperty<int>();
        private int customDeckID = 0;

        public CharacterState(int hp,int customDeckID)
        {
            this.hp.Value = hp;
            this.customDeckID = customDeckID;
        }
    }
}
