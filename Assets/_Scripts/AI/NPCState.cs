using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.UI.Image;

public class IdleState : NPCStateBase
{
    private Vector3 origin;
    public IdleState(NPCController npcController) : base(npcController)
    {
        origin = npcController._origin.position;
    }

    public override void Enter()
    {
        npcController.gameObject.GetComponent<NPC>()._npcStatus = NPCStatus.Idle;
        npcController.gameObject.SetActive(true);
        npcController.GetComponent<NavMeshAgent>().destination = origin;
    }

    public override void Exit()
    {
        npcController.gameObject.GetComponent<NPC>()._npcHp = 100f;
    }

    public override void Update()
    {
        if (Vector3.Distance(npcController.transform.position, origin) < 1.5f)
        {
            npcController.InvokePatrol();
        }
    }
}

public class PatrolState : NPCStateBase
{
    private Vector3 destination;
    public PatrolState(NPCController npcController) : base(npcController) 
    {
        destination = npcController._destination.position;
    }

    public override void Enter()
    {
        npcController.gameObject.GetComponent<NPC>()._npcStatus = NPCStatus.Patrol;
        npcController.gameObject.SetActive(true);
        npcController.GetComponent<NavMeshAgent>().destination = destination;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if (Vector3.Distance(npcController.transform.position, destination) < 1.5f)
        {
            npcController.SetState(new IdleState(npcController));
        }
    }
}

public class AttackState : NPCStateBase
{
    public AttackState(NPCController npcController) : base(npcController) { }

    public override void Enter()
    {
        npcController.gameObject.GetComponent<NPC>()._npcStatus = NPCStatus.Attack;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {

    }
}

public class DeadState : NPCStateBase
{
    private Vector3 origin;
    public DeadState(NPCController npcController) : base(npcController) 
    {
        origin = npcController._origin.position;
    }

    public override void Enter()
    {
        npcController.gameObject.GetComponent<NPC>()._npcStatus = NPCStatus.Dead;
        npcController.gameObject.transform.position = origin;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        npcController.gameObject.SetActive(false);
    }
}