import React, { useEffect, useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import NewsPostSummary from "../../components/NewsPostSummary/NewsPostSummary";
import { HiOutlinePlusCircle } from "react-icons/hi";
import { Link } from "react-router-dom";
import NewsPost from "../../components/NewsPost/NewsPost";

import Modal from "react-modal/lib/components/Modal";

import { Store } from "react-notifications-component";

import axios from "axios";
import { useParams } from "react-router-dom";

const customStyles = {
  content: {
    top: "50%",
    left: "50%",
    right: "auto",
    bottom: "auto",
    marginRight: "-50%",
    transform: "translate(-50%, -50%)",
  },
};

function Profile() {
  const { usernameParam } = useParams();

  const [news, setNews] = useState([]);

  const [loaded, setLoaded] = useState(false);
  const { user, isAuthenticated, isLoading, getAccessTokenSilently } =
    useAuth0();
  const { loginWithRedirect } = useAuth0();

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [citizenship, setCitizenship] = useState("");
  const [address, setAddress] = useState("");
  const [description, setDescription] = useState("");
  const [isPrivileged, setIsPrivileged] = useState(false);
  const [imageURI, setImageURI] = useState("");
  const [username, setUsername] = useState("");
  const [rating, setRating] = useState(0);

  const [modalIsOpen, setIsOpen] = useState(false);

  const [modalNewsId, setModalNewsId] = useState(null);
  const [modalNewsTitle, setModalNewsTitle] = useState("");
  const [modalNewsPostedBy, setModalNewsPostedBy] = useState("");
  const [modalNewsContent, setModalNewsContent] = useState("");
  const [modalNewsImgSrc, setModalNewsImgSrc] = useState("");

  if (!isAuthenticated && !isLoading) {
    loginWithRedirect();
  }

  const shortenText = (text) => {
    if (text === null || text === undefined) return "";

    const words = text.split(" ");

    if (words.length > 100) {
      return words.slice(0, 100).join(" ") + "...";
    }

    return text;
  };

  useEffect(() => {
    async function executeLogin() {
      if (user === null || user === undefined) return;

      const accessToken = await getAccessTokenSilently();

      const username =
        usernameParam === null || usernameParam === undefined
          ? user.name
          : usernameParam;

      axios
        .get(`https://localhost:7125/User?username=${username}`, {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        })
        .then((res) => {
          // check status code
          if (res.status > 299) {
            axios
              .post(
                `https://localhost:7125/User?username=${username}`,
                {},
                {
                  headers: {
                    Authorization: `Bearer ${accessToken}`,
                  },
                }
              )
              .then(() =>
                Store.addNotification({
                  title: "Welcome!",
                  message: "Profile created",
                  type: "success",
                  insert: "top",
                  container: "top-right",
                  animationIn: ["animate__animated", "animate__fadeIn"],
                  animationOut: ["animate__animated", "animate__fadeOut"],
                  dismiss: {
                    duration: 5000,
                    onScreen: true,
                  },
                })
              );
          }
        })
        .then((res) => {
          setUsername(username);

          if (
            res.data === null &&
            (usernameParam === null || usernameParam === undefined)
          ) {
            axios
              .post(
                `https://localhost:7125/User?username=${username}`,
                {},
                {
                  headers: {
                    Authorization: `Bearer ${accessToken}`,
                  },
                }
              )
              .then(() =>
                Store.addNotification({
                  title: "Welcome!",
                  message: "Profile created",
                  type: "success",
                  insert: "top",
                  container: "top-right",
                  animationIn: ["animate__animated", "animate__fadeIn"],
                  animationOut: ["animate__animated", "animate__fadeOut"],
                  dismiss: {
                    duration: 5000,
                    onScreen: true,
                  },
                })
              );
          } else {
            setFirstName(res.data.firstName);
            setLastName(res.data.lastName);
            setPhoneNumber(res.data.phoneNumber);
            setCitizenship(res.data.citizenship);
            setAddress(res.data.address);
            setDescription(res.data.description);
            setIsPrivileged(res.data.isPrivileged);
            setImageURI(res.data.imageURI);
            setRating(res.data.rating);

            setLoaded(true);
          }
        })
        .then(() => {
          axios
            .get(
              `https://localhost:7125/News?approved=true&username=${username}`
            )
            .then((res) => res.data)
            .then((res) => {
              setNews(res.data);
            });
        });
    }
    executeLogin();
  }, [user]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const accessToken = await getAccessTokenSilently();

    axios
      .put(
        `https://localhost:7125/User?username=${user.name}`,
        {
          username: user.name,
          firstName,
          lastName,
          phoneNumber,
          citizenship,
          address,
          description,
          isPrivileged,
        },
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      )
      .then((res) => res.data)
      .then((res) => {
        Store.addNotification({
          title: "Success!",
          message: "Your profile has been updated",
          type: "success",
          insert: "top",
          container: "top-right",
          animationIn: ["animate__animated", "animate__fadeIn"],
          animationOut: ["animate__animated", "animate__fadeOut"],
          dismiss: {
            duration: 5000,
            onScreen: true,
          }
        });
      });
  };

  return user != undefined && user != null && user.name ? (
    <>
      <div className="flex flex-row justify-center md:mb-6 md:mt-10">
        {/* Create two column layout 4 and 8 */}
        <div className="flex flex-col w-full md:w-3/12 text-center items-center">
          <div className="flex flex-row">
            <img
              src={
                imageURI !== undefined &&
                imageURI !== null &&
                imageURI.length > 0
                  ? imageURI
                  : user.picture !== null
                  ? user.picture
                  : "https://via.placeholder.com/40"
              }
              alt="profile"
              className="rounded-lg h-40 w-40 md:h-40 md:w-40"
            />
          </div>

          <div class="flex items-center mt-2 bg-gray p-2 rounded-lg">
            {
              Array.from(Array(5).keys()).map((key) => 
                <svg class={`w-5 h-5 ${Math.round(rating) <= key ? 'text-brown' : 'text-yellow'}`} fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path></svg>
              )
            }
          </div>

          <div className="flex flex-row md:w-8/12 sm:w-full">
            <input
              className="text-2xl font-bold mt-4 bg-gray text-center rounded-b-none border-b-2 rounded-t-lg py-2 w-full"
              value={
                username.length > 0
                  ? username
                  : user === null || user.name === null
                  ? ""
                  : user.name
              }
              disabled={true}
            ></input>
          </div>

          <div className="flex flex-row md:w-8/12 sm:w-full">
            <input
              className="text-2xl font-bold mt-4 bg-gray text-center rounded-b-none border-b-2 rounded-t-lg py-2 w-full"
              placeholder="First name"
              value={firstName}
              onChange={(e) => setFirstName(e.target.value)}
            ></input>
          </div>

          <div className="flex flex-row md:w-8/12 sm:w-full">
            <input
              className="text-2xl font-bold mt-4 bg-gray text-center rounded-b-none border-b-2 rounded-t-lg py-2 w-full"
              placeholder="Last name"
              value={lastName}
              onChange={(e) => setLastName(e.target.value)}
            ></input>
          </div>

          <div className="flex flex-row md:w-8/12 sm:w-full">
            <input
              className="text-2xl font-bold mt-4 bg-gray text-center rounded-b-none border-b-2 rounded-t-lg py-2 w-full"
              placeholder="Phone number"
              value={phoneNumber}
              onChange={(e) => setPhoneNumber(e.target.value)}
            ></input>
          </div>

          <div className="flex flex-row md:w-8/12 sm:w-full">
            <input
              className="text-2xl font-bold mt-4 bg-gray text-center rounded-b-none border-b-2 rounded-t-lg py-2 w-full"
              placeholder="Citizenship"
              value={citizenship}
              onChange={(e) => setCitizenship(e.target.value)}
            ></input>
          </div>

          <div className="flex flex-row md:w-8/12 sm:w-full">
            <input
              className="text-2xl font-bold mt-4 bg-gray text-center rounded-b-none border-b-2 rounded-t-lg py-2 w-full"
              placeholder="Address"
              value={address}
              onChange={(e) => setAddress(e.target.value)}
            ></input>
          </div>

          <div className="flex flex-row md:w-8/12 sm:w-full">
            <textarea
              className="text-2xl font-bold mt-4 bg-gray text-center rounded-b-none border-b-2 rounded-t-lg py-2 w-full"
              placeholder="Short description"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
            ></textarea>
          </div>

          {user["https://api.gosaveme.com/roles"].includes("Administrator") && (
            <div className="form-check mt-2 text-lg align-bottom">
              <input
                className="form-check-input h-8 w-4 border border-blue rounded-sm bg-white checked:bg-blue checked:border-blue focus:outline-none transition duration-200 bg-no-repeat bg-center bg-contain float-left mr-2 cursor-pointer"
                type="checkbox"
                checked={isPrivileged}
                onClick={(e) =>
                  setIsPrivileged(isPrivileged === true ? false : true)
                }
                id="flexCheckChecked"
              />
              <label
                className="form-check-label inline-block text-gray-800"
                for="flexCheckChecked"
              >
                Privileged user
              </label>
            </div>
          )}

          {(username === user.name ||
            user["https://api.gosaveme.com/roles"].includes(
              "Administrator"
            )) && (
            <div className="flex flex-row md:w-8/12 sm:w-full mt-6 text-center">
              <button
                className="bg-blue hover:bg-blue-hover text-white font-bold py-2 px-4 rounded-lg m-auto"
                onClick={handleSubmit}
              >
                Update
              </button>
            </div>
          )}
        </div>

        <div className="flex flex-col w-full md:w-9/12 text-center justify-center">
          <div className="flex flex-row justify-between">
            <div className="flex flex-col w-full md:w-2/12"></div>
            <div className="flex flex-col w-full  md:w-8/12 justify-center py-3 rounded-lg text-center text-4xl bg-gray">
              
              {username === user.name && (
                <Link to="/createnews" className="m-0 p-0 ml-4">
                  <HiOutlinePlusCircle
                    color="#2B0245"
                    size="40px"
                    className="p-0 absolute ml-4"
                  />
                </Link>
              )}
              Latest Posted News
            </div>
            <div className="flex flex-col w-full md:w-2/12"></div>
          </div>

          {news.map((newsData) => (
            <NewsPostSummary
              key={`news-${newsData.id}`}
              imgSrc={newsData.imageURI}
              postedBy={newsData.username}
              title={newsData.title}
              content={shortenText(newsData.text)}
              onClickFunc={() => {
                setModalNewsId(newsData.id);
                setModalNewsTitle(newsData.title);
                setModalNewsPostedBy(newsData.username);
                setModalNewsContent(newsData.text);
                setModalNewsImgSrc(newsData.imageURI);
                setIsOpen(true);
              }}
              id={newsData.id}
              approved={newsData.approved}
              username={newsData.username}
              reaction={newsData.reaction}
            />
          ))}
        </div>
      </div>
      <Modal
        isOpen={modalIsOpen}
        onRequestClose={() => setIsOpen(false)}
        style={customStyles}
      >
        <NewsPost
          key={`news-${modalNewsId}`}
          imgSrc={modalNewsImgSrc}
          postedBy={modalNewsPostedBy}
          title={modalNewsTitle}
          content={modalNewsContent}
        />
      </Modal>
    </>
  ) : (
    <div className="flex flex-col w-full md:w-9/12 text-center justify-center">
      Loading...
    </div>
  );
}

export default Profile;
