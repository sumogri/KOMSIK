using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;

namespace KOMSIK
{
    public class WordDetailView : MonoBehaviour
    {
        public static WordDetailView Instance { get; private set; }

        [SerializeField] private GameObject contentRoot;
        [SerializeField] private TMP_Text detailText;

        private RectTransform ownRectTransform;
        private Side side = Side.none;
        private bool opened = false;
        private WordState state = null;

        private void Awake()
        {
            Instance = this;
            ownRectTransform = gameObject.GetComponent<RectTransform>();

            contentRoot.SetActive(false);
        }

        public void Update()
        {
            if (opened)
            {
                if (Input.GetMouseButton(0))
                {
                    contentRoot.SetActive(false);
                }
                else
                {
                    contentRoot.SetActive(true);
                }
            }

            var nextPosition = Input.mousePosition;
            nextPosition.y += (ownRectTransform.rect.height + 40) / 2;
            
            switch (side)
            {
                case Side.right:
                    nextPosition.x += ownRectTransform.rect.width / 2;
                    break;
                case Side.left:
                    nextPosition.x -= ownRectTransform.rect.width / 2;
                    break;
            }

            ownRectTransform.position = nextPosition;
        }

        public enum Side
        {
            none,
            right,
            left
        }

        public void OpenDetail(WordState wordState,Side side)
        {
            state = wordState;

            var detail = wordState.GetDetailStr();
            detailText.text = detail;
            this.side = side;

            if (detail.Length <= 0)
            {
                opened = false;
            }
            else
            {
                opened = true;
                contentRoot.SetActive(true);
            }
        }

        public void CloseDetail(WordState wordState)
        {
            if(wordState != state)
            {
                return;
            }

            opened = false;
            contentRoot.SetActive(false);
            side = Side.none;
        }
    }
}
