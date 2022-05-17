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

        private readonly string openAnimatorTrigger = "Open";

        // Start is called before the first frame update
        void Start()
        {
            DoSubscribe(GameManager.GameSystem);
        }

        private void DoSubscribe(GameSystem gameSystem)
        {
            gameSystem.GameState.OnNowTurnValueChange
                .Subscribe(x => _ = Open(gameSystem.GameState.LastTurn))
                .AddTo(gameObject);

            gameSystem.GameState.OnChangeGamePhase
                .Where(x => x == GameState.GamePhase.Custom)
                .Subscribe(x => _ = Open(gameSystem.GameState.LastTurn))
                .AddTo(gameObject);
        }

        private async UniTask Open(int lastTurn)
        {
            turnText.text = $"{lastTurn}";
            animator.SetTrigger(openAnimatorTrigger);
        }
    }
}
