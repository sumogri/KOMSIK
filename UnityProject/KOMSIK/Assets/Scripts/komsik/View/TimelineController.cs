using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace KOMSIK
{
    public class TimelineController : MonoBehaviour
    {
        [SerializeField] private GameObject[] playOnlyObjects;
        [SerializeField] private AudioView.BGM playBGM;
        private PlayableDirector playableDirector;

        public void Start()
        {
            playableDirector = gameObject.GetComponent<PlayableDirector>();
        }

        public void ChangeToTitleSection()
        {
            ChangeGameSection(GameState.Section.Title);
        }

        public void ChangeToCustomSection()
        {
            GameManager.GameSystem.CustomStart();
        }

        private void ChangeGameSection(GameState.Section section)
        {
            var gameSystem = GameManager.GameSystem;

            gameSystem.GameState.ChangeSection(section);
        }
        
        public void BGMPlaySky()
        {
            AudioView.Instance.PlayBGM(AudioView.BGM.Sky);
        }
        public void BGMPlaySea()
        {
            AudioView.Instance.PlayBGM(AudioView.BGM.Sea);
        }
        public void BGMPlayYusha()
        {
            AudioView.Instance.PlayBGM(AudioView.BGM.Title);
        }

        public void SkipPreMovie()
        {
            playableDirector.Stop();
            ChangeToTitleSection();
            foreach(var p in playOnlyObjects)
            {
                p.SetActive(false);
            }
        }

        public void SkipAftTalk()
        {
            playableDirector.Stop();
            ChangeToCustomSection();
            foreach (var p in playOnlyObjects)
            {
                p.SetActive(false);
            }
            AudioView.Instance.PlayBGM(playBGM);
        }
    }
}
