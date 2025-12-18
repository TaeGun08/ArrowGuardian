using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class Store : MonoBehaviour
{
    [Header("Store Settings")]
    [SerializeField] private StoreCardUI storeCardUI;
    [SerializeField] private Transform content;
    [SerializeField] private Button closeButton;

    private ScrollRect scrollRect;

    private void Awake()
    {
        List<StoreData> storeDataList = CsvUtility.LoadCsv<StoreData>("StoreData");

        if (storeDataList == null || storeDataList.Count == 0) return;

        foreach (var data in storeDataList)
        {
            StoreCardUI card = Instantiate(storeCardUI, content);
            card.Initialize(data);
        }
        
        scrollRect = GetComponentInChildren<ScrollRect>();
        
        closeButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void OnEnable()
    {
        ResetScrollView();
    }

    private void ResetScrollView()
    {
        if (scrollRect != null) scrollRect.normalizedPosition = new Vector2(0, 1);
    }
}
