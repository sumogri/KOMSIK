using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace KOMSIK
{
    public class DebugSectionVeiw : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        private void Update()
        {
            text.text = $"{GameManager.GameSystem.GameState.NowSection}/{GameManager.GameSystem.GameState.NowGamePhase}\niteCout:{GameManager.GameSystem.GameState.PhaseIterateTime}";
        }
    }
}
