using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.UI.Image;
using System.Collections;

public class IdleState : NPCStateBase
{
    private Vector3 origin;
    private Vector3 destination;
    private Animator animator;
    public IdleState(NPCController npcController) : base(npcController)
    {
        origin = npcController.GetPosition(Location.Origin);
        destination = npcController.GetPosition(Location.Destination);
        animator = npcController.animator;
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
        
        animator.SetBool("isIdling", true);
    }

    public override void Exit()
    {
        npcController.gameObject.GetComponent<NPC>()._npcHp = 100f;

        animator.SetBool("isIdling", false);
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
    private Animator animator;
    public PatrolState(NPCController npcController) : base(npcController)
    {
        speed = npcController.gameObject.GetComponent<NPC>()._npcSpeed;
        origin = npcController.GetPosition(Location.Origin);
        destination = npcController.GetPosition(Location.Destination);
        animator = npcController.animator;
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

        animator.SetBool("isPatrolling", true);
    }

    public override void Exit()
    {
        animator.SetBool("isPatrolling", false);
    }

    public override void Update()
    {
        if (Vector3.Distance(npcController.transform.position, destination) < 1.5f)
        {
            npcController.GetComponent<NavMeshAgent>().destination = origin;
        }
    }
}

public class ChaseState : NPCStateBase
{
    private float speed;
    private float damage;
    private Animator animator;
    public ChaseState(NPCController npcController) : base(npcController) 
    {
        speed = npcController.gameObject.GetComponent<NPC>()._npcSpeed;
        damage = npcController.gameObject.GetComponent<NPC>()._npcDamage;
        animator = npcController.animator;
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

        animator.SetBool("isChasing", true);
    }

    public override void Exit()
    {
        animator.SetBool("isChasing", false);
    }

    public override void Update()
    {
        npcController.GetComponent<NavMeshAgent>().destination = PlayerController.Instance.GetCurrentPosition();
    }
}

public class DeadState : NPCStateBase
{
    private Vector3 origin;
    private Animator animator;
    private NavMeshAgent agent;
    public DeadState(NPCController npcController) : base(npcController)
    {
        origin = npcController.GetPosition(Location.Origin);
        animator = npcController.animator;
        agent = npcController.GetComponent<NavMeshAgent>();
    }

    public override void Enter()
    {
        npcController.gameObject.GetComponent<NPC>()._npcStatus = NPCStatus.Dead;

        agent.isStopped = true;
        agent.ResetPath();

        QuestManager.Instance.AddObjForQuest(QuestCategory.npc, npcController.gameObject);

        animator.SetTrigger("Dead");

        npcController.StartCoroutine(GoHome());

    }

    IEnumerator GoHome()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 2.0f);
        npcController.GetComponent<NavMeshAgent>().destination = origin;
        agent.isStopped = false;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if (Vector3.Distance(npcController.transform.position, origin) < 0.5f)
        {
            npcController.gameObject.SetActive(false);
        }
    }
}