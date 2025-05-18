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
            Item thisItem = this.GetComponent<Item>();
            playerData.hp += thisItem._hp;
            playerData.xp += thisItem._xp;
            InventoryManager.Instance.AddItem(this.gameObject);
        }

        if (this.CompareTag("Consumable") || this.CompareTag("Weapon"))
        {
            InventoryManager.Instance.AddItem(this.gameObject);
        }

        if (this.CompareTag("NPC"))
        {
            NPC thisNPC = this.GetComponent<NPC>();
            playerData.hp -= thisNPC._npcDamage;
        }
    }

}
