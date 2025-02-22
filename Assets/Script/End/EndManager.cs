using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    private bool bStart;
    private Fade fade;

    void Start()
    {
        bStart = false;
        fade = FindObjectOfType<Fade>();
        fade.FadeStart(EndStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EndStart()
    {
        bStart=true;
    }

}
