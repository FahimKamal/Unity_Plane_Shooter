using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;

public class WorldTimeAPI : MonoBehaviour {
    
    public bool isTimeLoaded = false;

    private DateTime _currentDateTime = DateTime.Now;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(GetInternetTime());
    }

    private IEnumerator GetInternetTime()
    {
        var myHttpWebRequest = UnityWebRequest.Get("https://www.microsoft.com");
        yield return myHttpWebRequest.SendWebRequest();
 
        var netTime = myHttpWebRequest.GetResponseHeader("date");
        
        if (myHttpWebRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Current internet time is: " + DateTime.Parse(netTime));
            _currentDateTime = DateTime.Parse(netTime);
            isTimeLoaded = true;
        }else
        {
            Debug.Log("Error: " + myHttpWebRequest.error.ToString());
        }
    }
    
    public DateTime GetCurrentDateTime (out bool isSuccess)
    {
        //here we don't need to get the datetime from the server again
        // just add elapsed time since the game start to _currentDateTime

        isSuccess = isTimeLoaded;
        return _currentDateTime.AddSeconds ( Time.realtimeSinceStartup );
    }
}

