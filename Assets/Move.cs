using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;

    private SpriteRenderer characterSprite;

    // Start is called before the first frame update
    void Start()
    {
        characterSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        //I am putting these placeholder variables here, to make the logic behind the code easier to understand
        //we differentiate the movement speed between horizontal(x) and vertical(y) movement, since isometric uses "fake perspective"
        float horizontalMovement = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        //since we are using this with isometric visuals, the vertical movement needs to be slower
        //for some reason, 50% feels too slow, so we will be going with 75%
        float verticalMovement = Input.GetAxisRaw("Vertical") * speed * 0.5f * Time.deltaTime;


        this.transform.Translate(horizontalMovement, verticalMovement, 0);
    }
}
