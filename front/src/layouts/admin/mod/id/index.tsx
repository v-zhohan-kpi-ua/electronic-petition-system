import { useForm, FormProvider } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import Button from "@src/components/buttons/Button";
import TextArea from "@src/components/inputs/TextArea";
import { useEffect, useState } from "react";
import { useTimer } from "react-timer-hook";
import { useRouter } from "next/router";
import fetcher from "@src/api/fetcher";
import { useMutation } from "react-query";

const schema = yup.object().shape({
  reason: yup.string().when(["decision"], (decision: string[]) => {
    if (decision[0] === "declined") {
      return yup
        .string()
        .required("Це поле є обов`язковим")
        .min(10, "Мінімальна кількість символів 10");
    }

    return yup.string();
  }),
  decision: yup.string().oneOf(["accepted", "declined"]),
});
type FormData = yup.InferType<typeof schema>;

type AdminModByIdPageLayoutProps = {
  petition: Petition;
};

export default function AdminModByIdPageLayout({
  petition,
}: AdminModByIdPageLayoutProps) {
  const [formData, setFormData] = useState<FormData>({});

  const methods = useForm<FormData>({
    mode: "onSubmit",
    resolver: yupResolver(schema),
    defaultValues: formData,
  });
  const { handleSubmit, setValue } = methods;

  const { isLoading, isSuccess, mutateAsync } = useMutation({
    mutationFn: async (data: FormData) => {
      await fetcher.post<Petition>(`/admin/petitions/${petition.id}/mod`, {
        reason: data.reason,
        status: data.decision,
      });
    },
  });

  const [modStage, setModStage] = useState<"write" | "check" | "success">(
    "write"
  );

  const handleFormPreSubmit = (data: FormData) => {
    setFormData(data);
    setModStage("check");
  };

  const handleFormSubmit = async (data: FormData) => {
    await mutateAsync(data);
  };

  useEffect(() => {
    if (isSuccess) {
      setModStage("success");
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
            <div className="text-black text-lg">Ініціатор</div>
            <div className="text-black text-xl font-semibold">
              {petition.creator.firstName} {petition.creator.lastName} (
              {petition.creator.email ?? "Електронна пошта відсутня"})
            </div>
          </div>
        </div>
      </div>
      <div className="mt-5">
        {modStage === "write" && (
          <FormProvider {...methods}>
            <form onSubmit={handleSubmit(handleFormPreSubmit)}>
              <TextArea
                id="reason"
                label="Коментар до модерації"
                rows={15}
                maxLength={Infinity}
              />
              <div className="flex flex-row gap-2 mt-2">
                <Button
                  type="submit"
                  onClick={() => setValue("decision", "declined")}
                >
                  Відхилити
                </Button>
                <Button
                  type="submit"
                  onClick={() => setValue("decision", "accepted")}
                >
                  Прийняти
                </Button>
              </div>
            </form>
          </FormProvider>
        )}

        {modStage === "check" && (
          <ApproveAnswer
            formData={formData}
            onApprove={() => handleFormSubmit(formData)}
            onCancel={() => setModStage("write")}
            isApproving={isLoading}
          />
        )}

        {modStage === "success" && <Success />}
      </div>
    </div>
  );
}

function ApproveAnswer({
  formData,
  onApprove,
  onCancel,
  isApproving,
}: {
  formData: FormData;
  onCancel: () => unknown;
  onApprove: () => unknown;
  isApproving: boolean;
}) {
  return (
    <div className="space-y-4">
      <div>
        <div className="font-semibold text-lg">
          Перевірьте ретельно вашу відповідь на модерацію
        </div>
      </div>
      <div className="text-black">
        <div>Коментар модерації</div>
        <div className="italic whitespace-pre-line">
          {formData.reason ? formData.reason : "<відсутній>"}
        </div>
      </div>
      <div className="text-black">
        <div>Рішення модерації</div>
        <div className="italic whitespace-pre-line">
          {formData.decision === "accepted" ? "Прийняти" : "Відхилити"}
        </div>
      </div>
      <div className="flex flex-row gap-2">
        <Button
          variant="outline"
          onClick={() => onCancel()}
          disabled={isApproving}
        >
          Повернутися назад
        </Button>
        <Button onClick={() => onApprove()} isLoading={isApproving}>
          Відправити результат
        </Button>
      </div>
    </div>
  );
}

function Success() {
  const router = useRouter();
  const timer = useTimer({
    expiryTimestamp: new Date(Date.now() + 3 * 1000),
    onExpire: () => router.push("/admin/mod"),
    autoStart: true,
  });

  return (
    <div className="text-black text-lg">
      <div>Петиція успішно промодерована</div>
      <div>Повернення на сторінку модерації через {timer.seconds} секунд</div>
    </div>
  );
}
