import { HiPhone, HiOutlineMail } from "react-icons/hi"
import { Link } from "react-router-dom";

const trimEmail = (email) => {
  if (email.length <= 16) {
    return email;
  }

  return email.substring(0, 15) + "...";
}

const OrganizationCard = ({ organization }) => {
  return (
    <Link to={`/profile/${organization.username}`} className="flex flex-col w-full text-center justify-center content-center max-w-xl m-auto bg-gray p-8 border-2 border-white mt-4 drop-shadow rounded-lg hover:border-2 hover:border-brown hover:cursor-pointer">
      <div className="flex flex-row text-center items-center justify-center content-center">
        <div className="flex flex-col md:w-6/12 pr-4 text-center justify-center content-center ">
          <div className="flex flex-row mb-3">
            <img src={organization.imageURI} alt="org-img" className="w-full" />
          </div>
          <div className="flex flex-row"><HiPhone color="#2B0245" size="26px" className="mr-4"/> {organization.phoneNumber}</div>
          <div className="flex flex-row pt-2" onClick={() => window.location.href = `mailto:${organization.username}`}><HiOutlineMail color="#2B0245" size="24px" className="mr-4"/> {trimEmail(organization.username)}</div>
        </div>
        <div className="flex flex-col md:w-6/12 pl-4 text-center justify-center content-center">
          <div className="flex flex-row text-center">
            <h1 className="text-2xl font-bold text-center m-auto">
              {organization.firstName + " " + organization.lastName}
            </h1>
          </div>
          <div className="flex flex-row">{organization.description}</div>
        </div>
      </div>
    </Link>
  );
};

export default OrganizationCard;
