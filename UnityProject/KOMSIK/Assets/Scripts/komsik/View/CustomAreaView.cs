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
        private bool detailActive = true;

        public void Start()
        {
            DoSubscribe(GameManager.GameSystem);
        }

        public void DoSubscribe(GameSystem system)
        {
            system.OwnState.OnChangeCustomDeckID
                .Subscribe(_ => SetStateByCharacter(system.OwnState))
                .AddTo(gameObject);
        }

        public void SetStateByCharacter(CharacterState characterState)
        {
            var cd = WordPool.CustomDecks[characterState.CustomDeckID];
            Debug.Log(cd.Length);
            for(var i = 0; i < cd.Length; i++)
            {
                Debug.Log(cd[i].Word);
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
