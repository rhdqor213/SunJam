using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public bool isPause = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShow()
    {
        if (!isPause)
        {
            isPause = true;
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }
        else
        {
            isPause = false;
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}
