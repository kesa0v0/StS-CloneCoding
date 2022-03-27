using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform enemyLocation;
    [SerializeField] Transform enemyLocationLeft;
    [SerializeField] Transform enemyLocationRight;
    [SerializeField] List<Enemy> enemyList;

	void EnemyAlignment() // 적 정렬
	{
		float targetY = 5f;
        float targetXRange = Mathf.Abs(enemyLocationLeft.position.x - enemyLocationRight.position.x);

		for (int i = 0; i < enemyList.Count; i++)
		{
			float targetX = enemyLocation.position.x + (enemyList.Count - 1) * -3.4f + i * 6.8f;

			var targetEntity = enemyList[i];
			targetEntity.originPos = new Vector3(targetX, targetY, 0);
			targetEntity.MoveTransform(targetEntity.originPos, true, 0.5f); // TODO: 위치 조정은 확실히 필요할듯 아니면 차라리 적의 크기를 조절하던가
			targetEntity.GetComponent<Order>()?.SetOriginOrder(i);
		}
	}


    public void SpawnEnemy(EnemyData enemyData, Vector3 spawnPos, int insertOrder=-1) // 적 스폰 
    { // insertOrder 오른쪽부터 0123
        var enemyObject = Instantiate(enemyPrefab, spawnPos, Utils.QI);
        var enemyComponent = enemyObject.GetComponent<Enemy>();

        enemyList.Add(enemyComponent); // TODO: 적 한도 만들기? 아님 적절히 위치 조정
        enemyComponent.Setup(enemyData);
        EnemyAlignment();
    }
}
