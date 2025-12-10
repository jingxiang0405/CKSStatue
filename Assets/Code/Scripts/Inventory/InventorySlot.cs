using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    //[SerializeField] private TMP_Text amountText;

    private InventoryItem currentItem;

    public void Setup(InventoryItem item)
    {
        currentItem = item;

        // 設定圖片
        if (item.itemData.icon != null)
        {
            iconImage.sprite = item.itemData.icon;
            iconImage.enabled = true;
        }
        else
        {
            iconImage.enabled = false;
        }

        
    }
}