import Link from "next/link";

export type PetitionsStatsProps = {
  numberOfOpened: number;
  numberOfAnswered: number;
};

export default function PetitionsStats({
  numberOfAnswered,
  numberOfOpened,
}: PetitionsStatsProps) {
  return (
    <div className="flex flex-col sm:flex-row gap-6 sm:gap-12 text-primary-700">
      <Link href="/petitions?state=open">
        <div className="flex flex-col hover:text-primary-500">
          <div className="text-4xl font-bold">{numberOfOpened}</div>
          <div className="text-lg font-medium">
            відкритих петицій для підпису
          </div>
        </div>
      </Link>
      <Link href="/petitions?state=answered">
        <div className="flex flex-col  hover:text-primary-500">
          <div className="text-4xl font-bold">{numberOfAnswered}</div>
          <div className="text-lg font-medium">
            петицій отримали відповідь від Уряду
          </div>
        </div>
      </Link>
    </div>
  );
}
