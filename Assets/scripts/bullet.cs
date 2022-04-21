using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Animator myAnim;
    Rigidbody2D myBody;
    [SerializeField] float speed_x;
    float speed_y=0;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject)
        {
            myAnim.SetBool("wasImpacted", true);

        }
        
    }
    private void FixedUpdate()
    {
        if(transform.localScale.x>0)
            myBody.velocity = new Vector2(speed_x, speed_y);
        else
            myBody.velocity = new Vector2(-speed_x, speed_y);


    }

}
