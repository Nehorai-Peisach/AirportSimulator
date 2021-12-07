import { useEffect, useState } from 'react';
import './App.css';
import AirportUi from './components/AirportUi/AirportUi';
import Connect from './components/Connect';

function App() {
  const [connection, setConnection] = useState();
  useEffect(() => {
    Connect(setConnection);
  }, [])
  return (
    <div className="App">
      {
        !connection
        ? <h1>Connecting...</h1>
        : <AirportUi/>
      }
    </div>
  );
}

export default App;
