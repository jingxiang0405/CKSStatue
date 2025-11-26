using UnityEngine;

[System.Serializable] // 這行一定要加，這樣我們才能在 Unity 面板上看到資料
public class Item
{
    public int id;              // 對應 CSV 第一欄 (1, 2, 3...)
    public string itemName;     // 對應 CSV 第二欄 (藍白拖...)
    public string description;  // 對應 CSV 第三欄 (獲取地點...)
}