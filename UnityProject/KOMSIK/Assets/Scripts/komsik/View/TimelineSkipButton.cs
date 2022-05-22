using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KOMSIK
{
    public class TimelineSkipButton : MonoBehaviour
    {
        private Button button;

        // Start is called before the first frame update
        void Start()
        {
            button = gameObject.GetComponent<Button>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void TimelineSkip()
        {
            // いま再生してるタイムラインとってきて、Skipする
        }
    }
}
