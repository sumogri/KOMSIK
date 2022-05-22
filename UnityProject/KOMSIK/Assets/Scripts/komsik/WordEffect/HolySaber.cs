using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public class HolySaber : IWordEffect
    {
        public string DetailStr(WordState state)
        {
            return "¹Œ•‚ğ•ú‚¿A–‚‰¤‚Æ‚Ìí‚¢‚ÉI~•„‚ğ‘Å‚Â";
        }

        public void DoEffect(WordState state, GameSystem gameSystem)
        {
            gameSystem.GameState.ChangeSection(GameState.Section.NormalEnd);
        }
    }
}
