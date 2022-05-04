using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

namespace KOMSIK
{
    public class WordSlotView : MonoBehaviour
    {
        [SerializeField] private GameObject detail1ViewRoot;
        [SerializeField] private TMP_Text word;
        [SerializeField] private TMP_Text hp;
        [SerializeField] private GameObject hpIcon;
        [SerializeField] private TMP_Text attackAndDeffence;
        [SerializeField] private GameObject attackIcon;
        [SerializeField] private GameObject deffenceIcon;
        [SerializeField] private GameObject highlightRoot;

        private WordState targetModel;

        // Start is called before the first frame update
        void Start()
        {
            SetDetailActivate(false);
        }

        public void DoSubscribe(WordState wordState)
        {
            targetModel = wordState;

            wordState.OnChangeHP
                .Subscribe(OnChangeHP)
                .AddTo(gameObject);

            wordState.OnChangeAttack
                .Subscribe(x => OnChangeAttackOrDeffence(x, -1))
                .AddTo(gameObject);

            wordState.OnChangeDeffence
                .Subscribe(x => OnChangeAttackOrDeffence(-1, x))
                .AddTo(gameObject);

            wordState.OnChangeWord
                .Subscribe(OnChangeWord)
                .AddTo(gameObject);
        }

        private void OnChangeHP(int changeTo)
        {
            this.hp.text = $"<color=red>{changeTo}</color>";
            hpIcon.SetActive(true);
        }

        private void OnChangeAttackOrDeffence(int changeAtk,int changeDef)
        {
            var atk = targetModel.Attack;
            var attackVissible = false;
            var def = targetModel.Deffence;
            var deffenceVissible = false;

            // 攻撃防御のアクティブ判定.
            /// 0以下なら表示しない
            if (atk > 0)
            {
                attackVissible = true;
            }
            if(def > 0)
            {
                deffenceVissible = true;
            }

            //テキスト作成.攻撃/防御.
            var text = "";
            if(attackVissible && deffenceVissible)
            {
                text = $"<color=red >{atk}</color>/<color=blue>{def}</color>";
            }
            else if (attackVissible)
            {
                text = $"<color=red>{atk}</color>";
            }
            else if (deffenceVissible)
            {
                text = $"<color=blue>{def}</color >";
            }

            //ui適用
            attackAndDeffence.text = text;
            attackIcon.SetActive(attackVissible);
            deffenceIcon.SetActive(deffenceVissible);
        }

        private void OnChangeWord(string changeTo)
        {
            word.text = changeTo;
        }

        public void SetDetailActivate(bool activate)
        {
            detail1ViewRoot.SetActive(activate);
        }
    }
}
