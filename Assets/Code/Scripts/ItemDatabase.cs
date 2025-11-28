using UnityEngine;
using System.Collections.Generic; // 讓我們可以使用 List 清單

public class ItemDatabase : MonoBehaviour
{
    // 在 Unity 編輯器裡填入你的檔名，不要副檔名 (例如: ItemData)
    public string csvFileName = "ItemData"; 

    // 用來儲存讀取到的所有道具
    public List<Item> allItems = new List<Item>();

    void Start()
    {
        LoadItemData(); // 遊戲一開始就執行讀取
    }

    void LoadItemData()
    {
        // 1. 去 Resources 資料夾找檔案
        TextAsset csvFile = Resources.Load<TextAsset>(csvFileName);

        // 如果找不到檔案就報錯
        if (csvFile == null)
        {
            Debug.LogError("找不到檔案！請確認：1.檔案放在 Resources 資料夾 2.檔名叫做 " + csvFileName);
            return;
        }

        // 2. 把整個檔案依照「換行符號」切成一行一行
        // Split('\n') 代表切行
        string[] rows = csvFile.text.Split('\n');

        // 3. 用迴圈讀取每一行
        // int i = 1 代表從「第2行」開始讀，因為第1行 (i=0) 是標題 (遊戲地圖中會撿到的道具,道具,地點...)
        for (int i = 1; i < rows.Length; i++)
        {
            // 如果這行是空的，就跳過
            if (string.IsNullOrWhiteSpace(rows[i])) continue;

            // 4. 把這一行依照「逗號」切開
            string[] cells = rows[i].Split(',');

            // 你的 CSV 應該要有 3 欄 (ID, 名稱, 地點)
            if (cells.Length >= 3)
            {
                Item newItem = new Item();

                // 解析 ID (把字串 "1" 變成數字 1)
                // cells[0] 是第一欄
                int.TryParse(cells[0], out newItem.id);

                // cells[1] 是第二欄 (名稱)
                newItem.itemName = cells[1].Trim(); // Trim() 可以去掉前後多餘的空白

                // cells[2] 是第三欄 (地點)
                newItem.description = cells[2].Trim();

                // 加到清單裡
                allItems.Add(newItem);
                
                // 在控制台印出來確認一下
                Debug.Log($"成功讀取: {newItem.itemName} (ID: {newItem.id})");
            }
        }
    }

    // 給其他腳本呼叫用的：給我一個 ID，我回傳那個道具的資料給你
    public Item GetItemByID(int id)
    {
        // 跑迴圈去找清單裡的每一個道具
        foreach (var item in allItems)
        {
            // 如果找到 ID 一樣的
            if (item.id == id)
            {
                return item; // 把這個道具資料交出去
            }
        }
        
        // 如果找遍了都沒有 (例如你輸入 ID: 999)，就回傳 null
        Debug.LogWarning("找不到 ID 為 " + id + " 的道具！");
        return null;
    }
}