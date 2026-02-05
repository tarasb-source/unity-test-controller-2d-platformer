using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Damage, Health")]
    public int damage;
    public int maxHealth = 100;
    int currentHealth;
    public Health playerHealth;

    [Header("Speed, Knockback")]
    public float speed;
    public float knockbackForce = 5f;

    private Rigidbody2D rb;
    private GameObject player;
    public Animator animator;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (player != null)
        {
            Chase();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with Player!");

            // Apply damage if the player has a health script
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Apply knockback to the enemy
            //ApplyKnockback(collision);
        }
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (animator.GetBool("IsDead") == false)
        {
            currentHealth -= damage;
            //Play hurt animation
            animator.SetTrigger("Hurt");

            if (currentHealth < 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        //Play die animation
        Debug.Log("Enemy died!");
        animator.SetBool("IsDead", true);
     
        //GetComponent<Collider2D>().enabled = false;
        //this.enabled = false;
    }
}
