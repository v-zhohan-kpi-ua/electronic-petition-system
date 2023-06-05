import PetitionInformation from "@src/layouts/petitions/id/information";
import PetitionSign from "@src/layouts/petitions/id/sign";
import PetitionSigned from "@src/layouts/petitions/id/signed";
import { useState } from "react";

type PetitionByIdPageStage = ["information", "sign", "signed"];

type PetitionByIdPageLayoutProps = {
  petition: Petition;
};

export default function PetitionByIdPageLayout({
  petition,
}: PetitionByIdPageLayoutProps) {
  const [stage, setStage] = useState<PetitionByIdPageStage[number]>(
    () => "information"
  );

  const render = () => {
    switch (stage) {
      case "information":
        return (
          <PetitionInformation
            petition={petition}
            wantToSign={() => setStage("sign")}
          />
        );
      case "sign":
        return (
          <PetitionSign
            petition={petition}
            backToPetitionInfo={() => setStage("information")}
            goToPetitionSigned={() => setStage("signed")}
          />
        );
      case "signed":
        return <PetitionSigned petition={petition} />;
      default:
        return null;
    }
  };

  return render();
}
