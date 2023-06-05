import Pagination from "@src/components/Pagination";
import Button from "@src/components/buttons/Button";
import Meta from "@src/layouts/petitions/id/information/Meta";
import SignatureCount from "@src/layouts/petitions/id/information/SignatureCount";

type PetitionInformationProps = {
  wantToSign: () => void;
  petition: Petition;
};

export default function PetitionInformation({
  petition,
  wantToSign,
}: PetitionInformationProps) {
  return (
    <>
      <div className="text-xl text-gray-500">Петиція №{petition.id}</div>
      <h1 className="text-3xl text-black font-bold">{petition.title}</h1>
      {petition.moderationResult && petition.status === "Declined" && (
        <div className="mt-5 pb-3 border-b-2 border-primary-700">
          <div className="text-2xl text-black font-semibold">
            Відповідь модерації:
          </div>
          <p className="text-xl whitespace-pre-line">
            {petition.moderationResult}
          </p>
        </div>
      )}
      {petition.answer && petition.status === "Answered" && (
        <div className="mt-5 pb-3 border-b-2 border-primary-700">
          <div className="text-2xl text-black font-semibold">
            Відповідь від Уряду:
          </div>
          <p className="text-xl whitespace-pre-line">{petition.answer}</p>
        </div>
      )}
      <p className="mt-5 text-lg whitespace-pre-line">{petition.body}</p>
      <div className="mt-5">
        <SignatureCount petition={petition} />
      </div>
      <div className="mt-3">
        <Meta petition={petition} />
      </div>
      {(petition.status === "Signing" ||
        petition.status === "WaitingForAnswer") && (
        <Button
          className="mt-3.5"
          variant="primary"
          onClick={() => wantToSign()}
        >
          Підписати цю петицію
        </Button>
      )}
    </>
  );
}
