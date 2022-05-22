using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using TMPro;

namespace KOMSIK
{
    public class TurnView : MonoBehaviour
    {
        [SerializeField] private TMP_Text turnText;
        [SerializeField] private Animator animator;
        [SerializeField] private Animator animatorSaber;
        [SerializeField] private WordProvider wordProvider;
        [SerializeField] private GameObject wordRoot;
        [SerializeField] private GameObject turnRootObj;

        private readonly string openAnimatorTrigger = "Open";

        // Start is called before the first frame update
        void Start()
        {
            DoSubscribe(GameManager.GameSystem);

            wordProvider.SetWordOrigin(WordPool.HolySaber);
        }

        private void DoSubscribe(GameSystem gameSystem)
        {
            gameSystem.GameState.OnNowTurnValueChange
                .Subscribe(x => Open(gameSystem.GameState.LastTurn))
                .AddTo(gameObject);

            gameSystem.GameState.OnChangeGamePhase
                .Where(x => x == GameState.GamePhase.Custom)
                .Subscribe(x => Open(gameSystem.GameState.LastTurn))
                .AddTo(gameObject);

            gameSystem.OnInit
                .Subscribe(x => Init())
                .AddTo(gameObject);
        }

        private void Init()
        {
//            wordRoot.SetActive(false); // 聖剣開放.
//            turnRootObj.SetActive(true); //ターン表示はおしまい.
        }

        private void Open(int lastTurn)
        {
            if(lastTurn <= 0)
            {
                wordRoot.SetActive(true); // 聖剣開放.
                turnRootObj.SetActive(false); //ターン表示はおしまい.

                animatorSaber.SetTrigger(openAnimatorTrigger);
            }
            else
            {
                wordRoot.SetActive(false); // 聖剣開放.
                turnRootObj.SetActive(true); //ターン表示はおしまい.

                turnText.text = $"{lastTurn}";
                animator.SetTrigger(openAnimatorTrigger);
            }

        }
    }
}
