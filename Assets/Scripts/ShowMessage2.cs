using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowMessage2 : MonoBehaviour
{
    //MessageCanvas에 메세지 띄우기

    public static UnityEngine.UI.Text Message2;
    

    //public static string Text1_day;
    public static string Text2_apple;
    public static string Text3_zombie;
    public static string Text4_bomb;

    public static bool text2_On = false;
    public float text2_OnTime;
    public static float timeNow;


    void Start()
    {
        // MessageCanvas 에 메세지 띄우기
        Message2 = GetComponent<UnityEngine.UI.Text>();
        Message2.text = "";
        Text2_apple = "Apple! HP+20";
        Text3_zombie = "Zombie...HP-10";
        Text4_bomb = "Bomb! Score+200"; // 이상혁 ShowMessage2.ShowMessage_Bomb();추가해 Bomb터지는 곳에 넣어조
        
        

    }


    void Update()
    {
        if(text2_On)
        {
            if(Time.time - timeNow >= text2_OnTime)
            {
                Message2.text = "";
            }
        }
    }

    public static void ShowMessage_Apple()
    {
        text2_On = true;
        Message2.text = ShowMessage2.Text2_apple;
        timeNow = Time.time;
    }

    public static void ShowMessage_Zombie()
    {
        Debug.Log("쇼메세지좀비 호출");
        text2_On = true;
        Message2.text = ShowMessage2.Text3_zombie;
        timeNow = Time.time;
    }

    public static void ShowMessage_Bomb()
    {
        text2_On = true;
        Message2.text = ShowMessage2.Text4_bomb;
        timeNow = Time.time;
    }

}
