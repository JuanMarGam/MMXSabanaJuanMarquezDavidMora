using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float vida;
    AIPath myPath;
    Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        myPath = GetComponent<AIPath>();
        myAnim = GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {

        ChasePlayer();
        if (vida == 0)
        {
            myAnim.SetBool("isDeath", true);

        }
        else
            myAnim.SetBool("isDeath", false);


    }
    void ChasePlayer()
    {
        //Alternativa 1 - vector2.distance
        /*float d = Vector2.Distance(transform.position, player.transform.position);
        if(d<8)
        {

        }

        //Debug.DrawLine(transform.position, player.transform.position, Color.red);*/

        //Alternativa 2 - Overlap

        Collider2D col = Physics2D.OverlapCircle(transform.position,6f, LayerMask.GetMask("Player"));
        if (col != null)
        {
            myPath.isStopped = false;
        }
        else
            myPath.isStopped = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 6f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "bullet(Clone)")
        {
            vida -= 1;
        }
            
            
        
    }
}

