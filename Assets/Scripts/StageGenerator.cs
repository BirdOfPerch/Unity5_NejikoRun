using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    const int StageChipSize = 30;

    int currentChipIndex;

    public Transform character; //�^�[�Q�b�g�L�����N�^�[�̎w��
    public GameObject[] stageChips; //�X�e�[�W�`�b�v�v���n�u�z��
    public int startChipIndex; //���������J�n�C���f�b�N�X
    public int preInstantiate; //������ǂ݉�
    public List<GameObject> generatedStageList = new List<GameObject>(); //�����ς݃X�e�[�W�`�b�v�ێ����X�g

    void Start()
    {
        //�X�^�[�g���ɏ�����
        currentChipIndex = startChipIndex - 1;
        UpdateStage(preInstantiate);
    }

    void Update()
    {
        //�L�����N�^�[�̈ʒu���Q�Ƃ��Č��݂̃X�e�[�W�`�b�v���v�Z
        int charaPositionIndex = (int)(character.position.z / StageChipSize);

        //���̃X�e�[�W�`�b�v�ɓ�������X�e�[�W�̍X�V���s��
        if (charaPositionIndex + preInstantiate > currentChipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }
    }

    //�w���Index�܂ŃX�e�[�W�`�b�v�𐶐����ĊǗ����ɒu��
    void UpdateStage(int toChipIndex)
    {
        if (toChipIndex <= currentChipIndex) return;

        //�w��̃X�e�[�W�`�b�v�܂ł��쐬
        for (int i = currentChipIndex + 1; i <= toChipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);
            //�����������̂��Ǘ����X�g�ɒǉ�
            generatedStageList.Add(stageObject);
        }

        //�X�e�[�W�ێ�����Ɏ��܂�悤�ɌÂ��X�e�[�W���폜
        while (generatedStageList.Count > preInstantiate + 2)
        {
            DestroyOldestStage();
        }

        currentChipIndex = toChipIndex;
    }

    //�w��̃C���f�b�N�X�̈ʒu�Ƀ����_����Stage�I�u�W�F�N�g��z�u
    GameObject GenerateStage(int chipIndex)
    {
        int nextStageChip = Random.Range(0, stageChips.Length);

        GameObject stageObject = Instantiate(stageChips[nextStageChip], new Vector3(0, 0, chipIndex * StageChipSize), Quaternion.identity);

        return stageObject;
    }


//��ԌÂ��X�e�[�W�̍폜
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
}
