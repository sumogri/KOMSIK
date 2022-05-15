using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UniRx;

namespace KOMSIK
{
    public class TalkView : MonoBehaviour
    {
        [SerializeField] private GameState.GamePhase gamePhase;
        [SerializeField] private int numOfPhase; //Ptetalk[num]
        [SerializeField] private PlayableDirector timeline;

        public void Start()
        {
            DoSubscribe(GameManager.GameSystem);
        }

        public void DoSubscribe(GameSystem gameSystem)
        {
            gameSystem.GameState.OnChangeGamePhase
                .Do(x => Debug.Log(x))
                .Where(x => x == gamePhase && gameSystem.GameState.PhaseIterateTime == numOfPhase)
                .Subscribe(_ => OnChangeGamePhase())
                .AddTo(gameObject);
        }

        private void OnChangeGamePhase()
        {
            Debug.Log("DO");
            timeline.Play();
        }
    }
}
