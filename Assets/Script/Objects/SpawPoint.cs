using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawPoint : MonoBehaviour
{
    public SpawPointData data;

    public Text txtCooldown;

    public RectTransform rect;
        

    void Update()
    {
        if (!data.spawned) return;

        CountTime();
    }

    private void CountTime()
    {
        data.inCooldown = data.cooldownTime < data.cooldown;
        if (data.inCooldown)
            data.cooldownTime += Time.deltaTime;
        else if (!data.inCooldown)
            data.cooldownTime = 0;
    }
}
