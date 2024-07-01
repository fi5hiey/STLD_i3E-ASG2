/*
 * Author: Lim Wee Han
 * Date Created: 27/06/2024
 * Description: Axe Melee Script
 */

using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the player's melee attacks with an axe or similar weapon.
/// Handles attack mechanics, including collision detection and animation.
/// </summary>
public class Melee : MonoBehaviour
{
    /// <summary>
    /// The player's input handler for attack actions.
    /// </summary>
    [SerializeField] private StarterAssetsInputs inputs;

    /// <summary>
    /// The Animator component to play attack animations.
    /// </summary>
    [SerializeField] private Animator animator;

    /// <summary>
    /// The Camera component to cast attack rays from.
    /// </summary>
    [SerializeField] private Camera cam;

    /// <summary>
    /// The distance at which the melee attack can hit enemies.
    /// </summary>
    public float attackDistance = 3f;

    /// <summary>
    /// The delay between the start of the attack and the attack effect.
    /// </summary>
    public float attackDelay = 0.4f;

    /// <summary>
    /// The time between consecutive attacks.
    /// </summary>
    public float attackSpeed = 1f;

    /// <summary>
    /// The amount of damage dealt by the melee attack.
    /// </summary>
    public int attackDamage;

    /// <summary>
    /// The LayerMask to define which layers are considered for attack hits.
    /// </summary>
    public LayerMask attackLayer;

    /// <summary>
    /// The effect to play when the attack hits an enemy.
    /// </summary>
    public GameObject hitEffect;

    /// <summary>
    /// The sound effect for swinging the axe.
    /// </summary>
    public AudioClip swordSwing;

    /// <summary>
    /// The sound effect for the attack action.
    /// </summary>
    public AudioClip attackSound;

    /// <summary>
    /// The AudioSource component for playing attack sounds.
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// Flag to check if the player is currently attacking.
    /// </summary>
    bool attacking = false;

    /// <summary>
    /// Flag to check if the player is ready to perform an attack.
    /// </summary>
    bool readyToAttack = true;

    /// <summary>
    /// Counter to track the number of attacks performed.
    /// </summary>
    int attackCount;

    /// <summary>
    /// Detects collisions with objects and applies damage if the object is an enemy.
    /// </summary>
    /// <param name="collision">The Collision data for the collision event.</param>
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(attackDamage);
        }
    }

    /// <summary>
    /// Checks for attack input from the player and triggers the attack if ready.
    /// </summary>
    private void Update()
    {
        if (inputs.attack)
        {
            animator.Play("Swing");
            AttacK();
        }
    }

    /// <summary>
    /// Initiates the attack process by setting flags and scheduling attack events.
    /// </summary>
    public void AttacK()
    {
        if (!readyToAttack || attacking)
        {
            return;
        }

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(swordSwing);
    }

    /// <summary>
    /// Resets the attack flags to allow for the next attack.
    /// </summary>
    public void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    /// <summary>
    /// Performs a raycast attack to deal damage to enemies within range.
    /// </summary>
    public void AttackRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            hit.transform.gameObject.GetComponent<EnemyAI>().TakeDamage(attackDamage);
        }
    }
}
