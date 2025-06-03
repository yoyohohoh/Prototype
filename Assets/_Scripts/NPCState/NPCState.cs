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

        npcController.gameObject.GetComponent<NavMeshAgent>().speed = 0.0f;

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
    private float speed;
    private Vector3 origin;
    private Vector3 destination;
    public PatrolState(NPCController npcController) : base(npcController)
    {
        speed = npcController.gameObject.GetComponent<NPC>()._npcSpeed;
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
        npcController.gameObject.GetComponent<NavMeshAgent>().speed = speed;
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
    }
}

public class AttackState : NPCStateBase
{
    private float speed;
    private float damage;
    public AttackState(NPCController npcController) : base(npcController) 
    {
        speed = npcController.gameObject.GetComponent<NPC>()._npcSpeed;
        damage = npcController.gameObject.GetComponent<NPC>()._npcDamage;
    }

    public override void Enter()
    {
        npcController.gameObject.SetActive(true);
        npcController.gameObject.GetComponent<NPC>()._npcStatus = NPCStatus.Attack;

        var agent = npcController.GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        agent.ResetPath();

        npcController.GetComponent<NavMeshAgent>().destination = PlayerController.Instance.GetCurrentPosition();
        npcController.gameObject.GetComponent<NavMeshAgent>().speed = speed * 2;
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        npcController.GetComponent<NavMeshAgent>().destination = PlayerController.Instance.GetCurrentPosition();
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