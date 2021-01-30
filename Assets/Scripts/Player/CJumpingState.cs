using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CJumpingState : CState<CPlayerController>
{
    public CJumpingState(CStateMachine<CPlayerController> stateMachine) : base(stateMachine) { }

    public override void onEnter(CPlayerController Controller)
    {
        Debug.Log("jumping");
        Controller.jump();
        Controller.m_jumpsLeft--;

    }

    public override void onExit(CPlayerController Controller)
    {
        Debug.Log("landed");
        Controller.m_jumpsLeft = Controller.m_maxJumps;
    }
     
    public override void update(CPlayerController Controller)
    {
        inputManage(Controller);
        if(Controller.m_rigidbody.velocity.y == 0f & Controller.btriggering)
        {
            if(Controller.bmoving)
            {
                m_fsm.setCurrentState(Controller.m_movingState, Controller);
            }
            else
            {
                m_fsm.setCurrentState(Controller.m_idleState, Controller);
            }
        }
        Controller.bmoving = false;
    }

    protected override void inputManage(CPlayerController Controller)
    {
        
        if (Input.GetButton("Horizontal"))
        {
            Controller.move();
            Controller.bmoving = true;
            Controller.bkeyPressed = true;
        }
        if (Input.GetButtonDown("Jump") & Controller.m_jumpsLeft > 0)
        {
            Controller.jump();
            Controller.m_jumpsLeft--;
        }
        if (Input.GetButtonDown("Vertical"))
        {
            if (Controller.bclimbLadder)
            {
                m_fsm.setCurrentState(Controller.m_ladderState, Controller);
            }
        }
        if(!Controller.bkeyPressed)
        {
            Controller.m_rigidbody.velocity = new Vector2(0, Controller.m_rigidbody.velocity.y);
        }
    }
}
