import { useState } from 'react';
import './AirportUi.css';
import Simulator from './simulator/Simulator';

const AirportUi = () => {
    const [flag, setFlag] = useState(false);
    return <div>
        {
            !flag
            ? <div className='welcomBtn free' onClick={() => setFlag(true)}>Welcome to Airport Simulator!</div>
            : <Simulator/>
        }
</div>
}

export default AirportUi;