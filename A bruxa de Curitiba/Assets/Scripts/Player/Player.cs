using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private UIManager uIManager;

    [Header("Properties")]
    [SerializeField] private float velocity = 10f;
    [SerializeField] private float laneVelocity = 10f;
    [SerializeField] private float minVelocity = 10f;
    [SerializeField] private float maxVelocity = 30f;
    [SerializeField] private float jumpDistance = 7.5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float lowerDistance = 10f;

    public int Coins { get; private set; }

    private int actualLane = 1;
    private Vector3 verticalTargetPosition;
    private bool isJumping = false;
    private float jumpStart;
    private bool isBowing = false;
    private float bowStart;
    private Vector3 boxColliderSize;
    private float points;
    private float bestScore;

#if !UNITY_EDITOR && UNITY_ANDROID
    private bool touching = false;
    private Vector2 startingTouch;
#endif

    private static readonly string JumpingAnim = "Jumping";
    private static readonly string SlidingAnim = "Sliding";
    private static readonly string DeadAnim = "Dead";

    private void Start()
    {
        boxColliderSize = boxCollider.size;
        velocity = minVelocity;

        uIManager.UpdateCoins(Coins);
    }

    private void Update()
    {
        points += Time.deltaTime * velocity;
        bestScore = points;
        uIManager.UpdatePoints((int)points);

#if !UNITY_EDITOR && UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            if(touching)
            {
                Vector2 diff = Input.GetTouch(0).position - startingTouch;
                diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);
                if(diff.magnitude > 0.01f)
                {
                    if(Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
                    {
                        if (diff.y < 0)
                            GetDown();
                        else
                            Jump();
                    }
                    else
                    {
                        if (diff.x < 0)
                            ChangeLane(-1);
                        else
                            ChangeLane(1);
                    }

                    touching = false;
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startingTouch = Input.GetTouch(0).position;
                touching = true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touching = false;
            }
        }
#else
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GetDown();
        }
#endif
        if (isJumping)
        {
            float razao = (transform.position.z - jumpStart) / jumpDistance;
            if(razao >= 1)
            {
                isJumping = false;
                anim.SetBool(JumpingAnim, false);
            }
            else
            {
                verticalTargetPosition.y = Mathf.Sin(razao * Mathf.PI) * jumpHeight;
            }
        }
        else
        {
            verticalTargetPosition.y = Mathf.MoveTowards(verticalTargetPosition.y, 0, 5 * Time.deltaTime);
        }

        if(isBowing)
        {
            float razao = (transform.position.z - bowStart) / lowerDistance;
            if(razao >=1f)
            {
                isBowing = false;
                anim.SetBool(SlidingAnim, false);
                boxCollider.size = boxColliderSize;
            }
        }

        Vector3 alvoPosicao = new(verticalTargetPosition.x, verticalTargetPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, alvoPosicao, laneVelocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.CoinTag))
        {
            Coins++;
            other.gameObject.SetActive(false);
            uIManager.UpdateCoins(Coins);
        }
        else if (other.CompareTag(Constants.ObstaclesTag))
        {
            velocity = 0;
            anim.SetBool(DeadAnim, true);
            uIManager.gameOver.SetActive(true);
            Invoke(nameof(CallMenu), 5f);

            playerSO.TotalNumberOfCoins += Coins;

            if (bestScore > playerSO.BestScore) playerSO.BestScore = (int)bestScore;
        }
    }

    private void ChangeLane(int direction)
    {
        int targetLane = actualLane + direction;

        if (targetLane < 0 || targetLane > 2)
            return;

        actualLane = targetLane;
        verticalTargetPosition = new Vector3(actualLane - 1, 0, 0);
    }

    private void Jump()
    {
        if(!isJumping)
        {
            jumpStart = transform.position.z;
            anim.SetBool(JumpingAnim, true);
            isJumping = true;
        }
    }

    private void GetDown()
    {
        if(!isJumping && !isBowing)
        {
            bowStart = transform.position.z;
            anim.SetBool(SlidingAnim, true);
            Vector3 newSize = boxCollider.size;
            newSize.y /= 2;
            boxCollider.size = newSize;
            isBowing = true;
        }
    }

    private void CallMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void IncreaseSpeed()
    {
        velocity *= 1.2f;
        if (velocity >= maxVelocity)
            velocity = maxVelocity;
    }
}
