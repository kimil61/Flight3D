using UnityEngine;

namespace Flight3D
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 20.0f;
        public float rotationSpeed = 100.0f;

        void Update()
        {
            float _x = Input.GetAxis("Horizontal");
            float _z = Input.GetAxis("Vertical");

            Vector3 newPosition = transform.position + new Vector3(_x, 0, _z) * (speed * Time.deltaTime);
            
            newPosition.x = Mathf.Clamp(newPosition.x,-25,25);
            newPosition.z = Mathf.Clamp(newPosition.z,-1,40);
            
            transform.position = newPosition;



        }
    }

}
