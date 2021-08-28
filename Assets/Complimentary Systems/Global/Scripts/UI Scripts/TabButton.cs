using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace DreamersInc.UI
{
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour,IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        private TabGroup tabGroup => GetComponentInParent<TabGroup>();
        public Image background;
        // Start is called before the first frame update
        void Start()
        {
            tabGroup.Subscribe(this);
            background = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}