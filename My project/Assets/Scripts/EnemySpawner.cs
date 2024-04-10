using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject boss;

    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.2f};

    [SerializeField]
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartEnemyRoutine();
    }

    void StartEnemyRoutine(){
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine(){
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine() {
        yield return new WaitForSeconds(3f);
        
        float moveSpeed = 5f;
        int spawnCount = 0;
        int enemyIndex = 0;

        while(true){
            // arrPosX에서 값을 하나씩 꺼내서 posX에 집어 넣음
            foreach(float posX in arrPosX){
                //int index = UnityEngine.Random.Range(0, enemies.Length);
                Spawnenemy(posX, /*index*/enemyIndex, moveSpeed);   
            }

            spawnCount++;
            if (spawnCount % 10 == 0){  // 10, 20, 30, ...
                enemyIndex += 1;
                moveSpeed += 2;
            }

            if(enemyIndex >= enemies.Length){
                SpawnBoss();
                enemyIndex = 0;
                moveSpeed = 5f;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Spawnenemy(float posX, int index, float moveSpeed){
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);

        if(UnityEngine.Random.Range(0, 5) == 0){
            index += 1;
        }

        if(index >= enemies.Length){
            index = enemies.Length - 1;
        }

        GameObject enemy = Instantiate(enemies[index], spawnPos, quaternion.identity);
    }

    void SpawnBoss(){
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}
