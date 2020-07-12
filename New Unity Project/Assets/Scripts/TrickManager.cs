using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickManager : MonoBehaviour
{
    public int keyItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Check(int n)
    {
        if (keyItem == n)
        {
            Destroy(transform.gameObject);
            return 1;
        }
        return 0;
    }
}
