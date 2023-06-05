import clsxTwMerge from "@src/utils/clsxTwMerge";
import { declensionOfSign } from "@src/utils/lang";

export default function SignatureCount({ petition }: { petition: Petition }) {
  return (
    <>
      <div className="text-2xl font-medium">
        <span className="text-3xl font-bold">
          {Number(petition.signsCount).toLocaleString()}
        </span>{" "}
        {declensionOfSign(petition.signsCount)}
      </div>
      <div className="mt-1.5">
        <div className="w-full bg-gray-300 h-2.5 rounded-lg overflow-hidden">
          <div
            className={clsxTwMerge(
              "bg-primary-600 h-2.5 rounded-lg",
              (petition.status === "Declined" ||
                petition.status === "NotEnoughSigns") &&
                "bg-gray-400"
            )}
            style={{
              width: `${
                (petition.signsCount / petition.signsRequiredToGetAnswer) * 100
              }%`,
            }}
          ></div>
        </div>
        <div className="flex justify-end text-gray-400 text-base">
          {petition.signsRequiredToGetAnswer.toLocaleString()}
        </div>
      </div>
    </>
  );
}
