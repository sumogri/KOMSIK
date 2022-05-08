using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public class CPGenerate : IWordEffect
    {
        public int GenerateCPNum { get; private set; }

        public CPGenerate(int cpNum)
        {
            GenerateCPNum = cpNum;
        }

        public CPGenerate(CPGenerate x)
            : this(x.GenerateCPNum)
        { 
        }

        public void DoEffect(WordState state, GameSystem gameSystem)
        {
            Debug.Log($"{state.Word}");

            gameSystem.GameState.AddCustomPoint(GenerateCPNum);
        }

        public string DetailStr(WordState state)
        {
            return $"Žg—pŽž{GenerateCPNum}cp‚ð“¾‚é";
        }
    }
}
