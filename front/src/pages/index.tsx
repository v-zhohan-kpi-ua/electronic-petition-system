import { getPetitions } from "@src/api";
import IndexPageLayout from "@src/layouts/index";
import { GetStaticProps, InferGetStaticPropsType } from "next";

type IndexPageProps = {
  openedPetitions: number;
  answeredPetitions: number;
  popularPetitions: Petition[];
};

export const getStaticProps: GetStaticProps<IndexPageProps> = async () => {
  const openedPetitions = await getPetitions({
    perPage: 0,
    status: ["Signing", "WaitingForAnswer"],
  });

  const answeredPetitions = await getPetitions({
    perPage: 0,
    status: ["Answered"],
  });

  const popularPetitions = await getPetitions({
    perPage: 5,
    order: "popularity",
    status: ["Signing", "WaitingForAnswer"],
  });

  return {
    props: {
      openedPetitions: openedPetitions.totalItems,
      answeredPetitions: answeredPetitions.totalItems,
      popularPetitions: popularPetitions.items,
    },
    revalidate: 5 * 60,
  };
};

export default function IndexPage({
  openedPetitions,
  answeredPetitions,
  popularPetitions,
}: InferGetStaticPropsType<typeof getStaticProps>) {
  return (
    <>
      <IndexPageLayout
        petitionsStats={{
          numberOfOpened: openedPetitions,
          numberOfAnswered: answeredPetitions,
        }}
        popularPetitions={popularPetitions}
      />
    </>
  );
}
