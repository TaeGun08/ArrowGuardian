using System;
using System.Collections;
using UnityEngine;

public class CoroutineStudy : UnityEngine.MonoBehaviour
{
    private IEnumerator Test; 
    
    private void Start()
    {
        Test = CoroutineTest();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Test.MoveNext();

            switch (Test.Current)
            {
                case (string a, string b) :
                    Debug.Log($"{a} |||| {b}");
                    break;
                case (int a, int b) :
                    Debug.Log($"{a} |||| {b}");
                    break;
                case (int a, int b, bool c) :
                    Debug.Log($"{a} |||| {b} |||| {c}");
                    break;
                case WaitForSeconds w:
                    Debug.Log($"{w}");
                    break;
                default:
                    break;
            }
        }
    }


    private IEnumerator CoroutineTest()
    {
        while (true)
        {
            transform.Translate(Vector3.up);
            Debug.Log("CoroutineStudy ::: 1");
            (int, int) reVal = (4, 4);
            yield return reVal;
        
            transform.Translate(Vector3.left);
            Debug.Log("CoroutineStudy ::: 2");
            (int, int, bool) reVal2 = (4, 4, true);
            yield return reVal2;
        
            transform.Translate(Vector3.down);
            yield return null;
        
            Debug.Log($"{reVal.Item1}, {reVal.Item2}");
            
            transform.Translate(Vector3.right);
            yield return null;

            yield return new WaitForSeconds(3.0f);
        }
    }
    
}