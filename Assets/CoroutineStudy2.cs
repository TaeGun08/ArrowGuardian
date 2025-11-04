using System.Collections.Generic;
using UnityEngine;

public class CoroutineStudy2 : MonoBehaviour
{
    public class Data : CoroutineStudy.ITest
    {
        public int Number { get; set; }
    }
    
    public CoroutineStudy study;
    private List<CoroutineStudy.ITest> testList = new List<CoroutineStudy.ITest>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Data data = new Data();
            testList.Add(data);
            study.AddTest(testList);
        }
    }
    
    
}
