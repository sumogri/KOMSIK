using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

namespace KOMSIK
{
    public class PreMovieSkipButton : MonoBehaviour
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
            button.onClick.AddListener(() => PreMovieSkip());
        }

        private void PreMovieSkip()
        {
            timelineController.SkipPreMovie();
            button.gameObject.SetActive(false);
        }

    }
}
