using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField, Header("HPÉAÉCÉRÉì")]
    private GameObject playerIcon;
    private PlayerController player;
    private int beforeHP;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        beforeHP = player.GetHP();
        CreateHPIcon();
    }

    private void CreateHPIcon()
    {
        for(int i = 0; i < player.GetHP(); i++)
        {
            GameObject playerHPObj = Instantiate(playerIcon);
            playerHPObj.transform.SetParent(transform, false);
        }
    }

    void Update()
    {
        ShowHpIcon();
    }

    private void ShowHpIcon()
    {
        if (beforeHP == player.GetHP()) return;

        Image[] icons = transform.GetComponentsInChildren<Image>();
        
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].gameObject.SetActive(i < player.GetHP());
        }
        beforeHP = player.GetHP();
    }
}
