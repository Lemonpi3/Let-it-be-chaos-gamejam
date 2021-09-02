using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Charecter
{
    [SerializeField]
    private GroundCheck groundCheck;
    [SerializeField]
    private Transform bulletSpawnpoint;
    
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
    [SerializeField]
    private int currentWeapon = 0;
    [SerializeField]
    private GameObject pauseMenu;
    [HideInInspector]
    public int _currentHealth { get { return currentHealth; } set { currentHealth = _currentHealth; } }

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
        Heal(maxHealth);
        UpdateStatusBars();
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
            FlipUpsideDown(groundCheck);
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
        if (Input.GetButtonDown("Cancel"))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
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
        animator.SetFloat("attackSpeed", 0.25f / weapon[currentWeapon].GetAttackSpeed());
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        UpdateStatusBars();
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);
        UpdateStatusBars();
    }

    public void UpdateStatusBars()
    {
        hpBar.UpdateBar(maxHealth, currentHealth);
        chaosBar.UpdateBar(ChaosManager._chaosLevelMax,ChaosManager._chaosLevel);
        maxHpBar.UpdateBar(defaultMaxHealth, maxHealth, true);
    }

    public override void ApplyChaosStats()
    {
        maxHealth = ChaosManager.instance.playerMaxHealth_ChaosMod;
        rb.gravityScale = ChaosManager.instance.playerGravity_ChaosMod;
        jumpForce = ChaosManager.instance.playerJumpForce_ChaosMod;
        speed = ChaosManager.instance.playerSpeed_ChaosMod;
        Debug.Log(maxHealth + " " + rb.gravityScale + " " + jumpForce + " " + speed);
        for (int i = 0; i < weapon.Length; i++)
        {
            if (!weapon[i].enabled)
            {
                weapon[i].gameObject.SetActive(true);
            }
            weapon[i].UpdateChaos();
            if (i != currentWeapon)
            {
                weapon[i].gameObject.SetActive(false);
            }
        }
        UpdateStatusBars();
        FlipUpsideDown(groundCheck);
    }

    public override void SetCharecterDefaultStats(bool heal = false)
    {
        base.SetCharecterDefaultStats();
        FlipUpsideDown(groundCheck);
        for (int i = 0; i < weapon.Length; i++)
        {
            if (!weapon[i].enabled)
            {
                weapon[i].gameObject.SetActive(true);
            }
            weapon[i].ResetStats();
            if (i != currentWeapon)
            {
                weapon[i].gameObject.SetActive(false);
            }
        }
        UpdateStatusBars();
    }

    public override void Die()
    {
        SetCharecterDefaultStats();
        levelLoader.LoadSavedGame();
    }

    public void SetCheckpoint(int checkpointIndex)
    {
        if(checkpointIndex >= currentCheckpoint)
        {
            currentCheckpoint = checkpointIndex;
        }
    }

    public void ResetWeapon()
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            if (!weapon[i].enabled)
            {
                weapon[i].gameObject.SetActive(true);
            }
            weapon[i].ResetStats();
            if (i != currentWeapon)
            {
                weapon[i].gameObject.SetActive(false);
            }
        }
    }
}
