using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamers.Global;
using MCADSystem;

namespace EquipmentStats
{
    public partial class MCAD_UI_Controller : MonoBehaviour
    {
        public int ActivationCost;
        public int ActivationMax;
        public int ActivationCur;

        public BaseEquipment EquipmentRef;

        public void setup(int activationCost, int activationMax, BaseEquipment equipmentRef)
        {
            ActivationCost = activationCost;
            ActivationMax = activationMax;
            EquipmentRef = equipmentRef;
        }

        public void IncreaseActivationPoints() { }
        public void DecreaseActivationPoints() { }

    }
}