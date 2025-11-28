using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // 這是我們要在 Unity 編輯器裡填的數字
    // 例如：填 1 代表它是藍白拖，填 2 代表它是雨傘
    public int itemID; 

    // 當滑鼠點擊這個物件時會自動執行
    void OnMouseDown()
    {
        // 1. 找到場景上的資料庫 (GameManager)
        ItemDatabase db = FindObjectOfType<ItemDatabase>();

        if (db == null)
        {
            Debug.LogError("找不到資料庫！請確認場景上有 GameManager 物件。");
            return;
        }

        // 2. 跟資料庫要資料
        Item data = db.GetItemByID(itemID);

        // 3. 如果有拿到資料
        if (data != null)
        {
            // 這裡之後可以寫「加到背包」的邏輯，現在先用文字顯示
            Debug.Log("【拾取】你撿到了：" + data.itemName);
            Debug.Log("【說明】" + data.description);

            // 4. 把地圖上的這個方塊刪掉 (代表被撿走了)
            Destroy(gameObject);
        }
    }
}