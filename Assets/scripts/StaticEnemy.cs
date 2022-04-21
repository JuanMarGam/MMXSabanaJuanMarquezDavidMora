using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    [SerializeField] GameObject bullet_enemy;
    [SerializeField] float firerate;
    Animator myAnim;
    float nextbullet=0;
    bool Detected = false;
    [SerializeField] float vida;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.left, 10f, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, Vector2.left * 10f, Color.red);
        Detected = (ray.collider != null);
        Fire();
        if (vida == 0)
        {
            myAnim.SetBool("isDeath", true);

        }
        else
            myAnim.SetBool("isDeath", false);
    }

    void Fire()
    {
        if (Detected && Time.time > nextbullet)
        {

            nextbullet = Time.time + firerate;
            Instantiate(bullet_enemy, transform.position, transform.rotation);
            myAnim.SetBool("PlayerDetected", true);
        }
        if(Detected== false)
        {
            myAnim.SetBool("PlayerDetected", false);
        }
           
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "bullet(Clone)")
        {
            vida -= 1;
        }

    }

}
