import { useForm, FormProvider } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import Button from "@src/components/buttons/Button";
import TextArea from "@src/components/inputs/TextArea";
import { useEffect, useState } from "react";
import { useTimer } from "react-timer-hook";
import { useRouter } from "next/router";
import { useMutation } from "react-query";
import fetcher from "@src/api/fetcher";

const schema = yup.object().shape({
  response: yup
    .string()
    .required("Відповідь є обов'язковою для заповнення")
    .min(10, "Мінімальна довжина 10 символів"),
});
type FormData = yup.InferType<typeof schema>;

type AdminReviewPageLayoutProps = {
  petition: Petition;
};

export default function AdminReviewByIdPageLayout({
  petition,
}: AdminReviewPageLayoutProps) {
  const [formData, setFormData] = useState<FormData>({
    response: "",
  });

  const methods = useForm<FormData>({
    mode: "onSubmit",
    resolver: yupResolver(schema),
    defaultValues: formData,
  });
  const { handleSubmit } = methods;

  const { isLoading, isSuccess, mutateAsync } = useMutation({
    mutationFn: async (data: FormData) => {
      await fetcher.post<Petition>(`/admin/petitions/${petition.id}/answer`, {
        answer: data.response,
      });
    },
  });

  const [answerStage, setAnswerStage] = useState<"write" | "check" | "success">(
    "write"
  );

  const handleFormPreSubmit = (data: FormData) => {
    setFormData(data);
    setAnswerStage("check");
  };

  const handleFormSubmit = async (data: FormData) => {
    await mutateAsync(data);
  };

  useEffect(() => {
    if (isSuccess) {
      setAnswerStage("success");
    }
  }, [isSuccess]);

  return (
    <div>
      <div className="text-xl text-black">Петиція №{petition.id}</div>
      <h1 className="text-3xl text-black font-bold">{petition.title}</h1>
      <p className="mt-3 text-lg">{petition.body}</p>
      <div className="mt-5">
        <div className="flex flex-col gap-3.5">
          <div>
            <div className="text-black text-lg">Кількість підписів</div>
            <div className="text-black text-xl font-semibold">
              {petition.signsCount}
            </div>
          </div>
          <div>
            <div className="text-black text-lg">Ініціатор</div>
            <div className="text-black text-xl font-semibold">
              {petition.creator.firstName} {petition.creator.lastName} (
              {petition.creator.email ?? "Електронна пошта відсутня"})
            </div>
          </div>
        </div>
      </div>
      <div className="mt-5">
        {answerStage === "write" && (
          <FormProvider {...methods}>
            <form onSubmit={handleSubmit(handleFormPreSubmit)}>
              <TextArea
                id="response"
                label="Відповідь"
                rows={15}
                maxLength={Infinity}
              />
              <Button className="mt-1" type="submit">
                Продовжити
              </Button>
            </form>
          </FormProvider>
        )}

        {answerStage === "check" && (
          <ApproveAnswer
            formData={formData}
            onApprove={() => handleFormSubmit(formData)}
            onCancel={() => setAnswerStage("write")}
            answerIsApproving={isLoading}
          />
        )}

        {answerStage === "success" && <Success />}
      </div>
    </div>
  );
}

function ApproveAnswer({
  formData,
  onApprove,
  onCancel,
  answerIsApproving,
}: {
  formData: FormData;
  onCancel: () => unknown;
  onApprove: () => unknown;
  answerIsApproving: boolean;
}) {
  return (
    <div className="space-y-4">
      <div>
        <div className="font-semibold text-lg">
          Перевірьте ретельно відповідь на петицію вище
        </div>
      </div>
      <div className="text-black">
        <div>Відповідь уряду</div>
        <div className="italic whitespace-pre-line">{formData.response}</div>
      </div>
      <div className="flex flex-row gap-2">
        <Button
          variant="outline"
          onClick={() => onCancel()}
          disabled={answerIsApproving}
        >
          Переписати відповідь
        </Button>
        <Button onClick={() => onApprove()} isLoading={answerIsApproving}>
          Опублікувати відповідь
        </Button>
      </div>
    </div>
  );
}

function Success() {
  const router = useRouter();
  const timer = useTimer({
    expiryTimestamp: new Date(Date.now() + 1000 * 3),
    onExpire: () => router.push("/admin/review"),
    autoStart: true,
  });

  return (
    <div className="text-black text-lg">
      <div>Відповідь опублікувано</div>
      <div>
        Повернення на сторінку петицій, які очікують відповідь через{" "}
        {timer.seconds} секунд
      </div>
    </div>
  );
}
