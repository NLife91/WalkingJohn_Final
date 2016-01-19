using UnityEngine;
using System.Collections;

public class BoomCollider : MonoBehaviour 
{
    public int zombieScore = 300;
    public float time = 5f;

    private float timer;

    // Use this for initialization
    void AWake()
    {
        timer = time;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            Destroy(gameObject);
            timer = time;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Boom Player");
            ManagerGame.john_hp -= 30;
        }
        else if (other.gameObject.tag == "Zombie")
        {
            ManagerGame.zombieList.RemoveAt(ManagerGame.zombieList.IndexOf(other.gameObject));
            Destroy(other.gameObject);
            ManagerGame.john_zombiekill++;
            ManagerGame.zombieScore += zombieScore;
        }

    }
}
