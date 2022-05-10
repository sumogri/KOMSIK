using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KOMSIK
{
    /// <summary>
    /// �J�X�^����ʂŒP����͂߂���悤WordSlot���g��
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

            // �f�b�L�J�X�^���̃��W�b�N�Ăяo��.
            GameManager.GameSystem.CustomSetWordOrigin(wordSlot.Model, wordStateOrigin);
        }

        /// <summary>
        /// ����Reciever���󗝂��邩.
        /// </summary>
        public void SetAcseptActive(bool active)
        {
            isAcseptActive = active;
        }
    }
}
