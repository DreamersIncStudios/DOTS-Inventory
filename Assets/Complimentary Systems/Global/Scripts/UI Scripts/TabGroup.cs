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
        public Color TabIdle, TabSelected, TabHovered;
        private TabButton selectedtab;

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

        public void OnTabEnter(TabButton button) {
            ResetTabs();
            if (selectedtab == null && button!=selectedtab)
                button.background.color = TabHovered;


        }
        public void OnTabExit(TabButton button) {
            ResetTabs();
        }

        public void OnTabSelected(TabButton button) {

            if (selectedtab != null && button != selectedtab) {
                selectedtab.Deselect();
            }

            selectedtab = button;
            selectedtab.Select();
            ResetTabs();
            button.background.color = TabSelected;
        }


        public void ResetTabs() {
            foreach (var button in tabButtons)
            {
                if (button == selectedtab && selectedtab != null)
                    continue;
                button.background.color = TabIdle;
            }

        }
    }
}