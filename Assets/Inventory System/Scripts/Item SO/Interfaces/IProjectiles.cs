using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreamers.InventorySystem
{
    public interface IProjectile
    {
        int RoundsPerMin { get; }
        int RoundsPerShot { get; }
        float NormalSpeed { get; }
        Vector3 ShootLocationOffset { get; }
    }
}