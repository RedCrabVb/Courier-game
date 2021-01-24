using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float drag_grounded = 7;
    [SerializeField] private float drag_inair = 0.2f;

    public DetectObs detectVaultObject; //checks for vault object
    public DetectObs detectVaultObstruction; //checks if theres somthing in front of the object e.g walls that will not allow the player to vault
    public DetectObs detectClimbObject; //checks for climb object
    public DetectObs detectClimbObstruction; //checks if theres somthing in front of the object e.g walls that will not allow the player to climb

    public DetectObs DetectWallL; //detects for a wall on the left
    public DetectObs DetectWallR; //detects for a wall on the right

    public Animator cameraAnimator;

    [SerializeField] private float WallRunUpForce = 4;
    [SerializeField] private float WallRunUpForce_DecreaseRate = 6;

    private float upforce;

    [SerializeField] private float WallJumpUpVelocity = 7;
    [SerializeField] private float WallJumpForwardVelocity = 2;
    [SerializeField] private float dragWallrun = -0.5f;
    // public float WallJumpUpVelocity, WallJumpForwardVelocity, dragWallrun - it doesn't read very well
    [SerializeField] private bool WallRunning;
    [SerializeField] private bool WallrunningLeft;
    [SerializeField] private bool WallrunningRight;
    private bool canWallRun; // ensure that player can only wallrun once before needing to hit the ground again, can be modified for double wallruns

    [SerializeField] private bool IsParkour;
    private float t_parkour;
    private float chosenParkourMoveTime;

    [SerializeField] private float VaultTime; //how long the vault takes
    [SerializeField] private Transform VaultEndPoint;

    private bool CanClimb;
    [SerializeField] private float ClimbTime; //how long the vault takes
    public Transform ClimbEndPoint;

    private RigidbodyFirstPersonController rbfps;
    private Rigidbody rb;
    private Vector3 RecordedMoveToPosition; //the position of the vault end point in world space to move the player to
    private Vector3 RecordedStartPosition; // position of player right before vault

    // Start is called before the first frame update
    void Start()
    {
        rbfps = GetComponent<RigidbodyFirstPersonController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //something like this is also possible
        if (rbfps.Grounded)
        {
            rb.drag = drag_grounded;
            canWallRun = true;
        }
        else if (WallRunning)
            rb.drag = dragWallrun;
        else
            rb.drag = drag_inair;

        bool CanVault;
        bool is_detects_a_vault_object_and_there_is_no_wall_in_front = (detectVaultObject.Obstruction && !detectVaultObstruction.Obstruction && !IsParkour && !WallRunning
                                                                        && Input.GetAxisRaw("Vertical") > 0f);
        CanVault = is_detects_a_vault_object_and_there_is_no_wall_in_front; // if detects a vault object and there is no wall in front then player can pressing space or in air and pressing forward
        if (CanVault)
        {
            CanVault = false; // so this is only called once
            rb.isKinematic = true; //ensure physics do not interrupt the vault
            RecordedMoveToPosition = VaultEndPoint.position;
            RecordedStartPosition = transform.position;
            IsParkour = true;
            chosenParkourMoveTime = VaultTime;

            cameraAnimator.CrossFade("Vault", 0.1f);
        }
        bool is_detects_a_Climb_object_and_there_is_no_wall_in_front = (detectClimbObject.Obstruction && !detectClimbObstruction.Obstruction && !IsParkour && !WallRunning
                                                                        && (Input.GetKeyDown(KeyCode.Space) || !rbfps.Grounded));// && Input.GetAxisRaw("Vertical") > 0f);
        CanClimb = is_detects_a_Climb_object_and_there_is_no_wall_in_front;
        if (CanClimb)
        {
            CanClimb = false; // so this is only called once
            rb.isKinematic = true; //ensure physics do not interrupt the vault
            RecordedMoveToPosition = ClimbEndPoint.position;
            RecordedStartPosition = transform.position;
            IsParkour = true;
            chosenParkourMoveTime = ClimbTime;

            cameraAnimator.CrossFade("Climb", 0.1f);
        }

        //Parkour movement
        if (IsParkour && t_parkour < 1f)
        {
            t_parkour += Time.deltaTime / chosenParkourMoveTime;
            transform.position = Vector3.Lerp(RecordedStartPosition, RecordedMoveToPosition, t_parkour);

            if (t_parkour >= 1f)
            {
                t_parkour = 0f;
                IsParkour = false;
                rb.isKinematic = false;
            }
        }

        //Wallrun
        bool is_wall_on_the_right_or_left = ((DetectWallR.Obstruction || DetectWallL.Obstruction) && !rbfps.Grounded);
        bool is_not_on_ground = (!IsParkour && canWallRun);
        if (is_wall_on_the_right_or_left && is_not_on_ground) // if detect wall on thr right and is not on the ground, if detect wall on the left and is not on the ground and not doing parkour(climb/vault)
        {
            WallrunningRight = DetectWallR.Obstruction;
            WallrunningLeft = DetectWallL.Obstruction;
            canWallRun = false;
            upforce = WallRunUpForce;
        }

        bool pressing_forward_or_forward_speed_more_1 = Input.GetAxisRaw("Vertical") <= 0f || rbfps.relativevelocity.magnitude < 1f;
        bool is_there_is_no_wall_on_the_left_or_right = ((WallrunningLeft && !DetectWallL.Obstruction) || (WallrunningRight && !DetectWallR.Obstruction));
        if (is_there_is_no_wall_on_the_left_or_right || pressing_forward_or_forward_speed_more_1) // if there is no wall on the lef tor pressing forward or forward speed < 1 (refer to fpscontroller script)
        {
            WallrunningLeft = false;
            WallrunningRight = false;
        }

        WallRunning = (WallrunningLeft || WallrunningRight);
        rbfps.Wallrunning = WallRunning;// this stops the playermovement (refer to fpscontroller script)

        cameraAnimator.SetBool("WallLeft", WallrunningLeft);
        cameraAnimator.SetBool("WallRight", WallrunningRight);//Wallrun camera tilt

        if (WallRunning)
        {
            rb.velocity = new Vector3(rb.velocity.x, upforce, rb.velocity.z); //set the y velocity while wallrunning
            upforce -= WallRunUpForce_DecreaseRate * Time.deltaTime; //so the player will have a curve like wallrun, upforce from line 136

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = transform.forward * WallJumpForwardVelocity + transform.up * WallJumpUpVelocity; //walljump
                WallrunningLeft = false;
                WallrunningRight = false;
            }
            if (rbfps.Grounded)
            {
                WallrunningLeft = false;
                WallrunningRight = false;
            }
        }
    }
}
