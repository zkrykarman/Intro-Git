using UnityEngine;
using UnityEngine.InputSystem;

public class Controler : MonoBehaviour
{
    public int walkSpeed;
    public int runSpeed;

    public float sensitivityX;
    public float sensitivityY;

    public float minY = -60f;
    public float maxY = 60f;

    int moveSpeed;

    CharacterController controller;
    Vector3 moveDirection;
    bool jump;
    bool run;

    Transform cam;
    Transform player;

    float lookX;
    float lookY;




    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        player = transform;
        if (Camera.main != null) cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
    }

    void Look()
    {
        lookY = Mathf.Clamp(lookY, minY, maxY);
        player.localEulerAngles = new Vector3(0, lookX, 0);
        cam.localEulerAngles = new Vector3(lookY, 0, 0);
    }

    void Move()
    {
        if (run)
        {
            moveSpeed = runSpeed;
        }

        if (!run)
        {
            moveSpeed = walkSpeed;
        }

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookX += context.ReadValue<Vector2>().x * sensitivityX * Time.deltaTime;
        lookY -= context.ReadValue<Vector2>().y * sensitivityY * Time.deltaTime;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump = context.started;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        run = context.performed;
    }
}
