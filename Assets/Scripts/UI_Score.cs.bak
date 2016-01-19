using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UI_Score : MonoBehaviour {

    public static UnityEngine.UI.Text UI_score;

    static int tempScore;

	void Start () 
    {
        //UI에 Score 초기화 : ManagerGame.john_score = 0 를 불러옴.
        UI_score = GetComponent<UnityEngine.UI.Text>();
        tempScore = ManagerGame.john_score;
        UI_score.text = tempScore.ToString();
	
	}

	void Update () 
    {
        
	}

    // day 업데이트할때 7->1로 갈때
    // john_score를 먼저 계산 하고 나서 이거 호출
    static void UpdateScore()
    {
        tempScore = ManagerGame.john_score;
        UI_score.text = tempScore.ToString();
        Debug.Log("Score update");
    }
}
