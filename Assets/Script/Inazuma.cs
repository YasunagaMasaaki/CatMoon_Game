using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inazuma : MonoBehaviour
{
    [SerializeField, Header("çUåÇóÕ")]
    private int attackPower;//1

    public void PlayerDamage(PlayerController player, Vector2 knockbackDir)
    {
        player.Damage(attackPower, knockbackDir);
    }
}
