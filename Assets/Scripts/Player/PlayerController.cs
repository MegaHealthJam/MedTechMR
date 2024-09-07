using UnityEngine;


public class PlayerController : MonoBehaviour
{
   public float moveSpeed = 5f;
   public float lookSpeed = 2f;
   public float jumpForce = 5f;
   public Transform playerCamera;

   private CharacterController controller;
   private float xRotation = 0f;

   // Start is called before the first frame update
   void Start()
   {
      controller = GetComponent<CharacterController>();
      Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center screen
      Cursor.visible = false;
   }

   // Update is called once per frame
   void Update()
   {
      MovePlayer();
      LookAround();
   }

   void MovePlayer()
   {
      // Get input for movement
      float moveX = Input.GetAxis("Horizontal"); // A and D
      float moveZ = Input.GetAxis("Vertical");   // W and S

      // Move relative to the camera's forward and right direction
      Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
      controller.Move(moveDirection * (moveSpeed * Time.deltaTime));

      // Optional: Jump logic (spacebar)
      if (Input.GetButtonDown("Jump") && controller.isGrounded)
      {
         Vector3 jump = Vector3.up * jumpForce;
         controller.Move(jump * Time.deltaTime);
      }
   }

   void LookAround()
   {
      // Get mouse input for looking
      float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
      float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

      // Rotate the player horizontally based on mouse X movement
      transform.Rotate(Vector3.up * mouseX);

      // Rotate the camera vertically based on mouse Y movement
      xRotation -= mouseY;
      xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit up/down rotation
      playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
   }
}
