
using Dreamers.InventorySystem.Base;
using Dreamers.InventorySystem.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreamers.InventorySystem
{
    [RequireComponent(typeof(BoxCollider))]
    public class Vender : MonoBehaviour
    {
        public StoreBase Base;
        DisplayStore Store;
        public GameObject player;

        private void Start()
        {
            Base.CharacterInventory = player.GetComponent<CharacterInventory>();
            Store = new DisplayStore(Base);
            Store.CloseStore();
        }


        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                if (Input.GetKeyUp(KeyCode.V) && Store.Displayed) { Store.CloseStore(); }
                if (Input.GetKeyUp(KeyCode.V) && !Store.Displayed)
                {
                    Store.OpenStore();
                }
            }
        }
    }
}