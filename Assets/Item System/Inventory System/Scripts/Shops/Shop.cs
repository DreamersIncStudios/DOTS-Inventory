using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamers.Global;
using UnityEngine.UI;
using Dreamers.InventorySystem;
namespace Dreamers.InventorySystem.Generic
{
    public class Shop 
    {
        private string shopName;
        private List<IPurchasable> itemsToSell;
        private List<IPurchasable> itemsToBuyback;
        private uint storeWallet;
        [Range(.5f, 1.0f)]
        public float Sell;
        [Range(.5f, 1.0f)]
        public float Buy;
        readonly UIManager manager;
        public bool Displayed { get { return (bool)MenuPanelParent; } }
        bool Buying = true;
        public Shop(string name = "", List<IPurchasable> itemToSell= default, uint SeedCapital = 1500)
        {
            this.shopName = name;
            this.storeWallet = SeedCapital;
            AddItemsToInventory(itemToSell);
            manager = UIManager.instance;

        }
        #region manage Inventory
        public void AddItemsToInventory(IPurchasable item)
        {
            itemsToSell.Add(item);
        }
        public void AddItemsToInventory(List<IPurchasable> items)
        {
            itemsToSell.AddRange(items);
        }

        public void SellItemToShop(IPurchasable item, out uint gold)
        {
            itemsToBuyback.Add(item);
            gold = item.Value;
        }
        public void SellItemToShop(List<IPurchasable> items, out uint gold)
        {
            itemsToSell.AddRange(items);
            gold = new uint();
            foreach (var item in items)
            {
                gold += item.Value;
            }
        }
        public void RemoveItemFromInventory(IPurchasable item)
        {
            itemsToSell.Remove(item);
        }

        public int GetItemIndex(IPurchasable item)
        {
            return itemsToSell.IndexOf(item);
        }
        public IPurchasable GetItem(int index)
        {
            return itemsToSell[index];
        }
        public Vector2 GetStoreMod(int luck, int Charsima) {

            // TODO Implement Logic
            return new Vector2(Sell, Buy);
        }
        bool PurchaseItem(IPurchasable item, uint PlayerCashOnHand)
        {
            if (item.Value <= PlayerCashOnHand)
            {
                storeWallet += item.Value;
                return true;
            }
            else { return false; }

        }
        bool PurchaseItem(int itemIndex, uint PlayerCashOnHand)
        {
            return PurchaseItem(GetItem(itemIndex), PlayerCashOnHand);

        }
        #endregion

        #region Manage UI

        GameObject MenuPanelParent;
        GameObject ItemPanel;
        GameObject playerGold; // possible just work with player inventory


        public void OpenStore(CharacterInventory characterInventory)
        {
            MenuPanelParent = CreateStoreUI(new Vector2(0, 0),
         new Vector2(0, 0) , characterInventory);
        }

        public void CloseStore()
        {
            Object.Destroy(MenuPanelParent);

        }

