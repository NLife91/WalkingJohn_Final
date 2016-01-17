﻿using UnityEngine;
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
    private Vector2 prevPosition;
    private Vector2 johnsNextPosition;



    void Awake()
    {
        transform = GetComponent<Transform>();
        prevPosition = transform.position;
        //_animator = GetComponent<Animator>();
        _layermask = LayerMask.GetMask("TouchPad");
        johnGhost.transform.position = transform.position;
        touchPad.transform.position = transform.position;
    }

    void Update()
    {
        if (ManagerGame.ghostMoved == true)
            Invoke(state.ToString(), 0.0f);

        if (state == State.Dead)
            return;

        if (ManagerGame.ghostMoved != true && ManagerGame.zombieMoved != true)
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

                    johnsNextPosition = transform.position;
                    prevPosition = transform.position;
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
                        ////Debug.Log("Mid");
                        //johnGhost.transform.position = johnsNextPosition;
                        //ManagerGame.ghostMoved = true;
                        //state = State.Idle;
                        //touchPad.transform.position = johnGhost.transform.position;
                        //touchPad.SetActive(false);
                        Mid();
                    }
                }
            }
        }
    }

    public void Mid()
    {
        johnsNextPosition = transform.position;
        prevPosition = transform.position;
        johnGhost.transform.position = johnsNextPosition;
        ManagerGame.ghostMoved = true;
        state = State.Idle;
        touchPad.transform.position = johnGhost.transform.position;
        touchPad.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Tile_Rock" || coll.gameObject.tag == "Tile_Sea")
        {
            johnGhost.transform.position = prevPosition;
            transform.position = prevPosition;
            touchPad.transform.position = prevPosition;
        }
        else if (coll.gameObject.tag == "Item_Apple")
        {
            ManagerGame.john_hp += 20;
            if (ManagerGame.john_hp > 100)
                ManagerGame.john_hp = 100;
            Debug.Log("Apple");
            Debug.Log(ManagerGame.john_hp);
            coll.gameObject.SetActive(false);
        }

    }


    /** For Animation State **/
    void Idle()
    {

        ManagerGame.ghostMoved = false;

        //ManagerGame.moveOn = false;
        touchPad.SetActive(true);
    }

    void Right()
    {
        if (MoveUtil.MoveByFrame(transform, johnGhost.transform.position, moveSpeed) == 0.0f)
        {
            transform.position = johnGhost.transform.position;
            state = State.Idle;
            touchPad.SetActive(true);
            return;
        }
    }

    void Left()
    {
        if (MoveUtil.MoveByFrame(transform, johnGhost.transform.position, moveSpeed) == 0.0f)
        {
            transform.position = johnGhost.transform.position;
            state = State.Idle;
            touchPad.SetActive(true);
            return;
        }
    }

    void Up()
    {
        if (MoveUtil.MoveByFrame(transform, johnGhost.transform.position, moveSpeed) == 0.0f)
        {
            transform.position = johnGhost.transform.position;
            state = State.Idle;
            touchPad.SetActive(true);
            return;
        }
    }

    void Down()
    {
        if (MoveUtil.MoveByFrame(transform, johnGhost.transform.position, moveSpeed) == 0.0f)
        {
            transform.position = johnGhost.transform.position;
            state = State.Idle;
            touchPad.SetActive(true);
            return;
        }
    }

}
