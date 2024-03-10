using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.2f};

    // Start is called before the first frame update
    void Start()
    {
        // arrPosX에서 값을 하나씩 꺼내서 posX에 집어 넣음
        foreach(float posX in arrPosX){
            int index = UnityEngine.Random.Range(0, enemies.Length);
            Spawnenemy(posX, index);   
        }   
    }

    void Spawnenemy(float posX, int index){
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);
        Instantiate(enemies[index], spawnPos, quaternion.identity);
    }
}
