using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float mvSpeed = 1.0f;
    public float rtSpeed = 1.0f;
    Transform targetPos;
    Vector3 fromPos = new Vector3();
    Quaternion fromRot = new Quaternion();
    int clickLayer = 9;
    bool isMoving = false;
    public List<Transform> pos = new List<Transform>();
    void Start()
    {
        targetPos = GameObject.Find("campos_1").transform;
        for(int i = 1; i < 20; i++)
        {
            pos.Add(GameObject.Find("campos_" + i).transform);
        }
        targetPos.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonUp(0) && !isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 100f))
            {
                int l = hitInfo.transform.parent.gameObject.layer;
                if (l == clickLayer)
                {
                    targetPos.gameObject.SetActive(true);
                    string targetName = hitInfo.transform.parent.name;
                    string index = "";
                    for (int i = 0; i < targetName.Length; i++)
                    {
                        if (targetName[i] == '_')
                        {
                            for (int j = i + 1; j < targetName.Length; j++)
                            {
                                index += targetName[j];
                            }
                            break;
                        }
                    }
                    targetPos = pos[Int32.Parse(index) - 1];
                    fromPos = transform.position;
                    fromRot = transform.rotation;
                    isMoving = true;
                }
            }
        }
        if (isMoving)
        {
            MovePos();
        }
    }
    void MovePos()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position,
            Vector3.Distance(fromPos, targetPos.position) * mvSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetPos.rotation,
            Quaternion.Angle(fromRot, targetPos.rotation) * Time.deltaTime * rtSpeed);
        if (transform.position == targetPos.position
            && transform.rotation == targetPos.rotation)
        {
            targetPos.gameObject.SetActive(false);
            isMoving = false;
        }
    }
}
