using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenStats : MonoBehaviour
{
    public GameObject StatsCanvas;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OpenStatsCanvas()
    {
        StatsCanvas.SetActive(true);
    }

    public void CloseStatsCanvas()
    {
        StatsCanvas.SetActive(false);
    }
}
