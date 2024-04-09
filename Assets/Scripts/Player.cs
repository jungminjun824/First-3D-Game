using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public int JumpPower;

    float hAxis;
    float vAxis;

    bool wDwon;
    bool jDwon;
    bool isJump;

    Vector3 moveVec;

    Rigidbody rigid;

    Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

    void Update()
    {
        GetInput();
        Move();
        turn();
        Jump();
    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDwon = Input.GetButton("Walk");
        jDwon = Input.GetButtonDown("Jump");
    }
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * (wDwon ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDwon);
    }
    void turn()
    {
        transform.LookAt(transform.position + moveVec);

    }
    void Jump()
    {
        if (jDwon && !isJump)
        {
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }
}
