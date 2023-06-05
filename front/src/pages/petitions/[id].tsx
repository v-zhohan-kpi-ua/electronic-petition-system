import { getPetition } from "@src/api";
import { useGetPetition } from "@src/api/hooks";
import LoadingPage from "@src/layouts/LoadingPage";
import PetitionByIdPageLayout from "@src/layouts/petitions/id";
import { AxiosError } from "axios";
import { GetServerSideProps, InferGetServerSidePropsType } from "next";
import Head from "next/head";
import { useRouter } from "next/router";
import { QueryClient, dehydrate } from "react-query";

export const getServerSideProps: GetServerSideProps = async ({ query }) => {
  const id = query.id;

  const queryClient = new QueryClient();

  try {
    await queryClient.fetchQuery(["petition", { id: Number(id) }], () =>
      getPetition({ id: Number(id) })
    );
  } catch (e) {
    if (e instanceof AxiosError) {
      if (e.response?.status === 404) {
        return {
          notFound: true,
        };
      }
    }
  }

  return {
    props: {
      dehydratedState: dehydrate(queryClient),
    },
  };
};

export default function PetitionById() {
  const router = useRouter();
  const { id } = router.query;

  const { data, isLoading, isSuccess } = useGetPetition({
    id: Number(id),
  });

  return (
    <>
      {isLoading && <LoadingPage />}
      {isSuccess && (
        <>
          <Head>
            <title key="title">
              {`Петиція №${data.id}: ${data.title}. Уряд Королівства Дикої Природи та Лісів`}
            </title>
          </Head>
          <PetitionByIdPageLayout petition={data} />
        </>
      )}
    </>
  );
}
