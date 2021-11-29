using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool isJump = true;
    bool isDead = false;
    bool isSlide = false;
    int idMove = 0;
    Animator anim;
    bool isStart = false;

    int platformTimer = 5;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                Idle();
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                Idle();
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Slide();
            }
            Move();
        }
    }
    public void GameStart()
    {
        isStart = true;
    }

    public void GameFinish()
    {
        isStart = false;
    }

    public void Idle()
    {
        if (!isJump && !isSlide)
        {
            anim.ResetTrigger("jump");
            anim.ResetTrigger("run");
            anim.SetTrigger("idle");
        }
        idMove = 0;
    }

     public void MoveRight()
    {
        idMove = 1;
    }

    public void MoveLeft()
    {
        idMove = 2;
    }

    public void Move()
    {
        //Move Right
        if(idMove == 1 && !isDead && !isSlide)
        {
            if (!isJump) anim.SetTrigger("run");
            transform.Translate(1 * Time.deltaTime * 5f, 0, 0);
            transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f);
        }

        //Move Left
        if (idMove == 2 && !isDead && !isSlide)
        {
            if (!isJump) anim.SetTrigger("run");
            transform.Translate(-1 * Time.deltaTime * 5f, 0, 0);
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }

        /*//Slide
        if (isSlide && idMove == 1)
        {
            if (!isJump) anim.SetTrigger("slide");
            //gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10f);
        }
        if (isSlide && idMove == 2)
        {
            if (!isJump) anim.SetTrigger("slide");
            //gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10f);
        }*/
    }

    public void Slide()
    {
        
        if (!isJump && idMove == 1)
        {
            isSlide = true;
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 700f);
        }
        if (!isJump && idMove == 2)
        {
            isSlide = true;
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 700f);
        }
    }

    public void Jump()
    {
        if (!isJump)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000f);
        }
    }

    public void Dead()
    {
        this.gameObject.SetActive(false);
        isStart = false;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //When touching ground
        if (isJump)
        {
            anim.ResetTrigger("jump");
            anim.ResetTrigger("slide");
            if (idMove == 0) anim.SetTrigger("idle");
            isJump = false;
            Data.jump = false;
            isSlide = false;
        }
        if (isSlide)
        {
            anim.SetTrigger("slide");
            StartCoroutine("StopSlide");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        anim.SetTrigger("jump");
        anim.ResetTrigger("run");
        anim.ResetTrigger("idle");
        anim.ResetTrigger("slide");
        isJump = true;
        isSlide = false;
        Data.jump = true;
    }

    IEnumerator StopSlide()
    {
        
        yield return new WaitForSeconds(0.5f);
        isSlide = false;
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move();
        } else
        {
            anim.SetTrigger("idle");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Data.score += 1;
            Destroy(collision.gameObject);
        }
    }
}
