using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public class EffectView : MonoBehaviour
    {
        public void OwnDestroyEvent()
        {
            Destroy(gameObject);
        }
    }
}
