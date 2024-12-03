using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float multiplier = 2.5f;
    [SerializeField] private float gravity = Physics.gravity.y;
    [SerializeField] private float jumpForce = 1.5f;

    private CharacterController characterController;
    private Transform cameraTransform;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private Vector2 velocity;
    private float verticalVelocity;
    private float verticalRotation = 0;

    private bool isMoving;
    private bool isSprinting;

    [SerializeField] private float lookSensitivity = 1f;
    private float maxLookAngle = 80f;

    [SerializeField] private float maxEnergy = 1000f;
    [SerializeField] private float energyToWalk = 2f;
    [SerializeField] private float energyToRun = 5f;
    [SerializeField] private float energyRecovery = 10f; //For use only in bars and restaurants
    private float currentEnergy;

    [SerializeField] private Slider energyBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        currentEnergy = maxEnergy;
        energyBar.maxValue = maxEnergy;
        energyBar.value = currentEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.Paused)
        {
            MovePlayer();
            LookAround();
            StartCoroutine(HandleEnergy());
        }
        if (currentEnergy < 0.2f)
        {
            speed = 1f;
        }
    }

    private void Awake()
    {
        isMoving = false;
        isSprinting = false;
    }

    private void MovePlayer()
    {
        //Falling Down
        if (characterController.isGrounded)
        {
            //Restart vertical velocity when touch ground
            verticalVelocity = 0f;
        }
        else
        {
            //when is falling down increment velocity with gravity and time
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 move = new Vector3(0, verticalVelocity, 0);
        characterController.Move(move * Time.deltaTime);

        //Movement 
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        moveDirection = transform.TransformDirection(moveDirection);
        float targetSpeed = isSprinting ? speed * multiplier : speed;
        characterController.Move(moveDirection * targetSpeed * Time.deltaTime);

        //Apply gravity constantly to posibility Jump
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void LookAround()
    {
        //Horizontal rotation (Y-axis) based on sensitivity and input
        float horizontalRotation = lookInput.x * lookSensitivity;
        transform.Rotate(Vector3.up * horizontalRotation);

        //Vertical rotation (X-axis) with clamping to prevent over-rotation
        verticalRotation -= lookInput.y * lookSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    private IEnumerator HandleEnergy()
    {
        if (currentEnergy > 0.2f)
        {
            if (isMoving && !isSprinting)
            {
                currentEnergy -= energyToWalk;
            }
            if (isSprinting)
            {
                currentEnergy -= energyToRun;
            }
            energyBar.value = currentEnergy;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        isMoving = moveInput != Vector2.zero;
        Debug.Log(isMoving);
    }

    public void Look(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        //if Player is touching ground
        if (characterController.isGrounded)
        {
            //Calculate the require velocity for a jump
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Mushroom"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Has taken a shroom"); //TODO manage the minigame thing
        }
        /*if (collision.gameObject.tag.Equals("Guayacan"))
        {
            //SceneManager.LoadScene("Guayacan");
            Debug.Log("Va a entrar en el bar pero no esta hecho"); //TODO falta la escena
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Guayacan"))
        {
            //SceneManager.LoadScene("Guayacan");
            Debug.Log("Va a entrar en el bar pero no esta hecho"); //TODO falta la escena
        }
    }
}
