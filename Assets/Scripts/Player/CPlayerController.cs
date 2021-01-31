using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CPlayerController : MonoBehaviour
{
    GameObject m_player = null;
    SpriteRenderer m_sprite = null;
    public GameObject m_bomb = null;
    GameObject m_bombheld = null;
    //Rigidbody2D m_rigidbody = null;
    public Rigidbody2D m_rigidbody;
    public GameObject m_lastLadder = null;
    public Transform m_hand = null;

    internal CStateMachine<CPlayerController> m_fsm = null;
    internal CIdleState m_idleState = null;
    internal CMovingState m_movingState = null;
    internal CJumpingState m_jumpingState = null;
    internal CLadderState m_ladderState = null;

    public bool bkeyPressed = false;
    public bool bmoving = false;
    public bool btriggering = false;
    public bool bclimbLadder = false;
    public bool m_facingRight = true;
    public bool bholdingbomb = false;
    public bool binvincible = false;

    public int m_lives = 5;
    public int m_fossil = 0;
    int maxfossil = 4;
    public float m_lastInvencibility = 0;
    [SerializeField] float m_invincibleTime = 2f;
    [SerializeField] float m_speed = 5f;
    [SerializeField] float m_jumpForce = 400f;
    [SerializeField] float m_climbSpeed = 2.5f;

    [SerializeField] internal int m_maxJumps = 2;
    [SerializeField] internal  int m_jumpsLeft = 2;

    [SerializeField] Vector2 bThrowForce;

    //Vector3 movement;
    Vector2 jumpForce;
    Vector2 moveForce;
    
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.Find("Player");
        m_sprite = GetComponent<SpriteRenderer>();
        //m_rigidbody = m_player.GetComponent<Rigidbody2D>();
        m_fsm = new CStateMachine<CPlayerController>();
        m_idleState = new CIdleState(m_fsm);
        m_movingState = new CMovingState(m_fsm);
        m_jumpingState = new CJumpingState(m_fsm);
        m_ladderState = new CLadderState(m_fsm);
        m_fsm.setCurrentState(m_idleState, this);

        jumpForce.y = m_jumpForce;
        moveForce.x = m_speed;

        
    }

    // Update is called once per frame
    void Update()
    {

        if (m_lives <= 0)
        {
            Application.Quit();
            //Debug.Log("deadxd");
        }

        if(m_fossil >= maxfossil)
        {
            Application.Quit();
            //Debug.Log("gratsxd");
        }

        m_fsm.update(this);
        bkeyPressed = false;
        bmoving = false;

        if (binvincible)
        {
            if (m_lastInvencibility < m_invincibleTime)
            {
                m_lastInvencibility += Time.deltaTime;
                float newAlpha = Mathf.Lerp(0, 1, m_lastInvencibility / 2);
                m_sprite.color = new Color(m_sprite.color.r, m_sprite.color.g, m_sprite.color.b, newAlpha);
                //Debug.Log("deberiamos estar sumando");
            }
            else
            {
                m_lastInvencibility = 0;
                binvincible = false;
            }
        }

        if (m_bombheld != null & !m_bombheld.GetComponent<CBomb>().bexplode)
        {
            if (bholdingbomb)
            {
                m_bombheld.transform.position = m_hand.transform.position;
                m_bombheld.transform.rotation = m_hand.transform.rotation;
            }
            
        }
        else
        {
            bholdingbomb = false;
        }

        
    }

    public void move()
    {
  
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            m_sprite.flipX = false;
            m_facingRight = true;
            if (m_hand.localPosition.x < 0)
            {
                m_hand.rotation = new Quaternion(0, 0, 0, 0);
                Vector3 pos = m_hand.localPosition;
                pos.x *= -1;
                m_hand.localPosition = pos;
            }

            m_rigidbody.velocity = new Vector2(m_speed, m_rigidbody.velocity.y);
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            m_sprite.flipX = true;
            m_facingRight = false;
            if(m_hand.localPosition.x > 0)
            {
                m_hand.rotation = new Quaternion(0, 180, 0, 0);
                Vector3 pos = m_hand.localPosition;
                pos.x *= -1;
                m_hand.localPosition = pos;
            }

            m_rigidbody.velocity = new Vector2(-m_speed, m_rigidbody.velocity.y);
        }
      
    }


    public void jump()
    {
        m_rigidbody.velocity = new Vector2 (0,1);
        m_rigidbody.AddForce(jumpForce);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ladder")
        {
            
            bclimbLadder = true;
            
        }
        else if (collision.gameObject.tag == "trap" & !binvincible)
        {
            binvincible = true;
            m_lives--;
        }
        else
        {
            if(m_lastLadder != null)
            {
                m_lastLadder.GetComponent<BoxCollider2D>().isTrigger = true;
                m_lastLadder = null;
            }
        }
        

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ladder")
        {
          
           

        }
        else if (collision.gameObject.tag == "trap" & !binvincible)
        {
            binvincible = true;
            m_lives--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ladder")
        {
            bclimbLadder = true;
        }
        else if(collision.gameObject.tag == "fossil")
        {
            m_fossil++;
            Destroy(collision.gameObject);
        }
        btriggering = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ladder")
        {
            bclimbLadder = true;
        }
        //else if (collision.gameObject.tag == "fossil")
        //{
        //    m_fossil++;
        //    Destroy(collision.gameObject);
        //}
        btriggering = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ladder")
        {
            bclimbLadder = false;
        }
        btriggering = false;
    }

    public void climb()
    {
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, m_climbSpeed);
        }
        else if (Input.GetAxisRaw("Vertical") == -1)
        {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, -m_climbSpeed);
        }

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            m_rigidbody.velocity = new Vector2(m_climbSpeed, m_rigidbody.velocity.y);
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            m_rigidbody.velocity = new Vector2(-m_climbSpeed, m_rigidbody.velocity.y);
        }
    }

    public void holdBomb()
    {
        bholdingbomb = true;
        m_bombheld = Instantiate(m_bomb, m_hand.position, m_hand.rotation);
        m_bombheld.GetComponent<Rigidbody2D>().gravityScale = 0f;

    }

    public void dropBomb()
    {
        bholdingbomb = false;
        m_bombheld.GetComponent<Rigidbody2D>().gravityScale = 1f;
        m_bombheld = null;
    }

    public void throwBomb()
    {
        bholdingbomb = false;
        m_bombheld.GetComponent<Rigidbody2D>().gravityScale = 1f;

        if (m_facingRight)
        {
            m_bombheld.GetComponent<Rigidbody2D>().AddForce(bThrowForce);
        }
        else
        {
            m_bombheld.GetComponent<Rigidbody2D>().AddForce(new Vector2(-bThrowForce.x, bThrowForce.y));
        }
       
        m_bombheld = null;
    }

    public void receiveDamage()
    {
        
    }

    public void recoverJumps()
    {
        m_jumpsLeft = m_maxJumps;
    }


    public void randomize()
    {
        m_lives++;
        m_speed = Random.Range(3f, 8f);
        m_maxJumps = Random.Range(2, 6);
        m_jumpsLeft = m_maxJumps;
        m_rigidbody.gravityScale = Random.Range(1.7f, 3f);
    }
}
