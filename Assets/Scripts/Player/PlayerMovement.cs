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
	public float moveSpeed = 8.0f;
	public float jumpForce = 8.0f;
	public float dashForce = 16.0f;
	public float allowedJumps = 2;

	[Header("Audios")]
	public AudioClip walkAudio;
	public AudioClip jumpAudio;
	public AudioClip[] dashAudio;

	[HideInInspector]
	public int currentJumpCount = 0;
    [HideInInspector]
    public int currentDirection = 1;
    [HideInInspector]
    public Vector2 movementVelocity;
    [HideInInspector]
    public Vector2 dashVelocity;

	private float timeBetweenFootsteps = 0.6f;
	private float timeSinceLastFootstep;

	private bool isDashAllowed = false;

    void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (GameManager.Instance.IsUserActionsDisabled()) return;
        ProcessUserInputs();		
		Movement();
	}

	void FixedUpdate() {
		
		rb.AddForce(dashVelocity);
	}

	void ProcessUserInputs() {
		Vector2 finalVelocity = new Vector2(rb.velocity.x, rb.velocity.y);
		float xAxis = Input.GetAxis("Horizontal");

		if (xAxis != 0)
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

		if (xAxis < 0) {
			sr.flipX = true;
			currentDirection = -1;
		} else if (xAxis > 0)
		{
            currentDirection = 1;
            sr.flipX = false;
		}

		finalVelocity.x = xAxis * moveSpeed;
	
        if (Input.GetButtonDown("Jump") && currentJumpCount < allowedJumps)
        {
            finalVelocity.y = jumpForce + (jumpForce * currentJumpCount * 0.5f);
            currentJumpCount++;
            audioSource.PlayOneShot(jumpAudio);
            anim.SetBool("isFalling", true);
        }

		if (xAxis < -0.5f || xAxis > 0.5f || currentJumpCount > 0)
		{
			isDashAllowed = true;
		}

        if (Input.GetButtonDown("Dash") && isDashAllowed) {
			StartCoroutine(PlayerDash());
		}

		movementVelocity = finalVelocity;
	}

	void Movement() {
		rb.velocity = movementVelocity;
	}

	IEnumerator PlayerDash() {
		dashVelocity = new Vector2((float) currentDirection * dashForce, movementVelocity.y * 2f);
		audioSource.PlayOneShot(dashAudio[Random.Range(0, dashAudio.Length)]);

		yield return new WaitForSeconds(0.25f);

		dashVelocity = new Vector2();
	}
}
