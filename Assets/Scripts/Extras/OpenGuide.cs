using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenGuide : MonoBehaviour
{
    public GameObject HelpCanvas;
    public GameObject SummaryCanvas;
    public GameObject UILayoutCanvas;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OpenGuideCanvas()
    {
        HelpCanvas.SetActive(true);
    }

    public void CloseGuideCanvas()
    {
        HelpCanvas.SetActive(false);
    }

    public void OpenSummaryCanvas()
    {
        SummaryCanvas.SetActive(true);
    }

    public void CloseSummaryCanvas()
    {
        SummaryCanvas.SetActive(false);
    }
    
    public void OpenUILayoutCanvas()
    {
        UILayoutCanvas.SetActive(true);
    }

    public void CloseUILayoutCanvas()
    {
        UILayoutCanvas.SetActive(false);
    }
}
