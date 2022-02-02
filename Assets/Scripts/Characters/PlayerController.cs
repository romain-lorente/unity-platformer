using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Camera cam;
    bool grounded;
    bool climbingLadder;
    bool isSprinting;
    bool isJumping;
    int currentCooldown;
    int ladderCooldown;
    Vector3 velocityBeforePhysicsUpdate;

    public float movSpeedPerTick = 4f;
    public float ladderMovSpeedPerTick = 5f;
    public float jumpForce = 7f;
    public float sprintingJumpForce = 4f;
    public float rayDistance = 0.5f;
    public float ladderRayDistance = 0.35f;
    public float cameraFov = 60.0f;
    public float cameraFovWhileSprinting = 70.0f;
    public float cameraMovementStep = 0.3f;
    public int shootingCooldownFrames = 30;
    public int ladderCooldownFrames = 20;
    public float projectileOffset = 0.5f;

    public GameObject projectile;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        velocityBeforePhysicsUpdate = rb.velocity;
    }

    private void Update()
    {
        if (ladderCooldown > 0)
        {
            ladderCooldown--;
        }

        ControlLadder();
        ControlLanding();

        if (climbingLadder && !isJumping)
        {
            ControlLadderClimbing();
        }
        else
        {
            ControlMovement();
            ControlShooting();
        }

        ControlJump();
    }

    public Vector3 getPreviousVelocity()
    {
        return velocityBeforePhysicsUpdate;
    }

    private void ControlLadder()
    {
        RaycastHit hit;

        climbingLadder = (Physics.Raycast(transform.position, transform.right, out hit, ladderRayDistance) && hit.transform.tag == "Ladder" && ladderCooldown < 1);

        if (climbingLadder)
            isJumping = false;
    }

    private void ControlLanding()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, rayDistance);

        if (grounded)
            isJumping = false;
    }

    private void ControlJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded || climbingLadder)
            {
                float actualJumpForce = isSprinting ? sprintingJumpForce : jumpForce;
                rb.velocity = new Vector3(0, actualJumpForce, 0);

                isJumping = true;

                if(climbingLadder)
                {
                    climbingLadder = false;
                    ladderCooldown = ladderCooldownFrames;
                }
            }
        }
    }

    private void ControlShooting()
    {
        if (currentCooldown == 0 && Input.GetButton("Fire1"))
        {
            currentCooldown = shootingCooldownFrames;

            Vector3 position = transform.position + (transform.right * projectileOffset);
            GameObject bullet = Instantiate(projectile, position, transform.rotation) as GameObject;

            Vector3 force = transform.right * 5;

            bullet.GetComponent<Rigidbody>().AddForce(force);
        }

        currentCooldown = currentCooldown > 0 ? currentCooldown - 1 : 0;
    }

    private void ControlSprinting(float vAxis)
    {
        if (grounded)
            isSprinting = Input.GetButton("Sprint") && vAxis > 0;

        if(isSprinting)
        {
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, cameraFovWhileSprinting, cameraMovementStep);
        }
        else
        {
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, cameraFov, cameraMovementStep * 2);
        }
    }

    private void ControlMovement()
    {
        var camera = Camera.main;

        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        ControlSprinting(verticalAxis);

        float facing = camera.transform.rotation.y;
        float DistanceFromNeutral = 0;
        float transformZ = 0;
        float transformX = 0;

        if (facing > -90 && facing <= 90)
        {
            if (facing >= 0)
            {
                DistanceFromNeutral = (90 - facing);
            }
            else
            {
                if (facing < 0)
                {
                    DistanceFromNeutral = (90 + facing);
                };
            };

            transformX = (1 / 90) * (DistanceFromNeutral);
            transformZ = 90 - transformX;
        };

        if(isSprinting)
        {
            horizontalAxis = 0;
            verticalAxis *= 1.5f;

            if (verticalAxis < 0)
                verticalAxis = 0;
        }

        float finalX = (transformX * -horizontalAxis) + (transformZ * verticalAxis);
        float finalZ = (transformZ * -horizontalAxis) + (transformX * verticalAxis);

        transform.Translate((new Vector3(finalX * 0.01f, 0f, finalZ * 0.01f)) * movSpeedPerTick * Time.deltaTime);

        rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
    }

    private void ControlLadderClimbing()
    {
        float verticalAxis = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(0f, verticalAxis * ladderMovSpeedPerTick * Time.deltaTime, 0f));

        rb.velocity = new Vector3(0f, 0f, 0f);
    }

    void OnDeath()
    {
        Debug.Log("u ded lmao");
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        switch(collider.gameObject.tag)
        {
            case "Coin":
                Destroy(collider.gameObject);
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "PlayerProjectile":
                Physics.IgnoreCollision(GetComponent<Collision>().collider, collision.collider);
                break;

            case "Hazard":
                OnDeath();
                break;
        }
    }
}
