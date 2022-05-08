using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace KOMSIK
{
    public class WordDetailPopuper : MonoBehaviour , IPointerEnterHandler,IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("PointerEnter");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("PointerExit");
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
