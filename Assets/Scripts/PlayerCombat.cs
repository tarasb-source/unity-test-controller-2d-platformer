using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 30;
    public LayerMask enemyLayer;

    public bool isAttacking = false;
    private bool canAttack = true; // Prevents spamming attacks
    public float attackRate = 0.5f; // Cooldown time in seconds
    float nextAttackTime = 0f;
    private void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Attack"].performed += Attack;
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            canAttack = true;
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && canAttack)
        {
            animator.SetTrigger("Sword");
            //PerformAttack();
            canAttack = false;
            isAttacking = true;
            nextAttackTime = Time.time + attackRate;
        }
    }

    private void PerformAttack()
    {
        // Detect enemies in the attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Assuming enemies have a script with a "TakeDamage" method
            Debug.Log("Hit " + enemy.name);
            enemy.GetComponent<EnemyScript>().TakeDamage(attackDamage); // Damage the enemy
        }
    }

    private void ResetAttackCheck()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        // Visualize the attack range in the scene view
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
