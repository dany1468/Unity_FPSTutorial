using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 500;
    
    float xRotation = 0f;
    float yRotation = 0f;
    
    public float topClamp = -90f;
    public float bottomClamp = 90f;
    
    void Start()
    {
        // 画面中央にロックし非表示
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        // マウスを上下に動かすと、体がX軸を中心に回転します
        // マウスを上に動かすと体が下に回転し、マウスを下に動かすと体が上に回転します
        xRotation -= mouseY;
        // 回転が90度でブロック
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);
        
        // マウスを左右に動かすと、体がY軸を中心に回転します
        yRotation += mouseX;
        
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
