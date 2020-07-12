using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CamInfo
{
    public Vector3 pos;
    public Quaternion rot;
    public int[] mb;
    public bool[] block;
    public CamInfo(Vector3 _pos, Quaternion _rot, int[] _mb, bool[] _block)
    {
        pos = _pos;
        rot = _rot;
        mb = _mb;
        block = _block;
    }
}

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    CamInfo[] ci = new CamInfo[] {
        new CamInfo(new Vector3(410.3f, 2.722f, -311f), Quaternion.Euler(0f, 0f, 0f), new int[] {-1, -1, 3, 1}, new bool[] {false, false, false, false}),
        new CamInfo(new Vector3(409.22f, 2.722f, -310.57f), Quaternion.Euler(0f, 90f, 0f), new int[] {-1, -1, 0, 2}, new bool[] {false, false, false, false}),
        new CamInfo(new Vector3(410.3f, 2.722f, -310.18f), Quaternion.Euler(0f, 180f, 0f), new int[] {-1, -1, 1, 3}, new bool[] {false, false, false, false}),
        new CamInfo(new Vector3(410.94f, 2.722f, -310.57f), Quaternion.Euler(0f, 270f, 0f), new int[] {-1, -1, 2, 0}, new bool[] {false, false, false, false}),
        new CamInfo(new Vector3(409.58f, 2.19f, -308.87f), Quaternion.Euler(30f, -45f, 0f), new int[] {-1, 0, -1, -1}, new bool[] {false, false, false, false}),
        new CamInfo(new Vector3(409.383f, 2.453f, -312.523f), Quaternion.Euler(40f, 260f, 0f), new int[] {-1, 3, -1, -1}, new bool[] {false, false, false, false}),
        new CamInfo(new Vector3(410.65f, 2.258f, -310.879f), Quaternion.Euler(30f, 90f, 0f), new int[] {-1, 1, -1, -1}, new bool[] {false, false, false, false}),
        new CamInfo(new Vector3(412.26f, 2.722f, -305.16f), Quaternion.Euler(0f, 270f, 0f), new int[] {8, -1, -1, -1}, new bool[] {true, false, false, false}),
        new CamInfo(new Vector3(408.08f, 2.722f, -305.16f), Quaternion.Euler(0f, 270f, 0f), new int[] {13, 19, -1, -1}, new bool[] {false, true, false, false}),
        new CamInfo(new Vector3(405.99f, 2.722f, -311f), Quaternion.Euler(0f, 0f, 0f), new int[] {-1, -1, 12, 10}, new bool[] {false, false, false, false}),
        new CamInfo(new Vector3(404.91f, 2.722f, -310.57f), Quaternion.Euler(0f, 90f, 0f), new int[] {-1, -1, 9, 11}, new bool[] {false, false, false, false}),
        new CamInfo(new Vector3(405.99f, 2.722f, -310.18f), Quaternion.Euler(0f, 180f, 0f), new int[] {-1, -1, 10, 12}, new bool[] {false, false, false, false}),
        new CamInfo(new Vector3(406.63f, 2.722f, -310.57f), Quaternion.Euler(0f, 270f, 0f), new int[] {-1, -1, 11, 9}, new bool[] {false, false, false, false}),
        new CamInfo(new Vector3(404.03f, 2.722f, -305.16f), Quaternion.Euler(0f, 270f, 0f), new int[] {14, 18, -1, -1}, new bool[] {false, false, true, false}),
        new CamInfo(new Vector3(400.07f, 2.722f, -305.16f), Quaternion.Euler(0f, 270f, 0f), new int[] {15, 17, -1, -1}, new bool[] {false, false, false, true}),
        new CamInfo(new Vector3(394.45f, 2.722f, -305.57f), Quaternion.Euler(0f, 220f, 0f), new int[] {-1, -1, 16, -1}, new bool[] {false, false, false, false}),
        new CamInfo(new Vector3(392.54f, 2.722f, -305.5f), Quaternion.Euler(0f, 90f, 0f), new int[] {17, -1, -1, 15}, new bool[] {false, false, true, false}),
        new CamInfo(new Vector3(397.92f, 2.722f, -305.16f), Quaternion.Euler(0f, 90f, 0f), new int[] {18, 14, -1, -1}, new bool[] {false, true, false, false}),
        new CamInfo(new Vector3(401.73f, 2.722f, -305.16f), Quaternion.Euler(0f, 90f, 0f), new int[] {19, 13, -1, -1}, new bool[] {true, false, false, false}),
        new CamInfo(new Vector3(406.11f, 2.722f, -305.16f), Quaternion.Euler(0f, 90f, 0f), new int[] {-1, 8, -1, -1}, new bool[] {false, false, false, false}),
        new CamInfo(new Vector3(390.24f, 2.722f, -306.25f), Quaternion.Euler(0f, 260f, 0f), new int[] {-1, -1, 21, 21}, new bool[] {false, false, false, false}),
        new CamInfo(new Vector3(387.21f, 2.722f, -306.17f), Quaternion.Euler(0f, 80f, 0f), new int[] {-1, -1, 20, 20}, new bool[] {false, false, false, false}),
    };
    GameObject[] MoveButton = new GameObject[4];
    GameObject[] Block = new GameObject[4];
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
        Fader = GameObject.Find("Fader");
        fader = Fader.GetComponent<Image>();
        Fader.SetActive(false);

        gameObject.transform.SetPositionAndRotation(ci[0].pos, ci[0].rot);
        for (int i = 0; i < 4; i++)
        {
            MoveButton[i] = GameObject.Find("MoveButton_" + i);
            MoveButton[i].SetActive(ci[0].mb[i] >= 0);
            Block[i] = GameObject.Find("block_" + i);
            Block[i].SetActive(ci[0].block[i]);
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
                    Block[i].SetActive(ci[cur].block[i]);
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
                    Block[i].SetActive(ci[cur].block[i]);
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