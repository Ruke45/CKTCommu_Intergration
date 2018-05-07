using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZkCommunicate_.Device_
{
    public class Device_Management
    {
        zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
        bool IsDeviceConnected = false;
        int iMachineNumber = 1;

        public string Connect_Tcpip(string IP, string Port)
        {
            string result_ = "Error";
            int idwErrorCode = 0;

            if (IP.Trim() == "" || Port.Trim() == "")
            {
                result_ = "IP and Port cannot be null !";            
            }
            bool bIsConnected = axCZKEM1.Connect_Net(IP, Convert.ToInt32(Port));
            if (bIsConnected == true)
            {
                axCZKEM1.RegEvent(iMachineNumber, 65535); // dwMechineNuber is ignored in TCP IP Connection
                result_ = "Connected";
                IsDeviceConnected = true;
                //Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                result_ =  "Unable to connect the device,ErrorCode=" + idwErrorCode.ToString();
            }
            return result_;
        }


        private string GetGeneralLogData()
        {
            string result_ = "Error";
            if (IsDeviceConnected == false)
            {
                result_= "Please connect the device first ! Error";
                return result_;
            }

            string sdwEnrollNumber = "";
            int idwTMachineNumber = 0;
            int idwEMachineNumber = 0;
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;

            int idwErrorCode = 0;
            int iGLCount = 0;
            int iIndex = 0;

            List<string> AttlogList = new List<string>();

            axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
            if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
            {
                while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
                           out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                {
                    iGLCount++;
                    AttlogList.Add(sdwEnrollNumber);//modify by Darcy on Nov.26 2009
                    AttlogList.Add(idwVerifyMode.ToString());
                    AttlogList.Add(idwInOutMode.ToString());
                    AttlogList.Add(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString());
                    AttlogList.Add(idwWorkcode.ToString());
                    iIndex++;
                }
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);

                if (idwErrorCode != 0)
                {
                    result_ = "Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString() + "Error";
                }
                else
                {
                    result_ = "No data from terminal returns ! Error";
                }
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            return result_;
        }
    }
}