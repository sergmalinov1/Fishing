using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using System;
using UnityEngine;


namespace CodeBase.Logic
{
    public class BackgroundManager : MonoBehaviour
    {
        public GameObject[] Lakes;

        private IPersistentProgress _progressService;
        private int _selecetedLakeId = -1;

        public void Construct(IPersistentProgress progressService)
        {
            _progressService = progressService;
        }

        public void Inizialize()
        {
            _progressService.Progress.EquipmentStats.ChangeStats += CheckLake;
            CheckLake();
        }

        private void CheckLake()
        {
            int lakeId =  _progressService.Progress.Inventory.GetSelectedEquipmentByKind(KindEquipmentId.Lake);
            //Debug.Log("lakeId" + lakeId);
           // Debug.Log("_selecetedLakeId" + _selecetedLakeId);

            if (_selecetedLakeId != lakeId)
            {
                _selecetedLakeId = lakeId;
                EnableLakeByNumber(lakeId);
            }
        }

        private void EnableLakeByNumber(int number)
        {
            for(int i=0; i < Lakes.Length; i++ )
            {
                if(i == number)
                {
                    Lakes[i].SetActive(true);
                }
                else
                {
                    Lakes[i].SetActive(false);
                }
            }
        }

        private void OnDestroy()
        {
            _progressService.Progress.EquipmentStats.ChangeStats -= CheckLake;
        }

    }
}
