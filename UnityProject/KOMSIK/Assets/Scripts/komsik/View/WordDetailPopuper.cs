using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace KOMSIK
{
    public class WordDetailPopuper : MonoBehaviour , IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField] private WordSlotView slotView;
        [SerializeField] private WordDetailView.Side side;

        public void OnPointerEnter(PointerEventData eventData)
        {
            WordDetailView.Instance.OpenDetail(slotView.Model,side);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            WordDetailView.Instance.CloseDetail(slotView.Model);
        }
    }
}
