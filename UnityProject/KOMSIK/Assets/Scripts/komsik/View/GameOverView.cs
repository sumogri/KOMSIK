using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace KOMSIK
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private GameObject contentRoot;
        [SerializeField] private Button cotinueButton;

        private bool requireContentDisactivate = false;

        // Start is called before the first frame update
        void Start()
        {
            DoSubscribe(GameManager.GameSystem);
            cotinueButton.onClick.AddListener(() => OnPressCotinue());
        }

        private void Update()
        {
            if (requireContentDisactivate)
            {
                contentRoot.SetActive(false);
                requireContentDisactivate = false;
            }
        }

        public void DoSubscribe(GameSystem gameSystem)
        {
            //‚â‚ß‚Ä‚à‚¢‚¢‚©?

            gameSystem.GameState.OnChangeGameSection
                .Do(x => Debug.Log($"Here {x}"))
                .Where(x => x != GameState.Section.BadEnd)
                .Subscribe(_ => OnInit())
                .AddTo(gameObject);

            gameSystem.GameState.OnChangeGameSection
                .Do(x => Debug.Log($"DODODO{x}"))
                .Where(x => x == GameState.Section.BadEnd)
                .Subscribe(_ => OnGameOver())
                .AddTo(gameObject);
        }

        public void OnGameOver()
        {
            contentRoot.SetActive(true);
        }

        public void OnInit()
        {
            requireContentDisactivate = true;
        }

        public void OnPressCotinue()
        {
            GameManager.GameSystem.Continue();
        }
    }
}
