using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class test : MonoBehaviour
{
    public bool IsTimeLodaed = false;

    private DateTime _currentDateTime = DateTime.Now;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetInternetTime());
    }

    public IEnumerator GetInternetTime()
    {
        UnityWebRequest myHttpWebRequest = UnityWebRequest.Get("https://www.microsoft.com");
        yield return myHttpWebRequest.SendWebRequest();
 
        string netTime = myHttpWebRequest.GetResponseHeader("date");
        
        if (myHttpWebRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(DateTime.Parse(netTime) + " was response");
            _currentDateTime = DateTime.Parse(netTime);
        }else
        {
            Debug.Log("Error: " + myHttpWebRequest.error.ToString());
        }
    }
    
    public DateTime GetCurrentDateTime ( out bool isSuccess )
    {
        //here we don't need to get the datetime from the server again
        // just add elapsed time since the game start to _currentDateTime

        isSuccess = IsTimeLodaed;
        return _currentDateTime.AddSeconds ( Time.realtimeSinceStartup );
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
