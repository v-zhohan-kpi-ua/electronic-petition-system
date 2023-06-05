import Button from "@src/components/buttons/Button";
import { useForm, FormProvider } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import Input from "@src/components/inputs/Input";
import fetcher from "@src/api/fetcher";
import { useRouter } from "next/router";
import { AxiosError } from "axios";
import { useMutation } from "react-query";
import { useEffect } from "react";

const schema = yup.object().shape({
  email: yup
    .string()
    .email("Електронна пошта має бути правильною")
    .required("Електронна пошта є обов'язкова для заповнення"),
  password: yup.string().required("Пароль є обов'язковим для заповнення"),
});
type FormData = yup.InferType<typeof schema>;

export default function AdminLoginPageLayout() {
  const router = useRouter();

  const methods = useForm<FormData>({
    mode: "onSubmit",
    resolver: yupResolver(schema),
  });
  const { handleSubmit, setError } = methods;

  const { isLoading, isSuccess, mutateAsync } = useMutation({
    mutationFn: async (data: FormData) => {
      await fetcher.post<Petition>("/auth/login", data);
    },
  });

  const onSubmit = async (data: FormData) => {
    try {
      await mutateAsync(data);
    } catch (e) {
      if (e instanceof AxiosError) {
        setError("email", {
          type: "manual",
          message: "Перевірьте правильність введеної пошти",
        });
        setError("password", {
          type: "manual",
          message: "Перевірьте правильність введеного паролю",
        });
      }
    }
  };

  useEffect(() => {
    if (isSuccess) {
      router.push("/admin");
    }
  }, [isSuccess, router]);

  return (
    <>
      <h2 className="text-black text-xl text-center font-semibold">
        Адмін Вхід
      </h2>
      <FormProvider {...methods}>
        <form
          className="space-y-6 py-8 px-5 max-w-sm mx-auto"
          onSubmit={handleSubmit(onSubmit)}
        >
          <div>
            <Input
              id="email"
              label="Пошта"
              type="email"
              placeholder="admin@petitions.goverment"
            />
          </div>
          <Input
            id="password"
            label="Пароль"
            type="password"
            placeholder="••••••••"
          />
          <div className="flex justify-center">
            <Button isLoading={isLoading} type="submit">
              Увійти
            </Button>
          </div>
        </form>
      </FormProvider>
    </>
  );
}
