import { useEffect, useRef } from 'react';
import './Stations.css';

const Stations = ({stations}) => {
    const allSations = useRef(null);
    useEffect(() => {
        for (let i = 1; i <= 8; i++) {
            let node = document.createElement('div');
            node.className = 'station ready station'+i
            allSations.current.appendChild(node);
        }
    }, [])
    useEffect(() => {
        if(!stations) return;
        for (let i = 1; i <= 8; i++) {
            stations[i].plane
            ? allSations.current.children[i].innerHTML = stations[i].plane.planeName
            : allSations.current.children[i].innerHTML = '';
        }
    }, [stations])
    return <div ref={allSations} className='land free'/>
}

export default Stations;