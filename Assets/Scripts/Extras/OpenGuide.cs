using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenGuide : MonoBehaviour
{
    public GameObject HelpCanvas;

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
}
