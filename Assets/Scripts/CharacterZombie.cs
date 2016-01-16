using UnityEngine;
using System.Collections;

public class CharacterZombie : MonoBehaviour 
{
    public enum State
    {
        Idle, Right, Left, Up, Down, Dead
    }

    public float movePixel = 1f;
    public float moveSpeed = 1.2f;
    public State state = State.Idle;

    [HideInInspector]
    public new Transform transform;
    private Animator _animator;

    public GameObject john; // 나중에 private로 만들어야됨. 리스폰매니저가 좀비 생성할때 johnGhost가지고 있다가 대입해주어야함
    private Vector2 prevPosition;
    private Vector2 nextPosition;


    private new Rigidbody2D rigidbody2D;
    private bool isCalcued;
    void Awake()
    {
        transform = GetComponent<Transform>();
        nextPosition = transform.position;
        //_animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        isCalcued = false;
    }

    void Update()
    {
        if (state == State.Dead)
            return;
        else if (state == State.Idle)
        {
            if (ManagerGame.zombieMove != true)
                isCalcued = false;
        }

        if (ManagerGame.ghostMoved == true && isCalcued == false)
        {
            ProcessNextPosition();
        }
        else if (ManagerGame.zombieMove == true && isCalcued == true)
        {
            Invoke(state.ToString(), 0.0f);
        }
    }
    
    void ProcessNextPosition()
    {
        float tx = transform.position.x;
        float ty = transform.position.y;
        float jx = john.transform.position.x;
        float jy = john.transform.position.y;
        float distanceX = Mathf.Abs(tx - jx);
        float distanceY = Mathf.Abs(ty - jy);
        Vector2 next = transform.position;

        float r = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);

        if (r <= 1f)
        {
            if (distanceX < distanceY)
            {
                if (ty < jy)
                {
                    // ^
                    next.y += movePixel;
                    nextPosition = next;
                    state = State.Up;
                }
                else
                {
                    // v
                    next.y -= movePixel;
                    nextPosition = next;
                    state = State.Down;
                }
            }
            else if (distanceX > distanceY)
            {
                if (tx < jx)
                {
                    // >
                    next.x += movePixel;
                    nextPosition = next;
                    state = State.Right;
                }
                else
                {
                    // <
                    next.x -= movePixel;
                    nextPosition = next;
                    state = State.Left;
                }
            }
        }
        else
        {

        }

        isCalcued = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "JohnGhost")
        {
            Debug.Log("PlayerCollision");
            Destroy(this.gameObject);
        }

    }

    /** For Animation State **/
    void Idle()
    {
        
    }

    void Right()
    {
        rigidbody2D.velocity = Vector2.right * moveSpeed;
        Debug.Log(Vector2.Distance(transform.position, nextPosition));
        if (Vector2.Distance(transform.position, nextPosition) <= 1f)
        {

            rigidbody2D.velocity = Vector2.zero;
            transform.position = nextPosition;
            state = State.Idle;
            return;
        }
    }

    void Left()
    {
        rigidbody2D.velocity = Vector2.left * moveSpeed;
        Debug.Log(Vector2.Distance(transform.position, nextPosition));

        if (Vector2.Distance(transform.position, nextPosition) <= 1f)
        {

            rigidbody2D.velocity = Vector2.zero;
            transform.position = nextPosition;
            state = State.Idle;
            return;
        }
    }

    void Up()
    {
        rigidbody2D.velocity = Vector2.up * moveSpeed;
        Debug.Log(Vector2.Distance(transform.position, nextPosition));
        if (Vector2.Distance(transform.position, nextPosition) <= 1f)
        {

            rigidbody2D.velocity = Vector2.zero;
            transform.position = nextPosition;
            state = State.Idle;
            return;
        }
    }

    void Down()
    {
        rigidbody2D.velocity = Vector2.down * moveSpeed;
        Debug.Log(Vector2.Distance(transform.position, nextPosition));
        if (Vector2.Distance(transform.position, nextPosition) <= 1f)
        {

            rigidbody2D.velocity = Vector2.zero;
            transform.position = nextPosition;
            state = State.Idle;
            return;
        }
    }
}
