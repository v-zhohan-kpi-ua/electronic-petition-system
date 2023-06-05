import { declensionOfDay } from "@src/utils/lang";
import { differenceInDays, format } from "date-fns";

type MetaProps = {
  petition: Petition;
};

export default function Meta({ petition }: MetaProps) {
  const status = (status: PetitionStatus) => {
    switch (status) {
      case "Answered":
        return "Отримала відповідь";
      case "Created":
        return "Створено, на модерації";
      case "Declined":
        return "Відхилено";
      case "NotEnoughSigns":
        return "Недостатньо підписів";
      case "Signing":
        return "Збір підписів";
      case "WaitingForAnswer":
        return "Очікує відповідь";
      default:
        return "Невизначений";
    }
  };

  const daysLeft = differenceInDays(
    new Date(petition.statusDeadline ?? ""),
    new Date()
  );

  return (
    <div className="flex flex-col gap-3.5">
      <div>
        <div className="text-black text-lg">Статус</div>
        <div className="text-black text-xl font-semibold">
          {status(petition.status)}
        </div>
      </div>
      <div>
        <div className="text-black text-lg">Ініціатор</div>
        <div className="text-black text-xl font-semibold">{`${petition.creator.firstName} ${petition.creator.lastName}`}</div>
      </div>
      {daysLeft > 0 && (
        <div>
          <div className="text-black text-lg">Залишилося</div>
          <div className="text-black text-xl font-semibold">
            {`${daysLeft} ${declensionOfDay(daysLeft)}`}
          </div>
        </div>
      )}
      {petition.statusDeadline && (
        <div>
          <div className="text-black text-lg">Дедлайн</div>
          <div className="text-black text-xl font-semibold">
            {format(new Date(petition.statusDeadline), "dd.MM.yyyy")}
          </div>
          <div className="text-gray-500 text-base">
            Усі петиції тривають 3 місяці
          </div>
        </div>
      )}
    </div>
  );
}
