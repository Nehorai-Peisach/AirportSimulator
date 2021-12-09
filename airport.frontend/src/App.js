import { useEffect, useState } from 'react';
import './App.css';
import AirportUi from './components/AirportUi';
import Connect from './components/Connect';
import { HubConnectionBuilder } from "@microsoft/signalr";

function App() {

  const [connection, setConnection] = useState(new HubConnectionBuilder());
  
  const isLandscape = () => 
  window.matchMedia('(orientation:landscape)').matches,
  [orientation, setOrientation] = useState(isLandscape() ? 'landscape' : 'portrait'),
  onWindowResize = () => {              
    clearTimeout(window.resizeLag)
    window.resizeLag = setTimeout(() => {
      delete window.resizeLag                       
      setOrientation(isLandscape() ? 'landscape' : 'portrait')
    }, 200)
  }

useEffect(() => (
  Connect(setConnection),
  onWindowResize(),
  window.addEventListener('resize', onWindowResize),
  () => window.removeEventListener('resize', onWindowResize)
),[])

  return (
    <div className="App">
      {
        orientation !== 'landscape'
        ? <h1>Please rotate your phone.</h1>
        : !connection
        ? <h1>Connecting...</h1>
        : <AirportUi/>
      }
    </div>
  );
}

export default App;
