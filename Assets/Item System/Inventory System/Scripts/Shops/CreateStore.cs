using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamers.InventorySystem.Generic;
using Dreamers.InventorySystem.Interfaces;

namespace Dreamers.InventorySystem
{
    [RequireComponent(typeof(BoxCollider))]

    public class CreateStore : MonoBehaviour
    {
        public StoreTypes StoreType;
     [SerializeReference]  public List<ItemBaseSO> InitialItems;
        public Shop shop;

        private void Awake()
        {
            shop = new Shop(StoreType.ToString(),InitialItems);
        }
        public void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                if (Input.GetKeyUp(KeyCode.V) && shop.Displayed) { shop.CloseStore(); }
                if (Input.GetKeyUp(KeyCode.V) && !shop.Displayed)
                {
                    shop.OpenStore(other.GetComponent<CharacterInventory>());
                }
            }
        }
    }

 
    public enum StoreTypes { General, Item, Weapon, Armor, Mission}

}