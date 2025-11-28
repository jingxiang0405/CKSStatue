using UnityEngine;
using TMPro;            // 1. 為了控制文字介面
using System.Collections; // 2. 為了使用計時器 (Coroutine)

public class ItemPickup : MonoBehaviour
{
    public int itemID; 

    void OnMouseDown()
    {
        ItemDatabase db = FindObjectOfType<ItemDatabase>();

        if (db == null)
        {
            Debug.LogError("找不到資料庫！請確認場景上有 GameManager 物件。");
            return;
        }

        Item data = db.GetItemByID(itemID);

        if (data != null)
        {
            // 這裡依然保留 Console 紀錄，方便你除錯
            Debug.Log("【拾取】你撿到了：" + data.itemName);

            // --- 新增的 UI 邏輯 ---
            GameObject textObj = GameObject.Find("MessageText");

            if (textObj != null)
            {
                TextMeshProUGUI uiText = textObj.GetComponent<TextMeshProUGUI>();
                
                // 設定要顯示的文字
                uiText.text = $"獲得：{data.itemName}\n<size=60%>{data.description}</size>";

                // ★ 關鍵改變：原本是直接 Destroy(gameObject)，現在改成呼叫「計時器」
                StartCoroutine(ShowTextAndDestroy(uiText));
            }
            else
            {
                // 如果找不到 UI，為了避免卡住，還是直接刪掉
                Debug.LogWarning("找不到 MessageText UI，直接刪除物件");
                Destroy(gameObject);
            }
        }
    }

    // --- 新增的計時器功能 ---
    IEnumerator ShowTextAndDestroy(TextMeshProUGUI uiText)
    {
        // 1. 先把方塊「藏起來」（看起來像消失了，但其實還在跑讀秒）
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // 2. 等待 3 秒
        yield return new WaitForSeconds(3.0f);

        // 3. 把螢幕上的字清空
        uiText.text = "";

        // 4. 最後才真正把這個方塊從記憶體刪除
        Destroy(gameObject);
    }
}