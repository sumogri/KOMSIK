using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;

namespace KOMSIK
{
    public class GameArieaView : MonoBehaviour
    {
        [SerializeField] private Button battleStartButton;

        private GameSystem targetModel;

        public void DoSubscribe(GameSystem target)
        {
            targetModel = target;

            battleStartButton.onClick
                .AddListener(OnPushBattleButton);
        }

        // Start is called before the first frame update
        void Start()
        {
            DoSubscribe(GameManager.GameSystem);
        }

        public void OnPushBattleButton()
        {
            targetModel.BattleStart();
        }
    }
}
