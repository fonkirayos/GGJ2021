using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLadderState : CState<CPlayerController>
{
    public CLadderState(CStateMachine<CPlayerController> stateMachine) : base(stateMachine) { }

    public override void onEnter(CPlayerController Controller)
    {
        Debug.Log("climbing ladder");
        Controller.m_rigidbody.gravityScale = 0f;
        Controller.climb();
        //Controller.m_rigidbody.velocity = new Vector2(0, 0);
    }

    public override void onExit(CPlayerController Controller)
    {
        Debug.Log("stopped climbing");
        Controller.m_rigidbody.gravityScale = 2.12f;
        
    }

    public override void update(CPlayerController Controller)
    {
        inputManage(Controller);
        if(!Controller.bclimbLadder)
        {
            if (Input.GetButton("Horizontal"))
            {
                
                Controller.bkeyPressed = true;
                m_fsm.setCurrentState(Controller.m_movingState, Controller);
            }
            if (Input.GetButtonDown("Jump"))
            {
                Controller.bkeyPressed = true;
                m_fsm.setCurrentState(Controller.m_jumpingState, Controller);
            }
            if (Controller.bkeyPressed == false)
            {
                m_fsm.setCurrentState(Controller.m_idleState, Controller);
            }
        }
    }

    protected override void inputManage(CPlayerController Controller)
    {
        if (Input.GetButton("Horizontal") )
        {
            if (Controller.bclimbLadder)
            {
                Controller.climb();
                Controller.bmoving = true;
            }            
        }
        if (Input.GetButton("Vertical"))
        {
            if (Controller.bclimbLadder)
            {
                Controller.climb();
                Controller.bkeyPressed = true;
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            Controller.bkeyPressed = true;
            m_fsm.setCurrentState(Controller.m_jumpingState, Controller);
        }
        if(Controller.bmoving == false)
        {
            Controller.m_rigidbody.velocity = new Vector2(0, Controller.m_rigidbody.velocity.y);
        }
        if (Controller.bkeyPressed == false)
        {
            Controller.m_rigidbody.velocity = new Vector2(Controller.m_rigidbody.velocity.x, 0);
        }
    }
}
