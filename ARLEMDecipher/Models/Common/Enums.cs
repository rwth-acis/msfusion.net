using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARLEMDecipher.Models.Common
{
    public enum PrimitiveTypes
    {
        Visual,
        Auditory,
        Ver
    }

    public enum TriggerModes
    {
        Enter,
        Exit,
        Click,
        Detect,
        Sensor
    }

    public enum TriggerOperationTypes
    {
        Activate,
        Deactivate
    }
}
