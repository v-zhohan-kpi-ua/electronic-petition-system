import { useForm, FormProvider } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import Input from "@src/components/inputs/Input";
import Button from "@src/components/buttons/Button";
import Checkbox from "@src/components/inputs/Checkbox";
import { useMutation } from "react-query";
import fetcher from "@src/api/fetcher";
import { useEffect } from "react";
import { AxiosError } from "axios";

const schema = yup.object().shape({
  firstName: yup.string().required("Ім'я є обов'язковим для заповнення"),
  lastName: yup.string().required("Прізвище є обов'язковим для заповнення"),
  email: yup
    .string()
    .email("Електронна пошта має бути правильною")
    .required("Електронна пошта є обов'язкова для заповнення"),
  isResident: yup
    .boolean()
    .oneOf(
      [true],
      "Вам необхідно підвердити що ви є резидентом або громадяном Королівства"
    ),
});
export type SignFormData = yup.InferType<typeof schema>;

type SignFormProps = {
  petition: Petition;
  onSuccess: () => unknown;
};

export default function SignForm({ petition, onSuccess }: SignFormProps) {
  const methods = useForm<SignFormData>({
    mode: "onSubmit",
    resolver: yupResolver(schema),
  });
  const { handleSubmit, setError } = methods;

  const { isLoading, isSuccess, mutateAsync } = useMutation({
    mutationFn: async (data: SignFormData) => {
      await fetcher.post(`/petitions/${petition.id}/sign`, data);
    },
  });

  const onSubmit = async (data: SignFormData) => {
    try {
      await mutateAsync(data);
    } catch (e) {
      if (e instanceof AxiosError) {
        setError("email", {
          type: "manual",
          message: "Ця пошта вже була використана для підпису цієї петиції",
        });
      }
    }
  };

  useEffect(() => {
    if (isSuccess) {
      onSuccess();
    }
  }, [isSuccess, onSuccess]);

  return (
    <FormProvider {...methods}>
      <form
        onSubmit={handleSubmit(onSubmit)}
        className="max-w-md mt-4 space-y-4"
      >
        <Input id="firstName" label="Ім'я" placeholder="Гуннвор" />
        <Input id="lastName" label="Прізвище" placeholder="Голуб" />
        <Input
          type="email"
          id="email"
          label="Електронна пошта"
          placeholder="gunnvor.holub@example.com"
        />
        <Checkbox
          id="isResident"
          label="Я є резидентом або громадяном Королівства"
        />
        <Button type="submit" isLoading={isLoading}>
          Підписати
        </Button>
      </form>
    </FormProvider>
  );
}
