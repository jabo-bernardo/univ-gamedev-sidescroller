using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (AudioSource))]
public class Player : MonoBehaviour
{

	/**
	* Components
	*/
	private Rigidbody2D rigidBody;
	private Animator animator;
	private AudioSource audioSource;

	[Header("Player Movement Configuration")]
	public float facingDirection = 1;
	public float movementSpeed = 4.0f;
	public float jumpForce = 6.0f;
	public float allowedJumps = 2;
	public float dashForce = 25.0f;
	public float dashDuration = 0.15f;
	public float dashCooldown = 0.5f;
	public float defaultGravity = 2f;


	[Header("Player Attack Configuration")]
	public GameObject primaryAttackObject;
	public GameObject secondaryAttackObject;

	public float primaryAttackCooldown = 0.25f;
	public float secondaryAttackCooldown = 2f;

	[Header("Player Attack Effects")]
	public GameObject primarySlashObject;

	[Header("Audios")]
	public AudioClip walkAudio;
	public AudioClip jumpAudio;
	public AudioClip[] dashAudio;
	public AudioClip deathSound;

	private bool isPrimaryCooldown;
	private bool isSecondaryCooldown;

	private int currentJumpCount = 0;

	private float TIME_BETWEEN_FOOTSTEPS = 0.6f;
	private float timeSinceLastFootstep;

	private bool isDashing;
	private bool isDashInCooldown;
	private bool isDying;

	void Start() {
		rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();

		if (GameManager.Instance.playerSavedLocationsBeforeTeleport.ContainsKey(SceneManager.GetActiveScene().name) && !SceneManager.GetActiveScene().name.Contains("Castle")) {
			transform.position = GameManager.Instance.playerSavedLocationsBeforeTeleport.GetValueOrDefault(SceneManager.GetActiveScene().name);
		}

		if (GameManager.Instance.GetLastCheckpoint() != null && SceneManager.GetActiveScene().name == "Playground" && GameManager.Instance.IsToRespawn()) {
			transform.position = GameManager.Instance.GetLastCheckpoint();
			GameManager.Instance.SetToRespawn(false);
			GameManager.Instance.EnableUserActions();
		}

		if (walkAudio) {
			TIME_BETWEEN_FOOTSTEPS = walkAudio.length;
		}
	}

	void Update() {
		bool IS_DEAD = GameManager.Instance.GetPlayerHealth() <= 0;
		if (IS_DEAD && !isDying) {
			isDying = true;
			StartCoroutine(KillKristan());
		}

		bool SHOULD_DRINK_BEER = Input.GetKeyUp(KeyCode.Q) && GameManager.Instance.GetBeerCount() > 0;
		if (SHOULD_DRINK_BEER) {
			float percentage = 25;
			float toAdd = GameManager.Instance.GetPlayerMaxHealth() * (percentage / 100);
			GameManager.Instance.ConsumeBeer();
			GameManager.Instance.SetPlayerMaxHealth(GameManager.Instance.GetPlayerMaxHealth() + toAdd);
			GameManager.Instance.SetPlayerHealth(GameManager.Instance.GetPlayerHealth() + toAdd);
		}

		HandlePlayerAttacks();
		HandlePlayerControls();
	}

	IEnumerator KillKristan() {
		GameManager.Instance.DisableUserActions();
		GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
		audioSource.PlayOneShot(deathSound);
		yield return new WaitForSeconds(2f);
		GameManager.Instance.SetPlayerHealth(GameManager.Instance.GetPlayerMaxHealth() * 0.5f);

		if (SceneManager.GetActiveScene().name != "Playground") {
			yield return FindObjectOfType<LevelLoader>().LoadLevel("Playground");
			GameManager.Instance.SetToRespawn(true);
			Destroy(gameObject);
			GetComponent<DialogueTrigger>().TriggerDialogue();
			GameManager.Instance.EnableUserActions();
		} else {
			GameManager.Instance.SetToRespawn(true);
			transform.position = GameManager.Instance.GetLastCheckpoint();
			GameManager.Instance.SetToRespawn(false);
			GameManager.Instance.EnableUserActions();
		}
	}

	void FixedUpdate() {
		HandleHorizontalMovement();
	}

	void HandlePlayerAttacks() {
		/**
			If the game does not allow user inputs/actions,
			Disregard any input from the player.

			e.g game is paused, a dialogue message is opened, etc.
		*/
		if (GameManager.Instance.IsUserActionsDisabled()) {
			return;
		}
		bool IS_ALLOWED_TO_PRIMARY_ATTACK = Input.GetMouseButtonDown(0) && !isPrimaryCooldown;
		if (IS_ALLOWED_TO_PRIMARY_ATTACK) {
			StartCoroutine(PrimaryAttack());
		}

		bool IS_ALLOWED_TO_SECONDARY_ATTACK = Input.GetMouseButtonDown(1) && !isSecondaryCooldown;
		if (IS_ALLOWED_TO_SECONDARY_ATTACK) {
			StartCoroutine(SecondaryAttack());
		}
	}