        GameObject CreateStoreUI(Vector2 Size, Vector2 Position, CharacterInventory characterInventory)
        {
            if (MenuPanelParent)
                Object.Destroy(MenuPanelParent);

            GameObject Parent = manager.UICanvas();
            GameObject MainPanel = manager.GetPanel(Parent.transform, Size, Position);
            MainPanel.name = shopName;
            RectTransform PanelRect = MainPanel.GetComponent<RectTransform>();
            PanelRect.pivot = new Vector2(0.5f, .5f);
            PanelRect.anchorMax = new Vector2(1, 1);
            PanelRect.anchorMin = new Vector2(.0f, .0f);

            VerticalLayoutGroup VLG = MainPanel.AddComponent<VerticalLayoutGroup>();
            VLG.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            VLG.childAlignment = TextAnchor.UpperCenter;
            VLG.childControlHeight = true; VLG.childControlWidth = true;
            VLG.childForceExpandHeight = false; VLG.childForceExpandWidth = true;

            Text titleGO = manager.TextBox(MainPanel.transform, new Vector2(400, 50)).GetComponent<Text>();
            titleGO.alignment = TextAnchor.MiddleCenter;
            titleGO.text = shopName;
            titleGO.fontSize = 24;
            HorizontalLayoutGroup BuySell = manager.GetPanel(MainPanel.transform, new Vector2(1920, 60), Position).AddComponent<HorizontalLayoutGroup>();
            BuySell.name = "BuySell Header";
            BuySell.childControlHeight = false;
            BuySell.childForceExpandHeight = false;

            manager.UIButton(BuySell.transform, "Buy Items")
                .onClick.AddListener(() =>
                {
                    Buying = true;
                    ItemPanel = DisplayItems(ItemType.None, MainPanel.transform, characterInventory);
                });

            manager.UIButton(BuySell.transform, "Sell Items")
                .onClick.AddListener(() =>
                {
                    Buying = false;

                    ItemPanel = DisplayItems(ItemType.None, MainPanel.transform, characterInventory);
                });
            #region header
            HorizontalLayoutGroup ButtonHeader = manager.GetPanel(MainPanel.transform, new Vector2(1920, 60), Position).AddComponent<HorizontalLayoutGroup>();
            ButtonHeader.name = "Button Header";
            ButtonHeader.childControlHeight = false;
            ButtonHeader.childForceExpandHeight = false;
            manager.UIButton(ButtonHeader.transform, "All")
                .onClick.AddListener(() =>
                {
                    ItemPanel = DisplayItems(ItemType.None, MainPanel.transform, characterInventory);
                });


            for (int i = 1; i < 6; i++)
            {
                int index = i;
                Button Temp = manager.UIButton(ButtonHeader.transform, ((ItemType)i).ToString());
                Temp.onClick.AddListener(() =>
                {
                    ItemPanel = DisplayItems((ItemType)index, MainPanel.transform, characterInventory);
                });
                ;
                Temp.name = ((ItemType)i).ToString();

            }
            #endregion

            playerGold = DisplayPlayerGold(MainPanel.transform, characterInventory);
            ItemPanel = DisplayItems(ItemType.None, MainPanel.transform,characterInventory);
            return MainPanel;
        }
        Button ItemButton(Transform Parent, IPurchasable item)
        {
            Button temp = manager.UIButton(Parent, item.ItemName);
            temp.name = item.ItemName;
            Text texttemp = temp.GetComponentInChildren<Text>();
            texttemp.alignment = TextAnchor.LowerCenter;
            if (item.Stackable)
                texttemp.text += item.Count;
            temp.GetComponentInChildren<Text>().alignment = TextAnchor.LowerCenter;
            temp.GetComponent<Image>().sprite = item.Icon;


            return temp;
        }

        GameObject DisplayItems(ItemType Filter, Transform Parent , CharacterInventory playerInventory)
        {
            if (ItemPanel)
                Object.Destroy(ItemPanel);

            GridLayoutGroup basePanel = manager.GetPanel(Parent.transform, new Vector2(1920, 0), new Vector2(0, 0))
                 .AddComponent<GridLayoutGroup>();
            basePanel.name = "Items Display";
            basePanel.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            basePanel.spacing = new Vector2(20, 20);

                //   inventory = Store.StoreInventory;
                for (int i = 0; i < itemsToSell.Count; i++)
                {
                    int index = i;
                    if (itemsToSell[i].Type == Filter)
                    {
                        Button temp = ItemButton(basePanel.transform, itemsToSell[index]);
                        temp.onClick.AddListener(() =>
                        {

                            GameObject pop = PopUpItemPanel(temp.GetComponent<RectTransform>().anchoredPosition
                                 + new Vector2(575, -175)
                                 ,  itemsToSell[index], playerInventory);
                            pop.AddComponent<PopUpMouseControl>();

                        });
                    }
                } 
            
 
            return basePanel.gameObject;
        }

        GameObject DisplayPlayerGold(Transform MainPanel, CharacterInventory playerInventory)
        {

            if (playerGold)
                Object.Destroy(playerGold);

            Text goldInWallet = manager.TextBox(MainPanel, new Vector2(400, 50)).GetComponent<Text>();
            goldInWallet.alignment = TextAnchor.LowerRight;
            goldInWallet.text = playerInventory.Gold.ToString() + "G";
            goldInWallet.fontSize = 16;
            return playerGold;
        }

