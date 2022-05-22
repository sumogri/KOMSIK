using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public class HolySaber : IWordEffect
    {
        public string DetailStr(WordState state)
        {
            return "����������A�����Ƃ̐킢�ɏI�~����ł�";
        }

        public void DoEffect(WordState state, GameSystem gameSystem)
        {
            gameSystem.GameState.ChangeSection(GameState.Section.NormalEnd);
        }
    }
}
