import './Simulator.css';
import {useEffect, useState} from 'react';
import Fly from './Fly';
import Stations from './stations/Stations';

const Simulator = ({stations}) => {
    
    const renderTime = 3000;
    const [kills, setKills] = useState(0);
    const [speed, setSpeed] = useState(1);
    const [position, setPosition] = useState(0);
    const [side, setSide] = useState(false);
    
    useEffect(() => {
        Fly(speed, position, side, setKills);
    })

    useEffect(() => {
        const interval = setInterval(() => setSide(!side), renderTime);
        setSpeed( 2+ Math.random() * 8);
        setPosition(Math.floor(Math.random() * 101)-50);
        return () => {
            clearInterval(interval);
        }
    }, [side]);

    return <section className='animation'>
        <h2>You Destroyed {kills} Planes!</h2>
        <Stations stations={stations}/>
        <div id='simulatorBg' className='simulatorBg'/>
    </section>
}

export default Simulator;