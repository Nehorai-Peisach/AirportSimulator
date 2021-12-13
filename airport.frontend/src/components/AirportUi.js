import { useState, useEffect } from 'react';
import './AirportUi.css';
import Simulator from './simulator/Simulator';

const AirportUi = ({connection,stations}) => {
    const [flag, setFlag] = useState(false);
    useEffect(() => {
        connection.connectionState == "Connected" && connection.invoke("StationsStatus");
    }, [connection.connectionState])
    return <div>
        {
            !flag
            ? <div className='welcomBtn free' onClick={() => setFlag(true)}>Welcome to Airport Simulator!</div>
            : <Simulator stations={stations}/>
        }
</div>
}

export default AirportUi;