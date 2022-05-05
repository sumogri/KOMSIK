using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    /// <summary>
    /// 単語カード. 静的テンプレ.
    /// </summary>
    public class WordStateOrigin
    {
        public string Word { get; }
        public int HP { get; }
        public int Attack { get; }
        public int Diffence { get; }
        public int CustomCost { get; }
        public List<IWordEffect> Effects { get; } = new List<IWordEffect>();

        public WordStateOrigin(string word,int hp,int attack,int diffence,int customCost)
        {
            this.Word = word;
            this.HP = hp;
            this.Attack = attack;
            this.Diffence = diffence;
            this.CustomCost = customCost;
        }
    }
}
