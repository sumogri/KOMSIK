using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace KOMSIK
{
    public class WordProvider : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler,IDropHandler
    {
        public WordSlotView SlotView => view;

        [SerializeField] private WordSlotView view;
        [SerializeField] private TMP_Text cpNumText;

        private WordStateOrigin wordStateOrigin;
        private Vector2 prevPos;

        #region UI evenvt
        public void OnBeginDrag(PointerEventData eventData)
        {
            // ドラッグ前の位置を記憶しておく
            prevPos = transform.position;
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            // ドラッグ中は位置を更新する
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // ドラッグ前の位置に戻す
            transform.position = prevPos;
        }

        public void OnDrop(PointerEventData eventData)
        {
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            foreach (var hit in raycastResults)
            {
                // もし DroppableField の上なら、その位置に固定する
                if (hit.gameObject.CompareTag("DroppableField"))
                {
                    Debug.Log($"{hit.gameObject.name}");
                    OnDropToReciever(hit.gameObject.GetComponent<WordReciever>());
                    //transform.position = hit.gameObject.transform.position;
                    //this.enabled = false;
                }
            }
        }
        #endregion

        private void OnDropToReciever(WordReciever wordReciever)
        {
            if (wordReciever == null)
            {
                return;
            }

            wordReciever.OnDrop(wordStateOrigin);
        }

        public void SetWordOrigin(WordStateOrigin origin)
        {
            if(view.Model == null)
            {
                view.DoSubscribe(new WordState());
            }

            wordStateOrigin = origin;
            cpNumText.text = $"{origin.CustomCost}";
            view.Model.SetFromOrigin(origin);
        }
    }
}
