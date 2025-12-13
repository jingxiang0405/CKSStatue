using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("設定")]
    [Tooltip("這是一個單例模式 (Singleton)，全域只能有一個")]
    public static InventoryManager Instance;

    [Header("資料儲存")]
    // 儲存實際的物品實體 (包含狀態)
    public List<InventoryItem> inventory = new List<InventoryItem>();

    public delegate void OnInventoryChanged();
    public event OnInventoryChanged onInventoryChangedCallback;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
            // 如果你需要換場景保留背包，取消下面這行的註解
            // DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// 新增物品到背包
    /// </summary>
    /// <param name="item">物品的原始資料 (模具)</param>
    /// <param name="amount">數量 (目前應該都是1) </param>
    public void AddItem(Item itemData, int amount = 1)
    {
        // --- 核心邏輯：不堆疊 ---
        // 直接建立一個新的實體物件 (Instance)
        // 這樣即使是兩把同樣的劍，也會是清單裡兩個不同的物件
        InventoryItem newItem = new InventoryItem(itemData, amount);

        inventory.Add(newItem);

        Debug.Log($"背包獲得新物品: {itemData.displayName}");

        // 觸發事件通知 UI 重繪
        onInventoryChangedCallback?.Invoke();
    }

    /// <summary>
    /// 移除特定的物品實體
    /// </summary>
    public void RemoveItem(InventoryItem itemToRemove)
    {
        if (inventory.Contains(itemToRemove))
        {
            inventory.Remove(itemToRemove);

            Debug.Log($"背包移除物品實體: {itemToRemove.itemData.displayName}");

            onInventoryChangedCallback?.Invoke();
        }
    }

    /// <summary>
    /// 移除指定類型的物品
    /// </summary>
    public void RemoveItemByData(Item dataToRemove, int amount = 1)
    {
        InventoryItem item = inventory.Find(x => x.itemData == dataToRemove);

        if (item != null)
        {
            // 在不堆疊的邏輯下，通常直接移除該格
            if (item.stackSize > amount)
            {
                item.RemoveFromStack(amount);
            }
            else
            {
                // 如果數量歸零或原本就只有1個，直接移除整格
                inventory.Remove(item);
            }

            onInventoryChangedCallback?.Invoke();
        }
        else
        {
            Debug.LogWarning("嘗試移除物品失敗：背包內找不到 " + dataToRemove.displayName);
        }
    }

    // 實用功能：檢查背包裡有沒有某種物品 (給任務系統用)
    public bool HasItem(Item itemData)
    {
        return inventory.Exists(x => x.itemData == itemData);
    }
}