using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float punchRange = 3f;
    public int punchDamage = 1;
    public float punchCooldown = 0.5f;

    private float lastPunchTime;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPunch();
        }
    }

    void TryPunch()
    {
        if (Time.time - lastPunchTime < punchCooldown) return;
        lastPunchTime = Time.time;

        Collider[] hits = Physics.OverlapSphere(transform.position, punchRange);
        foreach (Collider hit in hits)
        {
            EnemyAI enemy = hit.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(punchDamage);
                Debug.Log("HIT ENEMY!");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, punchRange);
    }
}
