import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

const Connect = (setConnection,setStations) => {
    let connection = new HubConnectionBuilder()
    .withUrl('https://airportbackend.azurewebsites.net/simulatorClient')
    .configureLogging(LogLevel.Information)
    .build();

    connection.on("StationsStatus", (list) =>{
        setStations(list);
    });
    connection.start();
    setConnection(connection);
    console.log('connecting complte!');
}

export default Connect;