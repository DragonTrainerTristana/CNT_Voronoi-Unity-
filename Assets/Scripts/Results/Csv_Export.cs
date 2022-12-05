using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Csv_Export : MonoBehaviour
{
    // Csv File Control Variables
    public string fileName = "Results.csv";
   
    // Time Control Variables
    private float originalTime;
    private float setTime;
    private bool timeStatus;

    void Start()
    {
        // Initialize Time Variables
        originalTime = 0.0f;
        setTime = 10.0f;
        timeStatus = true;

    }
    void Update()
    {
        originalTime += Time.deltaTime;
        if (originalTime >= setTime && timeStatus == true) {
            timeStatus = false;
            // 10초 뒤에 단 한번만 실행하는 부분




        }  
    }

    public void SaveCsvFile() {


    }
}
