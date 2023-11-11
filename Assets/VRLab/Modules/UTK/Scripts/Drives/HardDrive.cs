using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTKLab
{
    public class HardDrive : MonoBehaviour, IDevice
    {
        [SerializeField] private DriveData driveData;

        public Connector GetDeviceConnector()
        {
            return driveData.GetInterface();
        }

        public DriveData GetDriveData()
        {
            return driveData;
        }
    }
}