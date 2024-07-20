/* eslint-disable @typescript-eslint/no-explicit-any */
import { useEffect, useState } from 'react'
import './App.css'
import axios from 'axios';

function App() {
  const [activities, setActivities] = useState([]);

  useEffect(() => {
    axios.get('http://localhost:5213/api/activities')
      .then(response => {
        console.log(response.data);
        
        setActivities(response.data);
      });
      

  },[])

  return (
    <>
      <div>
        <ul>
          {
            activities.map((activity:any) => (
              <li key={activity.id}>{activity.title}</li>)
            )
            }
          
          <li></li>
        </ul>
       </div>
    </>
  )
}

export default App
