using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;

public class NPCController : MonoBehaviour
{
    private NPCStateBase currentState;
    public Transform _origin;
    public Transform _destination;

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
