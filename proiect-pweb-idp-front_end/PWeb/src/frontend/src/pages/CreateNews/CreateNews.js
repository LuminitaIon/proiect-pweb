import React, { useState, useEffect } from "react";

import { useAuth0 } from "@auth0/auth0-react";

import { Store } from "react-notifications-component";

import axios from "axios";

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

function CreateNews() {
  const { user, isAuthenticated, isLoading, getAccessTokenSilently, loginWithRedirect } = useAuth0();

  const [newsTitle, setNewsTitle] = useState("");
  const [newsContent, setNewsContent] = useState("");
  const [newsImageSrc, setNewsImageSrc] = useState("");

  if (!isAuthenticated && !isLoading) {
    loginWithRedirect();
  }

  useEffect(() => {}, []);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const accessToken = await getAccessTokenSilently();
    const newsApproved = user["https://api.gosaveme.com/roles"].includes(
      "Administrator"
    ) || user["https://api.gosaveme.com/roles"].includes(
      "PrivilegedUser"
    ) ? true : false;

    axios.post('https://localhost:7125/News', {
      title: newsTitle,
      text: newsContent,
      imageURI: newsImageSrc,
      username: user.name,
      approved: newsApproved
    }, {
      headers: {
        Authorization: `Bearer ${accessToken}`,
      }
    })
    .then(() => {
      setNewsContent("");
      setNewsTitle("");
      setNewsImageSrc("");

      // redirect
      window.location.href = "/news";

      Store.addNotification({
        title: "Success!",
        message: newsApproved ? "News posted" : "News submitted for approval",
        type: "success",
        insert: "top",
        container: "top-right",
        animationIn: ["animate__animated", "animate__fadeIn"],
        animationOut: ["animate__animated", "animate__fadeOut"],
        dismiss: {
          duration: 5000,
          onScreen: true,
        },
      });
    })
  }

  return (
    <div className="md:w-8/12 flex flex-col content-center justify-center items-center m-auto mt-4">
      <h1 className="text-3xl font-bold ">Create news</h1>
      <input
        value={newsTitle}
        className="bg-gray border-2 border-brown rounded-lg mt-4 p-2 w-full text-3xl"
        placeholder="Title"
        onChange={(e) => setNewsTitle(e.target.value)}
      />
      <textarea
        value={newsContent}
        className="bg-gray border-2 border-brown rounded-lg mt-4 p-2 w-full text-3xl"
        placeholder="News content"
        rows={10}
        onChange={(e) => setNewsContent(e.target.value)}
      />
      <input
        value={newsImageSrc}
        className="bg-gray border-2 border-brown rounded-lg mt-4 p-2 w-full text-3xl"
        placeholder="Image URL"
        onChange={(e) => setNewsImageSrc(e.target.value)}
      />
      <button
        className="bg-blue hover:bg-blue-hover text-white font-bold py-2 px-4 rounded-lg mt-4 m-auto"
        onClick={handleSubmit}
      >
        Post News
      </button>
    </div>
  );
}

export default CreateNews;
