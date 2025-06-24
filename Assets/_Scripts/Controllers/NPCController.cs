using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using static UnityEngine.UI.Image;
using Unity.VisualScripting;

public enum Location
{
    Origin,
    Destination
}
public class NPCController : MonoBehaviour
{
    public Vector3 destinationIndicator;
    [SerializeField] Transform _origin;
    [SerializeField] Transform _destination;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] public Animator animator;

    private NPCStateBase currentState;
    private Coroutine currentCoroutine;

    private bool isChasing = false;

    public bool IsChasing
    {
        get { return isChasing; }
    }

    public void SetLocation(Location location, Transform transform)
    {
        switch (location)
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
    public Vector3 GetPosition(Location location)
    {
        switch (location)
        {
            case Location.Origin:
                return _origin.position;
            case Location.Destination:
                return _destination.position;
            default:
                return Vector3.zero;
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
        SetState(new IdleState(this));
    }

    public void InvokePatrol(float timeGap)
    {
        if (currentCoroutine == null && this.gameObject.GetComponent<NPC>()._npcStatus != NPCStatus.Dead)
        {
            currentCoroutine = StartCoroutine(DelayedPatrol(timeGap));
        }
    }

    private IEnumerator DelayedPatrol(float timeGap)
    {
        yield return new WaitForSeconds(timeGap);
        SetState(new PatrolState(this));
        currentCoroutine = null;
    }

    private void Update()
    {
        if (this.gameObject.GetComponent<NPC>()._npcHp <= 0 && this.gameObject.GetComponent<NPC>()._npcStatus != NPCStatus.Dead)
        {
            SetState(new DeadState(this));
            return;
        }
        RotateHead();

        if (this.gameObject.GetComponent<NPC>()._npcStatus == NPCStatus.Patrol && 
            Vector3.Distance(this.transform.position, _origin.position) < 0.1f)
        {
            SetState(new IdleState(this));
        }

        if (this.gameObject.GetComponent<NPC>()._npcStatus == NPCStatus.Idle)
        {
            InvokePatrol(5f);
        }
        else if(!isChasing && this.gameObject.GetComponent<NPC>()._npcStatus != NPCStatus.Patrol)
        {
            InvokePatrol(0f);
        }
        DetectPlayer();
        

            
        destinationIndicator = _destination.position;
        currentState?.Update();
    }

    void RotateHead()
    {
        Vector3 velocity = this.gameObject.GetComponent<NavMeshAgent>().velocity;
        if (velocity.sqrMagnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(velocity.normalized);// movement direction
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position,
            50.0f,
            playerLayer
        );

        foreach (var hitCollider in hitColliders)
        {
            Vector3 directionToPlayer = (hitCollider.transform.position - transform.position).normalized;
            float distanceToPlayer = Vector3.Distance(transform.position, hitCollider.transform.position);

            int combinedMask = playerLayer | obstacleLayer;

            if (Physics.Raycast(
                    transform.position,
                    directionToPlayer,
                    out RaycastHit hitInfo,
                    distanceToPlayer + 0.1f,
                    combinedMask
                ))
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    isChasing = true;
                    SetState(new ChaseState(this));
                    return;
                }
            }
        }

        isChasing = false;

    }

}
