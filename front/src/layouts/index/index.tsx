import Button from "@src/components/buttons/Button";
import PetitionsStats, {
  PetitionsStatsProps,
} from "@src/layouts/index/PetitionsStats";
import PopularPetitions from "@src/layouts/index/PopularPetitions";
import Link from "next/link";

type IndexPageLayoutProps = {
  petitionsStats: PetitionsStatsProps;
  popularPetitions: Petition[];
};

export default function IndexPageLayout({
  petitionsStats,
  popularPetitions,
}: IndexPageLayoutProps) {
  return (
    <div className="mx-2 sm:mx-0">
      <PetitionsStats {...petitionsStats} />
      {popularPetitions.length > 0 && (
        <PopularPetitions petitions={popularPetitions} />
      )}
      <Link href="/start">
        <Button>Розпочати петицію</Button>
      </Link>
    </div>
  );
}
