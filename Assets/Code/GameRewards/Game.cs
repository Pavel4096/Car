﻿using UnityEngine;


using System;
using System.Xml.Serialization;
using System.IO;

namespace Car.Rewards
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private GameObject _item;
        [SerializeField]
        private Reward[] _rewards;
        [SerializeField]
        private RewardsView _rewardsView;
        [SerializeField]
        private AmountsInformationView _amountsInformationView;
        [SerializeField]
        private int _timeToNext = 1*60;
        [SerializeField]
        private int _timeToReset = 1*60;

        private IUserData _userData;
        private RewardsController _rewardsController;
        private IAmountsInformationController _amountsInformationController;

        private readonly string _fileName = "data";

        private void Start()
        {
            _userData = GetUserData();
            _amountsInformationController = new AmountsInformationController(_amountsInformationView, _userData);
            _rewardsController = new RewardsController(_rewardsView, _amountsInformationController, _rewards, _userData, _item, _timeToNext, _timeToReset);
            Application.quitting += SaveUserData;
        }

        private void Update()
        {
            _rewardsController.UpdateTimer();
        }

        private IUserData GetUserData()
        {
            UserData userData = null;

            if(File.Exists(_fileName))
            {
                using(var inputStream = File.Open(_fileName, FileMode.Open, FileAccess.Read))
                {
                    var xmlSerializer = new XmlSerializer(typeof(UserData));
                    userData = xmlSerializer.Deserialize(inputStream) as UserData;
                }
            }

            if(userData == null)
                userData = new UserData();
            userData.SetMaxRewardIndex(_rewards.Length - 1);

            return userData;
        }

        private void SaveUserData()
        {
            using(var outputStream = File.Open(_fileName, FileMode.Create, FileAccess.Write))
            {
                var xmlSerializer = new XmlSerializer(typeof(UserData));
                xmlSerializer.Serialize(outputStream, _userData);
            }
        }
    }
}
