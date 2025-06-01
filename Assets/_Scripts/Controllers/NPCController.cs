using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;
public enum Location
{
    Origin,
    Destination
}
public class NPCController : MonoBehaviour
{
    private NPCStateBase currentState;
    [SerializeField] Transform _origin;
    [SerializeField] Transform _destination;

    
    public void SetLocation(Location location, Transform transform)
    {
        switch(location)
        {
            case Location.Origin:
                _origin = transform;
                break;
            case Location.Destination:
                _destination = transform;
                break;
            default:
                Debug.LogError("Invalid location specified.");
                break;
        }
    }

    public void SetState(NPCStateBase newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    private void Start()
    {
        this.gameObject.transform.position = _origin.position;
        InvokePatrol();
    }

    public Vector3 GetPosition(Location location)
    {
        switch(location)
        {
            case Location.Origin:
                return _origin.position;
            case Location.Destination:
                return _destination.position;
            default:
                return Vector3.zero;
        }
    }
    public void InvokePatrol()
    {
        Invoke("OnPatrol", 5f);
    }
    void OnPatrol()
    {
        SetState(new PatrolState(this));
    }

    private void Update()
    {
        Vector3 velocity = this.gameObject.GetComponent<NavMeshAgent>().velocity;
        if (velocity.sqrMagnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(velocity.normalized);// movement direction
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        if (this.gameObject.GetComponent<NPC>()._npcHp <= 0)
        {
            SetState(new DeadState(this));
        }

        currentState?.Update();
    }
}
