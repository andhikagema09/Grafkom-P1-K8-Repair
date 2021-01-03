using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    public bool followVerticaly = true;

    [SerializeField]
    float enemyRange;

    [SerializeField]
    float stopingDistance;

    [SerializeField]
    float moveSpeed;
    new Rigidbody2D rigidbody2D;

    [SerializeField]
    Animator animator;

    float distToPlayer;
    Vector3 defaultPosition;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //distance to player
        distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < enemyRange && distToPlayer > stopingDistance)
        {
            //code to chase player
            ChasePlayer();
        }
        else
        {
            //stop chasing player
            StopChasingPlayer();
        }
    }

    void StopChasingPlayer()
    {
        if (distToPlayer > enemyRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, defaultPosition, moveSpeed * Time.deltaTime);
            if (transform.position.x == defaultPosition.x) animator.SetBool("canWalk", false);
            if (transform.position.x < defaultPosition.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
        }

    }

    void ChasePlayer()
    {
        //player in right of enemy, move right
        if (followVerticaly) transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), moveSpeed * Time.deltaTime);
        }
        animator.SetBool("canWalk", true);
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }

    }
}
