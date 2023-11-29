import React, { useState, useRef, useEffect } from "react";
import "./Map.css";
import { Map as MapRGL, Marker, Popup } from "react-map-gl";
import axios from "axios";

function Map() {
  const [safepoints, setSafepoints] = useState();
  const [showPopup, setShowPopup] = useState(true);
  const [popupLatitude, setPopupLatitude] = useState(0);
  const [popupLongitude, setPopupLongitude] = useState(0);
  const [popupText, setPopupText] = useState("");

  useEffect(() => {
    axios
      .get("https://localhost:7125/safepoint")
      .then((res) => res.data)
      .then((res) => setSafepoints(res.data));
  }, []);

  return (
    <div>
      <MapRGL
        initialViewState={{
          longitude: 24.9070199,
          latitude: 45.8811392,
          zoom: 6,
        }}
        style={{ width: "100vw", height: "calc(100vh - 4.3rem)" }}
        mapStyle="mapbox://styles/mapbox/streets-v9"
        mapboxAccessToken="pk.eyJ1IjoiYWxpbnAyNSIsImEiOiJjam44dWx5aGgyMG9xM3JudjM0YTkzZ2M3In0.AVz1mcQaLguebYx47TXdYA"
      >
        {true && (
          <Popup
            longitude={popupLongitude}
            latitude={popupLatitude}
            anchor="bottom"
            onClose={() => setShowPopup(false)}
            dynamicPosition={false}
            closeOnClick={false}
          >
            {popupText} <br/>
            <i>Latitude: {popupLatitude} <br/>
            Longitude: {popupLongitude}</i>
          </Popup>
        )}
        {safepoints &&
          safepoints.map((safepoint) => (
            <Marker
              longitude={safepoint.longitude}
              latitude={safepoint.latitude}
              anchor="bottom"
              onClick={() => {
                setShowPopup(true);
                setPopupLatitude(safepoint.latitude);
                setPopupLongitude(safepoint.longitude);
                setPopupText(safepoint.name);
              }}
              className="hover:cursor-pointer"
              clickTolerance={100}
              color={
                safepoint.type === "HOSPITAL"
                  ? "red"
                  : safepoint.type === "BORDER"
                  ? "blue"
                  : safepoint.type === "BUNKER"
                  ? "green"
                  : "brown"
              }
            />
          ))}
      </MapRGL>
    </div>
  );
}

export default Map;
