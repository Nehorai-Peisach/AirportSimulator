import { useEffect, useRef } from 'react';
import './Stations.css';

const Stations = ({stations}) => {
    const allSations = useRef(null);
    useEffect(() => {
        if(!stations) return;
debugger
        for (let i = 1; i <= 8; i++) {
            let node = document.createElement('div');
            node.className = 'station ready station'+i
            if(stations[i].plane)
            node.innerHTML = stations[i].plane.planeName;
            allSations.current.appendChild(node);
        }
    }, [stations])
    return <div ref={allSations} className='land free'/>
}

export default Stations;