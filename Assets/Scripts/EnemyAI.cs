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

    private void Awake()
    {

        cam = Camera.main;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (health >= 0)
        {
            //Check if Player is in sight/attack range
            playerInSight = Physics.CheckSphere(transform.position, sightRange, Player);
            playerInAttack = Physics.CheckSphere(transform.position, attackRange, Player);

            //Change Enemy State 
            if (!playerInSight && !playerInAttack) Patrol();
            if (playerInSight && !playerInAttack) Chase();
            if (playerInSight && playerInAttack) Attack();
        }

        //Rotate Healthbar
        healthBarCanvas.transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }

    public void UpdateHealthBar(float health, float maxHealth)
    {
        healthbar.fillAmount = health / maxHealth;
    }

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

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //Check that random point is ON the map
        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
        {
            walkPointSet = true;
        }
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }

    private void Attack()
    {
        Chase();
    }

    private void ResetAtk()
    {
        attacked = false;
    }

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

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
