using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<SlotData> slots = new List<SlotData>(); //인벤토리 선언
    int maxSlot = 6; //최대 인벤토리 갯수
    public GameObject slotPrefab;

    void Start() //인벤토리 관리
    {
        GameObject slotPanel = GameObject.Find("Inventory");

        for (int i = 0; i < maxSlot; i++) {
            GameObject go = Instantiate(slotPrefab, slotPanel.transform, false);

            go.name = "Slot_" + i;
            SlotData slot = gameObject.AddComponent<SlotData>();
            slot.isEmpty = true;
            slot.slotObj = go;
            slots.Add(slot);
        }
    }
}
