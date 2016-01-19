using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowMessage : MonoBehaviour 
{
    //MessageCanvas에 메세지 띄우기

    public static UnityEngine.UI.Text Message1;


    public static string Text1_day;

    public static bool dayOn = false;
    public float dayOnTime;
    public static float timeNow;



	void Start () 
    {
        Message1 = GetComponent<UnityEngine.UI.Text>();
        Message1.text = "";
        Text1_day = "+1Day! Score+100";
        
	}
	
	
	void Update () 
    {
	    if(dayOn)
        {
            
            if(Time.time - timeNow >= dayOnTime)
            {
                Message1.text = "";
            }
        }
	}

    public static void ShowMessage_Day()
    {
        dayOn = true;
        Message1.text = ShowMessage.Text1_day;
        timeNow = Time.time;
    }

 
    
}
