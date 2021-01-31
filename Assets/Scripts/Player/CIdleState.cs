using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CIdleState : CState <CPlayerController>
{
    public CIdleState(CStateMachine<CPlayerController> stateMachine) : base(stateMachine) { }

    public override void onEnter(CPlayerController Controller)
    {
        Debug.Log("idle");
        //Controller.m_rigidbody.velocity = new Vector2(0, 0);
    }

    public override void onExit(CPlayerController Controller)
    {
        Debug.Log("exiting idle");
    }

    public override void update(CPlayerController Controller)
    {
        inputManage(Controller);
    }

    protected override void inputManage(CPlayerController Controller)
    {
        if (Input.GetButtonDown("Horizontal"))
        {

            m_fsm.setCurrentState(Controller.m_movingState, Controller);
        }
        if (Input.GetButtonDown("Jump"))
        {

            m_fsm.setCurrentState(Controller.m_jumpingState, Controller);
        }
        if (Input.GetButtonDown("Vertical"))
        {
            if(Controller.bclimbLadder)
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
        if (Input.GetButtonDown("TBomb"))
        {
            if (Controller.bholdingbomb)
            {
                Controller.throwBomb();
            }
        }
        Controller.m_rigidbody.velocity = new Vector2(0, Controller.m_rigidbody.velocity.y);
    }
}
