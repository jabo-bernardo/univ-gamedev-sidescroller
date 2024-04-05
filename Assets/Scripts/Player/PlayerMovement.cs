using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource audioSource;

	[HideInInspector]
    public SpriteRenderer sr;


    [Header("Stats")]
    public float facingDirection = 1;
    public float movementSpeed = 8.0f;
	public float jumpForce = 8.0f;
	public float allowedJumps = 2;
	public float dashForce = 16.0f;
	public float dashDuration = 1f;
	public float dashCooldown = 6f;
	public float defaultGravity = 1f;

	[Header("Audios")]
	public AudioClip walkAudio;
	public AudioClip jumpAudio;
	public AudioClip[] dashAudio;

	public int currentJumpCount = 0;
    public Vector2 movementVelocity;
    public Vector2 dashVelocity;

	private float timeBetweenFootsteps = 0.6f;
	private float timeSinceLastFootstep;

	private bool isDashing;
	private bool isDashInCooldown;


    void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (GameManager.Instance.IsUserActionsDisabled())
		{
			rb.velocity = Vector2.zero;
			return;
		}
		HandleJumpControl();
		if (Input.GetButtonDown("Dash") && !isDashInCooldown) {
			StartCoroutine(PlayerDash());
		}
	}

	void HandleHorizontalMovement()
	{
		if (GameManager.Instance.IsUserActionsDisabled()) return;
		float direction = Input.GetAxisRaw("Horizontal");

		if (direction != 0)
		{
			anim.SetBool("isWalking", true);
			if (Time.time - timeSinceLastFootstep >= timeBetweenFootsteps)
			{
                audioSource.PlayOneShot(walkAudio);
                timeSinceLastFootstep = Time.time;
			}
		} else
		{
			anim.SetBool("isWalking", false);
        }

		if (direction < 0)
		{
			facingDirection = -1;
		} else if (direction > 0)
		{
			facingDirection = 1;
		}

		if (facingDirection < 0)
		{
			transform.localScale = new Vector2(Math.Abs(transform.localScale.x) * -1, transform.localScale.y);
		} else if (facingDirection > 0) { 
			transform.localScale = new Vector2(Math.Abs(transform.localScale.x), transform.localScale.y);
		}

		if (!isDashing)
		{
			rb.velocity = new Vector2(direction * movementSpeed, rb.velocity.y);
		}
	}

	void FixedUpdate() {
		HandleHorizontalMovement();
	}

	void HandleJumpControl() {
		if (Input.GetButtonDown("Jump") && currentJumpCount < allowedJumps)
        {
            rb.velocity = Vector2.up * (jumpForce + (jumpForce * currentJumpCount * 0.5f));
            currentJumpCount++;
            audioSource.PlayOneShot(jumpAudio);
            anim.SetBool("isFalling", true);
        }
	}

	IEnumerator PlayerDash() {
		isDashing = true;
		isDashInCooldown = true;
		rb.gravityScale = 0.0f;

		if (facingDirection == 1)
		{
			rb.velocity = Vector2.right * dashForce;
		} else if (facingDirection == -1)
		{
			rb.velocity = Vector2.left * dashForce;
		}

		Debug.Log(facingDirection);

		audioSource.PlayOneShot(dashAudio[Random.Range(0, dashAudio.Length)]);

		yield return new WaitForSeconds(dashDuration);

		isDashing = false;
		rb.gravityScale = defaultGravity;

		yield return new WaitForSeconds(dashCooldown);

		isDashInCooldown = false;
	}
}
