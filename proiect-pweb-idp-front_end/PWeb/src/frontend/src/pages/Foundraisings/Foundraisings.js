import React, { useEffect, useState } from "react";
import FoundraisingCard from "../../components/FoundraisingCard/FoundraisingCard";

import axios from "axios";

function Foundraisings() {
  const [foundraisings, setFoundraisings] = useState([]);

  useEffect(() => {
    axios
        .get(`https://localhost:7125/Foundraisings`)
        .then((res) => res.data)
        .then((res) => {
          setFoundraisings(res.data);
        })
  }, []);

  return (
    <div>
      {
        foundraisings.map((foundraising, idx) => 
          <FoundraisingCard key={idx} foundraising={foundraising} />
        )
      }
    </div>
  );
}

export default Foundraisings;
