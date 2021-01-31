using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBackground : MonoBehaviour
{
    public  SpriteRenderer m_middlesprite;

    public SpriteRenderer m_sprite2;
    public SpriteRenderer m_sprite3;

    Plane[] planes;
    float sizeX;
    Vector3 offset;
    bool bwatchingmid = true;
    // Start is called before the first frame update
    void Start()
    {
        sizeX = m_middlesprite.bounds.size.x * 0.5f;
        offset = new Vector3(sizeX, 0, 0);
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
    }

    // Update is called once per frame
    void Update()
    {

        //if (GeometryUtility.TestPlanesAABB(planes, m_middlesprite.bounds))
        //{
        //    Debug.Log("middle sprite has been detected!");
            
        //}
        //else if ((GeometryUtility.TestPlanesAABB(planes, m_sprite2.bounds)))
        //{
        //    Debug.Log("left sprite has been detected!");
        //    m_middlesprite.transform.position += offset;
        //    m_sprite2.transform.position += offset;
        //    m_sprite3.transform.position += offset;
            
        //}
        //else if ((GeometryUtility.TestPlanesAABB(planes, m_sprite3.bounds)))
        //{
        //    Debug.Log("right sprite has been detected!");
        //    m_middlesprite.transform.position -= offset;
        //    m_sprite2.transform.position -= offset;
        //    m_sprite3.transform.position -= offset;
            
        //}
        
    }

}

    

