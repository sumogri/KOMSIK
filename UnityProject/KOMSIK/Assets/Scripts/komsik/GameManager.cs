using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KOMSIK
{
    /// <summary>
    /// �Q�[���}�l�[�W��.
    /// 
    /// GameSystem�̊Ǘ��A���s���s��.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        static public GameSystem GameSystem { get; private set; }

        #region InGame ��UI
        [SerializeField] private DeckView ownDeck;
        [SerializeField] private DeckView enemyDeck;
        [SerializeField] private CharacterStatusView ownStatus;
        [SerializeField] private CharacterStatusView enemyStatus;
        #endregion

        private void Awake()
        {
            GameSystem = new GameSystem();
        }


        // Start is called before the first frame update
        void Start()
        {
            Init();
            GameStart(); // �f�o�b�O.�^�C�g���������Ă����Ȃ�Q�[����.
        }

        /// <summary>
        /// ������. Subscribe�����肷��.
        /// </summary>
        public void Init()
        {
            SetCharacter();
        }

        public void SetCharacter()
        {
            //�f�b�L�Z�b�g.
            ownDeck.DoSubscribe(GameSystem.OwnDeck);
            enemyDeck.DoSubscribe(GameSystem.EnemyDeck);

            ownStatus.DoSubscribe(GameSystem.OwnState);
            enemyStatus.DoSubscribe(GameSystem.EnemyState);
        }

        public void GameStart()
        {
            GameSystem.GameStart();
        }

        public void BattleStart()
        {
            GameSystem.BattleStart();

        }
    }
}
