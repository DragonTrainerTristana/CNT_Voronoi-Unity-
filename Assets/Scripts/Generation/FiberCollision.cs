using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiberCollision : MonoBehaviour
{
    public int beamerID;
    public int fiberiD;

    public int[] fiberCollision;
    public int[] beamerCollision;

    private int countNum;
    private int checkNum;
    private int arbitaryFiberNum;
    private int arbitaryBeamerNum;

    //bool val
    private bool colStatus;


    void Start()
    {
        countNum = 0;
        checkNum = 0;
        colStatus = true;


        //대충 할당 <- 메모리 미모리 공주 생각 안함 // 나중에 malloc이든 realloc이든 바꿔야할 듯 지금 까먹음
        fiberCollision = new int[100];
        beamerCollision = new int[100];
    }

    void Update()
    {
        
    }

  
    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Fiber") { // 얘가 Update랑 똑같아서 bool status로 난도질 코드 짜야할듯

            Debug.Log("HIT");

            //충돌한 다른 beamerID와 fiberID를 가지고 온다.
            arbitaryBeamerNum = collision.gameObject.GetComponent<FiberCollision>().beamerID;
            arbitaryFiberNum = collision.gameObject.GetComponent<FiberCollision>().fiberiD;
          
            if (countNum == 0) {
                if (beamerID != arbitaryBeamerNum)
                {
                    fiberCollision[countNum] = arbitaryFiberNum;
                    beamerCollision[countNum] = arbitaryBeamerNum;
                    countNum++;
                }
            }
            if (countNum > 0) {

                //이중 for문 없애고 싶긴 함, 2차원 배열 써도 이중 for 문인가?
                //이중 for문 삭제 완료 -> 변경

                if (beamerID != arbitaryBeamerNum)
                {
                    for (int i = 0; i < countNum; i++)
                    {
                        /*
                        if (beamerID == beamerCollision[i])
                        {
                            colStatus = false;
                            break;
                        }
                        */
                        if (arbitaryBeamerNum == beamerCollision[i] && arbitaryFiberNum == fiberCollision[i])
                        {
                            colStatus = false;
                            break;
                        }
                        colStatus = true;
                    }

                    if (colStatus == true)
                    {
                        fiberCollision[countNum] = arbitaryFiberNum;
                        beamerCollision[countNum] = arbitaryBeamerNum;
                        countNum++;
                    }
                }
            }
        }        
    }


}
