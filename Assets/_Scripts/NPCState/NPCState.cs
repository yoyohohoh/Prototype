using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using static UnityEngine.UI.Image;

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

public class AttackState : NPCStateBase
{
    private float speed;
    private float damage;
    private Animator animator;
    public AttackState(NPCController npcController) : base(npcController) 
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

        if (Vector3.Distance(npcController.transform.position, PlayerController.Instance.GetCurrentPosition()) <= 0.5f)
        {
            animator.SetTrigger("Attack");
        }
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
        GameObject reward = UnityEngine.Object.Instantiate(npcController.reward, npcController.gameObject.transform.position, Quaternion.identity);
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

public class WinState : NPCStateBase
{
    private Animator animator;
    private NavMeshAgent agent;
    public WinState(NPCController npcController) : base(npcController)
    {
        animator = npcController.animator;
        agent = npcController.GetComponent<NavMeshAgent>();
    }

    public override void Enter()
    {
        npcController.gameObject.GetComponent<NPC>()._npcStatus = NPCStatus.Win;

        agent.isStopped = true;
        agent.ResetPath();

        Vector3 endPos = GetRandomPointInFront(PlayerController.Instance.GetCurrentPosition(), PlayerController.Instance.transform.forward, 2f);
        npcController.gameObject.transform.position = endPos;
        npcController.gameObject.transform.LookAt(PlayerController.Instance.transform.position);

        animator.SetTrigger("Win");
    }

    Vector3 GetRandomPointInFront(Vector3 origin, Vector3 forward, float radius)
    {
        float angle = Random.Range(-90f, 90f);
        Quaternion rotation = Quaternion.Euler(0, angle, 0);

        Vector3 direction = rotation * forward.normalized;

        return origin + direction * radius;
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        
    }

}