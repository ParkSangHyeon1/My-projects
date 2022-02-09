using UnityEngine;

public class CardCtrl : MonoBehaviour
{

    // 이미지 번호
    int imgNum = 1;

    //카드 뒷면 번호

    int backNum = 1;

    //오픈된 카드 판별

    bool isOpen = false;

    Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        //Debug.Log("Animator" + anim);
    }

    void Update()
    {

        // 마우스 왼쪽 버튼 (모바일에선 터치) 

        if (Input.GetButtonDown("Fire1"))

        // Debug.Log("Fire1" + KeyCode.Mouse0);

        {
            Checkcard();
        }
    }

    void Checkcard()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        // 카드식별
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            string tag = hit.transform.tag;
            if (tag.Substring(0, 4) == "card")
            {
                //OpenCard() 함수 실행
                hit.transform.SendMessage("OpenCard", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    void OpenCard()
    {
        if (isOpen) return;
        isOpen = false;

        //카드번호 Substring() 문자열 일부분 추출하는 함수 // 카드0~카드32 문자4~끝까지 추출

        int cardNum = int.Parse(transform.tag.Substring(4));

        
        // 이미지 카드 두장에 하나씩 같은 이미지 할당  , (카드번호 + 1 )/ 2 >>>> 정수/정수 이므로 소수이하는 버림

        imgNum = (cardNum + 1) / 2;
        
        //애니매이션 실행

        anim.Play("aniOpen");

       // GameManager.cardNum = cardNum;
       // GameManager.state = GameManager.state.Hit;
    }

    void CloseCard()
    {
        anim.Play("aniClose");
        isOpen = false;
    }

    //카드 앞면 이미지
    void ShowImage()
    {
        transform.GetComponent<Renderer>().material.mainTexture = Resources.Load("card" + imgNum) as Texture2D;
    }

    void HideImage()
    {
        transform.GetComponent<Renderer>().material.mainTexture = Resources.Load("back" + backNum) as Texture2D;
    }
}
