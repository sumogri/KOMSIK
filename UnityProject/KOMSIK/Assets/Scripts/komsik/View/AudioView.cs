using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public class AudioView : MonoBehaviour
    {
        public static AudioView Instance { get; private set; }

        [SerializeField] private AudioSource bgmSource;
        [SerializeField] private AudioClip[] bgmClips;
        [SerializeField] private AudioSource seSource;
        [SerializeField] private AudioClip[] seClips;

        // Start is called before the first frame update
        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void PlayBGM(BGM bgm)
        {
            var playClip = bgmClips[(int)bgm];
            bgmSource.clip = playClip;
            bgmSource.Play();
            bgmSource.loop = true;
        }

        public void StopBGM()
        {
            bgmSource.Pause();
        }

        public enum BGM 
        {
            Title,
            Sky,
        }
    }
}
