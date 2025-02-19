using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIButton : MonoBehaviour
{
    [SerializeField] private Button reStartButton;
    [SerializeField] private Button tittleButton;
    
    void Start()
    {
        reStartButton.onClick.AddListener(OnReStartButtonClick);
        tittleButton.onClick.AddListener(OnTittleButtonClick);
    }

    void OnReStartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnTittleButtonClick()
    {
        SceneManager.LoadScene("Tittle");
    }
}
