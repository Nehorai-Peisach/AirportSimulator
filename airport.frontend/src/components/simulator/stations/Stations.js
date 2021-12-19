import { useEffect, useRef } from 'react';
import './Stations.css';

const Stations = ({stations}) => {
    const allSations = useRef(null);
    useEffect(() => {
        for (let i = 1; i <= 8; i++) {
            let node = document.createElement('div');
            if(i<=3 || i==5) node.className = 'landing station station'+i
            if(i==4 || i==6 || i==7) node.className = 'both station station'+i
            if(i==8) node.className = 'departing station station'+i
            
            allSations.current.appendChild(node);
        }
        for (let i = 1; i <= 8; i++) {
            let green = document.createElement('div');
            green.className='green green'+i;
            allSations.current.appendChild(green);
            if(i<6){
                let blue = document.createElement('div');
                blue.className='blue blue'+i;
                allSations.current.appendChild(blue);
            }
        }

    }, [])

    useEffect(() => {
        if(!stations) return;
        for (let i = 0; i < stations.length; i++) {
            stations[i].plane
            ? allSations.current.children[i].innerHTML = stations[i].plane.planeName
            : allSations.current.children[i].innerHTML = '';
        }
    }, [stations])

    return <div ref={allSations} className='land free'/>
}

export default Stations;