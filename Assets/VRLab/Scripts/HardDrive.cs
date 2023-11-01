using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VRLabEssentials{
    public class HardDrive : MonoBehaviour
    {
        public enum DriveInterface
        {
            SATA,
            mSATA,
            SAS,
            PCIe,
            U2,
            M2
        }

        public enum DriveType
        {
            HDD,
            SSD
        }

        [Header("Disk type data")]
        [Tooltip("Select disk type")]
        [SerializeField]private DriveType driveType;
        [Tooltip("Is disk external?")]
        [SerializeField]private bool isExternal;

        [Header("General disk settings")]
        [Tooltip("Select disk size in megabytes")]
        [SerializeField]private int totalDiskSpace;
        private int usedDiskSpace;
        [Tooltip("Select transfer rate in megabytes/second")]
        [SerializeField] private int readSpeed;
        [SerializeField] private int writeSpeed;
        [SerializeField] private int interfaceSpeed;
        [Tooltip("Power consumption in watts while active")]
        [SerializeField] private float powerConsumption;

        [Header("HDD data")]
        [Tooltip("Select disk's rotations per minute")]
        [SerializeField]private int rotationsPerMinute;
        [Tooltip("Select disk's rotations per minute")]
        [SerializeField]private DriveInterface driveInterface;

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
        public int GetUsedDiskSpace()
        {
            return usedDiskSpace;
        }
        public int GetRemainingDiskSpace()
        {
            return totalDiskSpace-usedDiskSpace;
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
        public DriveInterface GetInterface()
        {
            return driveInterface;
        }
        public void SetUsedDiskSpace(int usedDiskSpace)
        {
            this.usedDiskSpace = usedDiskSpace;
        }

        public void SetHardDrive(DriveType driveType, bool isExternal, int totalDiskSpace, int readSpeed, int writeSpeed, int interfaceSpeed, DriveInterface driveInterface){
            this.driveType = driveType;
            this.isExternal = isExternal;
            this.totalDiskSpace = totalDiskSpace;
            this.usedDiskSpace = 0;
            this.readSpeed = readSpeed;
            this.writeSpeed = writeSpeed;
            this.interfaceSpeed = interfaceSpeed;
            this.driveInterface = driveInterface;
        }
    }
}