using UnityEngine;
using System.Collections;

public class ManagerCharacter : MonoBehaviour
{
    public enum State
    {
        Idle, Right, Left, Up, Down, Dead
    }

    public float movePixel = 1f;
    public float moveSpeed = 3f;
    public State state = State.Idle;

    public GameObject johnGhost;
    public GameObject touchPad;

    [HideInInspector]
    public new Transform transform;
    private Animator _animator;

    private int _layermask;
    private new Rigidbody2D rigidbody2D;

    void Awake()
    {
        transform = GetComponent<Transform>();
        //_animator = GetComponent<Animator>();
        _layermask = LayerMask.GetMask("TouchPad");
        rigidbody2D = GetComponent<Rigidbody2D>();
        johnGhost.transform.position = transform.position;
        touchPad.transform.position = transform.position;
    }

    void Update()
    {
        if (ManagerGame.ghostMoved == true)
            Invoke(state.ToString(), 0.0f);

        if (state == State.Dead)
            return;

        if (ManagerGame.ghostMoved != true)
            ProcessInput();
    }

    void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ManagerGame.ghostMoved = false;
            //ManagerGame.moveOn = false;

            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(wp, Vector2.zero);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10000f, _layermask);

            if (hit.collider != null)
            {
                Debug.Log(hit.transform.name);
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
                        ManagerGame.zombieMove = true;
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
                        ManagerGame.zombieMove = true;
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
                        ManagerGame.zombieMove = true;
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
                        ManagerGame.zombieMove = true;
                        state = State.Down;
                        touchPad.transform.position = johnGhost.transform.position;
                        touchPad.SetActive(false);
                    }
                    else if (tag == "Mid")
                    {
                        //Debug.Log("Mid");
                        johnGhost.transform.position = johnsNextPosition;
                        ManagerGame.ghostMoved = true;
                        ManagerGame.zombieMove = true;
                        state = State.Idle;
                        touchPad.transform.position = johnGhost.transform.position;
                        //touchPad.SetActive(false);
                    }
                }
            }
        }
    }


    /** For Animation State **/
    void Idle()
    {
        ManagerGame.ghostMoved = false;
        ManagerGame.zombieMove = false;
        //ManagerGame.moveOn = false;
        //touchPad.SetActive(true);
    }

    void Right()
    {
        rigidbody2D.velocity = Vector2.right * moveSpeed;
        if (Vector2.Distance(transform.position, johnGhost.transform.position) <= 0.1f)
        {
            rigidbody2D.velocity = Vector2.zero;
            transform.position = johnGhost.transform.position;
            state = State.Idle;
            touchPad.SetActive(true);
            return;
        }
    }

    void Left()
    {
        rigidbody2D.velocity = Vector2.left * moveSpeed;
        if (Vector2.Distance(transform.position, johnGhost.transform.position) <= 0.1f)
        {
            rigidbody2D.velocity = Vector2.zero;
            transform.position = johnGhost.transform.position;
            state = State.Idle;
            touchPad.SetActive(true);
            return;
        }
    }

    void Up()
    {
        rigidbody2D.velocity = Vector2.up * moveSpeed;
        if (Vector2.Distance(transform.position, johnGhost.transform.position) <= 0.1f)
        {
            rigidbody2D.velocity = Vector2.zero;
            transform.position = johnGhost.transform.position;
            state = State.Idle;
            touchPad.SetActive(true);
            return;
        }
    }

    void Down()
    {
        rigidbody2D.velocity = Vector2.down * moveSpeed;
        if (Vector2.Distance(transform.position, johnGhost.transform.position) <= 0.1f)
        {
            rigidbody2D.velocity = Vector2.zero;
            transform.position = johnGhost.transform.position;
            state = State.Idle;
            touchPad.SetActive(true);
            return;
        }
    }

}
