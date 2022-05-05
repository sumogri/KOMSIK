using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace KOMSIK
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private WordSlotView[] wordSlotViews; //���ォ�珇�ɓ����Ă�z��.
        [SerializeField] private Power power;

        private WordDeck targetModel;
        private bool detailActive = false;

        public void DoSubscribe(WordDeck target)
        {
            DoSubscribe(target,GameManager.GameSystem);
        }

        public void DoSubscribe(WordDeck target,GameSystem system)
        {
            targetModel = target;

            for (var i = 0; i < target.Slot.Length; i++)
            {
                wordSlotViews[i].DoSubscribe(target.Slot[i]);
            }

            system.GameState.OnChangeGamePhase
                .Where(x => x == GameState.GamePhase.Battle)
                .Subscribe(x => OnStartBattle(system))
                .AddTo(gameObject);

            system.GameState.OnChangeGamePhase
                .Where(x => x == GameState.GamePhase.Custom)
                .Subscribe(x => OnStartCustom())
                .AddTo(gameObject);

            system.OnWordDoEffect
                .Subscribe(OnWordDoEffect)
                .AddTo(gameObject);
        }

        private WordState onWordDoPre = null;
        private void OnWordDoEffect(WordState word)
        {
            //�g���J�[�h���n�C���C�g
            foreach (var wv in wordSlotViews)
            {
                if (word == wv.Model)
                {
                    wv.SetHighilightActivate2(true);
                }
                if(onWordDoPre == wv.Model)
                {
                    wv.SetHighilightActivate2(false);
                }
            }

            onWordDoPre = word;
        }

        private void OnStartBattle(GameSystem system)
        {
            var words = system.OwnChosenWordStateChache;

            if(power == Power.Enemy)
            {
                words = system.EnemyChosenWordStateChache;
            }

            //���̏����Ŏg���J�[�h���n�C���C�g
            foreach(var wv in wordSlotViews)
            {
                if(words.Contains(wv.Model))
                {
                    wv.SetHighilightActivate(true);
                }
            }
        }

        private void OnStartCustom()
        {
            foreach(var view in wordSlotViews)
            {
                view.SetHighilightActivate(false);
            }
        }

        /// <summary>
        /// ���[�h�ڍו\���̃A�N�e�B�u/��A�N�e�B�u��؂�ւ���.
        /// ON�Ȃ�OFF,OFF�Ȃ�ON�ɁB
        /// </summary>
        public void WordDetailActiveTurn()
        {
            OnSetActivateWordDetail(!detailActive);
            detailActive = !detailActive;
        }

        public void OnSetActivateWordDetail(bool active)
        {
            foreach(var view in wordSlotViews)
            {
                view.SetDetailActivate(active);
            }
        }

        public enum Power
        {
            Own,
            Enemy
        }
    }
}
