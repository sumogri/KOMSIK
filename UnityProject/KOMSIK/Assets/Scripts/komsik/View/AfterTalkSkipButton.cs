using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

namespace KOMSIK
{
    public class AfterTalkSkipButton : MonoBehaviour
    {

        [SerializeField] private Button button;
        [SerializeField] private TimelineController timelineController;

        // Start is called before the first frame update
        void Start()
        {
            DoSubscribe();
        }

        private void DoSubscribe()
        {
            button.onClick.AddListener(() => MovieSkip());
        }

        private void MovieSkip()
        {
            timelineController.SkipAftTalk();
            button.gameObject.SetActive(false);
        }

    }

}
