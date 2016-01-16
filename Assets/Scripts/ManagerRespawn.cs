using UnityEngine;
using System.Collections;

public class ManagerRespawn : MonoBehaviour
{
    public enum State
    {
        level_1, level_2, level_3
    }

    public int[,] tile_index =
        new int[,] { { 0, 0, 0 }, { 0, 1, 0 }, { 1, 0, 0 }, { 0, -1, 0 },
            { -1, 0, 0 }, { 0, 2, 0 }, { 1, 1, 0 }, { 2, 0, 0 },
            { 1, -1, 0 }, { 0, -2, 0 }, { -1, -1, 0 }, { -2, 0, 0 },
            { -1, 1, 0 }, { 0, 3, 0 }, { 1, 2, 0 }, { 2, 1, 0 },
            { 3, 0, 0 }, { 2, -1, 0 }, { 1, -2, 0 }, { 0, -3, 0 },
            { -1, -2, 0 }, { -2, -1, 0 }, { -3, 0, 0 }, { -2, 1, 0 }, { -1, 2, 0 } };

    public float movePixel = 32.0f;

    public int balence_level_1_border = 10;
    public int balence_level_2_border = 20;

    public int balence_level_1_zombieResNum = 1;
    public int balence_level_1_appleResNum = 2;

    public int balence_level_2_zombieResNum = 2;
    public int balence_level_2_appleResNum = 1;

    public int balence_level_3_zombieResNum = 3;
    public int balence_level_3_appleResNum = 1;

    //public float moveSpeed = 1.0f;
    public State state = State.level_1;

    public GameObject johnGhost;

    [HideInInspector]
    public new Transform transform;
    private CharacterController _cc;
    private Animator _animator;

    private int _layermask;

    private GameObject _character_zombie;
    private GameObject _item_apple;
    //public Transform position;

    /*
    if (Input.GetMouseButtonDown(0))
        {
            ManagerGame.ghostMoved = false;
            ManagerGame.moveOn = false;

            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Ray2D ray = new Ray2D(wp, Vector2.zero);

    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10000f, _layermask);

            if(hit.collider != null)
            {
                int layer = hit.transform.gameObject.layer;

                if (layer == LayerMask.NameToLayer("TouchPad"))
                {
                    string tag = hit.transform.gameObject.tag;

    Vector2 johnsNextPosition = transform.position;

                    if (tag == "Right")
                    {
                        //Debug.Log("Right");
                        johnsNextPosition.x += movePixel;
                        johnGhost.transform.position = johnsNextPosition;
                        ManagerGame.ghostMoved = true;
                        state = State.Right;
                        touchPad.transform.position = johnGhost.transform.position;
                        touchPad.SetActive(false);
                    }
                    else if (tag == "Left")
                    {
                        // Debug.Log("Left");
                        johnsNextPosition.x -= movePixel;
                        johnGhost.transform.position = johnsNextPosition;
                        ManagerGame.ghostMoved = true;
                        state = State.Left;
                        touchPad.transform.position = johnGhost.transform.position;
                        touchPad.SetActive(false);
                    }
                    else if (tag == "Up")
                    {
                        //Debug.Log("Up");
                        johnsNextPosition.y += movePixel;
                        johnGhost.transform.position = johnsNextPosition;
                        ManagerGame.ghostMoved = true;
                        state = State.Up;
                        touchPad.transform.position = johnGhost.transform.position;
                        touchPad.SetActive(false);
                    }
                    else if (tag == "Down")
                    {
                        //Debug.Log("Down");
                        johnsNextPosition.y -= movePixel;
                        johnGhost.transform.position = johnsNextPosition;
                        ManagerGame.ghostMoved = true;
                        state = State.Down;
                        touchPad.transform.position = johnGhost.transform.position;
                        touchPad.SetActive(false);
                    }
                    else if (tag == "Mid")
                    {
                        //Debug.Log("Mid");
                        johnGhost.transform.position = johnsNextPosition;
                        ManagerGame.ghostMoved = true;
                        state = State.Idle;
                        touchPad.transform.position = johnGhost.transform.position;
                        //touchPad.SetActive(false);
                    }
                }
            }
        }
    
    */

    void Awake()
    {
        transform = GetComponent<Transform>();
        //_cc = GetComponent<CharacterController>();
        //_animator = GetComponent<Animator>();
        _layermask = LayerMask.GetMask("TouchPad");
        movePixel /= 100;
    }

    void Update()
    {

        //if(ManagerGame.moveOn == true)
        Invoke(state.ToString(), 0.0f);

        if (ManagerGame.gamePhase == 1)
            ProcessInput();
    }

