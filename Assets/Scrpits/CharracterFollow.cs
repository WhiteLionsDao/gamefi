using UnityEngine;

public class CharracterFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // Takip edilecek karakterin Transform bile�eni
    [SerializeField]private float smoothSpeed = 0.125f; // Takip etme h�z�
    [SerializeField]private Vector3 offset; // Kamera ve karakter aras�ndaki mesafe

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; 
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); 
        transform.position = smoothedPosition; // Kamera pozisyonu g�ncellenir
    }
}
