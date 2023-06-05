import fetcher from "@src/api/fetcher";
import StartForm, { StartFormData } from "@src/layouts/start/index/StartForm";
import StartFormReview from "@src/layouts/start/index/StartFormReview";
import StartFormSuccess from "@src/layouts/start/index/StartFormSuccess";
import { useEffect, useState } from "react";
import { useMutation } from "react-query";

type StartPetitionPageStage = ["write", "review", "success"][number];

export default function StartPetitionPageLayout() {
  const [stage, setStage] = useState<StartPetitionPageStage>(() => "write");
  const [formData, setFormData] = useState<StartFormData>(() => ({
    title: "",
    body: "",
    firstName: "",
    lastName: "",
    email: "",
    isResident: false,
  }));

  const { data, isLoading, isSuccess, mutateAsync } = useMutation({
    mutationFn: async (data: StartFormData) => {
      const response = await fetcher.post<Petition>("/petitions", data);

      return response.data;
    },
  });

  useEffect(() => {
    if (isSuccess) {
      setStage("success");
    }
  }, [isSuccess]);

  const scrollToStartPetitionTitle = () =>
    document
      .getElementById("start-petition-title")
      ?.scrollIntoView({ behavior: "smooth" });

  const render = () => {
    switch (stage) {
      case "write":
        return (
          <StartForm
            onSubmit={(data) => {
              setFormData(data);
              setStage("review");
              scrollToStartPetitionTitle();
            }}
            formData={formData}
          />
        );
      case "review":
        return (
          <StartFormReview
            onCancelWriteAgain={() => {
              setStage("write");
              scrollToStartPetitionTitle();
            }}
            onSubmit={async (data) => {
              setFormData(data);
              await mutateAsync(data);
              scrollToStartPetitionTitle();
            }}
            formData={formData}
            isSubmitting={isLoading}
          />
        );
      case "success":
        return (
          <StartFormSuccess formData={{ ...formData, id: data?.id ?? 0 }} />
        );
      default:
        return null;
    }
  };

  return (
    <>
      <h1 className="text-2xl text-black font-bold" id="start-petition-title">
        Розпочати петицію
      </h1>
      {render()}
    </>
  );
}
