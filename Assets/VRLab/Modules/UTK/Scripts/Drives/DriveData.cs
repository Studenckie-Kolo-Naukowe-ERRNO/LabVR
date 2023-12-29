using System.Collections.Generic;
using UnityEngine;

namespace UTKLab
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Components/Disc")]
    public class DriveData : ScriptableObject
    {
        public enum DriveType
        {
            HDD,
            SSD
        }

        [Header("Disk type data")]
        [Tooltip("Select disk type")]
        [SerializeField] private DriveType driveType;
        [Tooltip("Is disk external?")]
        [SerializeField] private bool isExternal;

        [Header("General disk settings")]
        [Tooltip("Select disk size in megabytes")]
        [SerializeField] private int totalDiskSpace;
        
        [Tooltip("Select transfer rate in megabytes/second")]
        [SerializeField] private int readSpeed;
        [SerializeField] private int writeSpeed;
        [SerializeField] private int interfaceSpeed;
        [Tooltip("Power consumption in watts while active")]
        [SerializeField] private float powerConsumption;

        [Header("HDD data")]
        [Tooltip("Select disk's rotations per minute")]
        [SerializeField] private int rotationsPerMinute;
        [Tooltip("Select disk's rotations per minute")] 
        [SerializeField] private Connector driveInterface;

        public DriveType GetDriveType()
        {
            return driveType;
        }

        public bool GetIsExternal()
        {
            return isExternal;
        }

        public int GetTotalDiskSpace()
        {
            return totalDiskSpace;
        }

        public int GetReadSpeed()
        {
            return readSpeed;
        }

        public int GetWriteSpeed()
        {
            return writeSpeed;
        }

        public int GetInterfaceSpeed()
        {
            return interfaceSpeed;
        }

        public int GetRotationsPerMinute()
        {
            return rotationsPerMinute;
        }

        public Connector GetInterface()
        {
            return driveInterface;
        }

    }
}
