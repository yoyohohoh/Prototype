using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.UI.Image;

public class IdleState : NPCStateBase
{
    private Vector3 origin;
    private Vector3 destination;
    public IdleState(NPCController npcController) : base(npcController)
    {
        origin = npcController.GetPosition(Location.Origin);
        destination = npcController.GetPosition(Location.Destination);
    }

    public override void Enter()
    {
        npcController.gameObject.SetActive(true);
        npcController.gameObject.GetComponent<NPC>()._npcStatus = NPCStatus.Idle;
        
        var agent = npcController.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        agent.ResetPath();

        Vector3 offset = new Vector3(1, 0, 1);
        npcController.GetComponent<NavMeshAgent>().Warp(origin + offset);
    }

    public override void Exit()
    {

        npcController.gameObject.GetComponent<NPC>()._npcHp = 100f;
    }

    public override void Update()
    {

    }
}

public class PatrolState : NPCStateBase
{
    private Vector3 origin;
    private Vector3 destination;
    public PatrolState(NPCController npcController) : base(npcController)
    {
        origin = npcController.GetPosition(Location.Origin);
        destination = npcController.GetPosition(Location.Destination);
    }

    public override void Enter()
    {
        npcController.gameObject.SetActive(true);
        npcController.gameObject.GetComponent<NPC>()._npcStatus = NPCStatus.Patrol;

        var agent = npcController.GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        agent.ResetPath();

        npcController.GetComponent<NavMeshAgent>().destination = destination;
        npcController.gameObject.GetComponent<NavMeshAgent>().speed = 10f;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if (Vector3.Distance(npcController.transform.position, destination) < 1.5f)
        {
            npcController.GetComponent<NavMeshAgent>().destination = origin;
        }

        if (Vector3.Distance(npcController.transform.position, origin) < 0.5f)
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
        origin = npcController.GetPosition(Location.Origin);
    }

    public override void Enter()
    {
        npcController.gameObject.GetComponent<NPC>()._npcStatus = NPCStatus.Dead;
        npcController.gameObject.transform.position = origin;
        QuestManager.Instance.AddObjForQuest(QuestCategory.npc, npcController.gameObject);
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        npcController.gameObject.SetActive(false);
    }
}