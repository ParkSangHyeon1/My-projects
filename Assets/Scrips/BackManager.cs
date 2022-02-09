using System.Collections;
using UnityEngine;

public class BackManager : MonoBehaviour
{
    //배경 이미지

    Transform back1;
    Transform back2;

    //오버랩 지연

    int delayTime = 3;

    //시간

    float currentTime = 0;

    int imgCnt = 6;

    //이미지 번호

    int imgNum = 1;


    void Start()
    {
        back1 = transform.Find("Background1");
        back2 = transform.Find("Background2");
    }


    void Update()
    {
        //지연시간 처리

        currentTime += Time.deltaTime;

        if (currentTime >= delayTime)
        {
            currentTime = 0;

            //이미지 오버랩

            StartCoroutine(OverlapImage());
        }
    }
    IEnumerator OverlapImage()
    {
        //이미지 알파값 설정
        // 이미지 투명도 0~1 0 = 투명 .1은 불투명

        for (float alpha = 1; alpha >= 0; alpha -= 0.05f)
        {
            //배경이미지 불투명
            back1.GetComponent<Renderer>().material.color = new Vector4(1, 1, 1, alpha);


            back2.GetComponent<Renderer>().material.color = new Vector4(1, 1, 1, -alpha);

            //갱신까지 대기 시간
            yield return new WaitForFixedUpdate();
        }
        Transform tmp = back1;
        back1 = back2;
        back2 = tmp;

        //Mathf.Repeat() 0~<한계값 -1> 반복으로 처리 
        imgNum = (int)Mathf.Repeat(++imgNum, imgCnt);

        back2.GetComponent<Renderer>().material.mainTexture = Resources.Load("imgBack" + imgNum) as Texture2D;

        currentTime = 0;
    }
}
