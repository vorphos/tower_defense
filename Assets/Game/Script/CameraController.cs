using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float zoomSpeed = 2f;
    
    [Header("Bord de l'écran")]
    [SerializeField] private bool useScreenEdges = true;
    [SerializeField] private float edgeSize = 20f;
    
    [Header("Limites")]
    [SerializeField] private float minHeight = 6f;
    [SerializeField] private float maxHeight = 30f;
    [SerializeField] private float limiteX = 20f;
    [SerializeField] private float minZ = -30f;
    [SerializeField] private float maxZ = 15f;
   
    // Update is called once per frame
    private void Update()
    {
        Keyboard keybord = Keyboard.current;
        Mouse mouse = Mouse.current;
        if (keybord == null || mouse == null) return;
        
        Vector3 direction = Vector3.zero;
        
        if (keybord.wKey.isPressed || keybord.upArrowKey.isPressed) direction.z += 1f;
        if (keybord.sKey.isPressed || keybord.downArrowKey.isPressed) direction.z -= 1f;
        if (keybord.aKey.isPressed || keybord.leftArrowKey.isPressed) direction.x -= 1f;
        if (keybord.dKey.isPressed || keybord.rightArrowKey.isPressed) direction.x += 1f;

        if (useScreenEdges)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            
            if (mousePosition.y >= Screen.height - edgeSize) direction.z += 1f;
            if (mousePosition.y <= edgeSize) direction.z -= 1f; 
            if (mousePosition.x <= edgeSize) direction.x -= 1f;
            if (mousePosition.x >= Screen.width - edgeSize) direction.x += 1f;
        }
        
        Vector3 position = transform.position + direction.normalized * moveSpeed * Time.unscaledDeltaTime;
        
        float scroll = mouse.scroll.ReadValue().y;
        if (scroll > 0f) position.y -= zoomSpeed;
        if (scroll < 0f) position.y += zoomSpeed;

        position.x = Mathf.Clamp(position.x,  -limiteX,  limiteX);
        position.y = Mathf.Clamp(position.y, minHeight , maxHeight);
        position.z = Mathf.Clamp(position.z, minZ, maxZ);
        
        transform.position = position;
        
        
    }
}
