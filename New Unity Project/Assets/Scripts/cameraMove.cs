using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

public class CamInfo
{
    public Vector3 pos;
    public Quaternion rot;
    public int[] mb;
    public CamInfo(Vector3 _pos, Quaternion _rot, int[] _mb)
    {
        pos = _pos;
        rot = _rot;
        mb = _mb;
    }
}

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    CamInfo[] ci = new CamInfo[] {
        new CamInfo(new Vector3(410.3f, 2.722f, -311f), Quaternion.Euler(0f, 0f, 0f), new int[] {-1, -1, 3, 1}),
        new CamInfo(new Vector3(409.22f, 2.722f, -310.57f), Quaternion.Euler(0f, 90f, 0f), new int[] {-1, -1, 0, 2}),
        new CamInfo(new Vector3(410.3f, 2.722f, -310.18f), Quaternion.Euler(0f, 180f, 0f), new int[] {-1, -1, 1, 3}),
        new CamInfo(new Vector3(410.94f, 2.722f, -310.57f), Quaternion.Euler(0f, 270f, 0f), new int[] {-1, -1, 2, 0}),
        new CamInfo(new Vector3(409.58f, 2.19f, -308.87f), Quaternion.Euler(30f, -45f, 0f), new int[] {-1, 0, -1, -1}),
        new CamInfo(new Vector3(409.383f, 2.453f, -312.523f), Quaternion.Euler(40f, 260f, 0f), new int[] {-1, 3, -1, -1}),
        new CamInfo(new Vector3(410.65f, 2.258f, -310.879f), Quaternion.Euler(30f, 90f, 0f), new int[] {-1, 1, -1, -1}),
        new CamInfo(new Vector3(412.26f, 2.722f, -305.16f), Quaternion.Euler(0f, 270f, 0f), new int[] {-1, -1, -1, -1}),
    };
    GameObject[] MoveButton = new GameObject[4];
    public float mvSpeed = 1.0f;
    public float rtSpeed = 1.0f;
    Vector3 targetPos;
    Quaternion targetRot;
    Vector3 fromPos = new Vector3();
    Quaternion fromRot = new Quaternion();
    int cur = 0;
    bool isMove = false;
    float mvtimer = 0;
    void Start()
    {
        gameObject.transform.SetPositionAndRotation(ci[0].pos, ci[0].rot);
        for (int i = 0; i < 4; i++)
        {
            MoveButton[i] = GameObject.Find("MoveButton_" + i);
            MoveButton[i].SetActive(ci[0].mb[i] >= 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        mvtimer += Time.deltaTime;
        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos,
            Vector3.Distance(fromPos, targetPos) * mvSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot,
                Quaternion.Angle(fromRot, targetRot) * Time.deltaTime * rtSpeed);
            if (mvtimer >= mvSpeed)
            {
                isMove = false;
                for (int i = 0; i < 4; i++)
                {
                    MoveButton[i].SetActive(ci[cur].mb[i] >= 0);
                }
            }
        }
    }

    public void MoveCam(int n)
    {
        if(isMove == false)
        {
            if (n < 0)
                cur = -n;
            else
                cur = ci[cur].mb[n];
            targetPos = ci[cur].pos;
            targetRot = ci[cur].rot;
            fromPos = transform.position;
            fromRot = transform.rotation;
            isMove = true;
            mvtimer = 0;
        }
    }
}