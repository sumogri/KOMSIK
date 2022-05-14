using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class TextPlayableAsset : PlayableAsset
{
    [SerializeField,TextArea(1, 6)] private string text;
    public Color startColor = Color.white;
    public Color endColor = Color.white;
    public ExposedReference<GameObject> charaObj;
    public float fontSize = 0;
    // public string text;

    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var behaviour = new TextPlayableBehaviour();
        behaviour.charaObject = charaObj.Resolve(graph.GetResolver());
        behaviour.Text = text;
        behaviour.StartColor = startColor;
        behaviour.EndColor = endColor;
        behaviour.FontSize = fontSize;

        // behaviour.text = text;
        return ScriptPlayable<TextPlayableBehaviour>.Create(graph, behaviour);
    }
}