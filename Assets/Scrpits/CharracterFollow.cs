using UnityEngine;

public class CharracterFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // Takip edilecek karakterin Transform bileþeni
    [SerializeField]private float smoothSpeed = 0.125f; // Takip etme hýzý
    [SerializeField]private Vector3 offset; // Kamera ve karakter arasýndaki mesafe

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; 
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); 
        transform.position = smoothedPosition; // Kamera pozisyonu güncellenir
    }
}
