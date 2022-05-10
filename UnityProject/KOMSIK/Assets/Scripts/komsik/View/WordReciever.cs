using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KOMSIK
{
    /// <summary>
    /// カスタム画面で単語をはめられるようWordSlotを拡張
    /// </summary>
    public class WordReciever : MonoBehaviour
    {
        [SerializeField] private WordSlotView wordSlot;
        private bool isAcseptActive = true;

        public void Start()
        {
            wordSlot = gameObject.GetComponent<WordSlotView>();
        }

        public void OnDrop(WordStateOrigin wordStateOrigin)
        {
            if (!isAcseptActive)
            {
                return;
            }

            // デッキカスタムのロジック呼び出し.
            GameManager.GameSystem.CustomSetWordOrigin(wordSlot.Model, wordStateOrigin);
        }

        /// <summary>
        /// このRecieverが受理するか.
        /// </summary>
        public void SetAcseptActive(bool active)
        {
            isAcseptActive = active;
        }
    }
}
