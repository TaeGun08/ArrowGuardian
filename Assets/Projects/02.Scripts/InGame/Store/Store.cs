using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class Store : MonoBehaviour
{
    [Header("Store Settings")]
    [SerializeField] private StoreCardUI storeCardUI;
    [SerializeField] private Transform content;

    private void Awake()
    {
        List<StoreData> storeDataList = CsvUtility.LoadCsv<StoreData>("StoreData");

        if (storeDataList == null || storeDataList.Count == 0) return;

        foreach (var data in storeDataList)
        {
            StoreCardUI card = Instantiate(storeCardUI, content);
            card.Initialize(data);
        }
    }
}
