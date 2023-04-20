using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class GoldenSkull_Animated_Character : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;

    [SerializeField]
    Animator animator;

    private Vector3 startScale;

    [SerializeField]
    private SpriteRenderer characterSprite;

    [SerializeField]
    private Transform gridBody;

    // Start is called before the first frame update
    void Start()
    {
        characterSprite = GetComponent<SpriteRenderer>();
        startScale = this.transform.localScale;

        ClickWalk();
        MoveCharacter();

        if (transform.position != targetPos && targetPos != Vector3.zero && canWalk)
        {
            float time = 0;
            Vector3 startPos = transform.position;
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(startPos, targetPos, step);
        }
        else
        {
            canWalk = false;
            walkingHoriz = false;
            walkingVert = false;
            targetPos = Vector3.zero;
        }
    }
    // Update is called once per frame
    void Update()
    {
        ClickWalk();
        MoveCharacter();

        if (transform.position != targetPos && targetPos != Vector3.zero && canWalk) 
        {
            float time = 0;
            Vector3 startPos = transform.position;
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(startPos, targetPos, step);
        }
        else
        {
            canWalk = false;
            walkingHoriz = false;
            walkingVert = false;
            targetPos = Vector3.zero;
        }
    }
    bool canWalk = false;
    bool walkingHoriz = false;
    bool walkingVert = false;
    private Vector3 lastDirection = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private Vector3 targetPos = Vector3.zero;
    float boostH = 0.0f;
    float boostV = 0.0f;
    void ClickWalk()
    {
        //targetPos = Vector3.zero;
        if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos.z = transform.position.z;

            }
        }

        if (Vector2.Distance(transform.position, targetPos) >= 0.25f)
        {
            canWalk = true;
        }

        else if (Vector2.Distance(transform.position, targetPos) < 0.25f)
        {
            canWalk = false;
            targetPos = Vector3.zero;
            walkingHoriz = false;
            walkingVert = false;
        }
        //yield return new WaitUntil(() => transform.position == targetPos);

        if (walkingHoriz)
        {
            boostH = 1.0f;
        }
        else
        {
            boostH = 0.0f;
        }
        if (walkingVert)
        {
            boostV = 1.0f;
        }
        else
        {
            boostV = 0.0f;
        }

        float horizontalMovement = boostH * speed * Time.deltaTime;
        
        float verticalMovement = boostV * speed * Time.deltaTime;

        float totalMovement = Mathf.Abs(horizontalMovement + verticalMovement);

        SetAnimatorStates(totalMovement * 100);

        if (totalMovement != 0)
        {
            FlipSpriteToMovement();
        }

        //this.transform.Translate(horizontalMovement, verticalMovement, 0);

    }

    void SetAnimatorStates(float _currSpeed)
    {
        if( animator!= null)
        {
            //animator.SetFloat("speed", _currSpeed);
        }

        //print($"X > PlayerX (RIGHT) --- {targetPos.x > transform.position.x}");
        //print($"Y > PlayerY (DOWN) --- {targetPos.y > transform.position.y}");

        lastDirection = direction;
        direction = Vector3.zero;

        if (targetPos != Vector3.zero)
        {
            if (targetPos.x < transform.position.x)
            {
                direction.x = -1;
                walkingHoriz = true;
            }
            else if (targetPos.x > transform.position.x)
            {
                direction.x = 1;
                walkingHoriz = true;
            }
            if (targetPos.y < transform.position.y)
            {
                direction.y = -1;
                walkingVert = true;
            }
            else if (targetPos.y > transform.position.y)
            {
                direction.y = 1;
                walkingVert = true;
            }
        }
        else
        {
            
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                direction.x = -1;
                //walkingHoriz = true;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                direction.x = 1;
                ///walkingHoriz = true;
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                direction.y = -1;
                //walkingVert = true;
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                direction.y = 1;
                //walkingVert = true;
            }
            
        }


        //Debug.Log("Input.GetAxisRaw(Horizontal) " + Input.GetAxisRaw("Horizontal"));
        //Debug.Log("Input.GetAxisRaw(Vertical) " + Input.GetAxisRaw("Vertical"));
        //Debug.Log("Direction" + direction + "direction.magnitude:"+ direction.magnitude);

        //Debug.Log($"X: {direction.x} --------- Y: {direction.y}");

        if ((Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) && (!walkingHoriz && !walkingVert) )
        {
            PlayAnimation("Idle", lastDirection);
        }
        else
        {
            PlayAnimation("Run", direction);
        }
    }
    
    void PlayAnimationAtTime(string _animName)
    {
        animator.PlayInFixedTime(_animName);
    }
    void PlayAnimation(string _animName , Vector3 _dir)
    {
        //UP
        if (_dir.y > 0 && _dir.x == 0)
        {
            PlayAnimationAtTime(_animName + "_Up");
        }
        //DOWN
        if (_dir.y < 0 && _dir.x == 0)
        {
            PlayAnimationAtTime(_animName + "_Down");
        }
        //LEFT
        if (_dir.y == 0 && _dir.x < 0)
        {
            PlayAnimationAtTime(_animName + "_Side");
        }
        //RIGHT
        if (_dir.y == 0 && _dir.x > 0)
        {
            PlayAnimationAtTime(_animName + "_Side");
        }
        //Up-Right
        if (_dir.y > 0 && _dir.x > 0)
        {
            PlayAnimationAtTime(_animName + "_Up-Side");
        }
        //Up-Left
        if (_dir.y > 0 && _dir.x < 0)
        {
            PlayAnimationAtTime(_animName + "_Up-Side");
        }
        //Down-Right
        if (_dir.y < 0 && _dir.x > 0)
        {
            PlayAnimationAtTime(_animName + "_Down-Side");
        }
        //Down-Left
        if (_dir.y < 0 && _dir.x < 0)
        {
            PlayAnimationAtTime(_animName + "_Down-Side");
        }
    }

    void MoveCharacter()
    {
        //print(Input.GetAxisRaw("Horizontal"));
        //I am putting these placeholder variables here, to make the logic behind the code easier to understand
        //we differentiate the movement speed between horizontal(x) and vertical(y) movement, since isometric uses "fake perspective"
        float horizontalMovement = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        //since we are using this with isometric visuals, the vertical movement needs to be slower
        //for some reason, 50% feels too slow, so we will be going with 75%
        float verticalMovement = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        float totalMovement = Mathf.Abs(horizontalMovement + verticalMovement);

        SetAnimatorStates(totalMovement * 100);

        if ( totalMovement != 0 )
        {
            FlipSpriteToMovement();
        }

        this.transform.Translate(horizontalMovement, verticalMovement, 0);
    }

    //if the player moves left, flip the sprite, if he moves right, flip it back, stay if no input is made
    void FlipSpriteToMovement()
    {
        Vector3 currScale = startScale;

        if (targetPos == Vector3.zero)
        {

            if (Input.GetAxisRaw("Horizontal") < 0)// || targetPos.x < transform.position.x)
                currScale.x = startScale.x;
            else if (Input.GetAxisRaw("Horizontal") > 0)// || targetPos.x > transform.position.x)
                currScale.x = -startScale.x;
        }
        else
        {
            if (targetPos.x < transform.position.x)// || targetPos.x < transform.position.x)
                currScale.x = startScale.x;
            else if (targetPos.x > transform.position.x)// || targetPos.x > transform.position.x)
                currScale.x = -startScale.x;
        }

        this.transform.localScale = currScale;

        return;
        /*
        if(characterSprite != null )
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
                characterSprite.flipX = true;
            else if (Input.GetAxisRaw("Horizontal") > 0)
                characterSprite.flipX = false;
        }
        */
    }
}
