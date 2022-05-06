using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KOMSIK
{
    public class WordReciever : MonoBehaviour
    {
        [SerializeField] private WordSlotView wordSlot;

        public void Start()
        {
            wordSlot = gameObject.GetComponent<WordSlotView>();
        }

        public void OnDrop(WordStateOrigin wordStateOrigin)
        {
            // デッキカスタムのロジック呼び出し.
            GameManager.GameSystem.CustomSetWordOrigin(wordSlot.Model, wordStateOrigin);
        }
    }
}
