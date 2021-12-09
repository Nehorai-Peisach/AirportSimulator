import gsap from 'gsap';

const Fly = (speed, position, side, setKills) => {

    let node = document.createElement('div');
    let simulator = document.getElementById('simulatorBg');
    
    node.onclick= () => {
        node.classList.add('active')
        gsap.killTweensOf(node);
        gsap.to(node, {y:'+=5vh', opacity:0}).then(() => {
            simulator.removeChild(node)
        });
        setKills(k => k+1);
    };
    simulator.appendChild(node);
    

    if(side){
        node.className= 'paper-plane left';
        gsap.fromTo(node,
        {x:'-5vw', y:`${position+30}vh`},
        {duration:speed, x:'105vw', y:`${position}vh`}).then(() => {
            simulator.removeChild(node)
        });
    }
    else{
        node.className= 'paper-plane right';
        gsap.fromTo(node,
            {x:'105vw', y:`${position+30}vh`},
            {duration:speed, x:'-5vw', y:`${position}vh`}).then(() => {
                simulator.removeChild(node)
        });
    }
}

export default Fly;