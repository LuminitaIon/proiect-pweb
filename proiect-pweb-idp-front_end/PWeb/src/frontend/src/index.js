import React from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Provider } from 'react-redux';
import { store } from './app/store';
import './index.css';

import Layout from './components/Layout/Layout';
import Map from './pages/Map/Map';
import News from './pages/News/News';
import Foundraisings from './pages/Foundraisings/Foundraisings';
import NGOs from './pages/NGOs/NGOs';
import Profile from './pages/Profile/Profile';

import { Auth0Provider } from "@auth0/auth0-react";

import { ReactNotifications } from 'react-notifications-component'
import 'react-notifications-component/dist/theme.css'

import 'animate.css/animate.min.css';
import CreateNews from './pages/CreateNews/CreateNews';
import NewsWaitingForApproval from './pages/NewsWaitingForApproval/NewsWaitingForApproval';

const container = document.getElementById('root');
const root = createRoot(container);

root.render(
  <Auth0Provider
    domain="apdev25.eu.auth0.com"
    clientId="hz2CJZLAZgnM2MGfi8CwsvQSTuTeqQHX"
    redirectUri={window.location.origin}
    audience="https://api.gosaveme.com"
    rolesKey="https://api.gosaveme.com/roles"
  >
    <React.StrictMode>
      <BrowserRouter >
        <ReactNotifications />
        <Routes>
          <Route path="/" element={<Layout />}>
            <Route path="/" element={<Map />} />
            <Route path="map" element={<Map />} />
            <Route path="news" element={<News />} />
            <Route path="ngos" element={<NGOs />} />
            <Route path="foundraisings" element={<Foundraisings />} />
            <Route path="profile" element={<Profile />} />
            <Route path="profile/:usernameParam" element={<Profile />} />
            <Route path="createnews" element={<CreateNews />} />
            <Route path="newswaiting" element={<NewsWaitingForApproval />} />
            <Route path="*" element={<Map />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </React.StrictMode>
  </Auth0Provider>,
);