	void HandlePlayerControls() {
		/**
			If the game does not allow user inputs/actions,
			Disregard any input and put the player in an idle
			state.

			e.g game is paused, a dialogue message is opened, etc.
		*/
		if (GameManager.Instance.IsUserActionsDisabled()) {
			rigidBody.velocity = Vector2.zero;
			animator.SetBool("isWalking", false);
			return;
		}

		bool IS_ALLOWED_TO_DASH = Input.GetButtonDown("Dash") && !isDashInCooldown;
		if (IS_ALLOWED_TO_DASH) {
			StartCoroutine(PlayerDash());
		}

		bool IS_ALLOWED_TO_JUMP = Input.GetButtonDown("Jump") && currentJumpCount <= (allowedJumps - 2);
		if (IS_ALLOWED_TO_JUMP) {
			HandleJumpControl();
		}
	}

	void HandleHorizontalMovement()
	{
		/**
			If the game does not allow user inputs/actions,
			Disregard any input and put the player in an idle
			state.

			e.g game is paused, a dialogue message is opened, etc.
		*/
		if (GameManager.Instance.IsUserActionsDisabled()) {
			rigidBody.velocity = Vector2.zero;
			animator.SetBool("isWalking", false);
			return;
		}

		float direction = Input.GetAxisRaw("Horizontal");

		if (direction != 0)
		{
			animator.SetBool("isWalking", true);
			if (Time.time - timeSinceLastFootstep >= TIME_BETWEEN_FOOTSTEPS)
			{
				audioSource.PlayOneShot(walkAudio);
				timeSinceLastFootstep = Time.time;
			}
		} else
		{
			animator.SetBool("isWalking", false);
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
			transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
		} else if (facingDirection > 0) { 
			transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
		}

		if (!isDashing)
		{
			rigidBody.velocity = new Vector2(direction * movementSpeed, rigidBody.velocity.y);
		}
	}

	void HandleJumpControl() {
		rigidBody.velocity = Vector2.up * (jumpForce + (jumpForce * currentJumpCount * 1.2f));
		currentJumpCount = currentJumpCount + 1;
		audioSource.PlayOneShot(jumpAudio);
		animator.SetBool("isFalling", true);
	}

	IEnumerator PlayerDash() {
		isDashing = true;
		isDashInCooldown = true;
		rigidBody.gravityScale = 0.0f;

		if (facingDirection == 1)
		{
			rigidBody.velocity = Vector2.right * dashForce;
		} else if (facingDirection == -1)
		{
			rigidBody.velocity = Vector2.left * dashForce;
		}

		audioSource.PlayOneShot(dashAudio[Random.Range(0, dashAudio.Length)]);

		yield return new WaitForSeconds(dashDuration);

		isDashing = false;
		rigidBody.gravityScale = defaultGravity;

		yield return new WaitForSeconds(dashCooldown);

		isDashInCooldown = false;
	}

	IEnumerator PrimaryAttack()
    {
        isPrimaryCooldown = true;
        primaryAttackObject.SetActive(true);

        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 startingScreenPos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(gameObject.transform.position);
        mouseScreenPos.x -= startingScreenPos.x;
        mouseScreenPos.y -= startingScreenPos.y;
        float initialAngle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
        Vector3 angle = new Vector3(0, 0, initialAngle);

        // Check if mouse is on left side of player
        if (angle.z > 90 || angle.z < -90)
        {
            SetFacingDirection(-1);
        }
        else
        {
            SetFacingDirection(1);
        }

        GameObject slashEffect = Instantiate(primarySlashObject, transform);
        slashEffect.transform.SetLocalPositionAndRotation(new Vector2(0.25f, -0.25f), transform.rotation);
        yield return new WaitForSeconds(0.1f);

        primaryAttackObject.SetActive(false);
        yield return new WaitForSeconds(primaryAttackCooldown);

        isPrimaryCooldown = false;
    }

		IEnumerator SecondaryAttack()
    {
        isSecondaryCooldown = true;
        secondaryAttackObject.SetActive(true);

        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 startingScreenPos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(gameObject.transform.position);
        mouseScreenPos.x -= startingScreenPos.x;
        mouseScreenPos.y -= startingScreenPos.y;
        float initialAngle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
        Vector3 angle = new Vector3(0, 0, initialAngle);

        // Check if mouse is on left side of player
        if (angle.z > 90 || angle.z < -90)
        {
            SetFacingDirection(-1);
        }
        else
        {
            SetFacingDirection(1);
        }

        GameObject slashEffect = Instantiate(primarySlashObject, transform);
        slashEffect.transform.SetLocalPositionAndRotation(new Vector2(0.25f, -0.25f), transform.rotation);

        yield return new WaitForSeconds(0.1f);

        secondaryAttackObject.SetActive(false);
        yield return new WaitForSeconds(secondaryAttackCooldown);

        isSecondaryCooldown = false;
    }


	public Player ResetJumpCount() {
		currentJumpCount = 0;
		return this;
	}

	public Player SetFacingDirection(float direction) {
		facingDirection = direction;
		return this;
	}

	public Player TakeDamage(float damage) {
		GameManager.Instance.SetPlayerHealth(GameManager.Instance.GetPlayerHealth() - damage);
		GameObject.Find("MainCamera").GetComponent<CameraShake>().StartShake();
		return this;
	}

	public bool GetIsPrimaryCooldown() {
		return isPrimaryCooldown;
	}

	public bool GetIsSecondaryCooldown() {
		return isSecondaryCooldown;
	}
}
