using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : ItemBase
{
    [SerializeField]
    private int recoveryValue;

    void OnCollisionEnter(Collision collision)
    {
        ItemEffect();
    }

    public override void ItemEffect()
    {
        PlayerController.Instance.SetHeart -= recoveryValue;
        base.ItemEffect();
    }
}
