using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreamers.InventorySystem.Interfaces {
    public interface IArmor {

        ArmorType ArmorType { get; }
        


         
    }

    public enum ArmorType { 
        Shield,Helmet,Chest,Arms,Legs, Signature
    }

}
