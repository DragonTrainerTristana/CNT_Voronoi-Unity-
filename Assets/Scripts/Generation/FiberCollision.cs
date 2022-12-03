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


        //���� �Ҵ� <- �޸� �̸� ���� ���� ���� // ���߿� malloc�̵� realloc�̵� �ٲ���� �� ���� �����
        fiberCollision = new int[100];
        beamerCollision = new int[100];
    }

    void Update()
    {
        
    }

  
    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Fiber") { // �갡 Update�� �Ȱ��Ƽ� bool status�� ������ �ڵ� ¥���ҵ�

            Debug.Log("HIT");

            //�浹�� �ٸ� beamerID�� fiberID�� ������ �´�.
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

                //���� for�� ���ְ� �ͱ� ��, 2���� �迭 �ᵵ ���� for ���ΰ�?
                //���� for�� ���� �Ϸ� -> ����

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
