const NewsPost = ({
  imgSrc,
  postedBy,
  title,
  content,
  displayScoringMethod,
}) => {
  return (
    <div
      className="flex flex-col w-full text-center content-center max-w-4xl overflow-scroll overflow-x-hidden m-auto p-8 mt-4"
      style={{ maxHeight: "80vh", minWidth: '800px', minHeight: '600px' }}
    >
      {imgSrc && (
        <div className="flex flex-row text-center items-center justify-center content-center">
          <img src={imgSrc} alt="news" className="w-full" />
        </div>
      )}
      <div className="flex flex-row text-center items-start justify-start content-start font-bold mt-2">
        Posted By {postedBy}
      </div>
      <div className="flex flex-row text-center items-center justify-center content-center font-bold mt-2 text-2xl">
        {title}
      </div>
      <div className="flex flex-row content-start justify-start text-justify items-start mt-2">
        {content}
      </div>
    </div>
  );
};

export default NewsPost;
