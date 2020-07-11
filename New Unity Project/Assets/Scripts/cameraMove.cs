using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
        new CamInfo(new Vector3(412.26f, 2.722f, -305.16f), Quaternion.Euler(0f, 270f, 0f), new int[] {8, -1, -1, -1}),
        new CamInfo(new Vector3(408.08f, 2.722f, -305.16f), Quaternion.Euler(0f, 270f, 0f), new int[] {-1, -1, -1, -1}),
    };
    GameObject[] MoveButton = new GameObject[4];
    public float mvTime = 1.0f;
    public float fadeTime = 1.0f;
    Vector3 targetPos;
    Quaternion targetRot;
    Vector3 fromPos = new Vector3();
    Quaternion fromRot = new Quaternion();
    GameObject Fader;
    Image fader;
    int cur = 0;
    bool isMove = false;
    bool isFade = false;
    float timer = 0f;

    void Start()
    {
        Fader = GameObject.FindGameObjectWithTag("fader");
        fader = Fader.GetComponent<Image>();
        Fader.SetActive(false);

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
        if (isMove)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos,
            Vector3.Distance(fromPos, targetPos) * mvTime * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot,
                Quaternion.Angle(fromRot, targetRot) * Time.deltaTime * mvTime);
            if (timer >= mvTime)
            {
                isMove = false;
                for (int i = 0; i < 4; i++)
                {
                    MoveButton[i].SetActive(ci[cur].mb[i] >= 0);
                }
            }
        }
        if (isFade)
        {
            if (timer == 0)
                StartCoroutine(CoFadeIn());
            timer += Time.deltaTime;
            if (timer >= fadeTime)
            {
                transform.position = targetPos;
                transform.rotation = targetRot;
                isFade = false;
                for (int i = 0; i < 4; i++)
                {
                    MoveButton[i].SetActive(ci[cur].mb[i] >= 0);
                }
                StartCoroutine(CoFadeOut());
            }
        }
    }

    public void MoveCam(int n)
    {
        if(!isMove && !isFade)
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
            timer = 0;
        }

    }
    
    public void FadeCam(int n)
    {
        if (!isMove && !isFade)
        {
            cur = n;
            targetPos = ci[cur].pos;
            targetRot = ci[cur].rot;
            isFade = true;
            timer = 0;
        }
    }

    // 투명 -> 불투명
    IEnumerator CoFadeIn()
    {
        Fader.SetActive(true);
        Color tempColor = fader.color;
        
        while (tempColor.a < 1f)
        {
            tempColor.a += Time.deltaTime / fadeTime;
            fader.color = tempColor;

            if (tempColor.a >= 1f) tempColor.a = 1f;

            yield return null;
        }
        fader.color = tempColor;
    }

    // 불투명 -> 투명
    IEnumerator CoFadeOut()
    {
        Color tempColor = fader.color;
        while (tempColor.a > 0f)
        {
            tempColor.a -= Time.deltaTime / fadeTime;
            fader.color = tempColor;

            if (tempColor.a <= 0f) tempColor.a = 0f;

            yield return null;
        }
        fader.color = tempColor;
        Fader.SetActive(false);
    }
}