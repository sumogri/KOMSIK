using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace KOMSIK
{
    /// <summary>
    /// ロジック
    /// 
    /// ゲーム開始 
    /// => (キャラ選 => バトル => 結果処理)*4 
    /// => エンディング
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
        /// ゲームInit.初期状態に戻す.
        /// </summary>
        public void Init()
        {
            gameState.Value.Init(10,3);

            ///やれたら : キャラDBから初期キャラ選択.
            ownState.Value = new CharacterState(100,1);
            enemyState.Value = new CharacterState(9999,0);

            //敵のデック初期化.固定.
            enemyWordDeck.SetDeckFromCharacter(enemyState.Value);
            ownWordDeck.SetDeckFromCharacter(ownState.Value);
        }

        /// <summary>
        /// キャラ選
        /// </summary>
        /// <param name="ownState"></param>
        public void SelectOwnCharacter(CharacterState ownState)
        {
            this.ownState.Value = ownState;
            ownWordDeck.SetDeckFromCharacter(ownState);
        }

        /// <summary>
        /// 今選んだキャラで確定.
        /// </summary>
        public void SubmitSelectCharacter()
        {

        }

        /// <summary>
        /// ゲーム開始.
        /// </summary>
        public void GameStart()
        {
            gameState.Value.ChangeSection(GameState.Section.Game);
            gameState.Value.ChangeGamePhase(GameState.GamePhase.PreTalk);
        }

        /// <summary>
        /// 勝負開始.
        /// </summary>
        public void BattleStart()
        {
            Debug.Log("BattleStart");

            battleWordQueue.Clear();
            /// デッキあみだくじ
            var ownDeckChosen = ownWordDeck.ChoseBattleWordSet();
            var enemyDeckChosen = enemyWordDeck.ChoseBattleWordSet();

            ///自分=>相手=>自分...と処理スタックに積む.
            for (int i = 0; i < ownDeckChosen.Length; i++)
            {
                battleWordQueue.Enqueue(ownDeckChosen[i]);
                battleWordQueue.Enqueue(enemyDeckChosen[i]);
            }

            gameState.Value.ChangeGamePhase(GameState.GamePhase.Battle);
        }

        /// <summary>
        /// バトル 次処理すべきワードを処理.
        /// </summary>
        public void BattleTopWordDo()
        {
            Debug.Log("BattleTopWordDo");
        }

        /// <summary>
        /// カットイン. デッキトップにワードをセット.
        /// </summary>
        public void BattleCutinWord(WordState cutined)
        {
            Debug.Log("BattleCutinWord");
        }

        /// <summary>
        /// カスタム開始.
        /// </summary>
        public void CustomStart()
        {
            Debug.Log("CustomStart");

            gameState.Value.ChangeGamePhase(GameState.GamePhase.Custom);
        }

        /// <summary>
        /// 戦闘終了.
        /// </summary>
        public void BattleEnd()
        {
            /// 自分のHPが0以下なら、死んでるので負け.
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
