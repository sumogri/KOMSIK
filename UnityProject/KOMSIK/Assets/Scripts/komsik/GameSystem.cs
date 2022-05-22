using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

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
            ///やれたら : キャラDBから初期キャラ選択.
            ownState.Value = new CharacterState(CharacterOrigin.GetOrigin(CharacterOrigin.CharacterID.Gran));
            enemyState.Value = new CharacterState(CharacterOrigin.GetOrigin(CharacterOrigin.CharacterID.Maou));

            // 勢力情報詰め直し,
            /// Systemのフィールドにしてしまったので、参照を代入. 今後なにか勢力単位で足す必要あるものはこれに.
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
        /// ゲームInit.初期状態に戻す.
        /// </summary>
        public void Init()
        {
            Debug.Log("Call Init");
            gameState.Value.Init(10, 13);

            /// 味方初期化.
            SelectOwnCharacter(CharacterOrigin.GetOrigin(CharacterOrigin.CharacterID.Gran));

            //敵の初期化.
            enemyState.Value.InitFromOring(CharacterOrigin.GetOrigin(CharacterOrigin.CharacterID.Maou));
            enemyWordDeck.SetDeckFromCharacter(enemyState.Value);
            onInitSubject.OnNext(Unit.Default);
        }

        public void Continue()
        {
            gameState.Value.Init(10, 13);

            /// 味方初期化.
            SelectOwnCharacter(CharacterOrigin.GetOrigin(CharacterOrigin.CharacterID.Gran));

            //敵の初期化.
            enemyState.Value.InitFromOring(CharacterOrigin.GetOrigin(CharacterOrigin.CharacterID.Maou));
            enemyWordDeck.SetDeckFromCharacter(enemyState.Value);
            onInitSubject.OnNext(Unit.Default);

            GameStart();
        }

        /// <summary>
        /// キャラ選
        /// </summary>
        /// <param name="ownState"></param>
        public void SelectOwnCharacter(CharacterOrigin origin)
        {
            ownState.Value.InitFromOring(origin);
            ownWordDeck.SetDeckFromCharacter(ownState.Value);
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
            //CustomStart(); //仮.イベント終わりで呼ぶ.
        }

        /// <summary>
        /// 勝負開始.
        /// </summary>
        public void SetupBattle()
        {
            Debug.Log("BattleStart");

            battleWordStack.Clear();
            /// デッキあみだくじ
            ownChosenWordStateChache = ownWordDeck.ChoseBattleWordSet();
            ownPower.SetChosenChache(ownChosenWordStateChache); //結果をキャッシュ.
            enemyChosenWordStateChache= enemyWordDeck.ChoseBattleWordSet();
            enemeyPower.SetChosenChache(enemyChosenWordStateChache); //結果をキャッシュ.

            ///相手=>自分=>相手=>...と処理スタックに積む.
            for (int i = ownChosenWordStateChache.Length - 1; i >= 0; i--)
            {
                battleWordStack.Push(enemyChosenWordStateChache[i]);
                battleWordStack.Push(ownChosenWordStateChache[i]);
            }

            gameState.Value.ChangeGamePhase(GameState.GamePhase.Battle);
            onSetupBattleSubject.OnNext(Unit.Default);
        }

        /// <summary>
        /// バトル 次処理すべきワードを処理.
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
            // てきとう.演出待ち
            await UniTask.Delay(500);

            while (BattleTopWordDo())
            {
                // てきとう.演出待ち.
                await UniTask.Delay(500);

                //ゲームオーバー判定.
                if (CheckAndDoBattleEnd())
                {
                    // ゲームオーバーしてたらバトル処理打ち切り.
                    return;
                }
            }

            //ひととおりカード消費したのでターン終了.
            TurnEnd();

            //カスタムに移動.
            CustomStart();
        }

        /// <summary>
        /// ゲームオーバー/クリア判定.
        /// </summary>
        /// <returns>終了条件を満たしたか</returns>
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
        /// カットイン. デッキトップにワードをセット.
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
        /// 負けてフェーズを切り替える場合.
        /// 
        /// 次のキャラに交代.
        /// </summary>
        public void PhaseChangeToDefeate()
        {
            gameState.Value.ChangeGamePhase(GameState.GamePhase.AftTalkBad);
            gameState.Value.OnPhaseEnd();

            // 4回まではキャラチェンで対応
            if(GameState.PhaseIterateTime < 4)
            {
                var nextCharaID = CharacterOrigin.CharacterID.Gran + GameState.PhaseIterateTime;
                SelectOwnCharacter(CharacterOrigin.GetOrigin(nextCharaID));
                TurnEnd();

                //CustomStart(); //仮.演出待つ.
            }
            else
            {
                gameState.Value.ChangeSection(GameState.Section.BadEnd); //仮.演出終わりに入れる.
            }
        }

        /// <summary>
        /// デッキにWordOriginを刺す.
        /// </summary>
        /// <param name="wordState"></param>
        /// <param name="wordStateOrigin"></param>
        /// <returns>成功ならtrue</returns>
        public bool CustomSetWordOrigin(WordState wordState, WordStateOrigin wordStateOrigin)
        {
            var cost = wordStateOrigin.CustomCost;
            if(GameState.CustomPoint < cost)
            {
                Debug.Log($"CP Over cp:{GameState.CustomPoint},cost:{cost}");
                return false;
            }

            // originをwordにset,Cost支払い.
            wordState.SetFromOrigin(wordStateOrigin);
            GameState.PaidCustomPoint(cost);
            return true;
        }
    }
}
