import React, { useEffect, useState } from "react";
import axios from 'axios';
import OrganizationCard from "../../components/OrganizationCard/OrganizationCard";

function NGOs() {
  const [organizations, setOrganizations] = useState([]);

  useEffect(() => {
    axios
      .get(`https://localhost:7125/Organizations`)
      .then((res) => res.data)
      .then((res) => setOrganizations(res.data));
  }, []);

  return (
    <div className="flex flex-row flex-wrap align-middle">
      {
        organizations.map((organization, idx) => (
          <OrganizationCard key={idx} organization={organization}/>
        ))
      }
    </div>
  );
}

export default NGOs;
