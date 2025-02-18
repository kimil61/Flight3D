using System;
using System.Collections;
using UnityEngine;

namespace Flight3D
{
    public class PlayerController : MonoBehaviour
    {
        [Header("미사일 발사 관련")]
        [SerializeField] private GameObject missileSpawner;
        [SerializeField] private GameObject missilePrefab;
        private GameObject supermissilePrefab;
        
        [Header("설정값들")]
        public float speed = 20.0f;
        public float rotationSpeed = 515f;
        public float canFireSuperMissileTime = 0.2f;
        
        private bool isRotating = false;
        private bool canFireSuperMissile = false;  // 슈퍼 미사일 발사 가능 여부
        private Collider playerCollider;
        private void Start()
        {
            playerCollider = GetComponent<Collider>();
        }

        private void Update()
        {
            float _x = Input.GetAxis("Horizontal");
            float _z = Input.GetAxis("Vertical");

            Vector3 newPosition = transform.position + new Vector3(_x, 0, _z) * (speed * Time.deltaTime);
            
            newPosition.x = Mathf.Clamp(newPosition.x,-10.3f,14.7f);
            newPosition.z = Mathf.Clamp(newPosition.z,-4,42);
            
            transform.position = newPosition;

            if (!isRotating)
            {
                //미사일 발사부분
                if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Space))
                {
                    Instantiate(missilePrefab, missileSpawner.transform.position, missileSpawner.transform.rotation);
                }
                //회전 부분
                if (Input.GetKeyDown(KeyCode.K))
                {
                    StartCoroutine(RotateAndEnableSuperMode());
                }
                //슈퍼 미사일 발사
                if(canFireSuperMissile && Input.GetKeyDown(KeyCode.J))
                {
                    FireSuperMissile();
                    canFireSuperMissile = false;
                }
            }

            
            //Debug.Log(isRotating.ToString());
        }
        
        private IEnumerator RotateAndEnableSuperMode()
        {
            isRotating = true;
            playerCollider.enabled = false;  // 무적 (충돌 비활성화)
            float rotatedAmount = 0f;

            while (rotatedAmount < 360f)
            {
                float rotateStep = rotationSpeed * Time.deltaTime;
                transform.Rotate(0f, 0f, rotateStep);
                rotatedAmount += rotateStep;
                yield return null;
            }

            transform.Rotate(0f, 0f, -(rotatedAmount - 360f)); // 오버슈트 보정
            playerCollider.enabled = true;  // 무적 해제
            isRotating = false;

            // ⭐ 0.2초 동안 슈퍼 미사일 모드 활성화 ⭐
            canFireSuperMissile = true;
            yield return new WaitForSeconds(canFireSuperMissileTime);
            canFireSuperMissile = false;  // 0.2초 지나면 슈퍼 미사일 기회 사라짐
        }
        
        private void FireSuperMissile()
        {
            GameObject superMissile = Instantiate(missilePrefab, missileSpawner.transform.position, missileSpawner.transform.rotation);
            superMissile.transform.localScale *= 3f;
        }

    }

}
