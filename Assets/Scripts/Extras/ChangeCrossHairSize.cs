using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCrossHairSize : MonoBehaviour
{
    public Image CrossHair;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InvokeOriginalSize()
    {
        CrossHair.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void changeSize(float time)
    {
        CrossHair.rectTransform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        Invoke("InvokeOriginalSize", time);
    }

}
