import { useEffect, useRef } from 'react';
import './Stations.css';

const Stations = () => {
    const allSations = useRef(null);
    useEffect(() => {
        for (let i = 1; i <= 8; i++) {
            let node = document.createElement('div');
            node.className = 'station ready station'+i
            node.innerHTML = i;
            allSations.current.appendChild(node);
        }
    }, [])
    return <div ref={allSations} className='land free'/>
}

export default Stations;