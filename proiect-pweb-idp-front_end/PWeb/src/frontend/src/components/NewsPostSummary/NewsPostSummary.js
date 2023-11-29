import { useAuth0 } from "@auth0/auth0-react";
import axios from "axios";
import { useState } from "react";

import { HiArrowUp, HiArrowDown } from "react-icons/hi";
import { Store } from "react-notifications-component";

const activeReactionCSS = `flex flex-col md:w-1/12 ml-2 border-2 py-2 border-blue bg-blue text-white`;
const inactiveReactionCSS = `flex flex-col md:w-1/12 ml-2 border-2 py-2 border-blue hover:bg-blue text-blue hover:text-white`;

const NewsPostSummary = ({
  id,
  imgSrc,
  postedBy,
  title,
  content,
  onClickFunc,
  approved,
  username,
  resultApprovalFunc,
  reaction,
}) => {
  const { getAccessTokenSilently } = useAuth0();
  const [newsReaction, setNewsReaction] = useState(reaction);

  const approve = async () => {
    const accessToken = await getAccessTokenSilently();

    axios
      .put(
        `https://localhost:7125/News?id=${id}`,
        {},
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      )
      .then(() => resultApprovalFunc(true, id));
  };

  const remove = async () => {
    const accessToken = await getAccessTokenSilently();

    axios
      .delete(`https://localhost:7125/News?id=${id}`, {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      })
      .then(() => resultApprovalFunc(false, id));
  };

  const reactToNews = async (paramReaction) => {
    const accessToken = await getAccessTokenSilently();

    if (newsReaction === paramReaction) {
      axios.delete(`https://localhost:7125/Reaction?newsId=${id}&reaction=${paramReaction}&username=${username}`, {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      }).then(() => setNewsReaction(""));

      return;
    }

    axios
      .post(
        `https://localhost:7125/Reaction`,
        {
          newsId: id,
          reaction: paramReaction,
          username: username,
        },
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      )
      .then(() => {
        setNewsReaction(paramReaction);

        Store.addNotification({
          title: "Reaction added",
          message: paramReaction === 'LIKE' ? "News liked" : "News disliked",
          type: "info",
          insert: "top",
          container: "top-right",
          animationIn: ["animate__animated", "animate__fadeIn"],
          animationOut: ["animate__animated", "animate__fadeOut"],
          dismiss: {
            duration: 5000,
            onScreen: true,
          },
        });
      });
  };

  return (
    <div className="flex flex-col w-full text-center justify-center content-center max-w-xl m-auto bg-gray p-8 border-2 border-white mt-4  drop-shadow rounded-lg hover:border-2 hover:border-brown hover:cursor-pointer">
      {imgSrc && (
        <div
          className="flex flex-row text-center items-center justify-center content-center"
          onClick={onClickFunc}
        >
          <img src={imgSrc} alt="news" className="w-full" />
        </div>
      )}
      <div
        className="flex flex-row text-center items-start justify-start content-start font-bold mt-2"
        onClick={onClickFunc}
      >
        Posted By {postedBy}
      </div>
      <div
        className="flex flex-row text-center items-center justify-center content-center font-bold mt-2 text-2xl"
        onClick={onClickFunc}
      >
        {title}
      </div>
      <div
        className="flex flex-row content-start justify-start text-justify items-start mt-2"
        onClick={onClickFunc}
      >
        {content}
      </div>
      {!approved && (
        <div className="flex flex-row text-center items-center justify-center content-center font-bold mt-4 text-lg">
          <div className="flex-col md:w-6/12">
            <button
              className="bg-red hover:bg-red-hover text-white font-bold py-2 px-4 rounded"
              onClick={remove}
            >
              Delete
            </button>
          </div>
          <div className="flex-col md:w-6/12">
            <button
              className="bg-blue hover:bg-blue-hover text-white font-bold py-2 px-4 rounded"
              onClick={approve}
            >
              Approve
            </button>
          </div>
        </div>
      )}

      {approved && username && username.length > 0 && (
        <div className="flex flex-row text-center items-center justify-center content-center font-bold mt-4 text-lg">
          <div
            className={
              newsReaction === "LIKE" ? activeReactionCSS : inactiveReactionCSS
            }
            onClick={() => reactToNews("LIKE")}
          >
            <HiArrowUp className="m-auto" />
          </div>
          <div
            className={
              newsReaction === "DISLIKE"
                ? activeReactionCSS
                : inactiveReactionCSS
            }
            onClick={() => reactToNews("DISLIKE")}
          >
            <HiArrowDown className="m-auto" />
          </div>
          <div className="flex flex-col md:w-10/12 text-right justify-end">
          </div>
        </div>
      )}
    </div>
  );
};

export default NewsPostSummary;
