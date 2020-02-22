using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public Text consoleTextBox;
    public GameObject console;
    public static GameObject consoleCanvas;

    static string myLog = "";
    private string output;
    private string stack;



    private void Start()
    {
        consoleCanvas = this.gameObject;
        DontDestroyOnLoad(consoleCanvas);
        //consoleTextBox = this.GetComponent<Text>();
        Application.logMessageReceived += Log;
    }


    public void ActivateConsoleLog()
    {
        if (console.activeInHierarchy == false)
        {
            console.SetActive(true);
        }
        else if (console.activeInHierarchy == true)
        {
            console.SetActive(false);
        }
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;
        myLog = output + "\n" + myLog;
        if (myLog.Length > 5000)
        {
            myLog = myLog.Substring(0, 4000);
        }
        consoleTextBox.text = myLog;
    }
    

}
