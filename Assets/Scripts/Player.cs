using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float jumpForce = 0f;
    [SerializeField] private LayerMask floorLayerMask = default;
    [SerializeField] private GameObject livesPanel = null;
    [SerializeField] private GameObject barrelSpawner = null;

    private Vector3 _startPosition;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private Animator _myAnimator;
    private int _currentLives = 3;
    private Image[] _lifeImages = new Image[3];

    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    private void Start()
    {
        _startPosition = transform.position;
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _myAnimator = GetComponent<Animator>();
        _lifeImages = livesPanel.GetComponentsInChildren<Image>();
    }

    private void FixedUpdate()
    {
        var xInput = Input.GetAxis("Horizontal");
        _rigidBody.velocity = new Vector2(xInput * moveSpeed, _rigidBody.velocity.y);

        var statusHorizontal = Input.GetButton("Horizontal");
        _myAnimator.SetBool(IsMoving, statusHorizontal);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
            _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        if (_rigidBody.velocity.x > 0)
            _spriteRenderer.flipX = false;
        else if (_rigidBody.velocity.x < 0)
            _spriteRenderer.flipX = true;
    }

    private bool IsGrounded()
    {
        var hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.2f, 0), Vector2.down, 0.33f, floorLayerMask);
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrel"))
        {
            if (_currentLives > 1 )
            {
                RemoveLife();
                transform.position = _startPosition;
                DestroyBarrels();
            }
            else
            {
                RemoveLife();

                SceneManager.LoadScene("Level1"); //Temporary
                //TODO: Game over
                //TODO: Return to main menu
            }
        }

        if (collision.gameObject.CompareTag("PrincessPeach"))
        {
            SceneManager.LoadScene("Level1"); //Temporary
            //TODO: Success Message
            //TODO: Return to main menu
        }
    }

    private void RemoveLife()
    {
        _currentLives--;
        _lifeImages[_currentLives - 1].gameObject.SetActive(false);
    }

    private void DestroyBarrels()
    {
        foreach (Transform child in barrelSpawner.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
