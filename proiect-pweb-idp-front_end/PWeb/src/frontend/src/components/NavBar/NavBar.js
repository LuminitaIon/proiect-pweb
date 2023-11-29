import React, { useState } from "react";
import { Transition } from "@headlessui/react";
import logopw from "../../assets/logopw.png";
import { Link } from "react-router-dom";
import { HiOutlineUserCircle } from "react-icons/hi";

import { useAuth0 } from "@auth0/auth0-react";

const navbarItemClass =
  "text-white border-b-2 border-brown hover:border-white px-3 pt-2 text-md font-medium hover:cursor-pointer";
const navbarItemMobileClass =
  "text-white border-b-2 border-brown hover:border-white block px-3 py-2 text-base font-medium";

function NavBar() {
  const [isDropdownOpen, toggleDropdown] = useState(false);
  const { user, isAuthenticated, isLoading, getAccessTokenSilently } =
    useAuth0();

  return (
    <nav className="bg-brown">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 pb-1">
        <div className="flex items-center justify-between h-16">
          <div className="flex items-center">
            <div className="flex-shrink-0">
              <Link to="/map">
                <img
                  src={logopw}
                  alt="logo here"
                  height="60"
                  width="60"
                  className="rounded-md mt-1 mb-1"
                />
              </Link>
            </div>
            <div className="hidden md:block">
              <div className="ml-10 flex items-baseline space-x-4">
                <Link to="/map" className={navbarItemClass}>
                  Map
                </Link>

                <Link to="/news" className={navbarItemClass}>
                  News
                </Link>

                <Link to="/ngos" className={navbarItemClass}>
                  NGOs
                </Link>

                <Link to="/foundraisings" className={navbarItemClass}>
                  Foundraisings
                </Link>
 
                {isAuthenticated &&
                  (user["https://api.gosaveme.com/roles"].includes(
                    "Administrator"
                  ) ||
                    user["https://api.gosaveme.com/roles"].includes(
                      "PrivilegedUser"
                    )) && (
                    <Link to="/newswaiting" className={navbarItemClass}>
                      News waiting for approval
                    </Link>
                  )}
              </div>
            </div>
          </div>
          <div className="hidden md:block">
            <Link to="/profile">
              <HiOutlineUserCircle color="#2B0245" size="50px" />
            </Link>
          </div>
          <div className="-mr-2 flex md:hidden">
            <button
              onClick={() => toggleDropdown(!isDropdownOpen)}
              type="button"
              className="bg-gray-900 inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:text-white hover:bg-gray-800 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white active:outline-none"
              aria-controls="mobile-menu"
              aria-expanded="false"
            >
              <span className="sr-only">Open main menu</span>
              {!isDropdownOpen ? (
                <svg
                  className="block h-6 w-6"
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                  stroke="currentColor"
                  aria-hidden="true"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth="2"
                    d="M4 6h16M4 12h16M4 18h16"
                  />
                </svg>
              ) : (
                <svg
                  className="block h-6 w-6"
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                  stroke="currentColor"
                  aria-hidden="true"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth="2"
                    d="M6 18L18 6M6 6l12 12"
                  />
                </svg>
              )}
            </button>
          </div>
        </div>
      </div>

      <Transition
        show={isDropdownOpen}
        enter="transition ease-out duration-100 transform"
        enterFrom="opacity-0 scale-95"
        enterTo="opacity-100 scale-100"
        leave="transition ease-in duration-75 transform"
        leaveFrom="opacity-100 scale-100"
        leaveTo="opacity-0 scale-95"
      >
        {(ref) => (
          <div className="md:hidden" id="mobile-menu">
            <div ref={ref} className="px-2 pt-2 pb-3 space-y-1 sm:px-3">
              <Link to="/map" className={navbarItemMobileClass}>
                Map
              </Link>

              <Link to="/news" className={navbarItemMobileClass}>
                News
              </Link>

              <Link to="/dashboard" className={navbarItemMobileClass}>
                Projects
              </Link>

              <Link to="/dashboard" className={navbarItemMobileClass}>
                Calendar
              </Link>
            </div>
          </div>
        )}
      </Transition>
    </nav>
  );
}

export default NavBar;
