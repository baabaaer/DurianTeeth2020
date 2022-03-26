using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public CharacterController characterController;
    public StickOfHappiness stickOfHappiness;
    public PlayerMovement playerMovement;


    [Header("Camera")]
    [SerializeField] private float cameraPitch;
    public Transform cameraPole;
    public Transform graphics;
    public float maxCameraDistance;
    public Transform tpCameraTransform;
    public LayerMask cameraObstacleLayers;
   
    [SerializeField] private Vector2 dir = new Vector2(0f, 0f);
    [SerializeField] private bool isMoving;
    
    // Start is called before the first frame update
    void Start()
    {
        // Do we need to implement moveInputDeadZone? Somewhat, yes
        playerMovement = GetComponent<PlayerMovement>();
        // Get the initial angle for the camera pole
        cameraPitch = cameraPole.localRotation.eulerAngles.x;
        // Set max distance to the distance of the camera is from the player in the editor
        // I should toggle this
        //maxCameraDistance = tpCameraTransform.localPosition.z;
        maxCameraDistance = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = stickOfHappiness.Horizontal();
        dir.y = stickOfHappiness.Vertical();
        isMoving = playerMovement.isMoving;

        LookAround(); 
        
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    private void LookAround() 
    {
        cameraPitch = Mathf.Clamp(cameraPitch - dir.y, -90f, 90f);
        cameraPole.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
        if ( !isMoving)
        {
            // Rotate graphics in opposite direction when stationary
            graphics.Rotate(graphics.up, dir.y);
        }
        transform.Rotate(transform.up, dir.x);
    }

    private void MoveCamera() 
    {
        Vector3 rayDir = tpCameraTransform.position - cameraPole.position;
        Debug.DrawRay(cameraPole.position, rayDir, Color.red);
        if(Physics.Raycast(cameraPole.position, rayDir, out RaycastHit hit, Mathf.Abs(maxCameraDistance), cameraObstacleLayers))
        { tpCameraTransform.position = hit.point; }
        else { tpCameraTransform.localPosition = new Vector3(0, 0, maxCameraDistance); }
    }

    
}
