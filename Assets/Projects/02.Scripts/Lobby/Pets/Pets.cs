using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pets : MonoBehaviour
{
    [Header("Pets Settings")]
    //[SerializeField] private StoreCardUI petsCardUI;
    [SerializeField] private Transform content;
    [SerializeField] private Button closeButton;

    private ScrollRect scrollRect;

    private void Awake()
    {
        // List<StoreData> storeDataList = CsvUtility.LoadCsv<StoreData>("StoreData");
        //
        // if (storeDataList == null || storeDataList.Count == 0) return;
        //
        // foreach (var data in storeDataList)
        // {
        //     StoreCardUI card = Instantiate(petsCardUI, content);
        //     card.Initialize(data);
        // }
        
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
