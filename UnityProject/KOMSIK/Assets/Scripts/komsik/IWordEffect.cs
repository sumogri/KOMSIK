using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    /// <summary>
    /// ワード効果
    /// 
    /// そのワード専用の効果はすべてここで処理.
    /// 原則、シングルトン的に振る舞う. OriginのオブジェクトがStateに使い回される.
    /// インスタンスが欲しいなら、DoEffect経由で再生性か、パラメータをWordStateに生やすか……(きけんがいっぱい)
    /// </summary>
    public interface IWordEffect
    {
        public void DoEffect(WordState state, GameSystem gameSystem);

        public string DetailStr(WordState state);
    }
}
