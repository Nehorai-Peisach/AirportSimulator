using System.ComponentModel;

namespace Airport.Simulator
{
    public enum Commands
    {
        [Description("connect to the server")]
        connect,
        [Description("disconnect to the server")]
        disconnect,
        [Description("switch true/false the auto land planes into the airport")]
        autoland,
        [Description("switch true/false the auto deport planes from the airport")]
        autodeport,
        [Description("land a single plane into the airport")]
        land,
        [Description("deport a single plain from the airport")]
        deport,
        [Description("show all the planes in the airport")]
        planes,
        [Description("end the program")]
        stop
    }
}
