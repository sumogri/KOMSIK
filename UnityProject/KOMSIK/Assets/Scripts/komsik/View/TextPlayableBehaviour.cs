using TMPro;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class TextPlayableBehaviour : PlayableBehaviour
{
    public GameObject charaObject;
    public string Text { get; set; }
    public Color StartColor { get; set; }
    public Color EndColor { get; set; }
    public float FontSize { get; set; }

    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {
        this.charaObject.GetComponent<TMP_Text>().text = "";
    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {
        this.charaObject.GetComponent<TMP_Text>().text = this.Text;
    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {

    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {

    }

    // Called each frame while the state is set to Play
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        // PlayableTrackのClip上でシークバーが移動するたびに呼ばれ続ける（PrepareFrameの後）
        if (charaObject == null) { return; }
        var percent = (float)playable.GetTime() / (float)playable.GetDuration();

        var textConponent = this.charaObject.GetComponent<TMP_Text>();
        textConponent.text =
            this.Text.Substring(0, (int)Mathf.Round(this.Text.Length * percent));

        var progress = (float)(playable.GetTime() / playable.GetDuration());
        textConponent.color = Color.Lerp(StartColor, EndColor, progress);

        if(FontSize > 0)
        {
            textConponent.fontSize = FontSize; //マージしない.
        }
    }
}