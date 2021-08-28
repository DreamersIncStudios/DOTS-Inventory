using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace DreamersInc.UI
{
    public class TabGroup : MonoBehaviour
    {
        public List<TabButton> tabButtons { get; private set; }

        public void Awake()
        {
            GetComponentsInChildren<TabButton>(true, tabButtons);
        }

        public void Subscribe(TabButton button)
        {
            if (tabButtons == null)
            {
                tabButtons = new List<TabButton>();
            }
            if(!tabButtons.Contains(button))
                tabButtons.Add(button);
        }
        public void Subscribe() { 
            GetComponentsInChildren<TabButton>(true, tabButtons);

        }

        public void OnTabEnter(TabButton button) { }
        public void OnTabExit(TabButton button) { }

        public void OnTabSelected(TabButton button) { }

    }
}