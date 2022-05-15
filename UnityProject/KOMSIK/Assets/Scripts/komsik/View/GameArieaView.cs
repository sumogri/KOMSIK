using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using TMPro;
using DG.Tweening;

namespace KOMSIK
{
    public class GameArieaView : MonoBehaviour
    {
        [SerializeField] private Button battleStartButton;
        [SerializeField] private DeckView ownDeck;
        [SerializeField] private DeckView enemyDeck;

        [SerializeField] private Vector3 customPosition;
        [SerializeField] private Vector3 battlePosition;

        private GameSystem targetModel;
        private bool doingBattle = false;

        public void DoSubscribe(GameSystem target)
        {
            targetModel = target;

            battleStartButton.onClick
                .AddListener(() => OnPushBattleButton());

            target.GameState.OnChangeGamePhase
                .Where(x => x == GameState.GamePhase.Battle)
                .Subscribe(x => OnGamePhaseStart())
                .AddTo(gameObject);

            target.GameState.OnChangeGamePhase
                .Where(x => x == GameState.GamePhase.Custom)
                .Subscribe(x => _ = OnCustomPhaseStart())
                .AddTo(gameObject);
        }

        private async UniTask OnCustomPhaseStart()
        {
            await UniTask.Delay(500);

            transform.DOLocalMove(customPosition, 0.5f);
            doingBattle = false;
        }

        private void OnGamePhaseStart()
        {
            transform.DOLocalMove(battlePosition,0.5f);
        }

        // Start is called before the first frame update
        void Start()
        {
            _ = OnCustomPhaseStart();
            DoSubscribe(GameManager.GameSystem);
        }

        public void OnPushBattleButton()
        {
            if (doingBattle)
            {
                return;
            }
            doingBattle = true;

            targetModel.SetupBattle();
        }
    }
}
