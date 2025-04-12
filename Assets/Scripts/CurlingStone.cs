using UnityEngine;
using UnityEngine.UI;

public class CurlingStone : MonoBehaviour
{
    public float maxForce;
    public float easeFactor;
    private float holdTime = 0f;
    private bool isHeld = false;
    public Rigidbody2D rb;
    private Vector2 launchDirection = Vector2.up; // Always launch upwards
    public bool beenLaunched = false; 
    public Transform winningTransform;
    public float winThreshold;
    public Slider forceAmountSlider; 
    private bool fluctuate; 
    public GameObject gameManager;
    public int stoneHealth = 15;
    public float burnTime = 15f;
    public GameObject fireParticles;
    public bool burning;
    public bool stoppedMoving = false;
    public bool blocker;

    public AudioSource launchAudioSource;
    public AudioClip launchSfx;
    public AudioSource winAudioSource;
    public AudioClip winSfx;
    public AudioSource resetAudioSource;
    public AudioClip resetSfx;
    bool playedYet;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");

        GameObject sliderObject = GameObject.FindGameObjectWithTag("Slider");
        forceAmountSlider = sliderObject.GetComponent<Slider>();

        winningTransform = GameObject.FindGameObjectWithTag("WinArea").transform;

        rb = GetComponent<Rigidbody2D>();
        forceAmountSlider.value = 0f;
    }

    void OnMouseDown()
    {
        if(beenLaunched==false)
        {
            isHeld = true;
            holdTime = 0f;
        }
    }

    void OnMouseUp()
    {
        if (isHeld)
        {
            isHeld = false;
            LaunchCurlingStone();
        }
    }

    void Update()
    {
        if (isHeld)
        {
            if (holdTime>=1)
            {
                fluctuate=true;
            }
            if (holdTime<=0)
            {
                fluctuate=false;
            }

            if (fluctuate==true)
            {
                holdTime -= Time.deltaTime;
            }
            else
            {
                holdTime += Time.deltaTime;
            }
            forceAmountSlider.value = holdTime;
        }

        if (rb.linearVelocity.magnitude > 0.1f) 
        {
            rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0, Time.deltaTime * easeFactor);
        }
    }

    public void LaunchCurlingStone()
    {
        beenLaunched=true; 
        float force = Mathf.Clamp(holdTime * maxForce, 0f, maxForce);
        rb.AddForce(launchDirection * force, ForceMode2D.Impulse);
        launchAudioSource.PlayOneShot(launchSfx);
        gameManager.GetComponent<GameManager>().stoneLaunched=true; 
        playedYet = false;
    }

    public void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > 0)//Movement and ease out code
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, Time.deltaTime * easeFactor);
        }

        if (rb.linearVelocity.magnitude < 0.1f && beenLaunched)//when the stone is done moving
        {
            float distanceToWinningPosition = Vector2.Distance(transform.position, winningTransform.position);
            if (stoppedMoving==false)
            {
                Debug.Log(stoppedMoving);
                if (distanceToWinningPosition < winThreshold)
                {
                    gameManager.GetComponent<GameManager>().WinGame();
                    DoSound(winAudioSource, winSfx);
                }
                else
                {
                    StartCoroutine(gameManager.GetComponent<GameManager>().ResetStone(gameObject,false));
                    DoSound(resetAudioSource, resetSfx);
                }
            }
            stoppedMoving=true;
        }
        TryBurn();
    }
    public virtual void HitByBeam()
    {
        if (stoneHealth > 0)
        {
            stoneHealth -= 1;
        }
    }

    public virtual void TryBurn()
    {
        if (stoneHealth == 0 && fireParticles.activeInHierarchy != true)
        {
            fireParticles.SetActive(true);
            burning = true;
        }
        if (burning == true)
        {
            burnTime -= 0.1f;
        }
        if (burnTime <= 0)
        {
            StartCoroutine(gameManager.GetComponent<GameManager>().ResetStone(gameObject,true));
        }
    }

    void DoSound(AudioSource CurrentAS, AudioClip CurrentAC)
    {
        if (playedYet != true)
        {
            CurrentAS.PlayOneShot(CurrentAC);
            playedYet = true;
        }
    }
}
