using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f;
    private float lastShotTime = 0f;

    // Update is called once per frame
    void Update()
    {
        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // //float verticalInput = Input.GetAxisRaw("Vertical");
        // Vector3 moveTo = new Vector3(horizontalInput, 0f/*verticalInput*/, 0f);
        // transform.position += moveTo * moveSpeed * Time.deltaTime;

        // Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        // // 입력한 값이 키보드 왼쪽 방향키
        // if (Input.GetKey(KeyCode.LeftArrow)){
        //     transform.position -= moveTo;
        // }
        // // 입력한 값이 키보드 오른쪽 방향키
        // else if(Input.GetKey(KeyCode.RightArrow)){
        //     transform.position += moveTo;
        // }

        // 마우스로 입력
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // mousePos.x 값이 -2.35보다 작으면 -2.35로 고정, 2.35보다 크면 2.35로 고정)
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        transform.position = new Vector3(toX, transform.position.y, transform.position.z);

        if(GameManager.instance.isGameOver == false){
            Shoot();
        }
    }

    void Shoot(){
        if(Time.time - lastShotTime > shootInterval){
            // 발사하는 무기 생성, quaternion.identity = 회전 x
            Instantiate(weapons[weaponIndex], shootTransform.position, quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss"){
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        } else if(other.gameObject.tag == "Coin"){
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void Upgrade(){
        weaponIndex += 1;
        if(weaponIndex >= weapons.Length){
            weaponIndex = weapons.Length - 1;
        }
    }
}
