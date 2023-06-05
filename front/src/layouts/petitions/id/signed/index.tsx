import { declensionOfSign } from "@src/utils/lang";
import Link from "next/link";
import CountUp from "react-countup";

type PetitionSignedProps = {
  petition: Petition;
};

export default function PetitionSigned({ petition }: PetitionSignedProps) {
  return (
    <>
      <div className="text-xl text-gray-500">Петиція №{petition.id}</div>
      <div className="text-3xl text-black font-bold">{petition.title}</div>
      <div className="mt-4 space-y-3">
        <div className="text-lg text-black">Ваш підпис враховано</div>
        <CountUp
          start={Math.max(petition.signsCount - 100, 0)}
          end={petition.signsCount + 1}
          delay={0}
          duration={2}
        >
          {({ countUpRef }) => (
            <div className="text-2xl text-black font-semibold">
              <span className="text-5xl" ref={countUpRef} />{" "}
              {declensionOfSign(petition.signsCount + 1)}
            </div>
          )}
        </CountUp>
        <div>
          <Link
            className="underline text-primary-700 hover:text-primary-500"
            href="/"
          >
            Повернутися на головну сторінку
          </Link>
        </div>
      </div>
    </>
  );
}
