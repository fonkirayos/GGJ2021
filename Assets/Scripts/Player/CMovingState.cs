using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMovingState : CState<CPlayerController>
{
    public CMovingState(CStateMachine<CPlayerController> stateMachine) : base(stateMachine) { }
    public override void onEnter(CPlayerController Controller)
    {
        Debug.Log("moving");
        Controller.move();
    }

    public override void onExit(CPlayerController controller)
    {
        Debug.Log("stopped moving");
    }

    public override void update(CPlayerController Controller)
    {
        inputManage(Controller);
    }

    protected override void inputManage(CPlayerController Controller)
    {
        if (Input.GetButton("Horizontal"))
        {
            Controller.bkeyPressed = true;
            
            Controller.move(); 
        }
        if (Input.GetButtonDown("Jump"))
        {
            Controller.bkeyPressed = true;
            m_fsm.setCurrentState(Controller.m_jumpingState, Controller);
        }
        if (Input.GetButtonDown("Vertical"))
        {
            if (Controller.bclimbLadder)
            {
                m_fsm.setCurrentState(Controller.m_ladderState, Controller);
            }
        }
        if (Input.GetButtonDown("Bomb"))
        {
            if (!Controller.bholdingbomb)
            {
                Controller.holdBomb();
            }
            else
            {
                Controller.dropBomb();
            }
        }
        if (Controller.bkeyPressed == false)
        {
            Controller.m_rigidbody.velocity = new Vector2(0, Controller.m_rigidbody.velocity.y);
            m_fsm.setCurrentState(Controller.m_idleState, Controller);
        }
        Controller.bkeyPressed = false;
       
    }
}