        GameObject PopUpItemPanel(Vector2 Pos, IPurchasable item, CharacterInventory playerInventory)
        {
            GameObject PopUp = manager.GetPanel(manager.UICanvas().transform, new Vector2(400, 400), Pos);
            Image temp = PopUp.GetComponent<Image>();
            Color color = temp.color; color.a = 1.0f;
            temp.color = color;

            HorizontalLayoutGroup group = PopUp.AddComponent<HorizontalLayoutGroup>();
            PopUp.AddComponent<PopUpMouseControl>();

            group.childControlWidth = false;

            Text info = manager.TextBox(PopUp.transform, new Vector2(250, 300));
            info.text = item.ItemName + "\n";
            info.text += item.Description + "\n";
            info.fontSize = 20;

            info.text += "Cost: " + Mathf.RoundToInt(item.Value * Sell) + " gil";

            VerticalLayoutGroup ButtonPanel = manager.GetPanel(PopUp.transform, new Vector2(150, 300), Pos).AddComponent<VerticalLayoutGroup>();
            if (Buying)
            {
                switch (item.Type)
                {
                    case ItemType.General:
                    case ItemType.Crafting_Materials:

                        Button Buy1 = manager.UIButton(ButtonPanel.transform, "Buy 1 ");
                       // Button Buy5 = manager.UIButton(ButtonPanel.transform, "Buy 5 ");
                        Buy1.onClick.AddListener(() =>
                        {
                            if (PurchaseItem(item, (uint)playerInventory.Gold))
                            {
                                playerInventory.AdjustGold(-(int)item.Value);
                                playerInventory.Inventory.AddToInventory(/* item*/); //TODO Implement 
                              // Todo implement change event
                                playerGold = DisplayPlayerGold(MenuPanelParent.transform, playerInventory);
                                Object.Destroy(PopUp);
                            }else
                            {
                                //TODO Implement Can't Afford message
                                Debug.Log("Player has NSF");
                            }
                        });
                        //Todo Consider Implement with UI Slider and for loop
                        //Buy5.onClick.AddListener(() =>
                        //{
                        //    Store.BuyXItemsFrom(Slot, 5);
                        //    playerGold = DisplayPlayerGold(MenuPanelParent.transform, playerInventory);
                        //    ItemPanel = DisplayItems(ItemType.None, MenuPanelParent.transform, playerInventory);
                        //    Object.Destroy(PopUp);

                        //});
                        break;
                    case ItemType.Armor:
                    case ItemType.Weapon:
                    case ItemType.Blueprint_Recipes:
                        Button Buy = manager.UIButton(ButtonPanel.transform, "Buy");
                        Buy.onClick.AddListener(() =>
                        {
                            if (PurchaseItem(item, (uint)playerInventory.Gold))
                            {
                                playerInventory.AdjustGold(-(int)item.Value);
                                playerInventory.Inventory.AddToInventory(/* item*/); //TODO Implement 
                                                                                     // Todo implement change event
                                playerGold = DisplayPlayerGold(MenuPanelParent.transform, playerInventory);
                                Object.Destroy(PopUp);
                            }
                            else
                            {
                                //TODO Implement Can't Afford message
                                Debug.Log("Player has NSF");
                            }
                        });

                        break;
                }
            }
            else
            {
                switch (item.Type)
                {
                    case ItemType.General:
                    case ItemType.Crafting_Materials:

                        Button Sell1 = manager.UIButton(ButtonPanel.transform, "Sell 1 ");
                       // Button Sell5 = manager.UIButton(ButtonPanel.transform, "Sell 5 ");
                        Sell1.onClick.AddListener(() =>
                        {
                            playerInventory.AdjustGold((int)item.Value);
                            playerInventory.Inventory.RemoveFromInventory(/*item*/);
                            Object.Destroy(PopUp);
                            //TODO implement event to handle changes
                            playerGold = DisplayPlayerGold(MenuPanelParent.transform, playerInventory);
                            ItemPanel = DisplayItems(ItemType.None, MenuPanelParent.transform, playerInventory);

                        });
                        //TODO implement with slided
                        //Sell5.onClick.AddListener(() =>
                        //{
                        //    Store.SellxItemsTo(Slot, IndexOf, 5);
                        //    ItemPanel = DisplayItems(ItemType.None, MenuPanelParent.transform, playerInventory);
                        //    playerGold = DisplayPlayerGold(MenuPanelParent.transform, playerInventory);
                        //    ItemPanel = DisplayItems(ItemType.None, MenuPanelParent.transform, playerInventory);
                        //    Object.Destroy(PopUp);

                        //});
                        break;
                    case ItemType.Armor:
                    case ItemType.Weapon:
                    case ItemType.Blueprint_Recipes:
                        Button Sell = manager.UIButton(ButtonPanel.transform, "sell");
                        Sell.onClick.AddListener(() =>
                        {
                            playerInventory.AdjustGold((int)item.Value);
                            playerInventory.Inventory.RemoveFromInventory(/*item*/);
                            Object.Destroy(PopUp);
                            //TODO implement event to handle changes
                            playerGold = DisplayPlayerGold(MenuPanelParent.transform, playerInventory);
                            ItemPanel = DisplayItems(ItemType.None, MenuPanelParent.transform, playerInventory);
                        });

                        break;
                }
            }
            return PopUp;
        }





        #endregion
    }
    //Todo Move to SO interfaces
    public interface IPurchasable
    {
        uint Value { get; }
        ItemType Type {get;}
        string ItemName { get; }
        string Description { get; }
        uint Count { get;  }
        void AdjustCount(int mod);
        bool Stackable { get; }
        Sprite Icon { get; }
    }
}