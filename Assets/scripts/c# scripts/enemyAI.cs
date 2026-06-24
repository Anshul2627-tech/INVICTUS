using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int health = 3;
    public float attackCooldown = 1f;

    private NavMeshAgent agent;
    private Transform player;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.SetDestination(transform.position);
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;
        lastAttackTime = Time.time;

        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(10);
            Debug.Log("Enemy attacked player!");
        }
    }

    public void TakeDamage(int damage)
{
    health -= damage;
    if (health <= 0)
    {
        UIManager uiManager = FindAnyObjectByType<UIManager>();
        if (uiManager != null)
            uiManager.AddScore(10);

        Destroy(gameObject);
    }
}
}
