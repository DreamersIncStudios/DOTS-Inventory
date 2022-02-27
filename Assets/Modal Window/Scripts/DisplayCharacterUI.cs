using Dreamers.Global;
using Dreamers.InventorySystem.Base;
using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dreamers.InventorySystem.UISystem
{
    public class DisplayCharacterUI : MonoBehaviour
    {
        UIManager Manager;

        bool OpenCloseMenu => Input.GetKeyUp(KeyCode.I);
        [SerializeField] Canvas getCanvas;
        public bool Displayed { get; private set; }
        InventoryBase Inventory => character.GetComponent<CharacterInventory>().Inventory;

        private void Awake()
        {
            Manager = UIManager.instance;
        }
      
        // Update is called once per frame
        void Update()
        {
            if (OpenCloseMenu)
            {
                if (!Displayed)
                    OpenCharacterMenu(Inventory);
                else
                    CloseCharacterMenu();
            }

        }



        public void CloseCharacterMenu()
        {
            foreach (Transform child in getCanvas.transform)
            {
                Object.Destroy(child.gameObject);
            }

            Displayed = false;
        }

       [SerializeField] BaseCharacter character;

        void CreateMenu()
        {
            Manager = UIManager.instance;

            CharacterStatModal characterStat = Object.Instantiate(Manager.StatsWindow, getCanvas.transform).GetComponent<CharacterStatModal>();
            characterStat.ShowAsCharacterStats(character);
            ItemModalWindow items = Object.Instantiate(Manager.InventoryWindow, getCanvas.transform).GetComponent<ItemModalWindow>();
            items.ShowAsCharacterInventory(character.GetComponent<CharacterInventory>());
        }
        public void OpenCharacterMenu(InventoryBase inventory)
        {

            CreateMenu();
            Displayed = true;
        }

    }
}