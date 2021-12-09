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
        [Description("switch true/false the auto depart planes from the airport")]
        autodepart,
        [Description("land a single plane into the airport")]
        land,
        [Description("depart a single plain from the airport")]
        depart,
        [Description("show all the planes in the airport")]
        planes,
        [Description("end the program")]
        stop
    }
}
