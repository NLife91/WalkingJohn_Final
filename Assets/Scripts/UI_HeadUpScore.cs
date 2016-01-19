using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_HeadUpScore : MonoBehaviour 
{

    public static UnityEngine.UI.Text HeadUI_Zombie;
    static Vector3 john_transform = GameObject.FindGameObjectWithTag("Player").transform.position;
    static GameObject HeadScore_Zombie = Resources.Load("Prefabs/HeadScore_Zombie") as GameObject;
    static GameObject HS_Zombie;    
    void Start () 
    {
	    
	}
	
	
	void Update () 
    {
        float speed = 1.0f;

        float time = Time.time;

        Vector3 pos = HS_Zombie.transform.position;
        pos.y += speed * Time.deltaTime;
        HS_Zombie.transform.position = pos;

        if (Time.time - time == 1.6f)
        {
            Destroy(HS_Zombie);
            Debug.Log("점수 디스트로이됨");
        }
	}

    public static void ShowHeadScore_Zombie()
    {
       
        Debug.Log("ShowZombieScore함수실행됨 //john_transform = " + john_transform);
       
        HS_Zombie = Instantiate(HeadScore_Zombie,john_transform,Quaternion.identity) as GameObject;
        HS_Zombie.transform.parent = GameObject.Find("UI_ScoreCanvas").transform;



      
     }

    //public void Destroy_HS_Zombie()
    //{
    //    Destroy(HS_Zombie);
    //    Debug.Log("점수 디스트로이됨");
    //}

}
