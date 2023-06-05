import SignForm from "@src/layouts/petitions/id/sign/SignForm";

type PetitionSignProps = {
  petition: Petition;
  backToPetitionInfo: () => void;
  goToPetitionSigned: () => void;
};

export default function PetitionSign({
  petition,
  backToPetitionInfo,
  goToPetitionSigned,
}: PetitionSignProps) {
  return (
    <>
      <button
        onClick={() => backToPetitionInfo()}
        className="text-base text-gray-500 hover:text-primary-700 transition-colors mb-2.5"
      >
        ← Повернутися до петиції
      </button>
      <h2 className="text-2xl text-black font-bold">{petition.title}</h2>
      <div className="text-lg text-black mt-3.5">
        Вам необхідно нададти деяку особисту інформацію для підписання цієї
        петиції:
      </div>
      <SignForm petition={petition} onSuccess={() => goToPetitionSigned()} />
    </>
  );
}
