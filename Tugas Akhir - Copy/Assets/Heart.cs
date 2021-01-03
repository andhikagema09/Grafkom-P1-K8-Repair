using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public int health;
    public int numOfHearth;

    public Image[] hearths;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Animator animator;

    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health > numOfHearth)
        {
            health = numOfHearth;
        }

        for (int i = 0; i < hearths.Length; i++)
        {
            if (i < health)
            {
                hearths[i].sprite = fullHeart;
            }
            else
            {
                hearths[i].sprite = emptyHeart;
            }

            if (i < numOfHearth)
            {
                hearths[i].enabled = true;
            }
            else
            {
                hearths[i].enabled = false;
            }
        }
        if (health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("Death", true);
        animator.SetFloat("Speed", 0);
        animator.SetInteger("isJumping", 0);
        animator.Play("Hawk_Death");
        //FindObjectOfType<GameManager>().EndGame();
        GetComponent<CharacterController2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        this.enabled = false;
    }

    public void TakeDamage()
    {
        Debug.Log("Ouch");
        animator.SetBool("isHit", true);
        if (health > 0)
        {
            health--;
        }
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Enemy")
            Debug.Log("Enemy");
    }
}
