using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{ 
    [SerializeField] GameObject Mbullet;
    [SerializeField] float speed;
    [SerializeField] int jumpForce;
    [SerializeField] float firerate;
    [SerializeField] private AudioClip bullet_sound;
    [SerializeField] private AudioClip death_sound;
    AudioSource _audioSource;
    Rigidbody2D myBody;
    Animator myAnim;
    Transform TransformBullet;
    bool isGrounded = true;
    private float nextbullet = 0;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
            Debug.Log("The audio is null");
        TransformBullet = Mbullet.GetComponent<Transform>();
        
    }

    IEnumerator GameOverCorutina()
    {

        myAnim.SetBool("isDeath", true);
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);
        AudioSource.PlayClipAtPoint(death_sound, transform.position, 3);
        yield return new WaitForSecondsRealtime(1);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;


    }

    

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1.3f,LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * 1.3f, Color.red);
        isGrounded = (ray.collider != null);
        Jump();
        Fire();


    }
    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Time.time > nextbullet)
        {
            myAnim.SetLayerWeight(1, 1);
            nextbullet = Time.time + firerate;
            Instantiate(Mbullet, transform.position, transform.rotation);
            _audioSource.PlayOneShot(bullet_sound);

        }
        else
        {
            myAnim.SetLayerWeight(1, 0);
        }
        
    }
    void Jump()
    {
        if (isGrounded)
            
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                myBody.AddForce(new Vector2(0, jumpForce),ForceMode2D.Impulse);
            }
        }
        if (myBody.velocity.y != 0 && !isGrounded)
            myAnim.SetBool("isJumping", true);
        else
            myAnim.SetBool("isJumping", false);

    }
    void FinishingRun()
    {
        //Debug.Log("Termina animacion de correr");
    }

    public void FixedUpdate()
    {
        float dirH = Input.GetAxis("Horizontal");
        
        if (dirH != 0)
        {
            myAnim.SetBool("isRunning", true);
            if (dirH < 0)
            {
                transform.localScale = new Vector2(-1, 1);
                TransformBullet.transform.localScale = new Vector2(-1, 1);

            }
            else
            {
                transform.localScale = new Vector2(1, 1);
                TransformBullet.transform.localScale = new Vector2(1, 1);

            }


        }
        else
            myAnim.SetBool("isRunning", false);

        myBody.velocity = new Vector2(dirH * speed, myBody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        {
          StartCoroutine("GameOverCorutina");

           
        }

    }
}
