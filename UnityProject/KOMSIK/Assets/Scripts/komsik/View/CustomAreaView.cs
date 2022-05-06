using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

namespace KOMSIK
{
    public class CustomAreaView : MonoBehaviour
    {
        [SerializeField] private WordProvider[] wordProvider;
        private bool detailActive = false;

        public void SetStateByCharacter(CharacterState characterState)
        {
            var cd = WordPool.CustomDecks[characterState.CustomDeckID];
            for(var i = 0; i < cd.Length; i++)
            {
                wordProvider[i].SetWordOrigin(cd[i]);
            }
        }

        public void WordDetailActiveTurn()
        {
            detailActive = !detailActive;
            foreach (var v in wordProvider)
            {
                v.SlotView.SetDetailActivate(detailActive);
            }
        }
    }
}
