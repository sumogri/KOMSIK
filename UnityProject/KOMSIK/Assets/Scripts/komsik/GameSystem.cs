using System;
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
        #region get Property
        public WordDeck OwnDeck => ownWordDeck;
        public WordDeck EnemyDeck => enemyWordDeck;
        public CharacterState OwnState => ownState.Value;
        public CharacterState EnemyState => enemyState.Value;
        /// <summary>
        /// 直前のBattleStartで選ばれた自分のワード
        /// </summary>
        public WordState[] OwnChosenWordStateChache => ownChosenWordStateChache;
        /// <summary>
        /// 直前のBattleStartで選ばれた敵のワード
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
        /// ゲームInit.初期状態に戻す.
        /// </summary>
        public void Init()
        {
            gameState.Value.Init(10,3);

            ///やれたら : キャラDBから初期キャラ選択.
            ownState.Value = new CharacterState(100,1,Power.Own);
            enemyState.Value = new CharacterState(9999,0,Power.Enemy);

            //敵のデック初期化.固定.
            enemyWordDeck.SetDeckFromCharacter(enemyState.Value);
            ownWordDeck.SetDeckFromCharacter(ownState.Value);

            // 勢力情報詰め直し,
            /// Systemのフィールドにしてしまったので、参照を代入. 今後なにか勢力単位で足す必要あるものはこれに.
            ownPower = new PowerState(OwnDeck, OwnState, ownChosenWordStateChache, Power.Own);
            enemeyPower = new PowerState(EnemyDeck, EnemyState, enemyChosenWordStateChache, Power.Enemy);
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
        public void SetupBattle()
        {
            Debug.Log("BattleStart");

            battleWordQueue.Clear();
            /// デッキあみだくじ
            ownChosenWordStateChache = ownWordDeck.ChoseBattleWordSet();
            ownPower.SetChosenChache(ownChosenWordStateChache); //結果をキャッシュ.
            enemyChosenWordStateChache= enemyWordDeck.ChoseBattleWordSet();
            enemeyPower.SetChosenChache(enemyChosenWordStateChache); //結果をキャッシュ.

            ///自分=>相手=>自分...と処理スタックに積む.
            for (int i = 0; i < ownChosenWordStateChache.Length; i++)
            {
                battleWordQueue.Enqueue(ownChosenWordStateChache[i]);
                battleWordQueue.Enqueue(enemyChosenWordStateChache[i]);
            }

            gameState.Value.ChangeGamePhase(GameState.GamePhase.Battle);
        }

        /// <summary>
        /// バトル 次処理すべきワードを処理.
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
        /// カットイン. デッキトップにワードをセット.
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
