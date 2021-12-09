import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

const Connect = (setConnection) => {
    let connection = new HubConnectionBuilder()
    .withUrl('https://localhost:44325/simulatorClient')
    .configureLogging(LogLevel.Information)
    .build();

    connection.start();
    setConnection(connection);
    console.log('connecting complte!');
}

export default Connect;