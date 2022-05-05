using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace KOMSIK
{
    /// <summary>
    /// ���W�b�N
    /// 
    /// �Q�[���J�n 
    /// => (�L�����I => �o�g�� => ���ʏ���)*4 
    /// => �G���f�B���O
    /// </summary>
    public class GameSystem 
    {
        #region get Property
        public WordDeck OwnDeck => ownWordDeck;
        public WordDeck EnemyDeck => enemyWordDeck;
        public CharacterState OwnState => ownState.Value;
        public CharacterState EnemyState => enemyState.Value;
        /// <summary>
        /// ���O��BattleStart�őI�΂ꂽ�����̃��[�h
        /// </summary>
        public WordState[] OwnChosenWordStateChache => ownChosenWordStateChache;
        /// <summary>
        /// ���O��BattleStart�őI�΂ꂽ�G�̃��[�h
        /// </summary>
        public WordState[] EnemyChosenWordStateChache => enemyChosenWordStateChache;
        public GameState GameState => gameState.Value;
        #endregion

        #region event
        public IObservable<WordState> OnWordDoEffect => onDoEffectWordSubject;
        #endregion

        private ReactiveProperty<GameState> gameState = new ReactiveProperty<GameState>(new GameState());
        private ReactiveProperty<CharacterState> ownState = new ReactiveProperty<CharacterState>();
        private ReactiveProperty<CharacterState> enemyState = new ReactiveProperty<CharacterState>();

        private WordDeck ownWordDeck = new WordDeck();
        private WordDeck enemyWordDeck = new WordDeck();
        private Queue<WordState> battleWordQueue = new Queue<WordState>();
        private WordState[] ownChosenWordStateChache = null;
        private WordState[] enemyChosenWordStateChache = null;
        private Subject<WordState> onDoEffectWordSubject = new Subject<WordState>();

        private PowerState ownPower;
        private PowerState enemeyPower;

        public GameSystem()
        {
            this.Init();
        }

        public PowerState GetAntiPower(Power power)
        {
            switch (power)
            {
                case Power.Enemy:
                    return ownPower;
                case Power.Own:
                    return enemeyPower;
            }

            Debug.LogError($"GetAntiChar None Power {power}");
            return null;
        }
        public PowerState GetMinePower(Power power)
        {
            switch (power)
            {
                case Power.Enemy:
                    return enemeyPower;
                case Power.Own:
                    return ownPower;
            }

            Debug.LogError($"GetMineChar None Power {power}");
            return null;
        }


        /// <summary>
        /// �Q�[��Init.������Ԃɖ߂�.
        /// </summary>
        public void Init()
        {
            gameState.Value.Init(10,3);

            ///��ꂽ�� : �L����DB���珉���L�����I��.
            ownState.Value = new CharacterState(100,1,Power.Own);
            enemyState.Value = new CharacterState(9999,0,Power.Enemy);

            //�G�̃f�b�N������.�Œ�.
            enemyWordDeck.SetDeckFromCharacter(enemyState.Value);
            ownWordDeck.SetDeckFromCharacter(ownState.Value);

            // ���͏��l�ߒ���,
            /// System�̃t�B�[���h�ɂ��Ă��܂����̂ŁA�Q�Ƃ���. ����Ȃɂ����͒P�ʂő����K�v������̂͂����.
            ownPower = new PowerState(OwnDeck, OwnState, ownChosenWordStateChache, Power.Own);
            enemeyPower = new PowerState(EnemyDeck, EnemyState, enemyChosenWordStateChache, Power.Enemy);
        }

        /// <summary>
        /// �L�����I
        /// </summary>
        /// <param name="ownState"></param>
        public void SelectOwnCharacter(CharacterState ownState)
        {
            this.ownState.Value = ownState;
            ownWordDeck.SetDeckFromCharacter(ownState);
        }

        /// <summary>
        /// ���I�񂾃L�����Ŋm��.
        /// </summary>
        public void SubmitSelectCharacter()
        {

        }

        /// <summary>
        /// �Q�[���J�n.
        /// </summary>
        public void GameStart()
        {
            gameState.Value.ChangeSection(GameState.Section.Game);
            gameState.Value.ChangeGamePhase(GameState.GamePhase.PreTalk);
        }

        /// <summary>
        /// �����J�n.
        /// </summary>
        public void SetupBattle()
        {
            Debug.Log("BattleStart");

            battleWordQueue.Clear();
            /// �f�b�L���݂�����
            ownChosenWordStateChache = ownWordDeck.ChoseBattleWordSet();
            ownPower.SetChosenChache(ownChosenWordStateChache); //���ʂ��L���b�V��.
            enemyChosenWordStateChache= enemyWordDeck.ChoseBattleWordSet();
            enemeyPower.SetChosenChache(enemyChosenWordStateChache); //���ʂ��L���b�V��.

            ///����=>����=>����...�Ə����X�^�b�N�ɐς�.
            for (int i = 0; i < ownChosenWordStateChache.Length; i++)
            {
                battleWordQueue.Enqueue(ownChosenWordStateChache[i]);
                battleWordQueue.Enqueue(enemyChosenWordStateChache[i]);
            }

            gameState.Value.ChangeGamePhase(GameState.GamePhase.Battle);
        }

        /// <summary>
        /// �o�g�� ���������ׂ����[�h������.
        /// </summary>
        public bool BattleTopWordDo()
        {
            if(battleWordQueue.Count <= 0)
            {
                return false;
            }

            var word = battleWordQueue.Dequeue();
            onDoEffectWordSubject.OnNext(word);
            word.DoEffects(this);

            Debug.Log("BattleTopWordDo");
            return true;
        }

        /// <summary>
        /// �J�b�g�C��. �f�b�L�g�b�v�Ƀ��[�h���Z�b�g.
        /// </summary>
        public void BattleCutinWord(WordState cutined)
        {
            Debug.Log("BattleCutinWord");
        }

        public void TurnEnd()
        {
            gameState.Value.OnTurnEnd();
            ownPower.CharacterState.OnTurnEnd();
            enemeyPower.CharacterState.OnTurnEnd();
        }

        /// <summary>
        /// �J�X�^���J�n.
        /// </summary>
        public void CustomStart()
        {
            Debug.Log("CustomStart");

            gameState.Value.ChangeGamePhase(GameState.GamePhase.Custom);
        }

        /// <summary>
        /// �퓬�I��.
        /// </summary>
        public void BattleEnd()
        {
            /// ������HP��0�ȉ��Ȃ�A����ł�̂ŕ���.
            if(ownState.Value?.HP <= 0)
            {
                gameState.Value.ChangeGamePhase(GameState.GamePhase.AftTalkBad);
            }
            else
            {
                gameState.Value.ChangeGamePhase(GameState.GamePhase.AftTalkGood);
            }
        }
    }
}
