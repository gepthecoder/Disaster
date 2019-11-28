using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GranadeParabola : MonoBehaviour
{
    public enum ItemType { NoAds }

    public ItemType itemType;

    public Text priceText;

    private string defaultText;

    void Start()
    {
        defaultText = priceText.text;
        StartCoroutine(LoadPriceRoutine());
    }

    public void ClickBuy()
    {
        switch (itemType)
        {
            case ItemType.NoAds:
                IAPmanager.Instance.BuyNO_ADS();
                break;
        }
    }

    private IEnumerator LoadPriceRoutine()
    {
        while (IAPmanager.Instance.IsInitialized())
            yield return null;

        string loadedPrice = "";

        switch (itemType)
        {
            case ItemType.NoAds:
                IAPmanager.Instance.GetProductPriceFromStore(IAPmanager.Instance.NO_ADS);
                break;
        }

        priceText.text = defaultText + " " + loadedPrice;
    }
}
