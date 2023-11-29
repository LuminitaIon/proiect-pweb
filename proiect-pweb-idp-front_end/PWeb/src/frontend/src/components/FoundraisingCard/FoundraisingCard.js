import { Foundation } from "@mui/icons-material";
import { HiPhone, HiOutlineMail } from "react-icons/hi";
import { Link } from "react-router-dom";

const shortenText = (text) => {
  const words = text.split(" ");

  if (words.length <= 100) {
    return text;
  }

  return words.slice(0, 100).join(" ") + "...";
};

const FoundraisingCard = ({ foundraising }) => {
  return (
    <a
      href={foundraising.donationURI}
      target="_blank"
      className="flex flex-col w-full text-center justify-center content-center max-w-xl m-auto bg-gray p-8 border-2 border-white mt-4 drop-shadow rounded-lg hover:border-2 hover:border-brown hover:cursor-pointer"
    >
      <div className="flex flex-row text-center items-center justify-center content-center">
        <div className="flex flex-col w-full pr-4 text-center justify-center content-center ">
          {foundraising.imageURI && (
            <div className="flex flex-row mb-3">
              <img
                src={foundraising.imageURI}
                alt="org-img"
                className="w-full"
              />
            </div>
          )}
          <div className="flex w-full flex-row justify-start content-center italic">
            Posted by {foundraising.username}
          </div>
          <div className="flex flex-row justify-center content-center">
            <h1 className="text-lg font-bold mb-2">
              Donation "{foundraising.name}"
            </h1>
          </div>
          <div className="flex w-full flex-row justify-center content-center">
            {shortenText(foundraising.description)}
          </div>
          <div className="w-full mt-2 bg-white rounded-full h-3.5 dark:bg-white border-2 border-blue">
            <div
              className="bg-blue h-2.5 rounded-full"
              style={{ width: "45%" }}
            ></div>
          </div>
        </div>
      </div>
    </a>
  );
};

export default FoundraisingCard;
