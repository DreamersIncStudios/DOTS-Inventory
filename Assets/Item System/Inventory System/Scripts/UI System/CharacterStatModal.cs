using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Stats;
using Dreamers.InventorySystem.Base;

namespace Dreamers.InventorySystem.UISystem
{
    public class CharacterStatModal : MonoBehaviour
    {
        [Header("Header")]
        [SerializeField] Transform headerArea;
        [SerializeField] TextMeshProUGUI titleField;
        [Header("Body")]
        [SerializeField] Transform contentArea;
        [SerializeField] TextMeshProUGUI attributeNames;
        [SerializeField] TextMeshProUGUI attributeValues;
        [SerializeField] Button EquipmentButton;

        [Header("Footer")]
        [SerializeField] Transform footerArea;


        public void ShowAsCharacterStats(BaseCharacter character, EquipmentBase equipmentBase) {
            titleField.text = character.name;
            attributeNames.text = "Lvl: ";
            attributeValues.text = character.Level.ToString() +"\n";
            attributeNames.text += "\nHealth:\t";
            attributeValues.text += character.CurHealth + "/" + character.MaxHealth;
            attributeNames.text += "\nMana:\t\n";
            attributeValues.text +="\n" + character.CurMana + "/" + character.MaxMana + "\n";

            for (int i = 1; i < System.Enum.GetValues(typeof(AttributeName)).Length; i++)
            {
                attributeNames.text +=   "\n" + ((AttributeName)i).ToString() + ":";
                attributeValues.text +=  "\n" + character.GetPrimaryAttribute(i).BaseValue;
                attributeValues.text += " + " + character.GetPrimaryAttribute(i).BuffValue;
                attributeValues.text += " + " + character.GetPrimaryAttribute(i).AdjustBaseValue;
            }
            ShowEquipmentGrid(equipmentBase);
        }

        public void ShowEquipmentGrid(EquipmentBase equipmentBase) {
            List<Button> selectionButtons = new List<Button>();
            selectionButtons.Add(EquipmentButton);
            for (int i = 0; i < 8; i++)
            {
                selectionButtons.Add(Instantiate(EquipmentButton, EquipmentButton.transform.parent));
            }

        }

        public void UpdateEquipmentGrid(EquipmentBase equipmentBase) { }

    }
}