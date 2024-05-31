using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour, IDamageable, IMoveable
{

    [Header("Player Stats")]

    [SerializeField] float health;
    [SerializeField] float maxHealth;
    #region Getter and Setter implementations for IDamageable interface.
    public float Health
    {
        get
        {
            return this.health;
        }

        private set
        {
            if (value > MaxHealth)
            {
                this.health = MaxHealth;
                return;
            }

            if (value > 0)
            {
                this.health = value;
            }
        }

    }
    public float MaxHealth
    {
        get { return this.maxHealth; }
        private set
        {
            if (value > 0)
            {
                this.maxHealth = value;
            }

        }
    }




    #endregion
    [SerializeField] float currentRunSpeed;
    [SerializeField] float maxRunSpeed;
    [SerializeField] float normalRunSpeed;
    [SerializeField] float jumpSpeed;
    #region Getter and Setter implementations for IMoveable interface.
    public float CurrentRunSpeed
    {
        get { return currentRunSpeed; }
        set
        {
            if (value >= 0)
            {
                currentRunSpeed = value;
            }
        }
    }
    public float MaxRunSpeed
    {
        get { return maxRunSpeed; }
        set
        {
            if (value >= 0)
            {
                maxRunSpeed = value;
            }
        }
    }
    public float NormalRunSpeed
    {
        get { return normalRunSpeed; }
        set { }
    }
    public float JumpSpeed
    {
        get { return jumpSpeed; }
        set
        {
            if (value >= 0)
            {
                jumpSpeed = value;
            }
        }
    }


    #endregion
    [SerializeField] float[] TargetXPList = { 5, 15, 35, 50, 85, 100 };
    [SerializeField] public LevelManager levelStats;

    [Header("Helper GameObjects")]
    [SerializeField] Transform GroundCheckObject;
    [SerializeField] float groundCheckDistance = 0.01f;
    [SerializeField] GameObject CollectibleTriggerObject;

    [Header("Player Status")]
    [SerializeField] bool isFlip;
    [SerializeField] public bool isGrounded;


    [HideInInspector] public PlayerStateManager playerStateManager;
    [HideInInspector] public Animator playerAnimator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public QuestController questController;


    void Awake()
    {

        playerStateManager = new PlayerStateManager(this);
        DialogManager.Instance.OnQuestAttached += OnQuestReceivedHandler;
    }

    void Start()
    {
        MaxHealth = 100;
        Health = MaxHealth;
        isFlip = transform.localScale.x != 1;

        levelStats = new LevelManager(TargetXPList);

        playerAnimator = GetComponent<Animator>();
        playerStateManager.Initialization(playerStateManager.STATE_IDLE);
        rb = GetComponent<Rigidbody2D>();
        questController = GetComponent<QuestController>();
    }

    void FixedUpdate()
    {
        playerStateManager.UpdateState();
        Move();
    }

    private void Update()
    {
        Jump();

    }

    private void OnDestroy()
    {
        DialogManager.Instance.OnQuestAttached -= OnQuestReceivedHandler;
    }

    public void SetFlipping(bool flip)
    {
        transform.localScale = new Vector3(flip ? -1 : 1, transform.localScale.y, transform.localScale.z);
        isFlip = flip;
    }

    public bool CheckIsGrounded()
    {

        Collider2D[] cols = Physics2D.OverlapCircleAll(GroundCheckObject.position, groundCheckDistance, 1 << 6);
        isGrounded = cols.Length > 0;
        return isGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(GroundCheckObject.position, -transform.up * groundCheckDistance);

    }

    public void OnQuestReceivedHandler(List<Quest> quests)
    {

        foreach (Quest quest in quests)
        {
            questController.AddQuest(quest);
        }
    }

    #region IDamageable methods
    public void Damage(int amount)
    {
        if (amount > 0)
        {
            Health -= amount;
        }

        if (Health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount > 0)
        {
            Health += amount;
        }

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    public void Die()
    {
        if (Health <= 0)
        {
            Debug.Log("The Player die!");
            Destroy(gameObject);
            return;
        }
        Debug.Log("The Player is still alive!");
    }

    #endregion


    #region IMoveable methods
    public void Move()
    {

        CheckIsGrounded();

        float xAxis = Input.GetAxis("Horizontal");
        CurrentRunSpeed = Mathf.Abs(xAxis * MaxRunSpeed);

        if (xAxis < 0)
        {
            SetFlipping(true);
        }
        else if (xAxis > 0)
        {
            SetFlipping(false);
        }



        if (CurrentRunSpeed == 0 && isGrounded)
        {
            playerStateManager.ChangeState(playerStateManager.STATE_IDLE);
        }
        else if (CurrentRunSpeed != 0 && playerStateManager.GetCurrrentState() != playerStateManager.STATE_RUN)
        {
            playerStateManager.ChangeState(playerStateManager.STATE_RUN);
        }

    }


    public void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerStateManager.ChangeState(playerStateManager.STATE_JUMP);

        }
    }
    #endregion







}
