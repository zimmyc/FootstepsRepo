using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController: MonoBehaviour

{
    public float speed = 6f;
    public float jumpForce = 8f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float gravity = -9.81f;

    private float _timer;
    private bool canStep;
    public float walkAudioDelay, runAudioDelay;

    public StudioEventEmitter footstepsEmitter;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        if(IsThereInput()) {PlayFootsteps();}

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (canStep) return;
        _timer += Time.deltaTime;
        if (!(_timer >= walkAudioDelay)) return;
        _timer = 0;
        canStep = true;
    }

    private bool IsThereInput()
    {
        return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D);
    }

    private void PlayFootsteps()
    {
        if (canStep)
        {
            footstepsEmitter.Play();
            canStep = false;
        }
        else
        {
            Debug.Log("brb can't step rn");
        }
    }
}
