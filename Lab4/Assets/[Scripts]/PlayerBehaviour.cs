using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public CharacterController controller;

    [Header("Movement Properties")]
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;
    public Vector3 velocity;

    [Header("Ground Detection Properties")]
    public Transform groundPoint;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;
    public bool isGrounded;

    private HealthBarController _healthBar;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        _healthBar = FindAnyObjectByType<HealthBarController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundPoint.position, groundRadius, groundMask);

        if (isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * maxSpeed * Time.deltaTime);

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hazard"))
        {
            Debug.Log("I got hit by a nice hazard!");

            _healthBar.GetDamage(20);
        }
    }
}
