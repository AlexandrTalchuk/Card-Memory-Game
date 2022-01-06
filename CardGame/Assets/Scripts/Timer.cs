using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    int count=60;
    public Text textcount;

    private void Start()
    {
        StartCoroutine("TimeScore");
    }
  
    public void Update()
    {
        if (count == 0)
        StopCoroutine("TimeScore");
    }
    IEnumerator TimeScore()
    {
        while(true)
        {
            count--;
            textcount.text = count.ToString();
            yield return new WaitForSeconds(1f);
        }
    }
}
