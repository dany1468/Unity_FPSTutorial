using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    private Vector3 velocity;
    
    private bool isGrounded;
    private bool isMoving;
    private Vector3 lastPosition;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 地面に接触しているかどうかを確認
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // 地面に接触している場合、速度をリセット
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        // 移動
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        
        var move = transform.right * x + transform.forward * z;
        
        // 移動を実行
        controller.Move(move * speed * Time.deltaTime);
        
        // ジャンプ
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        // 落下 (重力)
        velocity.y += gravity * Time.deltaTime;
        
        // ジャンプを実行
        controller.Move(velocity * Time.deltaTime);
        
        // 移動中かどうかを確認
        if (gameObject.transform.position != lastPosition && isGrounded)
        {
            isMoving = true; // 後で使う用
        }
        else
        {
            isMoving = false;
        }
        
        lastPosition = gameObject.transform.position;
    }
}
