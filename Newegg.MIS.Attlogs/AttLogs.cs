using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zkemkeeper;
using Newegg.ESD.Utility;
using System.Threading;

namespace Newegg.MIS.Attlogs
{
    [Run("data-synchronization", "")]
    public class AttLogs : BaseCommand
    {
        static object syncHandle = new object();
        bool bIsConnected = false;//the boolean value identifies whether the device is connected
        int iMachineNumber = 1;//the serial number of the device.After connecting the device ,this value will be changed.
        int idwErrorCode = 0;
        string ipfix = "10.16.225.";       
        private List<AttLogsEntity> AttLogsEntity = new List<AttLogsEntity>();
        protected override void Execute()
        {
            LogManager.LogDebug("ready to connect");
            Parallel.For(130, 140, (i) =>
        {
            zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();//Create Standalone SDK class dynamicly
            connect(i, axCZKEM1);           
        });
            AttLogsDAO.Synchronization(AttLogsEntity.UserId, AttLogsEntity.CHECKTIME, AttLogsEntity.CHECKType, AttLogsEntity.VERIFYCODE, AttLogsEntity.SENSORID, AttLogsEntity.Memoinfo, AttLogsEntity.WorkCode, AttLogsEntity.sn, AttLogsEntity.UserExtFmt);
        }
        private void connect(int i, zkemkeeper.CZKEMClass axCZKEM1)
        {
            LogManager.LogDebug("wether into the function");
            Thread.Sleep(0);
            string ip = null;
                ip = ipfix + i;
                    bool bIsConnected = axCZKEM1.Connect_Net(ip, 4370);
                    if (bIsConnected == true)
                    {
                        int idwErrorCode = 0;

                        int idwEnrollNumber = 0;
                        int idwVerifyMode = 0;
                        int idwInOutMode = 0;

                        int idwYear = 0;
                        int idwMonth = 0;
                        int idwDay = 0;
                        int idwHour = 0;
                        int idwMinute = 0;
                        int idwSecond = 0;
                        int idwWorkCode = 0;
                        int idwReserved = 0;

                        int iGLCount = 0;
                        int iIndex = 0;
                        LogManager.LogDebug("The device is connected");
                        iMachineNumber = 1;
                        axCZKEM1.RegEvent(iMachineNumber, 65535);//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                        axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
                        if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
                        {
                            while (axCZKEM1.GetGeneralExtLogData(iMachineNumber, ref idwEnrollNumber, ref idwVerifyMode, ref idwInOutMode,
                                     ref idwYear, ref idwMonth, ref idwDay, ref idwHour, ref idwMinute, ref idwSecond, ref idwWorkCode, ref idwReserved))//get records from the memory
                            {
                                AttLogsEntity.Add(new AttLogsEntity
                                {
                                    UserId =idwEnrollNumber,
                                    CheckTime =Convert.ToDateTime(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString()),
                                    CheckType =idwInOutMode,
                                    VerifyCode =idwVerifyMode,
                                    SensoRid =idwTMachineNumber,
                                    Memoinfo ="null",//test
                                    intWorkCode =idwWorkCode,
                                    sn = "5831492010096",//test
                                    UserExtFmt =idwReserved 
                                });
                            }
                        }
                        else
                        {
                            axCZKEM1.GetLastError(ref idwErrorCode);
                            if (idwErrorCode != 0)
                            {
                                LogManager.LogDebug("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString());
                            }
                            else
                            {
                                LogManager.LogDebug("No data from terminal returns!");
                            }
                        }
                        axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
                    }
                    else
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        LogManager.LogDebug("The device is disconnected");
                    }
        }
    }

}
