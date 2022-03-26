using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<Enemy> enemyEntities;
    [SerializeField] Enemy enemyEntity;

    bool ExistMyEmptyEntity => enemyEntities.Exists(x => x == enemyEntity);
    int EnemyIndex => enemyEntities.FindIndex(x => x == enemyEntity);


    void EntityAlignment() // 적 정렬
    {
        float targetY = 5f;
        var targetEntities = enemyEntities;

        for (int i = 0; i < targetEntities.Count; i++)
        {
            float targetX = (targetEntities.Count - 1) * -3.4f + i * 6.8f;

            var targetEntity = targetEntities[i];
            targetEntity.originPos = new Vector3(targetX, targetY, 0);
            targetEntity.MoveTransform(targetEntity.originPos, true, 0.5f);
            targetEntity.GetComponent<Order>().SetOriginOrder(i);
        }
    }

    public void InsertEnemyEntity(float xPos) // 적엔티티 삽입! 
    {
        if (!ExistMyEmptyEntity)
            enemyEntities.Add(enemyEntity);
        
        Vector3 entityPos = enemyEntity.transform.position;
        entityPos.x = xPos;
        enemyEntity.transform.position = entityPos;

        int _enemyIndex = EnemyIndex;
        enemyEntities.Sort((entity1, entity2) => entity1.transform.position.x.CompareTo(entity2.transform.position.x));
        if (EnemyIndex != _enemyIndex)
            EntityAlignment();
    }

    public void RemoveEnemyEntity()
    {
        if (!ExistMyEmptyEntity)
            return;
        
        enemyEntities.RemoveAt(EnemyIndex);
        EntityAlignment();
    }

    public bool SpawnEnemy(EnemyData enemyData, Vector3 spawnPos)
    {
        InsertEnemyEntity(Utils.MousePos.x);

        var enemyObject = Instantiate(enemyPrefab, spawnPos, Utils.QI);
        var enemy = enemyObject.GetComponent<Enemy>();

        print(enemyEntities.ToString());
        print(EnemyIndex.ToString());
        enemyEntities[EnemyIndex] = enemy;

        enemy.Setup(enemyData);
        EntityAlignment();

        return true;
    }
}
