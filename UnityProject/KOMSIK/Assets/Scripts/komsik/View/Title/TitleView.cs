using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace KOMSIK
{
    public class TitleView : MonoBehaviour
    {
        [SerializeField] private GameObject[] objRoots;
        [SerializeField] private Button startButton;
        [SerializeField] private Button openTutorialButton;
        [SerializeField] private GameObject tutorialRoot;

        // Start is called before the first frame update
        void Start()
        {
            DoSubscribe(GameManager.GameSystem);
            //デバッグ.
            GameManager.GameSystem.GameState.ChangeSection(GameState.Section.Title);
        }
        
        public void DoSubscribe(GameSystem gameSystem)
        {
            gameSystem.GameState.OnChangeGameSection
                .Where(x => x == GameState.Section.Title)
                .Subscribe(_ => OnEnterTitle())
                .AddTo(gameObject);

            gameSystem.GameState.OnChangeGameSection
                .Pairwise()
                .Where(x => x.Previous == GameState.Section.Title)
                .Subscribe(_ => OnExitTitle())
                .AddTo(gameObject);

            startButton.onClick.AddListener(() => OnPressStartButton());
            openTutorialButton.onClick.AddListener(() => OnPressOpenTutorialButton());
        }

        private void OnEnterTitle()
        {
            foreach(var objRoot in objRoots)
            {
                objRoot.SetActive(true);
            }
            AudioView.Instance.PlayBGM(AudioView.BGM.Title);
        }

        private void OnExitTitle()
        {
            foreach (var objRoot in objRoots)
            {
                objRoot.SetActive(false);
            }
        }

        private void OnPressStartButton()
        {
            GameManager.GameSystem.GameStart();
        }

        private void OnPressOpenTutorialButton()
        {
            tutorialRoot.SetActive(true);
        }
    }
}
