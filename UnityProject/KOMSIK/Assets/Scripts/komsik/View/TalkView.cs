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
        [SerializeField] private bool isUseSection = false;
        [SerializeField] private GameState.Section gameSection;

        public void Start()
        {
            DoSubscribe(GameManager.GameSystem);
        }

        public void DoSubscribe(GameSystem gameSystem)
        {
            if (isUseSection)
            {
                gameSystem.GameState.OnChangeGameSection
                    .Where(x => x == gameSection)
                    .Subscribe(_ => timeline.Play())
                    .AddTo(gameObject);
            }
            else
            {
                gameSystem.GameState.OnChangeGamePhase
                    .Do(x => Debug.Log($"{x} == {gamePhase} && {gameSystem.GameState.PhaseIterateTime} == {numOfPhase}"))
                    .Where(x => x == gamePhase && gameSystem.GameState.PhaseIterateTime == numOfPhase)
                    .Subscribe(_ => OnChangeGamePhase())
                    .AddTo(gameObject);
            }
        }

        private void OnChangeGamePhase()
        {
            Debug.Log($"{gameObject.name}");
            timeline.Play();
        }
    }
}
