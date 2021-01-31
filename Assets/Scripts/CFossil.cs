using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFossil : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GameObject.Find("Player").GetComponent<CircleCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
