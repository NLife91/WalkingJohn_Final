using UnityEngine;
using System.Collections;

public class BombCollider : MonoBehaviour
{
    public int maxCount = 3;

    private bool isCollision;

    public int count;

    public GameObject Boom;

    // Use this for initialization
    void AWake()
    {
        count = 0;
        isCollision = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollision == true)
        {
            if (count >= maxCount)
            {
                ManagerGame.bombList.RemoveAt(ManagerGame.bombList.IndexOf(this.gameObject));
                Instantiate(Boom, transform.position, Quaternion.Euler(0, 0, 0));
                Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("BombCollision");
            isCollision = true;
            ManagerGame.bombList.Add(this.gameObject);
        }
    }
}
