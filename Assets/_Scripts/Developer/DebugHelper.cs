using UnityEngine;

public class DebugHelper : MonoBehaviour
{
    [SerializeField] public GameObject[] trackingObjs;
    [SerializeField] public bool isDebug = false;
    [SerializeField] public bool position = false;
    [SerializeField] public bool rotation = false;
    [SerializeField] public bool gameSave = false;
    [SerializeField] public bool gameLoad = false;
    bool isSave = false;
    bool isLoad = false;

    public bool isLevelUp = false;
    bool isLevelUpDone = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDebug)
        {
            foreach (GameObject trackingObj in trackingObjs)
            {
                if (trackingObj != null)
                {
                    if (position)
                    {
                        Debug.Log($"{trackingObj.name} Position: ({trackingObj.transform.position.x}, {trackingObj.transform.position.y}, {trackingObj.transform.position.z})");
                    }

                    if (rotation)
                    {
                        Debug.Log($"{trackingObj.name} Rotation: ({trackingObj.transform.rotation.x}, {trackingObj.transform.rotation.y}, {trackingObj.transform.rotation.z})");
                    }
                }
                else
                {
                    Debug.LogWarning("Tracking object is null.");
                }
            }
        }

        if (isLevelUp && !isLevelUpDone)
        {
            PlayerController.Instance.UpdatePlayerData(2);
            isLevelUpDone = true;
        }

        if (gameSave && !isSave)
        {
            LevelManager.Instance.GameSave();
            isSave = true;
        }
        if (gameLoad && !isLoad)
        {
            PlayerController.Instance._playerData = GameSaveManager.Instance().LoadPlayerData();
        }
    }
}
