using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Charecter
{
    [SerializeField]
    private GroundCheck groundCheck;
    [SerializeField]
    private Transform bulletSpawnpoint;
    
    private Transform checkPoint;
    private int currentCheckpoint;

    public Weapon[] weapon;
    [SerializeField]
    private StatusBar hpBar;
    [SerializeField]
    private StatusBar chaosBar;
    [SerializeField]
    private StatusBar maxHpBar;
    [SerializeField]
    private LevelLoader levelLoader;
    private int currentWeapon = 0;

    public float _chaosResistance { get => chaosResistance; }

    bool isShooting
    {
        get
        {
            return Input.GetButton("Fire1");
        }
    }

    protected override void Start()
    {
        base.Start();
        UpdateStatusBars();
        animator.SetFloat("attackSpeed", 0.25f / weapon[currentWeapon].attackSpeed);
    }

    private void FixedUpdate()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        isGrounded = groundCheck.isGrounded;
        animator.SetBool("shooting", isShooting);
    }

    protected override void Update()
    {
        Inputs();
        base.Update();
        
    }

    private void Inputs()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Move(direction);
        }
        if (Input.GetButton("Fire1"))
        {
            weapon[currentWeapon].Shoot(bulletSpawnpoint);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            weapon[currentWeapon].StopShooting();
            SwapWeapon();
        }
    }

    private void SwapWeapon()
    {
        weapon[currentWeapon].gameObject.SetActive(false);
        currentWeapon += 1;
        if(currentWeapon >= weapon.Length)
        {
            currentWeapon = 0;
        }
        weapon[currentWeapon].gameObject.SetActive(true);
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        UpdateStatusBars();
    }

    public void UpdateStatusBars()
    {
        hpBar.UpdateBar(maxHealth, currentHealth);
        chaosBar.UpdateBar(ChaosManager._chaosLevelMax,(ChaosManager._chaosLevel - (ChaosManager._chaosLevel * chaosResistance)));
        maxHpBar.UpdateBar(defaultMaxHealth, maxHealth, true);
    }

    public override void UpdateStats()
    {
        float chaos = (ChaosManager.PlayerChaosLevel - (ChaosManager.PlayerChaosLevel * chaosResistance));
        maxHealth = (int)Mathf.Clamp(defaultMaxHealth + chaos, 1, 100);
        speed = Mathf.Clamp(defaultSpeed * chaos, -defaultSpeed * 3, defaultSpeed * 3);
        if(speed == 0)
        {
            speed = defaultSpeed;
        }

        jumpForce = Mathf.Clamp(defaultJumpForce +(defaultJumpForce* chaos), 0, defaultJumpForce * 3);

        for (int i = 0; i < weapon.Length; i++)
        {
            weapon[i].UpdateChaos(chaosResistance);
        }
        base.UpdateStats();
        Heal((int)Random.Range(1, maxHealth));
        UpdateStatusBars();
        Debug.Log("MaxHealth: " + maxHealth + " Speed: " + speed + " JumpForce: " + jumpForce + " GrabScale: "+rb.gravityScale);
    }

    public override void SetCharecterDefaultStats()
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            if (!weapon[i].enabled)
            {
                weapon[i].gameObject.SetActive(true);
            }
            weapon[i].ResetStats();
            if(i != currentWeapon)
            {
                weapon[i].gameObject.SetActive(false);
            }
        }

        base.SetCharecterDefaultStats();
        UpdateStatusBars();
    }

    public override void Die()
    {
        levelLoader.ReloadLevel();
        SetCharecterDefaultStats();
    }

    public void SetCheckpoint(Transform _checkpoint,int checkpointIndex)
    {
        if(checkpointIndex >= currentCheckpoint)
        {
            checkPoint = _checkpoint;
            currentCheckpoint = checkpointIndex;
        }
    }
    
}
