using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : MonoBehaviour
{
    PlayerManager pm;
    CharacterStats myStats;

    private void Start()
    {
        pm = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }

    public void Interact()
    {
        CharacterCombat playerCombat = pm.player.GetComponent<CharacterCombat>();
        if(playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
}
