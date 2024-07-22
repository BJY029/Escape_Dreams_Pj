using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<SlotData> slots = new List<SlotData>(); //�κ��丮 ����
    int maxSlot = 6; //�ִ� �κ��丮 ����
    public GameObject slotPrefab;

    void Start() //�κ��丮 ����
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
