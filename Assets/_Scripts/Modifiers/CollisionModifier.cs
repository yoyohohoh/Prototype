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
    [SerializeField] string newTag = "none";
    [SerializeField] float delayDestroy = 0.5f;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (this.CompareTag("Collectible"))
        {
            this.GetComponent<Collider>().enabled = false;
            Item thisItem = this.GetComponent<Item>();
            PlayerController.Instance.UpdatePlayerData(thisItem._hp, thisItem._xp);
            Invoke("Stocking", delayDestroy);
        }

        if (this.CompareTag("Consumable"))
        {
            this.GetComponent<Collider>().enabled = false;
            Invoke("Stocking", delayDestroy);
        }

        if (this.CompareTag("Weapon"))
        {
            if (this.GetComponent<Weapon>()._status == WeaponStatus.Available)
            {
                this.GetComponent<Collider>().enabled = false;
                Invoke("Stocking", delayDestroy);
            }
        }

        if (this.CompareTag("NPC"))
        {
            NPC thisNPC = this.GetComponent<NPC>();
            PlayerController.Instance.UpdatePlayerData(-thisNPC._npcDamage, 0f);
        }

        if (this.CompareTag("Checkpoint"))
        {
            QuestManager.Instance.AddCheckPoint(this.gameObject);
        }

        if (!string.IsNullOrEmpty(newTag) && this.CompareTag(newTag))
        {
            Debug.Log($"Player collide {newTag}");
            PlayerController.Instance.UpdatePlayerData(100f);
            Destroy(this.gameObject);
        }
    }

    void Stocking()
    {
        InventoryManager.Instance.AddItem(this.gameObject);
        QuestManager.Instance.AddObjForQuest(QuestCategory.item, this.gameObject);
    }
}
