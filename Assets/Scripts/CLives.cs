using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CLives : MonoBehaviour
{
    // Start is called before the first frame update
    int m_currentLives;
    public Text m_displayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_currentLives = GameObject.Find("Player").GetComponent<CPlayerController>().m_lives;
        m_displayer.text = m_currentLives.ToString();
        
    }
}
