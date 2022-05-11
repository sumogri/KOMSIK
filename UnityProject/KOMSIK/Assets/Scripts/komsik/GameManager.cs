using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;

namespace KOMSIK
{
    /// <summary>
    /// ゲームマネージャ.
    /// 
    /// GameSystemの管理、実行を行う.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        static public GameSystem GameSystem { get; private set; }

        #region InGame のUI
        [SerializeField] private DeckView ownDeck;
        [SerializeField] private DeckView enemyDeck;
        [SerializeField] private CharacterStatusView ownStatus;
        [SerializeField] private CharacterStatusView enemyStatus;
        #endregion

        private void Awake()
        {
            GameSystem = new GameSystem();
            GameSystem.OnSetupBattle
                .Subscribe(x => BattleRun())
                .AddTo(gameObject);
        }


        // Start is called before the first frame update
        void Start()
        {
            Init();
            //GameStart(); // デバッグ.タイトル無視していきなりゲームへ.
        }

        /// <summary>
        /// 初期化. Subscribeしたりする.
        /// </summary>
        public void Init()
        {
            SetCharacter();
        }

        public void SetCharacter()
        {
            //デッキセット.
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
            GameSystem.SetupBattle();
        }

        public void BattleWordDo()
        {
            GameSystem.BattleTopWordDo();
        }

        public void BattleRun()
        {
            _ = GameSystem.BattleRun();
        }
    }
}
