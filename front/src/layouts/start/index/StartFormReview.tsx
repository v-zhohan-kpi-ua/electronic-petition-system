import Button from "@src/components/buttons/Button";
import { StartFormData } from "@src/layouts/start/index/StartForm";

type StartFormReviewProps = {
  formData: StartFormData;
  onCancelWriteAgain: () => void;
  onSubmit: (data: StartFormData) => unknown;
  isSubmitting: boolean;
};

export default function StartFormReview({
  formData,
  onCancelWriteAgain,
  onSubmit,
  isSubmitting,
}: StartFormReviewProps) {
  return (
    <div className="space-y-4">
      <div>
        <h3 className="text-xl text-black font-bold">
          Етап 2/2: Перевірка інформації
        </h3>
        <div className="text-black">
          <div>Перевірьте ретельно введену інформацію</div>
          <div className="font-semibold italic">
            Інформацію неможливо змінити після пітвердження
          </div>
        </div>
      </div>
      <div>
        <h3 className="text-lg text-black font-bold">
          Розкажіть про пропоновану петицію
        </h3>
        <div className="space-y-4">
          <div className="text-black">
            <div>Що ви хочете щоб Уряд виконав?</div>
            <div className="italic">{formData.title}</div>
          </div>
          <div className="text-black">
            <div>
              Опишіть що конкретно та чому Уряд має виконати Вашу пропозицію?
            </div>
            <div className="italic whitespace-pre-line">{formData.body}</div>
          </div>
        </div>
      </div>
      <div>
        <h3 className="text-lg text-black font-bold mt-4">
          Розкажіть про себе (уся інформація є конфеденційною, окрім деяких
          полів)
        </h3>
        <div className="space-y-4">
          <div className="text-black">
            <div>{`Ім'я (ця відповідь є публічною)`}</div>
            <div className="italic">{formData.firstName}</div>
          </div>
          <div className="text-black">
            <div>Прізвище (ця відповідь є публічною)</div>
            <div className="italic">{formData.lastName}</div>
          </div>
          <div className="text-black">
            <div>Електронна пошта</div>
            <div className="italic">{formData.email}</div>
          </div>
          <div className="text-black">
            <div>Я є резидентом або громадяном Королівства</div>
            <div className="italic">{formData.isResident ? "Так" : "Ні"}</div>
          </div>
        </div>
      </div>
      <div className="flex flex-row gap-2">
        <Button
          variant="outline"
          onClick={() => onCancelWriteAgain()}
          disabled={isSubmitting}
        >
          Скасувати та переписати
        </Button>
        <Button onClick={() => onSubmit(formData)} isLoading={isSubmitting}>
          Підписати та опублікувати
        </Button>
      </div>
    </div>
  );
}
