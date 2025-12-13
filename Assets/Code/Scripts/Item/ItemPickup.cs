using UnityEngine;
using UnityEngine.EventSystems; // 引用這個是為了防止點穿 UI

[RequireComponent(typeof(Collider))] // 強制要求這個物件必須有 Collider，否則點不到
public class ItemPickup : MonoBehaviour
{
    [Header("物品設定 (於Assets/Resources/Items定義)")]
    public Item itemData; // 記得把 ItemData 拖進來
    public int amount = 1;    // 預設數量

    [Header("拾取設定")]
    [Tooltip("玩家必須在這個距離內才能撿起")]
    public float pickupRange = 3f;

    // Unity 內建函式：當滑鼠點擊這個 Collider 時會觸發
    private void OnMouseDown()
    {
        // 1. 檢查是否點在 UI 上 (重要！)
        // 如果滑鼠游標正停在按鈕或介面上，就不要觸發撿東西
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // 2. 檢查距離 (防止隔空取物)
        // 這裡假設你的玩家物件 Tag 是 "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > pickupRange)
            {
                Debug.Log("太遠了，撿不到！");
                return; // 距離太遠，直接結束，不執行下面程式
            }
        }

        // 3. 執行撿取
        Pickup();
    }

    private void Pickup()
    {
        if (InventoryManager.Instance != null)
        {
            // 呼叫我們剛剛寫好的 Manager
            InventoryManager.Instance.AddItem(itemData, amount);

            // 撿起來後銷毀場景上的物件
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("找不到 InventoryManager！請確認場景中有 Managers 物件。");
        }
    }

    // (選用功能) 當滑鼠移上去時顯示提示
    private void OnMouseEnter()
    {
        // 這裡可以做變色效果，或更改滑鼠游標
        // Debug.Log("滑鼠指到了: " + itemData.displayName);
    }
}