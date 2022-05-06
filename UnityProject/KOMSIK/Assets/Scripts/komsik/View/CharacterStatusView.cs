using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

namespace KOMSIK
{
    public class CharacterStatusView : MonoBehaviour
    {
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private TMP_Text defText;

        private CharacterState targetModel;

        public void DoSubscribe(CharacterState target)
        {
            targetModel = target;

            targetModel.OnChangeHP
                .Subscribe(OnChangeHP)
                .AddTo(gameObject);

            targetModel.OnChangeDiffence
                .Subscribe(OnChangeDef)
                .AddTo(gameObject);
        }

        private void OnChangeHP(int changeTo)
        {
            // •‰‚É‚Í‚µ‚È‚¢.
            if (changeTo < 0)
            {
                changeTo = 0;
            }

            hpText.text = $"{changeTo}";
        }

        private void OnChangeDef(int changeTo)
        {
            // •‰‚É‚Í‚µ‚È‚¢.
            if (changeTo < 0)
            {
                changeTo = 0;
            }

            defText.text = $"{changeTo}";
        }
    }
}
