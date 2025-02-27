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
    void Update()
    {
        
    }

    private void EndStart()
    {
        
    }

}
