using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 6.0f;
    public float turnSpeed = 120.0f;
    public static bool Attack;
    private Animator anim;
    private CharacterController characterController;

    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        anim.SetBool("IsMovingForward", false);
        anim.SetBool("IsMovingBackward", false);
        anim.SetBool("IsStrafeLeft", false);
        anim.SetBool("IsStrafeRight", false);
    }

    void Update()
    {
        // Nu permite mișcarea dacă personajul este mort sau în atac
        if (Attack || Fighter4.IsDead) return;

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);
        if (movement.magnitude > 1)
        {
            movement.Normalize(); // Normalizează mișcarea pentru a evita mișcarea rapidă pe diagonală
        }
        movement *= speed * Time.deltaTime;

        // Transformă direcția de mișcare din local în global
        movement = transform.TransformDirection(movement);

        // Muta caracterul folosind CharacterController
        characterController.Move(movement);

        // Rotirea caracterului
        if (horizontalInput != 0)
        {
            transform.Rotate(Vector3.up, horizontalInput * turnSpeed * Time.deltaTime);
        }

        anim.SetBool("Idle", verticalInput == 0 && horizontalInput == 0);
        anim.SetBool("IsMovingForward", verticalInput > 0);
        anim.SetBool("IsMovingBackward", verticalInput < 0);
        anim.SetBool("IsStrafeLeft", horizontalInput < 0);
        anim.SetBool("IsStrafeRight", horizontalInput > 0);
    }
}
