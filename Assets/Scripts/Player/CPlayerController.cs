using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CPlayerController : MonoBehaviour
{
    GameObject m_player = null;
    //Rigidbody2D m_rigidbody = null;
    public Rigidbody2D m_rigidbody;
    public GameObject m_lastLadder = null;

    internal CStateMachine<CPlayerController> m_fsm = null;
    internal CIdleState m_idleState = null;
    internal CMovingState m_movingState = null;
    internal CJumpingState m_jumpingState = null;
    internal CLadderState m_ladderState = null;

    public bool bkeyPressed = false;
    public bool bmoving = false;
    public bool btriggering = false;
    public bool bclimbLadder = false;
   


    [SerializeField] float m_speed = 5f;
    [SerializeField] float m_jumpForce = 400f;
    [SerializeField] float m_climbSpeed = 2.5f;

    [SerializeField] internal int m_maxJumps = 2;
    [SerializeField] internal  int m_jumpsLeft = 2;


    //Vector3 movement;
    Vector2 jumpForce;
    Vector2 moveForce;
    
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.Find("Player");
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
        m_fsm.update(this);
        bkeyPressed = false;
    }

    public void move()
    {
  
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
                m_rigidbody.velocity = new Vector2(m_speed, m_rigidbody.velocity.y);
        }
        else
        {
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ladder")
        {
            bclimbLadder = true;
        }
        btriggering = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ladder")
        {
            bclimbLadder = true;
        }
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
}
