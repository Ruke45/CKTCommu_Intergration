using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZkCommunicate_.Models
{
    public class Connect_deviceModel
    {
        [Required(ErrorMessage = "Device IP Required")]
        public string Device_IP { get; set; }

        [Required(ErrorMessage = "Password  Required")]
        public string Port { get; set; }

    }
}