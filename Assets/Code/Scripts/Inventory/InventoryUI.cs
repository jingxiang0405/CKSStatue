using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Header("參考物件")]
    [Tooltip("放置格子的父物件 (通常有 Grid Layout Group)")]
    public Transform itemsParent;

    [Tooltip("單一格子的 Prefab")]
    public InventorySlot slotPrefab;

    private void Start()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.onInventoryChangedCallback += UpdateUI;
        }

        UpdateUI();
    }

    private void OnDestroy()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.onInventoryChangedCallback -= UpdateUI;
        }
    }

    private void UpdateUI()
    {
        // 1. 清空：把 itemsParent 底下所有的舊格子全部刪除
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        // 2. 生成：讀取 InventoryManager 的清單，一個一個生出來
        // 注意：這裡我們讀取的是 inventory 清單
        foreach (InventoryItem item in InventoryManager.Instance.inventory)
        {
            // 生成格子
            InventorySlot newSlot = Instantiate(slotPrefab, itemsParent);

            // 設定格子內容 (呼叫我們之前寫好的 Setup)
            newSlot.Setup(item);
        }
    }
}