using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZkCommunicate_.Device_
{
    public class Device_Management
    {
        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();

        public string Connect_Tcpip(string IP, string Port)
        {
            string result_ = "Error";

            if (IP.Trim() == "" || Port.Trim() == "")
            {
                result_ = "IP and Port cannot be null !";            
            }
            int idwErrorCode = 0;
            bool bIsConnected = axCZKEM1.Connect_Net(IP, Convert.ToInt32(Port));
            if (bIsConnected == true)
            {
                axCZKEM1.RegEvent(1, 65535); // dwMechineNuber is ignored in TCP IP Connection
                result_ = "Connected";
                //Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                result_ =  "Unable to connect the device,ErrorCode=" + idwErrorCode.ToString();
            }
            return result_;
        }
    }
}