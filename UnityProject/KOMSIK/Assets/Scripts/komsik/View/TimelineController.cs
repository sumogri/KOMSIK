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

        private void ChangeGameSection(GameState.Section section)
        {
            var gameSystem = GameManager.GameSystem;

            gameSystem.GameState.ChangeSection(section);
        }
    }
}
