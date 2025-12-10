using System;

[Serializable]
public class InventoryItem
{
    public Item itemData;
    public int stackSize;

    // 未來擴充範例：
    // public float durability; // 耐久度
    // public bool isEquipped;  // 是否裝備中

    public InventoryItem(Item source, int amount)
    {
        itemData = source;
        stackSize = amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }
}