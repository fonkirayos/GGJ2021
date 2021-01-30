using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CJumpForce : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(new Vector2(0f, 6f));
    }

    private void FixedUpdate()
    {
        //rigidbody.AddForce(new Vector2(0f, 5f));
    }
}
