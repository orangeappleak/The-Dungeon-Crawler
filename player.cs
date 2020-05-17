using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    private Sprite _sprite;

    [SerializeField]
    private bool _grounded = true;

    [SerializeField]
    private float _jumpForce = 5.0f;

    [SerializeField]
    private float _speed = 2.5f;

    private bool reset_jump = false;

    private playerAnimation _anim;

    private SpriteRenderer _renderer;
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<playerAnimation>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        _grounded = _isOnGround();
    }

    void Movement(){

        float move = Input.GetAxisRaw("Horizontal");
        Debug.DrawRay(transform.position,Vector2.down,Color.red);
        if(move<0) {
            _renderer.flipX = true;
        }
        else if(move > 0) _renderer.flipX = false;

        if(Input.GetKeyDown(KeyCode.Space) && _grounded){
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x ,_jumpForce);
            _anim.jumping(true);
            StartCoroutine(JumpResetCoroutine());
            
        }
        _rigidBody.velocity = new Vector2(move * _speed,_rigidBody.velocity.y);
        _anim.Move(move);
    }

    bool _isOnGround(){
        RaycastHit2D _hitGround = Physics2D .Raycast(transform.position,Vector2.down,1.2f, 1 << 8);
        

        if(_hitGround.collider!=null){
            Debug.Log("hit:" + _hitGround.collider.name);
            if(reset_jump == false){
                _anim.jumping(false);
                return true;
            }
        }
        return false;
    }


    IEnumerator JumpResetCoroutine(){
        reset_jump = true;
        yield return new WaitForSeconds(0.5f);
        reset_jump = false;
    }

}
