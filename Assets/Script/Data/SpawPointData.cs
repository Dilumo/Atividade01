using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawPointData
{
    public bool spawned = false;

    public float cooldown;
    public float cooldownTime;

    public bool inCooldown;
}