    void ProcessInput()
    {
        

        if (0 <= ManagerGame.gameDays && ManagerGame.gameDays <= balence_level_1_border)
            state = State.level_1;
        else if (balence_level_1_border < ManagerGame.gameDays && ManagerGame.gameDays <= balence_level_2_border)
            state = State.level_2;
        else if (balence_level_2_border < ManagerGame.gameDays)
            state = State.level_3;

        int selectedTile = 0;

        if (state == State.level_1)
        {
            for (int i = 0; i < balence_level_1_zombieResNum; i++)
            {
                selectedTile = Random.Range(13, 24);

                Instantiate(Resources.Load("Prefabs/Character_Zombie"), johnGhost.transform.position +
                    new Vector3(tile_index[selectedTile, 0], tile_index[selectedTile, 1], tile_index[selectedTile, 2]), Quaternion.identity);


            }

            for (int i = 0; i < balence_level_1_appleResNum; i++)
            {
                selectedTile = Random.Range(13, 24);

                Instantiate(Resources.Load("Prefabs/Item_Apple"), johnGhost.transform.position +
                    new Vector3(tile_index[selectedTile, 0], tile_index[selectedTile, 1], tile_index[selectedTile, 2]), Quaternion.identity);
            }
        }
        else if (state == State.level_2)
        {
            for (int i = 0; i < balence_level_2_zombieResNum; i++)
            {
                selectedTile = Random.Range(5, 24);

                Instantiate(Resources.Load("Prefabs/Character_Zombie"), johnGhost.transform.position +
                   new Vector3(tile_index[selectedTile, 0], tile_index[selectedTile, 1], tile_index[selectedTile, 2]), Quaternion.identity);
            }

            for (int i = 0; i < balence_level_2_appleResNum; i++)
            {
                selectedTile = Random.Range(13, 24);

                Instantiate(Resources.Load("Prefabs/Item_Apple"), johnGhost.transform.position +
                   new Vector3(tile_index[selectedTile, 0], tile_index[selectedTile, 1], tile_index[selectedTile, 2]), Quaternion.identity);
            }
        }
        else if (state == State.level_3)
        {
            for (int i = 0; i < balence_level_3_zombieResNum; i++)
            {
                selectedTile = Random.Range(1, 24);

                Instantiate(Resources.Load("Prefabs/Character_Zombie"), johnGhost.transform.position +
                  new Vector3(tile_index[selectedTile, 0], tile_index[selectedTile, 1], tile_index[selectedTile, 2]), Quaternion.identity);
            }

            for (int i = 0; i < balence_level_3_appleResNum; i++)
            {
                selectedTile = Random.Range(13, 24);

                Instantiate(Resources.Load("Prefabs/Character_Apple"), johnGhost.transform.position +
                   new Vector3(tile_index[selectedTile, 0], tile_index[selectedTile, 1], tile_index[selectedTile, 2]), Quaternion.identity);
            }
        }

        ManagerGame.gamePhase = 2;
        //Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Ray2D ray = new Ray2D(wp, Vector2.zero);
        //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        /*
        if (hit.collider != null)
        {
            int layer = hit.transform.gameObject.layer;

            if (layer == LayerMask.NameToLayer("TouchPad"))
            {
                string tag = hit.transform.gameObject.tag;

                Vector2 johnsNextPosition = transform.position;

                if (tag == "Right")
                {
                    Debug.Log("Right");
                    johnsNextPosition.x += movePixel;
                    johnGhost.transform.position = johnsNextPosition;
                    ManagerGame.ghostMoved = true;
                    state = State.Right;
                }
                else if (tag == "Left")
                {
                    Debug.Log("Left");
                    johnsNextPosition.x -= movePixel;
                    johnGhost.transform.position = johnsNextPosition;
                    ManagerGame.ghostMoved = true;
                    state = State.Left;
                }
                else if (tag == "Up")
                {
                    Debug.Log("Up");
                    johnsNextPosition.y += movePixel;
                    johnGhost.transform.position = johnsNextPosition;
                    ManagerGame.ghostMoved = true;
                    state = State.Up;
                }
                else if (tag == "Down")
                {
                    Debug.Log("Down");
                    johnsNextPosition.y -= movePixel;
                    johnGhost.transform.position = johnsNextPosition;
                    ManagerGame.ghostMoved = true;
                    state = State.Down;
                }
                else if (tag == "Mid")
                {
                    Debug.Log("Mid");
                    johnGhost.transform.position = johnsNextPosition;
                    ManagerGame.ghostMoved = true;
                    state = State.Idle;
                }
            }
            */

    }




    /** For Animation State **/
    void Idle()
    {

    }



}
