using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private WordSlotView[] wordSlotViews; //左上から順に入ってる想定.

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
        /// ワード詳細表示のアクティブ/非アクティブを切り替える.
        /// ONならOFF,OFFならONに。
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
