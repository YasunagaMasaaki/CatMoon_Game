using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    private Fade fade;

    void Start()
    {
        fade = FindObjectOfType<Fade>();
        fade.FadeStart(EndStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EndStart()
    {
        
    }

}
