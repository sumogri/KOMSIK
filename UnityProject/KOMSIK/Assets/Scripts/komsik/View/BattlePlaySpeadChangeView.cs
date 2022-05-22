using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace KOMSIK
{
    public class BattlePlaySpeadChangeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        private Button button;

        // Start is called before the first frame update
        void Start()
        {
            button = gameObject.GetComponent<Button>();
            button.onClick.AddListener(() => SpeedChange());
        }

        private void SpeedChange() 
        {
            var gameSystem = GameManager.GameSystem;

            var speed = gameSystem.NowPlaySpeed;

            if (speed == GameSystem.PlaySpeed.Quick)
            {
                speed = GameSystem.PlaySpeed.Normal;
            }
            else
            {
                speed++;
            }

            gameSystem.SetPlaySpeed(speed);

            // ���x�ς�������Ƃɂ��A�\���ύX.
            var nt = "�o�g�����x�ύX[";
            for(int i = 0; i <= (int)speed; i++)
            {
                nt += ">";
            }
            nt += "]";
            text.text = nt;
        }

    }
}
