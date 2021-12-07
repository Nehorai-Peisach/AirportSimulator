import './Simulator.css';
import {useRef, useEffect, useState} from 'react';
import gsap from 'gsap';
import { MotionPathPlugin, Power0 } from 'gsap/all';

const Simulator = () => {
    
    const [scale, setScale] = useState(1);
    const [speed, setSpeed] = useState(1);
    const [position, setPosition] = useState(0);
    const [side, setSide] = useState(false);
    const [index, setIndex] = useState(0);
    const [kills, setKills] = useState(0);
    
    const renderTime = 3000;
    function fly() {
        setIndex(index+1);

        console.log(index);
    }
    
    useEffect(() => {
        let node = document.createElement('div');
        node.id = 'plane'+index;
        node.className = 'paper-plane';
        node.onclick= () => {
            node.classList.add('active')
            gsap.killTweensOf(`#plane${index}`);
            gsap.to(`#plane${index}`, {y:'+=5vh', opacity:0}).then(() => {
                simulator.removeChild(node)
            });
            setKills(kills+1);
        };
        let simulator = document.getElementById('simulatorBg');
        simulator.appendChild(node);
        

        if(side){
            node.classList.add('left')
            gsap.fromTo(`#plane${index}`,
            {x:'-5vw', y:`${position+30}vh`},
            {duration:speed, x:'105vw', y:`${position}vh`}).then(() => {
                simulator.removeChild(node)
            });
        }
        else{
            node.classList.add('right');
            gsap.fromTo(`#plane${index}`,
                {x:'105vw', y:`${position+30}vh`},
                {duration:speed, x:'-5vw', y:`${position}vh`}).then(() => {
                    simulator.removeChild(node)
                });
            }

    }, [side])

    useEffect(() => {
        const interval = setInterval(fly, renderTime);
        setSpeed( 2+ Math.random() * 8);
        setPosition(Math.floor(Math.random() * 101)-50);
        setSide(!side);

        return () => {
            clearInterval(interval);
        }
    }, [index]);

    return <section className='animation'>
        <h2>You Kill {kills} Planes!</h2>
        <div id='simulatorBg' className='simulatorBg'/>
        <div className='simulator free'></div>
        
    </section>
}

export default Simulator;