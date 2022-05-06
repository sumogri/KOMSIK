using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

namespace KOMSIK
{
    public class CPView : MonoBehaviour
    {
        [SerializeField] private TMP_Text cpText;

        // Start is called before the first frame update
        void Start()
        {
            GameManager.GameSystem.GameState.OnCustomPointChange
                .Subscribe(OnChangeCP)
                .AddTo(gameObject);
        }

        private void OnChangeCP(int cp)
        {
            cpText.text = $"{cp}";
        }
    }
}
