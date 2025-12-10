using System;
using UnityEngine;
using System.Collections.Generic;

public class Store : MonoBehaviour
{
    [Header("Store Settings")]
    [SerializeField] private StoreCardUI storeCardUI;

    private void Awake()
    {
        List<StoreData> storeDataList = CsvUtility.LoadCsv<StoreData>("StoreData");

        if (storeDataList == null || storeDataList.Count == 0)
        {
            Debug.LogWarning("StoreData CSV is empty or missing.");
            return;
        }

        foreach (var data in storeDataList)
        {
            // StoreCardUI card = Instantiate(storeCardUI, parentContent);
            // card.Initialize(data);
        }
    }
}
