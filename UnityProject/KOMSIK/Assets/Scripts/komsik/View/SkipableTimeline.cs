using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace KOMSIK
{
    [RequireComponent( typeof(PlayableDirector) )]
    public class SkipableTimeline : MonoBehaviour
    {
        private static List<SkipableTimeline> nowPlayTimelines;

        private PlayableDirector timeline;

        // Start is called before the first frame update
        void Start()
        {
            timeline = gameObject.GetComponent<PlayableDirector>();
        }

        private void Skip()
        {

        }

        public static void NowPlayingSkip()
        {
            foreach(var s in nowPlayTimelines)
            {
                s.Skip();
            }
        }
    }
}
