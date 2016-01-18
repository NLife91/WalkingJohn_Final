using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_HPGauge : MonoBehaviour {

    public static Scrollbar HPGauge;
    //public static float HP; //초기화

    
	void Start () 
    {
        //ManagerGame.john_hp = 100; 으로 초기화된다.
        //HP = ManagerGame.john_hp;	
        HPGauge = this.GetComponent<Scrollbar>();
	}
	
	
	void Update () 
    {
	
	}

    public static void HPDamage(int value)
    {
        if(ManagerGame.john_hp - value <= 0)
        {
            ManagerGame.john_hp = 0;
            HPGauge.size = 0;
        }
        else
        {
           ManagerGame.john_hp -= value;
           HPGauge.size = ManagerGame.john_hp / 100f;
        }
     
    }

    public static void HPPlus(int value)
    {
        if(ManagerGame.john_hp + value >= 100)
        {
            ManagerGame.john_hp = 100;
            HPGauge.size = 1;
        }
        else
        {
            ManagerGame.john_hp += value;
            HPGauge.size = ManagerGame.john_hp / 100f;
        }
    
    }

    public static void HPReset()
    {

        ManagerGame.john_hp = 100;
        HPGauge.size = ManagerGame.john_hp / 100f;

    }
    

}
