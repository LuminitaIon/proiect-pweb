import React, { useState, useEffect } from "react";

import NewsPost from "../../components/NewsPost/NewsPost";
import NewsPostSummary from "../../components/NewsPostSummary/NewsPostSummary";

import Modal from "react-modal/lib/components/Modal";

import axios from "axios";

import { useAuth0 } from "@auth0/auth0-react";

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

function News() {
  const { user, getAccessTokenSilently } =
    useAuth0();
  const [news, setNews] = useState([]);
  const [modalIsOpen, setIsOpen] = useState(false);

  const [modalNewsId, setModalNewsId] = useState(null);
  const [modalNewsTitle, setModalNewsTitle] = useState("");
  const [modalNewsPostedBy, setModalNewsPostedBy] = useState("");
  const [modalNewsContent, setModalNewsContent] = useState("");
  const [modalNewsImgSrc, setModalNewsImgSrc] = useState("");

  useEffect(() => {
    axios
      .get(`https://localhost:7125/News?approved=true`)
      .then((res) => res.data)
      .then((res) => setNews(res.data));
  }, []);

  const shortenText = (text) => {
    if (text === null)
      return "";

    const words = text.split(" ");

    if (words.length > 100) {
      return words.slice(0, 100).join(" ") + "...";
    }

    return text;
  };

  return (
    <>
      <div className="md:w-8/12 flex flex-col content-center justify-center items-center m-auto">
        {news.map((news) => (
          <NewsPostSummary
            username={user ? user.name : ""}
            key={`news-${news.id}`}
            imgSrc={news.imageURI}
            postedBy={news.username}
            title={news.title}
            content={shortenText(news.text)}
            onClickFunc={() => {
              setModalNewsId(news.id);
              setModalNewsTitle(news.title);
              setModalNewsPostedBy(news.username);
              setModalNewsContent(news.text);
              setModalNewsImgSrc(news.imageURI);
              setIsOpen(true);
            }}
            id={news.id}
            approved={news.approved}
            reaction={news.reaction}
          />
        ))}
      </div>
      <Modal
        isOpen={modalIsOpen}
        onRequestClose={() => setIsOpen(false)}
        style={customStyles}
        contentLabel="Example Modal"
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
  );
}

export default News;
