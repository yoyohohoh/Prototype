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
        
        if (this.CompareTag("Collectible"))
        {
            Item thisItem = this.GetComponent<Item>();
            PlayerController.Instance.UpdatePlayerData(thisItem._hp, thisItem._xp);
            InventoryManager.Instance.AddItem(this.gameObject);
        }

        if (this.CompareTag("Consumable"))
        {
            InventoryManager.Instance.AddItem(this.gameObject);
        }

        if (this.CompareTag("Weapon"))
        {
            if(this.GetComponent<Weapon>()._status == WeaponStatus.Available)
            {
                InventoryManager.Instance.AddItem(this.gameObject);
            }          
        }

        if (this.CompareTag("NPC"))
        {
            NPC thisNPC = this.GetComponent<NPC>();
            PlayerController.Instance.UpdatePlayerData(-thisNPC._npcDamage, 0f);
        }
    }

}
