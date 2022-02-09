using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //클릭한 카드 번호
    static public int CardNum;


    //직전 카드 번호
    int lastNum = 0;

    //스테이지 카드 수

    int CardCnt;

    //카드 클릭 횟수

    int hitCnt = 0;

    //스테이지 번호

    static public int stageNum = 1;

    //스테이지 숫자

    int stageCnt = 6;

    //카드 배열 섞기용

    int[] arCards = new int[50];

    //게임 시작 시간

    float startTime;


    public enum STATE
    {


        START,HIT,WAIT,IDEL,CLEAR

    };

    static public STATE state = STATE.START;



    //스테이지 경과 시간

    float stageTime;

    void Start()
    {

        Screen.orientation = ScreenOrientation.LandscapeRight;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;


        //시간 초기화
        startTime = stageTime = Time.time;

        //스테이지 
        StartCoroutine(MakeStage());

    }

    void Update()
    {
        switch(state)
        {

        }

    }


    IEnumerator MakeStage()
    {
        //시작카드 x 좌표

        float sx = 0;

        //시작카드 z 좌표

        float sz = 0;

        SetCardPos(out sx, out sz);

        //카드섞기
        ShuffleCard();

        //시작 번호 

        int n = 1;

        //카드 배열 읽기 1행의 변수 t 에 할당

        string[] str = StageSet.stage[stageNum - 1];

        //배열 행 수만큼 반복

        foreach (string t in str)
        {
            char[] ch = t.Trim().ToCharArray();

            //카드 x 축 좌표

            float x = sx;

            //1행의 문자열 길이 만큼 반복
            // 배열의 ch 한문자 변수 c 에 할당
            
           // Debug.Log("STR: " + str);
           // Debug.Log("t: " + t);
           // Debug.Log("CH: " + ch);
          //  Debug.Log("CH LEN: " + ch.Length);
            

            foreach (char c in ch)
            {
                Debug.Log("c: " + c);

                switch (c)
                {
                    //맵의 내용이 *이면 그위치 카드 배치
                    case '*':

                        //카드만들기

                        GameObject card = Instantiate(Resources.Load("Prefab/card")) as GameObject;

                        //카드 좌표

                        card.transform.position = new Vector3(x, 0, sz);

                        //태그 달기

                        card.tag = "card" + n++;
                        //break;

                        //섞인 카드
                        card.tag = "card" + arCards[n++];
                        x++;
                        break;

                    //빈칸

                    case '.':
                        x++;
                        break;

                    //빈칸 공백 거리

                    case '>':
                        x += 0.5f;
                        break;

                    //반 줄 행간 처리

                    case '^':
                        sz += 0.5f;
                        break;

                }
                //카드 표시후 지연시간
                if (c == '*')
                {
                    yield return new WaitForSeconds(0.05f);
                }
            }
            //한줄 아래로 이동
            sz--;
        }
    }


    //카드 시작 위치 계산
    void SetCardPos(out float sx, out float sz)
    {
        //가로 카드 빈칸 공백

        float x = 0;

        //세로 행수 반줄 행간 포함

        float z = 0;

        //가로 카드 최대 수

        float maxX = 0;

        //스테이지 전체 카드수 

        CardCnt = 0;

        //카드 배열 조사 맵 배열 읽음

        string[] str = StageSet.stage[stageNum - 1];


        for (int i = 0; i < str.Length; i++)
        {

            //1행 읽기

            string t = str[i].Trim();

            //각행의 카드수

            x = 0;


            //각행의 글자수 만큼 반복

            for (int j = 0; j < t.Length; j++)
            {
                //문자열 단일문자 취급

                switch (t[j])
                {
                    case '.':
                    case '*':

                        //카드배치 공간

                        x++;
                        if (t[j] == '*')
                        {
                            CardCnt++;
                        }
                        break;

                    //x 카드 출현 간격


                    case '>':
                        x += 0.5f;
                        break;

                    //z 카드 출현 간격

                    case '^':
                        z -= 0.5f;
                        break;
                }
            }

            //각 행의 최대 카드 수 계산
            if (x > maxX)
            {
                maxX = x;
            }
            //전체 행의 수

            z++;

        }
        //카드 가로 시작위치

        sx = -maxX / 2;
        sz = (z - 1) / 2;

        //StartCoroutine(CardOpen(CardCnt));
    }

    void ShuffleCard()
    {
        for (int i = 1; i <= CardCnt; i++)
        {
            arCards[i] = i;

        }
        //return;

        //카드 섞기 반복
        for (int i = 1; i <= 15; i++)
        {
            int n1 = Random.Range(1, CardCnt + 1);
            int n2 = Random.Range(1, CardCnt + 1);

            int t = arCards[n1]; ;
            arCards[n1] = arCards[n2];
            arCards[n2] = t;

            //StartCoroutine(CardOpen(arCards[n1]));

        }
    }

}
