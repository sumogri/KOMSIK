using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace KOMSIK
{
    public class EffectQueikNoizePlayer : MonoBehaviour
    {
        private CinemachineImpulseSource source;

        // Start is called before the first frame update
        void Start()
        {
            GetComponents();
        }

        private void GetComponents()
        {
            if(source == null)
            {
                source = gameObject.GetComponent<CinemachineImpulseSource>();
            }
        }

        public void PlayImpulse()
        {
            GetComponents();
            Debug.Log("Play!");
            source.GenerateImpulse();
        }
    }
}
