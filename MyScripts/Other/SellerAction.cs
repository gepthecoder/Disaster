using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerAction : MonoBehaviour
{
    public bool hasSteppedOn = ShopTriggerPlatformScript.SteppedOnPlatform;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSteppedOn)
        {
            anim.SetTrigger("BuyerHasNoMoney");
        }
    }
}
