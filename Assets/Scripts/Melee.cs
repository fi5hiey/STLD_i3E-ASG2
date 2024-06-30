/*
 * Author: Lim Wee Han
 * Date Created: 27/06/2024
 * Description: Axe Melee Script
 */

using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{

    [SerializeField] private StarterAssetsInputs inputs;
    [SerializeField] private Animator animator;
    [SerializeField] private Camera cam;

    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage;
    public LayerMask attackLayer;

    public GameObject hitEffect;
    public AudioClip swordSwing;
    public AudioClip attackSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(attackDamage);
        }
    }

    private void Update()
    {
        if (inputs.attack)
        {
            animator.Play("Swing");
            AttacK();
        }
    }

    public void AttacK()
    {
        if (!readyToAttack || attacking) {
            return;
        }

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

        /*audioSource.pitch = Random.Range(0.9f, 1.1f);
        AudioSource.playOneShot(swordSwing);*/
    }

    public void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    public void AttackRaycast()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            hit.transform.gameObject.GetComponent<EnemyAI>().TakeDamage(attackDamage);
        }
    }
}