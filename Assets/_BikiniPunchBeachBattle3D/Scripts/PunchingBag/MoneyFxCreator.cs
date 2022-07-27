﻿using _BikiniPunchBeachBattle3D.Characters;
using _BikiniPunchBeachBattle3D.GameServices;
using RH.Utilities.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace _BikiniPunchBeachBattle3D.PunchingBag
{
    public class MoneyFxCreator : MonoBehaviour
    {
        [SerializeField] private Transform _anchor;
        
        private DataService _data;
        private EventsMediator _events;
        private ConfigsService _configs;

        private void Start()
        {
            _data = Services.Get<DataService>();
            _events = Services.Get<EventsMediator>();
            _configs = Services.Get<ConfigsService>();
            
            _events.PunchPerformed.AddListener(CreateFxIfPlayerPunched);
        }

        private void OnDestroy() => 
            _events.PunchPerformed.RemoveListener(CreateFxIfPlayerPunched);

        private void CreateFxIfPlayerPunched(CharacterType characterType, string side)
        {
            if (characterType == CharacterType.Player) 
                CreateFx();
        }

        private void CreateFx()
        {
            int incomeValue = _data.GetPunchIncome();
            GameObject fx = Instantiate(_configs.IncomeFxPrefab, _anchor);
            
            fx.GetComponentInChildren<Text>().text = $"+{incomeValue}";
            
            Destroy(fx, 1f);
        }
    }
}