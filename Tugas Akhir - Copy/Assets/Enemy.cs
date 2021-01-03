using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public SpriteRenderer sr;

    public int maxHealth = 100;
    int currentHealth;

    public float attackRange = 0.5f;
    public int attackDelay = 2;
    float lastAttacked = -9999;
    public LayerMask playerLayers;

    //materials
    private Material matWhite;
    private Material matDefault;

    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;

        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Attack();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        sr.material = matWhite;
        Debug.Log("Kena");
        if (currentHealth <= 0)
        {
            Invoke("ResetMaterial", .03f);
            Die();
        }
        else
        {
            KnockBack();
            Invoke("ResetMaterial", .1f);
        }
    }

    private void KnockBack()
    {
        // Update position
        Debug.Log("Mundur");
        if (transform.position.x < player.position.x)
        {
            rigidbody.AddForce(transform.right * -50000);
        }
        else
        {
            rigidbody.AddForce(transform.right * 50000);
        }

    }

    void ResetMaterial()
    {
        sr.material = matDefault;
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        animator.SetBool("isDead", true);

        //animator.SetBool("PlayerApproach",false);
        Destroy(gameObject, 2);
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Enemy>().enabled = false;
        GetComponent<Enemy_SKL>().enabled = false;
        GetComponent<EnemyGhost>().enabled = false;
    }

    void Attack()
    {
        //Detect player
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayers);
        //Damage him
        foreach (Collider2D player in hitPlayer)
        {
            if (Time.time > lastAttacked + attackDelay)
            {
                animator.SetBool("Attack", true);
                player.GetComponent<Heart>().TakeDamage();
                lastAttacked = Time.time;
            }
        }
    }

}