import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

const Connect = (setConnection,setStations) => {
    let connection = new HubConnectionBuilder()
    .withUrl('https://localhost:44325/simulatorClient')
    .configureLogging(LogLevel.Information)
    .build();

    connection.on("StationsStatus", (list) =>{
        debugger
        setStations(list);
    });
    connection.start();
    setConnection(connection);
    console.log('connecting complte!');
}

export default Connect;