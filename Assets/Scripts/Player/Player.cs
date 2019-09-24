using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    [SerializeField]
    private float _speed = 5.0f;
    private bool _grounded = false;
    //handle  to PlayerAnimation
    private PlayerAnimation _PlayerAnim;
    private SpriteRenderer _PlayerSprite;
    private SpriteRenderer _swordArcSprite;


	void Start ()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _PlayerAnim = GetComponent<PlayerAnimation>();
        _PlayerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Movement();

        if (Input.GetMouseButtonDown(0) && IsGrounded() == true)
        {
            _PlayerAnim.Attack();
        }    
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();
        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()==true)
        {

            Debug.Log("Jump!");
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(RestJumpRoutine());
            _PlayerAnim.Jump(true);
            
        }

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);
        _PlayerAnim.Move(move);

    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfor = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, 1 << 8);

        if (hitInfor.collider != null)
        {
            if (_resetJump == false)
            {
                _PlayerAnim.Jump(false);
               
                return true;
            }
            
        }
        return false;
    }

    void Flip(bool faceRight)
    {
        if (faceRight == true)
        {
            _PlayerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newpo = _swordArcSprite.transform.localPosition;
            newpo.x = 1.01f;
            _swordArcSprite.transform.localPosition = newpo;
        }
        else if (faceRight == false)
        {
            _PlayerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newpo = _swordArcSprite.transform.localPosition;
            newpo.x = -1.01f;
            _swordArcSprite.transform.localPosition = newpo;
        }
    }
    IEnumerator RestJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

}
