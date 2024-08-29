using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    const int StageChipSize = 30;

    int currentChipIndex;

    public Transform character; //ターゲットキャラクターの指定
    public GameObject[] stageChips; //ステージチッププレハブ配列
    public int startChipIndex; //自動生成開始インデックス
    public int preInstantiate; //生成先読み回数
    public List<GameObject> generatedStageList = new List<GameObject>(); //生成済みステージチップ保持リスト

    void Start()
    {
        //スタート時に初期化
        currentChipIndex = startChipIndex - 1;
        UpdateStage(preInstantiate);
    }

    void Update()
    {
        //キャラクターの位置を参照して現在のステージチップを計算
        int charaPositionIndex = (int)(character.position.z / StageChipSize);

        //次のステージチップに入ったらステージの更新を行う
        if (charaPositionIndex + preInstantiate > currentChipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }
    }

    //指定のIndexまでステージチップを生成して管理下に置く
    void UpdateStage(int toChipIndex)
    {
        if (toChipIndex <= currentChipIndex) return;

        //指定のステージチップまでを作成
        for (int i = currentChipIndex + 1; i <= toChipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);
            //生成したものを管理リストに追加
            generatedStageList.Add(stageObject);
        }

        //ステージ保持上限に収まるように古いステージを削除
        while (generatedStageList.Count > preInstantiate + 2)
        {
            DestroyOldestStage();
        }

        currentChipIndex = toChipIndex;
    }

    //指定のインデックスの位置にランダムなStageオブジェクトを配置
    GameObject GenerateStage(int chipIndex)
    {
        int nextStageChip = Random.Range(0, stageChips.Length);

        GameObject stageObject = Instantiate(stageChips[nextStageChip], new Vector3(0, 0, chipIndex * StageChipSize), Quaternion.identity);

        return stageObject;
    }


//一番古いステージの削除
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
}
