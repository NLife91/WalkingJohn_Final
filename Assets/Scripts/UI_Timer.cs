using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{

    public static UnityEngine.UI.Text UI_timer;


    public static float timeLeft; // 시간 얼마나 남았는지 알려줌

    public Scrollbar TimeBar;
    public float time=5f; // 카운트 다운 하는 초




    void Start()
    {
       // 카운트 다운하는 초를 초기화 
	    timeLeft = time;

        // 카운트 다운 숫자 나타내기 위해 
        UI_timer = GetComponent<UnityEngine.UI.Text>();
       // UI_timer.text = timeLeft.ToString("f0"); //f0:소수점 자리 0개
	}

    void Update()
    {
 		//if(//게임 시작 되면 타이머가 시작되게)
        {        }

        Timer();
        
        
	}

    // Time이 0으로 줄어드는 것
    void Timer()
    {
        timeLeft -= Time.deltaTime;

        TimeBar.size = timeLeft * (0.2f);

        //Debug.Log("timeLeft = " + timeLeft);

        if (ManagerGame.gamePhase == 4)
        {
            ResetTimer();
            TurnCount();
            ManagerGame.gamePhase = 2;

            //Day Text - 상단, 하단로그 + 스코어 업데이트
            UI_DayCount.UpdateDayCount();
            UI_Score.UpdateScore();
            ShowMessage.ShowMessage_Day();

        }

        if (timeLeft < 0)
        {
            ResetTimer();
            TurnCount();
            ManagerGame.gamePhase = 5;

        }
    }

    void TurnCount()
    {
        ManagerGame.john_day++;
        foreach (GameObject bomb in ManagerGame.bombList)
        {
            bomb.GetComponent<BombCollider>().count++;
        }
    }

    // 타이머를 (변수Time) 만큼 남게 리셋함
    void ResetTimer()
    {
        timeLeft = time;
        //Debug.Log("timeLeft = " + timeLeft + " // 타임 리셋");
    }

    //// 타이머를 숫자로 나타냄 (상단UI)
    //void ShowTimeLeft()
    //{
    //    UI_timer.text = timeLeft.ToString("f0");

    //    TimeBar.size = 1;
    //}

}