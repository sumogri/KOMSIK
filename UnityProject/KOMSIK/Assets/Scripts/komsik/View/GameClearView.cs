using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace KOMSIK
{
    public class GameClearView : MonoBehaviour
    {
        [SerializeField] private GameObject normalEndcontentRoot;
        [SerializeField] private GameObject goodEndContentRoot;
        [SerializeField] private Button cotinueButton;

        private bool requireContentDisactivate = false;

        // Start is called before the first frame update
        void Start()
        {
            //DoSubscribe(GameManager.GameSystem);
            cotinueButton.onClick.AddListener(() => OnPressCotinue());
        }

        private void Update()
        {
            if (requireContentDisactivate)
            {
                Disable();
            }
        }

        private void Disable()
        {
            normalEndcontentRoot.SetActive(false);
            goodEndContentRoot.SetActive(false);
            requireContentDisactivate = false;
        }

        public void DoSubscribe(GameSystem gameSystem)
        {
            gameSystem.GameState.OnChangeGameSection
                .Where(x => x != GameState.Section.GoodEnd)
                .Subscribe(_ => OnInit())
                .AddTo(gameObject);

            gameSystem.GameState.OnChangeGameSection
                .Where(x => x == GameState.Section.GoodEnd)
                .Subscribe(_ => OnGameClearGood())
                .AddTo(gameObject);

            gameSystem.GameState.OnChangeGameSection
                .Where(x => x == GameState.Section.NormalEnd)
                .Subscribe(_ => OnGameClearNormal())
                .AddTo(gameObject);
        }

        public void OnGameClearGood()
        {
            goodEndContentRoot.SetActive(true);
            AudioView.Instance.PlayBGM(AudioView.BGM.Ending);
        }
        public void OnGameClearNormal()
        {
            normalEndcontentRoot.SetActive(true);
        }

        public void OnInit()
        {
            requireContentDisactivate = true;
        }

        public void OnPressCotinue()
        {
            GameManager.GameSystem.Continue();
            AudioView.Instance.PlayBGM(AudioView.BGM.Title);

            goodEndContentRoot.SetActive(false);
            normalEndcontentRoot.SetActive(false);
        }
    }
}
