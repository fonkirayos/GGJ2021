using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOTPARALLAX : MonoBehaviour
{
    float m_parallax = 0.02f;

    float m_lengthX;   
    float m_mStartPosX;
    float m_lStartPosX;
    float m_rStartPosX;
    

    float camerax;
    float lastx;

    public Transform m_middleSprite;
    public Transform m_spriteL;
    public Transform m_spriteR;
    void Start()
    {

        m_mStartPosX = m_middleSprite.position.x;
        m_lStartPosX = m_spriteL.position.x;
        m_rStartPosX = m_spriteR.position.x;
        camerax = transform.position.x;
        lastx = camerax;


        m_lengthX = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        float distx = (transform.position.x * m_parallax);

        lastx = transform.position.x;

        //transform.position = new Vector3(m_startPosX + distx, m_camerapos.position.y, transform.position.z);
        m_middleSprite.position = new Vector3(m_mStartPosX + distx, transform.position.y, 0);
        m_spriteL.position = new Vector3(m_lStartPosX + distx, transform.position.y, 0);
        m_spriteR.position = new Vector3(m_rStartPosX + distx, transform.position.y, 0);


        if ((lastx - camerax) > m_lengthX)
        {

            Debug.Log(lastx);
            m_mStartPosX = lastx - distx;
            m_lStartPosX = m_mStartPosX - m_lengthX;
            m_rStartPosX = m_mStartPosX + m_lengthX;
            //m_mStartPosX += m_lengthX;
            //m_lStartPosX += m_lengthX;
            //m_rStartPosX += m_lengthX;

            camerax = lastx;
        }
        if ((camerax - lastx) > m_lengthX)
        {
            

            Debug.Log(lastx);
            m_mStartPosX = lastx - distx; 
            m_lStartPosX = m_mStartPosX - m_lengthX;
            m_rStartPosX = m_mStartPosX + m_lengthX;
            camerax = lastx;
        }
    }

}
