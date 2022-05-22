using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

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
        public IObservable<Unit> OnSetupBattle => onSetupBattleSubject;
        public IObservable<BattleResult> OnBattleEnd => onButtleEndSubject;
        public IObservable<Unit> OnInit => onInitSubject;
        #endregion

        private ReactiveProperty<GameState> gameState = new ReactiveProperty<GameState>(new GameState());
        private ReactiveProperty<CharacterState> ownState = new ReactiveProperty<CharacterState>();
        private ReactiveProperty<CharacterState> enemyState = new ReactiveProperty<CharacterState>();

        private WordDeck ownWordDeck = new WordDeck();
        private WordDeck enemyWordDeck = new WordDeck();
        private Stack<WordState> battleWordStack = new Stack<WordState>();
        private WordState[] ownChosenWordStateChache = null;
        private WordState[] enemyChosenWordStateChache = null;
        private Subject<WordState> onDoEffectWordSubject = new Subject<WordState>();
        private Subject<Unit> onSetupBattleSubject = new Subject<Unit>();
        private Subject<BattleResult> onButtleEndSubject = new Subject<BattleResult>();
        private Subject<Unit> onInitSubject = new Subject<Unit>();

        private PowerState ownPower;
        private PowerState enemeyPower;

        public GameSystem()
        {
            ///��ꂽ�� : �L����DB���珉���L�����I��.
            ownState.Value = new CharacterState(CharacterOrigin.GetOrigin(CharacterOrigin.CharacterID.Gran));
            enemyState.Value = new CharacterState(CharacterOrigin.GetOrigin(CharacterOrigin.CharacterID.Maou));

            // ���͏��l�ߒ���,
            /// System�̃t�B�[���h�ɂ��Ă��܂����̂ŁA�Q�Ƃ���. ����Ȃɂ����͒P�ʂő����K�v������̂͂����.
            ownPower = new PowerState(OwnDeck, OwnState, ownChosenWordStateChache, Power.Own);
            enemeyPower = new PowerState(EnemyDeck, EnemyState, enemyChosenWordStateChache, Power.Enemy);

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
            Debug.Log("Call Init");
            gameState.Value.Init(10, 13);

            /// ����������.
            SelectOwnCharacter(CharacterOrigin.GetOrigin(CharacterOrigin.CharacterID.Gran));

            //�G�̏�����.
            enemyState.Value.InitFromOring(CharacterOrigin.GetOrigin(CharacterOrigin.CharacterID.Maou));
            enemyWordDeck.SetDeckFromCharacter(enemyState.Value);
            onInitSubject.OnNext(Unit.Default);
        }

        public void Continue()
        {
            gameState.Value.Init(10, 13);

            /// ����������.
            SelectOwnCharacter(CharacterOrigin.GetOrigin(CharacterOrigin.CharacterID.Gran));

            //�G�̏�����.
            enemyState.Value.InitFromOring(CharacterOrigin.GetOrigin(CharacterOrigin.CharacterID.Maou));
            enemyWordDeck.SetDeckFromCharacter(enemyState.Value);
            onInitSubject.OnNext(Unit.Default);

            GameStart();
        }

        /// <summary>
        /// �L�����I
        /// </summary>
        /// <param name="ownState"></param>
        public void SelectOwnCharacter(CharacterOrigin origin)
        {
            ownState.Value.InitFromOring(origin);
            ownWordDeck.SetDeckFromCharacter(ownState.Value);
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
            //CustomStart(); //��.�C�x���g�I���ŌĂ�.
        }

        /// <summary>
        /// �����J�n.
        /// </summary>
        public void SetupBattle()
        {
            Debug.Log("BattleStart");

            battleWordStack.Clear();
            /// �f�b�L���݂�����
            ownChosenWordStateChache = ownWordDeck.ChoseBattleWordSet();
            ownPower.SetChosenChache(ownChosenWordStateChache); //���ʂ��L���b�V��.
            enemyChosenWordStateChache= enemyWordDeck.ChoseBattleWordSet();
            enemeyPower.SetChosenChache(enemyChosenWordStateChache); //���ʂ��L���b�V��.

            ///����=>����=>����=>...�Ə����X�^�b�N�ɐς�.
            for (int i = ownChosenWordStateChache.Length - 1; i >= 0; i--)
            {
                battleWordStack.Push(enemyChosenWordStateChache[i]);
                battleWordStack.Push(ownChosenWordStateChache[i]);
            }

            gameState.Value.ChangeGamePhase(GameState.GamePhase.Battle);
            onSetupBattleSubject.OnNext(Unit.Default);
        }

        /// <summary>
        /// �o�g�� ���������ׂ����[�h������.
        /// </summary>
        public bool BattleTopWordDo()
        {
            if(battleWordStack.Count <= 0)
            {
                return false;
            }

            var word = battleWordStack.Pop();
            onDoEffectWordSubject.OnNext(word);
            word.DoEffects(this);

            Debug.Log("BattleTopWordDo");
            return true;
        }

        public async UniTask BattleRun()
        {
            // �Ă��Ƃ�.���o�҂�
            await UniTask.Delay(500);

            while (BattleTopWordDo())
            {
                // �Ă��Ƃ�.���o�҂�.
                await UniTask.Delay(500);

                //�Q�[���I�[�o�[����.
                if (CheckAndDoBattleEnd())
                {
                    // �Q�[���I�[�o�[���Ă���o�g�������ł��؂�.
                    return;
                }
            }

            //�ЂƂƂ���J�[�h������̂Ń^�[���I��.
            TurnEnd();

            //�J�X�^���Ɉړ�.
            CustomStart();
        }

        /// <summary>
        /// �Q�[���I�[�o�[/�N���A����.
        /// </summary>
        /// <returns>�I�������𖞂�������</returns>
        public bool CheckAndDoBattleEnd()
        {
            if(EnemyState.HP <= 0 || OwnState.HP <= 0 || GameState.NowSection != GameState.Section.Game)
            {
                BattleEnd();
                return true;
            }

            return false;
        }

        /// <summary>
        /// �J�b�g�C��. �f�b�L�g�b�v�Ƀ��[�h���Z�b�g.
        /// </summary>
        public void BattleCutinWord(WordState cutined)
        {
            Debug.Log("BattleCutinWord");
            battleWordStack.Push(cutined);
        }

        public void TurnEnd()
        {
            gameState.Value.OnTurnEnd();
            ownPower.CharacterState.OnTurnEnd();
            enemeyPower.CharacterState.OnTurnEnd();

            battleWordStack.Clear();
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
                PhaseChangeToDefeate();
            }
            else if(gameState.Value.NowSection == GameState.Section.NormalEnd)
            {
                gameState.Value.ChangeGamePhase(GameState.GamePhase.AftTalkGood);
            }
            else
            {
                gameState.Value.ChangeGamePhase(GameState.GamePhase.AftTalkGood);
                gameState.Value.ChangeSection(GameState.Section.GoodEnd);
            }
        }

        /// <summary>
        /// �����ăt�F�[�Y��؂�ւ���ꍇ.
        /// 
        /// ���̃L�����Ɍ��.
        /// </summary>
        public void PhaseChangeToDefeate()
        {
            gameState.Value.ChangeGamePhase(GameState.GamePhase.AftTalkBad);
            gameState.Value.OnPhaseEnd();

            // 4��܂ł̓L�����`�F���őΉ�
            if(GameState.PhaseIterateTime < 4)
            {
                var nextCharaID = CharacterOrigin.CharacterID.Gran + GameState.PhaseIterateTime;
                SelectOwnCharacter(CharacterOrigin.GetOrigin(nextCharaID));
                TurnEnd();

                //CustomStart(); //��.���o�҂�.
            }
            else
            {
                gameState.Value.ChangeSection(GameState.Section.BadEnd); //��.���o�I���ɓ����.
            }
        }

        /// <summary>
        /// �f�b�L��WordOrigin���h��.
        /// </summary>
        /// <param name="wordState"></param>
        /// <param name="wordStateOrigin"></param>
        /// <returns>�����Ȃ�true</returns>
        public bool CustomSetWordOrigin(WordState wordState, WordStateOrigin wordStateOrigin)
        {
            var cost = wordStateOrigin.CustomCost;
            if(GameState.CustomPoint < cost)
            {
                Debug.Log($"CP Over cp:{GameState.CustomPoint},cost:{cost}");
                return false;
            }

            // origin��word��set,Cost�x����.
            wordState.SetFromOrigin(wordStateOrigin);
            GameState.PaidCustomPoint(cost);
            return true;
        }
    }
}
