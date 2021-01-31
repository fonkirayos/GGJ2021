using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPARAL : MonoBehaviour
{
    public Transform m_camerapos;
    public float m_parallax;

    float m_lengthX;
    //float m_lengthY;
    float m_mStartPosX;
    float m_lStartPosX;
    float m_rStartPosX;
    float m_left = 0;
    //float m_startPosY;

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
        camerax = m_camerapos.position.x;
        lastx = camerax;


        m_lengthX = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        float distx = (m_camerapos.position.x * m_parallax);

        lastx = m_camerapos.position.x;

        //transform.position = new Vector3(m_startPosX + distx, m_camerapos.position.y, transform.position.z);
        m_middleSprite.position = new Vector3(m_mStartPosX + distx, m_camerapos.position.y, 0);
        m_spriteL.position = new Vector3(m_lStartPosX + distx, m_camerapos.position.y, 0);
        m_spriteR.position = new Vector3(m_rStartPosX + distx, m_camerapos.position.y, 0);

        if ((lastx - camerax) > (m_lengthX / m_parallax))
        {
            Debug.Log("debimos poner enmedio a: " + lastx);
            m_mStartPosX = lastx;
            camerax = lastx;
            //m_lStartPosX = m_mStartPosX - m_lengthX;
            //m_rStartPosX = m_mStartPosX + m_lengthX;
        }

        //if ((lastx - camerax) > (m_lengthX / m_parallax))
        //{
        //    //m_middleSprite.position = new Vector3(lastx, m_camerapos.transform.position.y, 0);
        //    //m_spriteL.position = new Vector3(m_middleSprite.position.x - m_lengthX, m_camerapos.transform.position.y, 0);
        //    //m_spriteR.position = new Vector3(m_middleSprite.position.x + m_lengthX, m_camerapos.transform.position.y, 0);

        //    Debug.Log(lastx);
        //    m_mStartPosX = Camera.main.transform.position.x;
        //    m_lStartPosX = m_mStartPosX - m_lengthX;
        //    m_rStartPosX = m_mStartPosX + m_lengthX;
        //    //m_mStartPosX += m_lengthX;
        //    //m_lStartPosX += m_lengthX;
        //    //m_rStartPosX += m_lengthX;

        //    camerax = lastx;
        //}
        //if ((camerax - lastx) > (m_lengthX / m_parallax))
        //{
        //    //m_middleSprite.position = new Vector3(lastx, m_camerapos.transform.position.y, 0);
        //    //m_spriteL.position = new Vector3(m_middleSprite.position.x - m_lengthX, m_camerapos.transform.position.y, 0);
        //    //m_spriteR.position = new Vector3(m_middleSprite.position.x + m_lengthX, m_camerapos.transform.position.y, 0);

        //    Debug.Log(lastx);
        //    m_mStartPosX = lastx;
        //    m_lStartPosX = m_mStartPosX - m_lengthX;
        //    m_rStartPosX = m_mStartPosX + m_lengthX;
        //    //m_mStartPosX += m_lengthX;
        //    //m_lStartPosX += m_lengthX;
        //    //m_rStartPosX += m_lengthX;

        //    camerax = lastx;
        //}
    }

}

