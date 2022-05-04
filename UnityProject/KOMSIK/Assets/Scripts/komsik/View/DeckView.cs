using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private WordSlotView[] wordSlotViews; //���ォ�珇�ɓ����Ă�z��.

        private WordDeck targetModel;
        private bool detailActive = false;

        public void DoSubscribe(WordDeck target)
        {
            targetModel = target;

            for(var i = 0; i < target.Slot.Length; i++)
            {
                wordSlotViews[i].DoSubscribe(target.Slot[i]);
            }
        }

        /// <summary>
        /// ���[�h�ڍו\���̃A�N�e�B�u/��A�N�e�B�u��؂�ւ���.
        /// ON�Ȃ�OFF,OFF�Ȃ�ON�ɁB
        /// </summary>
        public void WordDetailActiveTurn()
        {
            OnSetActivateWordDetail(!detailActive);
            detailActive = !detailActive;
        }

        public void OnSetActivateWordDetail(bool active)
        {
            foreach(var view in wordSlotViews)
            {
                view.SetDetailActivate(active);
            }
        }
    }
}
