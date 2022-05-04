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
        public WordDeck OwnDeck => ownWordDeck;
        public WordDeck EnemyDeck => enemyWordDeck;
        public CharacterState OwnState => ownState.Value;
        public CharacterState EnemyState => enemyState.Value;

        private ReactiveProperty<GameState> gameState = new ReactiveProperty<GameState>(new GameState());
        private ReactiveProperty<CharacterState> ownState = new ReactiveProperty<CharacterState>();
        private ReactiveProperty<CharacterState> enemyState = new ReactiveProperty<CharacterState>();

        private WordDeck ownWordDeck = new WordDeck();
        private WordDeck enemyWordDeck = new WordDeck();
        private Queue<WordState> battleWordQueue = new Queue<WordState>();

        public GameSystem()
        {
            this.Init();
        }

        /// <summary>
        /// �Q�[��Init.������Ԃɖ߂�.
        /// </summary>
        public void Init()
        {
            gameState.Value.Init(10,3);

            ///��ꂽ�� : �L����DB���珉���L�����I��.
            ownState.Value = new CharacterState(100,1);
            enemyState.Value = new CharacterState(9999,0);

            //�G�̃f�b�N������.�Œ�.
            enemyWordDeck.SetDeckFromCharacter(enemyState.Value);
            ownWordDeck.SetDeckFromCharacter(ownState.Value);
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
        public void BattleStart()
        {
            Debug.Log("BattleStart");

            battleWordQueue.Clear();
            /// �f�b�L���݂�����
            var ownDeckChosen = ownWordDeck.ChoseBattleWordSet();
            var enemyDeckChosen = enemyWordDeck.ChoseBattleWordSet();

            ///����=>����=>����...�Ə����X�^�b�N�ɐς�.
            for (int i = 0; i < ownDeckChosen.Length; i++)
            {
                battleWordQueue.Enqueue(ownDeckChosen[i]);
                battleWordQueue.Enqueue(enemyDeckChosen[i]);
            }

            gameState.Value.ChangeGamePhase(GameState.GamePhase.Battle);
        }

        /// <summary>
        /// �o�g�� ���������ׂ����[�h������.
        /// </summary>
        public void BattleTopWordDo()
        {
            Debug.Log("BattleTopWordDo");
        }

        /// <summary>
        /// �J�b�g�C��. �f�b�L�g�b�v�Ƀ��[�h���Z�b�g.
        /// </summary>
        public void BattleCutinWord(WordState cutined)
        {
            Debug.Log("BattleCutinWord");
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
