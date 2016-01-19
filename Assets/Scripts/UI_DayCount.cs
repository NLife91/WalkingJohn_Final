using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_DayCount : MonoBehaviour {

    public static UnityEngine.UI.Text UI_day;

    static int temp_day;

	void Start () 
    {
        //UI에 Day 초기화 : ManagerGame.john_day = 1 를 불러옴.
        UI_day = GetComponent<UnityEngine.UI.Text>();
        temp_day = ManagerGame.john_day;
        UI_day.text = temp_day.ToString();

	}
	
	void Update () 
    {
        
	}

    // day 업데이트할때 7->1로 갈때
    //john_day를 먼저 +1 하고 나서 이거 호출
    public static void UpdateDayCount()
    {
        temp_day = ManagerGame.john_day;
		ManagerGame.john_score += 100;
        UI_day.text = temp_day.ToString();
        Debug.Log("Day update");
    }
}
