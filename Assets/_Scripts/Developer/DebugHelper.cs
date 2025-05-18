using UnityEngine;

public class DebugHelper : MonoBehaviour
{
    [SerializeField] public GameObject[] trackingObjs;
    [SerializeField] public bool isDebug = false;
    [SerializeField] public bool position = false;
    [SerializeField] public bool rotation = false;

    public bool isLevelUp = false;
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

        if(isLevelUp)
        {
            PlayerController.Instance.UpdatePlayerData(2);
        }

    }
}
