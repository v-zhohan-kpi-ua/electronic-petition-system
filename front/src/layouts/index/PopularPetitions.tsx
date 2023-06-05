import { declensionOfDay, declensionOfSign } from "@src/utils/lang";
import { differenceInDays } from "date-fns";
import Link from "next/link";

type PopularPetitionsProps = {
  petitions: Petition[];
};

export default function PopularPetitions({ petitions }: PopularPetitionsProps) {
  return (
    <div className="mt-12">
      <h3 className="text-black text-2xl font-bold">Популярні петиції</h3>
      <div className="space-y-4 my-3">
        {petitions.map((p) => (
          <Petition key={p.id} petition={p} />
        ))}
      </div>
    </div>
  );
}

function Petition({ petition }: { petition: Petition }) {
  const daysLeft = differenceInDays(
    new Date(petition.statusDeadline ?? ""),
    new Date()
  );

  return (
    <div>
      <Link href={`/petitions/${petition.id}`}>
        <div className="text-xl text-primary-700 hover:text-primary-500 font-semibold">
          {petition.title}
        </div>
      </Link>
      <div className="text-md text-gray-500">
        {`${petition.signsCount.toLocaleString()} ${declensionOfSign(
          petition.signsCount
        )}`}
      </div>
      {daysLeft > 0 && (
        <div className="text-md text-gray-500">
          Залишилося <span className="font-semibold text-lg">{daysLeft}</span>{" "}
          {declensionOfDay(daysLeft)}
        </div>
      )}
      {petition.status === "WaitingForAnswer" && (
        <div className="text-md text-gray-500">Очікує відповідь</div>
      )}
    </div>
  );
}
