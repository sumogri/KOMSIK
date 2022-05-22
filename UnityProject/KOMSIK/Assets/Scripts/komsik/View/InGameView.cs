using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;

namespace KOMSIK
{
    public class InGameView : MonoBehaviour
    {
        [SerializeField] private GameObject uiRoot;
        [SerializeField] private GameObject stageRoot;
        [SerializeField] private GameObject cameraRoot;

        [SerializeField] private GameObject[] ownCharaRoots;
        [SerializeField] private GameObject enemyCharaRoot;

        [SerializeField] private Button resetButton;


        // Start is called before the first frame update
        void Start()
        {
            DoSubscribe(GameManager.GameSystem);
        }

        public void DoSubscribe(GameSystem gameSystem)
        {
            gameSystem.GameState.OnChangeGamePhase
                .Pairwise()
                .Where(x => x.Previous == GameState.GamePhase.PreTalk || x.Previous == GameState.GamePhase.AftTalkBad)
                .Subscribe(_ => OnBattleStart(gameSystem.GameState.PhaseIterateTime))
                .AddTo(gameObject);

            gameSystem.GameState.OnChangeGamePhase
                .Where(x => x == GameState.GamePhase.AftTalkBad || x == GameState.GamePhase.AftTalkGood)
                .Subscribe(x => _ = OnBattleEnd())
                .AddTo(gameObject);

            resetButton.onClick.AddListener(() =>
            {
                if(gameSystem.GameState.NowGamePhase == GameState.GamePhase.Battle)
                {
                    return; //バトル中はスキップ無視.
                }

                gameSystem.Continue();
                AudioView.Instance.PlayBGM(AudioView.BGM.Title);
            });

            gameSystem.OnInit
                .Subscribe(x =>
                {
                    cameraRoot.SetActive(false);
                    uiRoot.SetActive(false);
                    stageRoot.SetActive(false);
                })
                .AddTo(gameObject);
        }

        private void OnBattleStart(int iterateNum)
        {
            if (iterateNum >= ownCharaRoots.Length)
            {
                Debug.LogWarning($"OnBattleStart iterate num Over {iterateNum} >= {ownCharaRoots.Length}");
                return;
            }

            uiRoot.SetActive(true);
            stageRoot.SetActive(true);
            cameraRoot.SetActive(true);

            foreach (var o in ownCharaRoots)
            {
                o.SetActive(false);
            }
            ownCharaRoots[iterateNum].SetActive(true);

            enemyCharaRoot.SetActive(true);
        }

        private async UniTask OnBattleEnd()
        {
            cameraRoot.SetActive(false);
            uiRoot.SetActive(false);
            AudioView.Instance.StopBGM();

            await UniTask.Delay(1000);

            stageRoot.SetActive(false);
        }
    }
}
