// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using UnityEngine;

public class CollisionModifier : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerData playerData = other.gameObject.GetComponent<PlayerController>()._playerData;

        if (this.CompareTag("Collectible"))
        {
            playerData.hp += this.GetComponent<Item>()._itemHp;
            playerData.xp += this.GetComponent<Item>()._itemXp;
            // Add to Inventory
        }

        if (this.CompareTag("Consumable") || this.CompareTag("Weapon"))
        {
            // Add to Inventory
        }

        if (this.CompareTag("NPC"))
        {
            playerData.hp -= this.GetComponent<NPC>()._npcDamage;
        }
    }

}
