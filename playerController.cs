using UnityEngine;

public class playerController : MonoBehaviour
{
    public float topSpeed = 10f; // Controls Horizontal Player Speed
    bool facingRight = true; // Determines player direction
    Animator anim; // Calls animator. anim used for shortening of text within script.

    bool grounded = false; // Determines if player is on ground via box detector
    public Transform GroundCheck; //Exists to determine whether player is on the tile/ground
    float groundRadius = 0.2f; // Radius of the ground
    public LayerMask whatIsGround;
    public float jumpForce = 300f; //Force of which the character will jump

    void Start()
    {anim = GetComponent<Animator>();}



	// Update is called once per frame (unity default)
    void Update()
    {
        if (grounded && Input.GetKeyDown(KeyCode.Space)) //if the player is grounded and the player presses space
        {
            anim.SetBool("Grounded", false); // player will not be grounded
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));  //jump force is added, and will be applied to the sprite causing the sprite to go into the air. 
        }

    }
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(GroundCheck.position, groundRadius, whatIsGround); //Dependent on the positionof ground, the radius of the ground and the value of whatIsGround, 
        anim.SetBool("Grounded", grounded); // Player will be set as grounded

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y); //vSpeed variable. Float value for animator. 

        float move = Input.GetAxis("Horizontal"); // WSAD Keys used
        Debug.Log(move);

        GetComponent<Rigidbody2D>().velocity = new Vector2(move * topSpeed, GetComponent<Rigidbody2D>().velocity.y); // Gets RigidBody2D, creates new vector. move x topSpeed, will change velocity on y axis

        anim.SetFloat("Speed", Mathf.Abs(move));

        if (move > 0 && !facingRight) //If the player is not facing right then
            Flip(); //Call Flip
        else if (move < 0 && facingRight) //If the player is facing riught then
            Flip(); //Call Flip

    }

    void Flip() // used to change the direction of the sprite as the player moves
    {
        facingRight = !facingRight; //facingRight equals not facing right. 
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
