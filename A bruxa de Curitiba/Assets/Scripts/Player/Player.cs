using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("References")]
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

    private int actualLane = 1;
    private Vector3 verticalTargetPosition;
    private bool isJumping = false;
    private float jumpStart;
    private bool isBowing = false;
    private float bowStart;
    private Vector3 boxColliderSize;
    private bool touching = false;
    private Vector2 startingTouch;
    private int coins;
    private int totalCoins;
    private float points;
    private float bestScore;

    private static readonly string RunStartAnim = "runStart";
    private static readonly string JumpingAnim = "Jumping";
    private static readonly string JumpSpeedAnim = "JumpSpeed";
    private static readonly string SlidingAnim = "Sliding";
    private static readonly string DeadAnim = "Dead";

    private void Start()
    {
        boxColliderSize = boxCollider.size;
        anim.Play(RunStartAnim);
        velocity = minVelocity;

        if (PlayerPrefs.GetInt(Constants.EarnedCoinsPref) <= 0)
            PlayerPrefs.SetInt(Constants.EarnedCoinsPref, 0);
        if (PlayerPrefs.GetFloat(Constants.ScorePref) <= 0)
            PlayerPrefs.SetFloat(Constants.ScorePref, 0);

        if (SceneManager.GetActiveScene().name == Constants.Level1Scene)
            PlayerPrefs.SetInt(Constants.CoinsCurrentRunPref, 0);
        else
            uIManager.UpdateCoins(PlayerPrefs.GetInt(Constants.CoinsCurrentRunPref));
    }

    private void Update()
    {
        points += Time.deltaTime * velocity;
        bestScore = points;
        uIManager.UpdatePoints((int)points);

        //Inputs para computador
        if(Input.GetKeyDown(KeyCode.LeftArrow))
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

        if(Input.touchCount == 1)
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

        Vector3 alvoPosicao = new Vector3(verticalTargetPosition.x, verticalTargetPosition.y, transform.position.z);
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
            coins = PlayerPrefs.GetInt(Constants.CoinsCurrentRunPref) + 1;
            uIManager.UpdateCoins(coins);
            other.transform.parent.gameObject.SetActive(false);
            PlayerPrefs.SetInt(Constants.CoinsCurrentRunPref, coins);
        }
        else if (other.CompareTag(Constants.ObstaclesTag))
        {
            velocity = 0;
            anim.SetBool(DeadAnim, true);
            uIManager.gameOver.SetActive(true);
            Invoke(nameof(CallMenu), 5f);

            totalCoins = PlayerPrefs.GetInt(Constants.EarnedCoinsPref) + coins;
            PlayerPrefs.SetInt(Constants.EarnedCoinsPref, totalCoins);

            if (bestScore > PlayerPrefs.GetFloat(Constants.ScorePref))
                PlayerPrefs.SetFloat(Constants.ScorePref, bestScore);
        }
        else if (other.CompareTag(Constants.FinishTag))
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Level1":
                    if (PlayerPrefs.GetInt(Constants.CoinsCurrentRunPref) >= 50)
                        RepeatOrNo.Instance.RepeatNo();
                    else
                        RepeatOrNo.Instance.RepeatYes();
                    break;
                case "Level2":
                    if (PlayerPrefs.GetInt(Constants.CoinsCurrentRunPref) >= 120)
                        RepeatOrNo.Instance.RepeatNo();
                    else
                        RepeatOrNo.Instance.RepeatYes();
                    break;
            }
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
            anim.SetFloat(JumpSpeedAnim, velocity / jumpDistance);
            anim.SetBool(JumpingAnim, true);
            isJumping = true;
        }
    }

    private void GetDown()
    {
        if(!isJumping && !isBowing)
        {
            bowStart = transform.position.z;
            anim.SetFloat(JumpSpeedAnim, velocity / lowerDistance);
            anim.SetBool(SlidingAnim, true);
            Vector3 newSize = boxCollider.size;
            newSize.y /= 2;
            boxCollider.size = newSize;
            isBowing = true;
        }
    }

    private void CallMenu()
    {
        SceneManager.LoadScene(Constants.MenuScene);
    }

    public void IncreaseSpeed()
    {
        velocity *= 1.2f;
        if (velocity >= maxVelocity)
            velocity = maxVelocity;
    }
}
