using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.CosecApi.Dtos
{
    public class DeleteDeviceDto
    {
        public int DeviceId { get; set; }
        public int DeviceTypeId { get; set; }
    }
}
