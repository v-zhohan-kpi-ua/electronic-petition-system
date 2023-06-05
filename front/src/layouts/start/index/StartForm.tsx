import Button from "@src/components/buttons/Button";
import Checkbox from "@src/components/inputs/Checkbox";
import Input from "@src/components/inputs/Input";
import TextArea from "@src/components/inputs/TextArea";
import { useForm, FormProvider } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";

const schema = yup.object().shape({
  title: yup
    .string()
    .max(100, "Максимальна довжина: 100 символів")
    .required("Це поле є обов'язковим для заповнення"),
  body: yup
    .string()
    .max(1000, "Максимальна довжина: 1000 символів")
    .required("Це поле є обов'язковим для заповнення"),
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
export type StartFormData = yup.InferType<typeof schema>;

type StartFormProps = {
  formData: StartFormData;
  onSubmit: (data: StartFormData) => unknown;
};

export default function StartForm({ onSubmit, formData }: StartFormProps) {
  const methods = useForm<StartFormData>({
    mode: "onSubmit",
    resolver: yupResolver(schema),
    defaultValues: formData,
  });
  const { handleSubmit } = methods;

  const titlePlaceholder = `Наприклад, \nЗнизити вік голосування на місцевих виборах до 16 років`;
  const bodyPlaceholder = `Наприклад, \nПропоную знизити вік голосування на місцевих виборах до 16 років. Молоді люди у 16 років мають право взяти відповідальність за своє майбутнє, включаючи політичну участь, на рівні місцевої громади. \nУряд повинен виконати цю петицію, оскільки місцева влада має прямий вплив на життя молодих людей, як-от, доступ до освіти, інфраструктури та роботи. Зниження вікового порогу для голосування на місцевих виборах може допомогти залучити більше молодих людей до процесу прийняття рішень на місцевому рівні та забезпечити більш представницьке управління містом, селом або громадою. \nЩоб виконати цю вимогу Уряду необхідно зробити зміни у законодавстві, щоб забезпечити право молодих людей на участь у виборах на рівні місцевих громад, зокрема, шляхом зниження вікового порогу. Також Уряд повинен запровадити заходи для надання освіти та інформації для молодих громадян щодо політичного процесу та їх прав у цьому процесі на місцевому рівні.`;

  return (
    <div className="space-y-4">
      <div>
        <h3 className="text-xl text-black font-bold">
          Етап 1/2: Заповнення інформації
        </h3>
        <div className="text-black">
          <div>Надайте інформацію для створення петиції</div>
        </div>
      </div>
      <div>
        <h3 className="text-lg text-black font-bold">
          Розкажіть про пропоновану петицію
        </h3>
        <FormProvider {...methods}>
          <form className="space-y-4">
            <TextArea
              id="title"
              label="Що ви хочете, щоб Уряд виконав?"
              rows={5}
              placeholder={titlePlaceholder}
              maxLength={100}
            />
            <TextArea
              id="body"
              label="Опишіть що конкретно та чому Уряд має виконати Вашу пропозицію?"
              rows={10}
              placeholder={bodyPlaceholder}
              maxLength={1000}
            />
          </form>
        </FormProvider>
      </div>
      <div>
        <h3 className="text-lg text-black font-bold mt-4">
          Розкажіть про себе (уся інформація є конфеденційною, окрім деяких
          полів)
        </h3>
        <FormProvider {...methods}>
          <form
            className="max-w-md space-y-4"
            onSubmit={handleSubmit(onSubmit)}
          >
            <Input
              id="firstName"
              label="Ім'я (ця відповідь є публічною)"
              placeholder="Гуннвор"
            />
            <Input
              id="lastName"
              label="Прізвище (ця відповідь є публічною)"
              placeholder="Голуб"
            />
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
            <Button type="submit">Продовжити</Button>
          </form>
        </FormProvider>
      </div>
    </div>
  );
}
