using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public class TimelineController : MonoBehaviour
    {
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
    }
}
