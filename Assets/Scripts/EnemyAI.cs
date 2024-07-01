/*
 * Author: Lim Wee Han
 * Date: 25/06/2024
 * Description: Enemy, NavMesh
 */

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    private Transform player;

    public LayerMask Ground, Player;

    public float maxHealth;
    private float health;
    [SerializeField] private int damage;
    [SerializeField] private Image healthbar;
    [SerializeField] private GameObject healthBarCanvas;
    private Camera cam;
    private Animator animator;
    private GameManager gameManager;

    //Patrol
    private Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attack
    public float timeBetweenAttacks;
    bool attacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttack;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Initializes the main camera, animator, player reference, NavMeshAgent, health, and GameManager.
    /// </summary>
    private void Awake()
    {
        cam = Camera.main;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// Called every frame to check the player's position and update the enemy's state.
    /// Also rotates the health bar to face the camera.
    /// </summary>
    private void Update()
    {
        if (health >= 0)
        {
            // Check if Player is in sight/attack range
            playerInSight = Physics.CheckSphere(transform.position, sightRange, Player);
            playerInAttack = Physics.CheckSphere(transform.position, attackRange, Player);

            // Change Enemy State
            if (!playerInSight && !playerInAttack) Patrol();
            if (playerInSight && !playerInAttack) Chase();
            if (playerInSight && playerInAttack) Attack();
        }

        // Rotate Healthbar
        healthBarCanvas.transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }

    /// <summary>
    /// Updates the health bar based on the enemy's current and maximum health.
    /// </summary>
    /// <param name="health">The current health of the enemy.</param>
    /// <param name="maxHealth">The maximum health of the enemy.</param>
    public void UpdateHealthBar(float health, float maxHealth)
    {
        healthbar.fillAmount = health / maxHealth;
    }

    /// <summary>
    /// Handles the enemy's patrol behavior when the player is not in sight or attack range.
    /// </summary>
    private void Patrol()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    /// <summary>
    /// Searches for a random walk point within the patrol range.
    /// </summary>
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Check that random point is ON the map
        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
        {
            walkPointSet = true;
        }
    }

    /// <summary>
    /// Handles the enemy's chase behavior when the player is in sight but not in attack range.
    /// </summary>
    private void Chase()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }

    /// <summary>
    /// Handles the enemy's attack behavior when the player is in sight and within attack range.
    /// </summary>
    private void Attack()
    {
        Chase();
    }

    /// <summary>
    /// Resets the attack state of the enemy after the attack delay.
    /// </summary>
    private void ResetAtk()
    {
        attacked = false;
    }

    /// <summary>
    /// Reduces the enemy's health by the specified damage amount.
    /// Updates the health bar and destroys the enemy if health drops to zero or below.
    /// </summary>
    /// <param name="damage">The amount of damage taken by the enemy.</param>
    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            agent.SetDestination(transform.position);
            Invoke(nameof(DestroyEnemy), 1f);
        }
    }

    /// <summary>
    /// Called when a collider enters the trigger collider attached to this object.
    /// Damages the player if the player is in the attack range and the enemy is not currently attacking.
    /// </summary>
    /// <param name="other">The Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!attacked)
            {
                gameManager.PlayerDamage(damage);

                attacked = true;
                Invoke(nameof(ResetAtk), timeBetweenAttacks);
            }
        }
    }

    /// <summary>
    /// Called when a collider stays within the trigger collider attached to this object.
    /// Damages the player if the player is in the attack range and the enemy is not currently attacking.
    /// </summary>
    /// <param name="other">The Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!attacked)
            {
                gameManager.PlayerDamage(damage);

                attacked = true;
                Invoke(nameof(ResetAtk), timeBetweenAttacks);
            }
        }
    }

    /// <summary>
    /// Destroys the enemy game object.
    /// </summary>
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
